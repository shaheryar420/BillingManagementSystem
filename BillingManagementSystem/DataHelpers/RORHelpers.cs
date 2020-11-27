using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;
using BillingManagementSystem.SubDataHelpers;

namespace BillingManagementSystem.DataHelpers
{
    public class RORHelpers
    {
        public RORResponseModel GenerateRor(RORRequestModel model)
        {
            RORResponseModel toReturn = new RORResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(model.month))
                {
                    using (db_bmsEntities db = new db_bmsEntities()) 
                    {
                        if (!string.IsNullOrEmpty(model.consumerNo))
                        {
                            var response = (from x in db.tbl_approvereadings
                                            join y in db.tbl_residents on x.fk_resident equals y.resident_id
                                            where x.reading_meterno == model.consumerNo && x.reading_month == model.month && x.fk_ror==0 select new
                                            {
                                                x.reading_id,
                                                x.bill_amount,
                                                x.fk_resident,
                                                y.resident_name,
                                                x.reading_datetime
                                            }).OrderBy(x=>x.reading_datetime).ToList();
                            var location = db.tbl_consummer_pool.Where(x => x.consummer_no == model.consumerNo).FirstOrDefault();
                            if (!string.IsNullOrEmpty(model.residentName))
                            {
                                response = response.Where(x => x.resident_name.Contains(model.residentName)).ToList();
                            }
                            if (response.Count() > 0)
                            {
                                var monthnYear = model.month.Split('-');
                                string month = (int.Parse(monthnYear[0]) - 1) + "-" + monthnYear[1];
                                var fk_resident = response[0].fk_resident;
                                var lastMonthoutstanding = db.tbl_outstanding.Where(x => x.outstanding_month == month && x.fk_resident == fk_resident && x.fk_consummer_no == model.consumerNo).FirstOrDefault();
                                var approvedReadings = response.Where(x => x.fk_resident == fk_resident).ToList();
                                double amount = 0.0;
                                double outstanding = 0.0;
                                if (lastMonthoutstanding != null)
                                {
                                    outstanding = lastMonthoutstanding.outstanding_amount;                                    
                                }
                                foreach( var x in approvedReadings)
                                {
                                    amount = amount + x.bill_amount;
                                    
                                }
                                var newRor = new tbl_ror()
                                {
                                    fk_resident = fk_resident,
                                    consummer_no = model.consumerNo,
                                    ror_amount = amount,
                                    ror_datetime = DateTime.UtcNow.AddHours(5),
                                    ror_month = model.month,
                                    ror_outstanding = outstanding+amount,
                                    ror_status = 3,

                                };
                                db.tbl_ror.Add(newRor);
                                db.SaveChanges();
                                var newOutstandings = new tbl_outstanding()
                                {
                                    fk_resident = fk_resident,
                                    fk_consummer_no = model.consumerNo,
                                    fk_location = location.fk_location,
                                    outstanding_amount = newRor.ror_amount,
                                    outstanding_date = newRor.ror_datetime,
                                    outstanding_month = model.month,
                                };
                                db.tbl_outstanding.Add(newOutstandings);
                                db.SaveChanges();

                                foreach( var x in approvedReadings)
                                {

                                    var reading = db.tbl_approvereadings.Where(y => y.reading_id == x.reading_id).FirstOrDefault();
                                    reading.fk_ror = newRor.id;
                                    db.SaveChanges();
                                }
                                approvedReadings = response.Where(x => x.fk_resident != fk_resident).ToList();
                                if (approvedReadings.Count() > 0)
                                {
                                    fk_resident = approvedReadings[0].fk_resident;
                                    lastMonthoutstanding = db.tbl_outstanding.Where(x => x.outstanding_month == month && x.fk_resident == fk_resident && x.fk_consummer_no == model.consumerNo).FirstOrDefault();
                                    amount = 0.0;
                                    if (lastMonthoutstanding != null)
                                    {
                                        outstanding = lastMonthoutstanding.outstanding_amount;
                                    }
                                    foreach (var x in approvedReadings)
                                    {
                                        amount = amount + x.bill_amount;
                                    }
                                    newRor = new tbl_ror()
                                    {
                                        fk_resident = fk_resident,
                                        consummer_no = model.consumerNo,
                                        ror_amount = amount,
                                        ror_datetime = DateTime.UtcNow.AddHours(5),
                                        ror_month = model.month,
                                        ror_outstanding = outstanding,
                                        ror_status= 3,

                                    };
                                    db.tbl_ror.Add(newRor);
                                    db.SaveChanges();
                                    newOutstandings = new tbl_outstanding()
                                    {
                                        fk_resident = fk_resident,
                                        fk_consummer_no = model.consumerNo,
                                        fk_location = location.fk_location,
                                        outstanding_amount = newRor.ror_amount,
                                        outstanding_date = newRor.ror_datetime,
                                        outstanding_month = model.month,
                                    };
                                    db.tbl_outstanding.Add(newOutstandings);
                                    db.SaveChanges();
                                    foreach (var x in approvedReadings)
                                    {

                                        var reading = db.tbl_approvereadings.Where(y => y.reading_id == x.reading_id).FirstOrDefault();
                                        reading.fk_ror = newRor.id;
                                        db.SaveChanges();
                                    }
                                }
                                toReturn = new RORResponseModel()
                                {
                                    remarks = "Success",
                                    resultCode = "1100"
                                };


                            }
                            else
                            {
                                toReturn = new RORResponseModel()
                                {
                                    resultCode = "1200",
                                    remarks = "No Record Found Or ROR Already Generated For This Month"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new RORResponseModel()
                            {
                                remarks = "Please Provide Concumer No",
                                resultCode = "1300"
                            };
                        }
                    }
                }
                else
                {
                    toReturn = new RORResponseModel()
                    {
                        remarks = "Please Provide Reading Month",
                        resultCode = "1300"
                    };
                }
            }
            catch(Exception Ex)
            {
                toReturn = new RORResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public List<BillResponseModel> GetAllROR(BillRequestModel model)
        {
            List<BillResponseModel> toReturn = new List<BillResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    toReturn = new SubDataHelpers.RORSubHelpers().GetAllROR(model);
                    if (!string.IsNullOrEmpty(model.billElectricMonth))
                    {
                        toReturn = toReturn.Where(x => x.billMonth == model.billElectricMonth).ToList();
                    }
                    if (!string.IsNullOrEmpty(model.paNo))
                    {
                        toReturn = toReturn.Where(x => x.residentPaNo == model.paNo).ToList();
                    }
                    if (!string.IsNullOrEmpty(model.unit))
                    {
                        toReturn = toReturn.Where(x => x.residentUnit == model.unit).ToList();
                    }
                    if (!string.IsNullOrEmpty(model.rank))
                    {
                        toReturn = toReturn.Where(x => x.residentRank == model.rank).ToList();
                    }
                    if (!string.IsNullOrEmpty(model.residentName))
                    {
                        toReturn = toReturn.Where(x => x.residentName.Contains( model.residentName)).ToList();
                    }
                    if (!string.IsNullOrEmpty(model.meterNo))
                    {
                        toReturn = toReturn.Where(x => x.locationMeterNo == model.meterNo).ToList();
                    }
                    if (!string.IsNullOrEmpty(model.areaid))
                    {
                        toReturn = toReturn.Where(x => x.areaId == model.areaid).ToList();
                    }
                    if (!string.IsNullOrEmpty(model.billDateTime))
                    {
                        toReturn = toReturn.Where(x => x.billDateTime == model.billDateTime).ToList();
                    }
                    if (!string.IsNullOrEmpty(model.billAmount))
                    {
                        toReturn = toReturn.Where(x => x.billAmount == model.billAmount).ToList();
                    }
                    if (!string.IsNullOrEmpty(model.fk_location))
                    {
                        toReturn = toReturn.Where(x => x.fk_location == model.fk_location).ToList();
                    }
                    if (toReturn.Count()==0)
                    {
                        toReturn.Add(new BillResponseModel()
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new BillResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public BillResponseModel GetRORById(BillRequestModel model)
        {
            BillResponseModel toReturn = new BillResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.billId))
                    {
                        int id = int.Parse(model.billId);
                        toReturn = new SubDataHelpers.RORSubHelpers().GetAllRORById(id);

                        if (toReturn == null)
                        {
                            toReturn = (new BillResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            });
                        }
                    }
                    else
                    {
                        toReturn = new BillResponseModel()
                        {
                            remarks = "Please Provide Ror",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn=(new BillResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }




    }
}