using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.SubDataHelpers;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.DataHelpers
{
    public class ReadingElectricHelpers
    {
        public ReadingElectricResponseModel AddReadingElectric(ReadingElectricRequestModel model)
        {
            ReadingElectricResponseModel toReturn = new ReadingElectricResponseModel();
            try
            {
                if(new ModelsValidatorHelper().validateint(model.readingElectricCurrentReading))
                {
                    if (new ModelsValidatorHelper().validateint(model.meter_status))
                    {
                        int meterStatus = int.Parse(model.meter_status);
                        if (new ModelsValidatorHelper().validateint(model.readingElectricAddedby))
                        {
                            if (!string.IsNullOrEmpty(model.readingElectricMonth))
                            {
                                if (new ModelsValidatorHelper().validateint(model.readingElectricPrevReading))
                                {
                                    using (db_bmsEntities db = new db_bmsEntities())
                                    {
                                        var resident = (from x in db.tbl_location
                                                        join z in db.tbl_consummer_pool on x.location_id equals z.fk_location
                                                        join y in db.tbl_residents on z.id equals y.resident_consumer_no
                                                        where z.consummer_no == model.readingElectricMeterNo
                                                        select new { y.resident_id,
                                                            x.location_id }).FirstOrDefault();

                                        var existingReading = (from x in db.tbl_readingelectric where x.readingelectric_month == model.readingElectricMonth && x.fk_resident == resident.resident_id && x.readingelectric_meterno == model.readingElectricMeterNo && x.is_secondary == meterStatus select x).FirstOrDefault();
                                        var existingApprovedReading = (from x in db.tbl_approvereadings where x.reading_month == model.readingElectricMonth && x.fk_resident == resident.resident_id && x.reading_meterno == model.readingElectricMeterNo && x.is_secondary == meterStatus select x).FirstOrDefault();
                                        if (existingReading == null && existingApprovedReading == null)
                                        {
                                            if (!string.IsNullOrEmpty(model.readingpicture_data))
                                            {
                                                var newReadingPicture = new tbl_readingpicture()
                                                {
                                                    readingpicture_data = model.readingpicture_data,
                                                    readingpicture_size = int.Parse(model.readingpicture_size),
                                                    readingpicture_type = model.readingpicture_type
                                                };
                                                db.tbl_readingpicture.Add(newReadingPicture);
                                                db.SaveChanges();
                                                var newReadingElectric = new tbl_readingelectric()
                                                {
                                                    fk_readingpicture = newReadingPicture.readingpicture_id,
                                                    readingelectric_addedby = int.Parse(model.readingElectricAddedby),
                                                    readingelectric_units = int.Parse(model.readingElectricUnits),
                                                    readingelectric_month = model.readingElectricMonth,
                                                    fk_resident = resident.resident_id,
                                                    fk_location = resident.location_id,
                                                    is_secondary = int.Parse(model.meter_status),
                                                    readingelectric_currentreading = double.Parse(model.readingElectricCurrentReading),
                                                    readingelectric_datetime = DateTime.UtcNow.AddHours(5),
                                                    readingelectric_prevreading = double.Parse(model.readingElectricPrevReading),
                                                    readingelectric_meterno = !string.IsNullOrEmpty(model.readingElectricMeterNo) ? model.readingElectricMeterNo : "",
                                                    readingelectric_remarks = !string.IsNullOrEmpty(model.readingElectricRemarks) ? model.readingElectricRemarks : "",
                                                };
                                                db.tbl_readingelectric.Add(newReadingElectric);
                                                db.SaveChanges();
                                            }
                                            else
                                            {
                                                var newReadingElectric = new tbl_readingelectric()
                                                {
                                                    fk_resident = resident.resident_id,
                                                    fk_location = resident.location_id,
                                                    fk_readingpicture = 0,
                                                    readingelectric_addedby = int.Parse(model.readingElectricAddedby),
                                                    readingelectric_units = int.Parse(model.readingElectricUnits),
                                                    readingelectric_month = model.readingElectricMonth,
                                                    readingelectric_currentreading = double.Parse(model.readingElectricCurrentReading),
                                                    readingelectric_datetime = DateTime.UtcNow.AddHours(5),
                                                    readingelectric_prevreading = double.Parse(model.readingElectricPrevReading),
                                                    readingelectric_meterno = !string.IsNullOrEmpty(model.readingElectricMeterNo) ? model.readingElectricMeterNo : "",
                                                    readingelectric_remarks = !string.IsNullOrEmpty(model.readingElectricRemarks) ? model.readingElectricRemarks : "",
                                                };
                                                db.tbl_readingelectric.Add(newReadingElectric);
                                                db.SaveChanges();
                                            }

                                            toReturn = new ReadingElectricResponseModel()
                                            {
                                                remarks = "Successfully Added",
                                                resultCode = "1100"
                                            };
                                        }
                                        else
                                        {
                                            toReturn = new ReadingElectricResponseModel()
                                            {
                                                remarks = "Reading Already Exists for This Month",
                                                resultCode = "1200"
                                            };
                                        }
                                    }
                                }
                                else
                                {
                                    toReturn = new ReadingElectricResponseModel()
                                    {
                                        remarks = "Please Provide Previous Reading",
                                        resultCode = "1300"
                                    };
                                }
                            }
                            else
                            {
                                toReturn = new ReadingElectricResponseModel()
                                {
                                    remarks = "Please Provide Reading Month",
                                    resultCode = "1300"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new ReadingElectricResponseModel()
                            {
                                remarks = "Please Provide Added By",
                                resultCode = "1300"
                            };
                        } 
                    }
                    else
                    {
                        toReturn = new ReadingElectricResponseModel()
                        {
                            remarks = "Please Provide Meter Status",
                            resultCode = "1300"
                        };
                    }
                }
                else
                {
                    toReturn = new ReadingElectricResponseModel()
                    {
                        remarks = "Please Provide Current Reading",
                        resultCode="1300"
                    };
                }
            }
            catch(Exception Ex)
            {
                toReturn = new ReadingElectricResponseModel()
                {
                    remarks = "There Was Fatal Error "+ Ex.ToString(),
                    resultCode="1000"
                };
            }
            return toReturn;
        }
        public ReadingElectricResponseModel EditReadingElectric(ReadingElectricRequestModel model)
        {
            ReadingElectricResponseModel toReturn = new ReadingElectricResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.readingElectricId))
                {
                    var readingElectricId = int.Parse(model.readingElectricId);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var readingElectric = (from x in db.tbl_readingelectric where x.readingelectric_id == readingElectricId select x).FirstOrDefault();
                        if (readingElectric != null)
                        {
                            var readingPicture = (from x in db.tbl_readingpicture where x.readingpicture_id == readingElectric.fk_readingpicture select x).FirstOrDefault();
                            if(readingPicture!=null)
                            {
                                readingPicture.readingpicture_data = !string.IsNullOrEmpty(model.readingpicture_data)?model.readingpicture_data :readingPicture.readingpicture_data;
                                readingPicture.readingpicture_size = !string.IsNullOrEmpty(model.readingpicture_size)?int.Parse(model.readingpicture_size):readingPicture.readingpicture_size;
                                readingPicture.readingpicture_type = !string.IsNullOrEmpty(model.readingpicture_type)?model.readingpicture_type :readingPicture.readingpicture_type;
                                db.SaveChanges();
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(model.readingpicture_data))
                                {
                                    if (!string.IsNullOrEmpty(model.readingpicture_type))
                                    {
                                        if (new ModelsValidatorHelper().validateint(model.readingpicture_size))
                                        {
                                            var newReadingPicture = new tbl_readingpicture()
                                            {
                                                readingpicture_data = model.readingpicture_data,
                                                readingpicture_size = int.Parse(model.readingpicture_size),
                                                readingpicture_type = model.readingpicture_type
                                            };
                                            db.tbl_readingpicture.Add(newReadingPicture);
                                            db.SaveChanges();
                                            readingElectric.fk_readingpicture = newReadingPicture.readingpicture_id;
                                        }
                                        else
                                        {
                                            toReturn = new ReadingElectricResponseModel()
                                            {
                                                remarks = "Please Provide Picture Size",
                                                resultCode = "1300"
                                            };
                                        }
                                    }
                                    else
                                    {
                                        toReturn = new ReadingElectricResponseModel()
                                        {
                                            remarks = "Please Provide Picture Type",
                                            resultCode = "1300"
                                        };
                                    }
                                }
                                else
                                {
                                    toReturn = new ReadingElectricResponseModel()
                                    {
                                        remarks = "Please Provide Picture Data",
                                        resultCode = "1300"
                                    };
                                }
                            }
                            readingElectric.readingelectric_addedby = !string.IsNullOrEmpty(model.readingElectricAddedby)?int.Parse(model.readingElectricAddedby):readingElectric.readingelectric_addedby;
                            readingElectric.readingelectric_units = !string.IsNullOrEmpty(model.readingElectricUnits)?double.Parse(model.readingElectricUnits):readingElectric.readingelectric_units;
                            readingElectric.readingelectric_month = !string.IsNullOrEmpty(model.readingElectricMonth)?model.readingElectricMonth : readingElectric.readingelectric_month;
                            readingElectric.readingelectric_currentreading = !string.IsNullOrEmpty(model.readingElectricCurrentReading)?double.Parse(model.readingElectricCurrentReading):readingElectric.readingelectric_currentreading;
                            readingElectric.readingelectric_datetime = DateTime.UtcNow.AddHours(5);
                            readingElectric.readingelectric_prevreading = !string.IsNullOrEmpty(model.readingElectricPrevReading)?double.Parse(model.readingElectricPrevReading):readingElectric.readingelectric_prevreading;
                            readingElectric.readingelectric_meterno = !string.IsNullOrEmpty(model.readingElectricMeterNo) ? model.readingElectricMeterNo : readingElectric.readingelectric_meterno;
                            readingElectric.readingelectric_remarks = !string.IsNullOrEmpty(model.readingElectricRemarks) ? model.readingElectricRemarks : readingElectric.readingelectric_remarks;
                            db.SaveChanges();
                            toReturn = new ReadingElectricResponseModel()
                            {
                                remarks = "Successfully Updated",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new ReadingElectricResponseModel()
                            {
                                remarks= "No Record Found",
                                resultCode= "1200"
                            };
                        }
                    }
                }
                else
                {
                    toReturn = new ReadingElectricResponseModel()
                    {
                        remarks="Please Select Electric Reading",
                        resultCode ="1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new ReadingElectricResponseModel()
                {
                    remarks = "There Was Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public ReadingElectricResponseModel DeleteReadingElectric(ReadingElectricRequestModel model)
        {
            ReadingElectricResponseModel toReturn = new ReadingElectricResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.readingElectricId))
                {
                    var readingElectricId = int.Parse(model.readingElectricId);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var readingElectric = (from x in db.tbl_readingelectric where x.readingelectric_id == readingElectricId select x).FirstOrDefault();
                        if (readingElectric != null)
                        {
                            var readingPicture = (from x in db.tbl_readingpicture where x.readingpicture_id == readingElectric.fk_readingpicture select x).FirstOrDefault();
                            if (readingPicture != null)
                            {
                                db.tbl_readingpicture.Remove(readingPicture);
                                db.SaveChanges();
                            }
                            db.tbl_readingelectric.Remove(readingElectric);
                            db.SaveChanges();
                            toReturn = new ReadingElectricResponseModel()
                            {
                                remarks = "Successfully Deleted",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new ReadingElectricResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                }
                else
                {
                    toReturn = new ReadingElectricResponseModel()
                    {
                        remarks = "Please Select Electric Reading",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new ReadingElectricResponseModel()
                {
                    remarks = "There Was Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        /// <summary>
        /// old method not being used because calculated wrong bill still kept it .. might revert back to it, delete it when it is confirmed
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReadingElectricResponseModel ApproveReadingElectric(ReadingElectricRequestModel model)
        {
            ReadingElectricResponseModel toReturn = new ReadingElectricResponseModel();
            try
            {
                if(new ModelsValidatorHelper().validateint(model.userId))
                {
                    int userId = int.Parse(model.userId);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        if (new ModelsValidatorHelper().validateint(model.readingElectricId))
                        {
                            int readingElectricId = int.Parse(model.readingElectricId);
                            var readingElectric = (from x in db.tbl_readingelectric where x.readingelectric_id == readingElectricId select x).FirstOrDefault();
                            double _units = readingElectric.readingelectric_units;
                            var location = (from x in db.tbl_location
                                            join y in db.tbl_consummer_pool on x.location_id equals y.fk_location
                                            where y.consummer_no == readingElectric.readingelectric_meterno
                                            select x).FirstOrDefault();
                            if (location != null)
                            {
                                var residentBuilding = (from x in db.tbl_residentbuilding where x.fk_building == location.location_id select x).FirstOrDefault();
                                if (residentBuilding != null)
                                {
                                    var previousPendingBill = (from x in db.tbl_billelectric where x.fk_paymentstatus == 3 && x.fk_location == residentBuilding.fk_building select x).FirstOrDefault();
                                    var outstanding = "";
                                    if (previousPendingBill != null)
                                    {
                                        outstanding = previousPendingBill.billelectric_outstanding.ToString();
                                        previousPendingBill.fk_paymentstatus = 2;
                                        db.SaveChanges();
                                        var newPaymentHistory = new tbl_paymenthistory();
                                        newPaymentHistory.paymentmonth = new MonthFinderHelpers().GetPreviousMonth(readingElectric.readingelectric_month);
                                        newPaymentHistory.payment_amount = 0;
                                        newPaymentHistory.fk_paymenttype = 0;
                                        newPaymentHistory.paymenthistory_datetime = DateTime.UtcNow.AddHours(5);
                                        db.tbl_paymenthistory.Add(newPaymentHistory);
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        outstanding = "0";
                                    }

                                    var readingPicture = (from x in db.tbl_readingpicture where x.readingpicture_id == readingElectric.fk_readingpicture select x).FirstOrDefault();
                                    if (readingPicture != null)
                                    {
                                        var newBillPicture = new tbl_billpicture()
                                        {
                                            billpicture_date = readingPicture.readingpicture_data,
                                            billpicture_size = readingPicture.readingpicture_size,
                                            billpicture_type = readingPicture.readingpicture_type
                                        };
                                        db.tbl_billpicture.Add(newBillPicture);
                                        db.SaveChanges();

                                        //Calculating Bill Amount & Adding Bill
                                        var fpa = (from x in db.tbl_fixedrates where x.fixedrates_id == 1 select x.fixedrates_amount).FirstOrDefault();
                                        var meterRent = (from x in db.tbl_fixedrates where x.fixedrates_id == 2 select x.fixedrates_amount).FirstOrDefault();
                                        var slabs = (from x in db.tbl_slabs select x).ToList();
                                        //Calculations
                                        double totalAmount = 0.0;
                                        double totalEnergyCharges = 0.0;
                                        double totalFPA = 0.0;
                                        for(int i=0;i<slabs.Count;i++)
                                        {
                                            if(totalEnergyCharges == 0)
                                            {

                                                if (slabs[i].slab_tariff.Contains(">"))
                                                {
                                                    var limit = double.Parse(slabs[i].slab_tariff.Replace(">", " ").Trim());
                                                    if (_units <= limit)
                                                    {
                                                        totalAmount = slabs[i].slab_net_rate.Value * _units;
                                                        totalEnergyCharges = slabs[i].slab_energy_charges.Value * _units;
                                                        totalFPA = fpa * _units;
                                                    }

                                                }
                                                else if (slabs[i].slab_tariff.Contains("-"))
                                                {
                                                    var limitLow = double.Parse(slabs[i].slab_tariff.Split('-')[0].Trim());
                                                    var limitHigh = double.Parse(slabs[i].slab_tariff.Split('-')[1].Trim());
                                                    if (_units >= limitLow && _units <= limitHigh)
                                                    {
                                                        double extraUnits = limitHigh - _units;
                                                        _units = _units - extraUnits;
                                                        double extraTotalAmount = slabs[i].slab_net_rate.Value * extraUnits;
                                                        double extraEnergy = slabs[i].slab_energy_charges.Value * extraUnits;
                                                        double extraFPA = fpa * extraUnits;
                                                        totalAmount = (slabs[i - 1].slab_net_rate.Value * _units) + extraTotalAmount;
                                                        totalEnergyCharges = (slabs[i - 1].slab_energy_charges.Value * _units) + extraEnergy;
                                                        totalFPA = (fpa * _units) + extraFPA;
                                                    }
                                                }
                                                else if (slabs[i].slab_tariff.Contains("<"))
                                                {
                                                    var limit = double.Parse(slabs[i].slab_tariff.Replace("<", " ").Trim());
                                                    if (_units > limit)
                                                    {
                                                        double extraUnits = _units - limit;
                                                        _units = _units - extraUnits;
                                                        double extraTotalAmount = slabs[i].slab_net_rate.Value * extraUnits;
                                                        double extraEnergy = slabs[i].slab_energy_charges.Value * extraUnits;
                                                        double extraFPA = fpa * extraUnits;
                                                        totalAmount = (slabs[i-1].slab_net_rate.Value * _units) + extraTotalAmount;
                                                        totalEnergyCharges = (slabs[i-1].slab_energy_charges.Value * _units) + extraEnergy;
                                                        totalFPA = (fpa * _units) + extraFPA;
                                                    }
                                                }
                                                else
                                                {
                                                    totalAmount = slabs[i].slab_net_rate.Value * _units;
                                                    totalEnergyCharges = slabs[i].slab_energy_charges.Value * _units;
                                                    totalFPA = fpa * _units;
                                                }
                                            }
                                        }
                                        totalEnergyCharges = totalEnergyCharges / 2;
                                        totalAmount = totalAmount + (totalFPA  + meterRent);
                                        totalAmount = (totalAmount - totalEnergyCharges) ;
                                        /// Entry
                                        var newBill = new tbl_billelectric()
                                        {
                                            fk_billpicture = newBillPicture.billpicture_id,
                                            fk_location = residentBuilding.fk_building,
                                            fk_resident = residentBuilding.fk_resident,
                                            fk_paymentstatus = 3,
                                            billelectric_amount = totalAmount,
                                            billelectric_currentreading = readingElectric.readingelectric_currentreading,
                                            billelectric_month = readingElectric.readingelectric_month,
                                            billelectric_outstanding = 0,
                                            billelectric_prevreading = readingElectric.readingelectric_prevreading,
                                            billelectric_units = readingElectric.readingelectric_units,
                                            billelectric_fpa = totalFPA,
                                            billelectric_rebate = totalEnergyCharges,
                                            billelectric_datetime = readingElectric.readingelectric_datetime,
                                            billelectric_remarks = readingElectric.readingelectric_remarks
                                        };
                                        db.tbl_billelectric.Add(newBill);
                                        db.SaveChanges();
                                        newBill.billelectric_outstanding = newBill.billelectric_amount + double.Parse(outstanding);
                                        newBill.billelectric_amount = newBill.billelectric_amount + double.Parse(outstanding);
                                        var newReadingElectricLog = new tbl_readingelectriclog()
                                        {
                                            readingelectriclog_addedby = readingElectric.readingelectric_addedby,
                                            readingelectriclog_archivedby = userId,
                                            readingelectriclog_archivedon = DateTime.UtcNow.AddHours(5),
                                            readingelectriclog_currentreading = readingElectric.readingelectric_currentreading,
                                            readingelectriclog_datetime = readingElectric.readingelectric_datetime,
                                            readingelectriclog_meterno = readingElectric.readingelectric_meterno,
                                            readingelectriclog_month = readingElectric.readingelectric_month,
                                            readingelectriclog_prevreading = readingElectric.readingelectric_prevreading,
                                            readingelectriclog_remarks = readingElectric.readingelectric_remarks,
                                            readingelectriclog_units = readingElectric.readingelectric_units,
                                        };
                                        db.tbl_readingelectriclog.Add(newReadingElectricLog);
                                        db.SaveChanges();
                                        db.tbl_readingelectric.Remove(readingElectric);
                                        db.tbl_readingpicture.Remove(readingPicture);
                                        db.SaveChanges();
                                        toReturn = new ReadingElectricResponseModel()
                                        {
                                            remarks = "Reading Successfully Approved",
                                            resultCode = "1100"
                                        };
                                    }
                                    else
                                    {
                                        toReturn = new ReadingElectricResponseModel()
                                        {
                                            remarks = "No Reading Picture Found",
                                            resultCode = "1200"
                                        };
                                    }
                                }
                                else
                                {
                                    toReturn = new ReadingElectricResponseModel()
                                    {
                                        remarks = "No Resident Found",
                                        resultCode = "1200"
                                    };
                                }
                            }
                            else
                            {
                                toReturn = new ReadingElectricResponseModel()
                                {
                                    remarks = " No Meter Found With Meter No " + model.readingElectricMeterNo,
                                    resultCode = "1200"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new ReadingElectricResponseModel()
                            {
                                remarks = "Please Provide Reading",
                                resultCode = "1300"
                            };
                        }
                    } 
                }
                else
                {
                    toReturn = new ReadingElectricResponseModel()
                    {
                        remarks = "Please provide Logged In User",
                        resultCode = "1300"
                    };
                }
            }
            catch(Exception Ex)
            {
                toReturn = new ReadingElectricResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public ReadingElectricResponseModel ApproveElectricReading(ReadingElectricRequestModel model)
        {
            ReadingElectricResponseModel toReturn = new ReadingElectricResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.userId))
                {
                    int userId = int.Parse(model.userId);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        if (new ModelsValidatorHelper().validateint(model.readingElectricId))
                        {
                            int readingElectricId = int.Parse(model.readingElectricId);
                            var readingElectric = (from x in db.tbl_readingelectric where x.readingelectric_id == readingElectricId select x).FirstOrDefault();
                            double _units = readingElectric.readingelectric_units;
                            var location = (from x in db.tbl_location
                                            join y in db.tbl_consummer_pool on x.location_id equals y.fk_location
                                            where y.consummer_no == readingElectric.readingelectric_meterno select x).FirstOrDefault();
                            if (location != null)
                            {
                                var resident = db.tbl_residents.Where(x => x.resident_id == readingElectric.fk_resident).FirstOrDefault();
                                if (resident != null)
                                {
                                   

                                    var readingPicture = (from x in db.tbl_readingpicture where x.readingpicture_id == readingElectric.fk_readingpicture select x).FirstOrDefault();
                                    if (readingPicture != null)
                                    {
                                       

                                        var response = new SubDataHelpers.ReadingElectricSubHelpers().CalculateBill(_units, resident.resident_pin_code, location.location_id);
                                        double totalAmount = Math.Round(response.totalAmount);
                                        double totalEnergyCharges = Math.Round(response.totalEnergyCharges);
                                        double totalFPA = Math.Round(response.totalFPA);
                                        /// Entry
                                        var newBill = new tbl_approvereadings()
                                        {
                                            reading_meterno = readingElectric.readingelectric_meterno,
                                            reading_addedby = readingElectric.readingelectric_addedby,
                                            fk_readingpicture = readingPicture.readingpicture_id,
                                            fk_location = location.location_id,
                                            fk_resident = resident.resident_id,
                                            bill_amount = totalAmount,
                                            reading_currentreading = readingElectric.readingelectric_currentreading,
                                            reading_month = readingElectric.readingelectric_month,
                                            reading_prevreading = readingElectric.readingelectric_prevreading,
                                            reading_units = readingElectric.readingelectric_units,
                                            bill_fpa = totalFPA,
                                            bill_rebate = totalEnergyCharges,
                                            reading_datetime = readingElectric.readingelectric_datetime,
                                            readinge_remarks = readingElectric.readingelectric_remarks,
                                            bill_tv = response.tvCharges,
                                            bill_meter_rent = response.meterRent,
                                            bill_water = response.waterCharges,
                                            
                                        };
                                        db.tbl_approvereadings.Add(newBill);
                                        db.tbl_readingelectric.Remove(readingElectric);
                                        db.SaveChanges();
                                        var newReadingElectricLog = new tbl_readingelectriclog()
                                        {
                                            readingelectriclog_addedby = readingElectric.readingelectric_addedby,
                                            readingelectriclog_archivedby = userId,
                                            readingelectriclog_archivedon = DateTime.UtcNow.AddHours(5),
                                            readingelectriclog_currentreading = readingElectric.readingelectric_currentreading,
                                            readingelectriclog_datetime = readingElectric.readingelectric_datetime,
                                            readingelectriclog_meterno = readingElectric.readingelectric_meterno,
                                            readingelectriclog_month = readingElectric.readingelectric_month,
                                            readingelectriclog_prevreading = readingElectric.readingelectric_prevreading,
                                            readingelectriclog_remarks = readingElectric.readingelectric_remarks,
                                            readingelectriclog_units = readingElectric.readingelectric_units,
                                        };
                                        db.tbl_readingelectriclog.Add(newReadingElectricLog);
                                        db.SaveChanges();
                                
                                        db.SaveChanges();
                                        toReturn = new ReadingElectricResponseModel()
                                        {
                                            remarks = "Reading Successfully Approved",
                                            resultCode = "1100"
                                        };
                                        //var newOutstanding = new tbl_outstanding()
                                        //{
                                        //    fk_consummer_no = readingElectric.readingelectric_meterno,
                                        //    outstanding_amount = newBill.billelectric_amount,
                                        //    outstanding_date = DateTime.UtcNow.AddHours(5),
                                        //    outstanding_month = newBill.billelectric_month,
                                        //    fk_resident= resident.resident_id,
                                        //    fk_location = location.location_id
                                        //};
                                        //db.tbl_outstanding.Add(newOutstanding);
                                        //db.SaveChanges();
                                    }
                                    else
                                    {
                                        toReturn = new ReadingElectricResponseModel()
                                        {
                                            remarks = "No Reading Picture Found",
                                            resultCode = "1200"
                                        };
                                    }
                                }
                                else
                                {
                                    toReturn = new ReadingElectricResponseModel()
                                    {
                                        remarks = "No Resident Found",
                                        resultCode = "1200"
                                    };
                                }
                            }
                            else
                            {
                                toReturn = new ReadingElectricResponseModel()
                                {
                                    remarks = " No Location Found With Meter No " + model.readingElectricMeterNo,
                                    resultCode = "1200"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new ReadingElectricResponseModel()
                            {
                                remarks = "Please Provide Reading",
                                resultCode = "1300"
                            };
                        }
                    }
                }
                else
                {
                    toReturn = new ReadingElectricResponseModel()
                    {
                        remarks = "Please provide Logged In User",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new ReadingElectricResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public ApproveElectricResponseModel calculateReadingElectric(ReadingElectricRequestModel model)
        {
            ApproveElectricResponseModel toReturn = new ApproveElectricResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.userId))
                {
                    int userId = int.Parse(model.userId);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        if (new ModelsValidatorHelper().validateint(model.readingElectricId))
                        {
                            int readingElectricId = int.Parse(model.readingElectricId);
                            var readingElectric = (from x in db.tbl_readingelectric where x.readingelectric_id == readingElectricId select x).FirstOrDefault();
                            double _units = readingElectric.readingelectric_units;
                            var location = (from x in db.tbl_location
                                            join y in db.tbl_subarea on x.fk_subarea equals y.subarea_id
                                            join z in db.tbl_area on y.fk_area equals z.area_id
                                            join c in db.tbl_consummer_pool on x.location_id equals c.fk_location
                                            where c.consummer_no == readingElectric.readingelectric_meterno
                                            select new
                                            {
                                                x.location_id,
                                                x.location_name,
                                                y.subarea_name,
                                                z.area_name
                                            }).FirstOrDefault();
                            if (location != null)
                            {
                                var resident= db.tbl_residents.Where(x=>x.resident_id== readingElectric.fk_resident).FirstOrDefault();
                                if (resident != null)
                                {
                                   
                                    var response = new SubDataHelpers.ReadingElectricSubHelpers().CalculateBill(_units, resident.resident_pin_code, location.location_id);
                                    double totalAmount = Math.Round(response.totalAmount);
                                    double totalEnergyCharges = Math.Round(response.totalEnergyCharges);
                                    double totalFPA = Math.Round(response.totalFPA);
                                        

                                    /// Entry
                                    toReturn = new ApproveElectricResponseModel()
                                    {
                                        locationMeterNo = readingElectric.readingelectric_meterno,
                                        billElectricAmount = totalAmount.ToString(),
                                        billElectricCurrentReading = readingElectric.readingelectric_currentreading.ToString(),
                                        billElectricMonth = !String.IsNullOrEmpty(readingElectric.readingelectric_month) ? readingElectric.readingelectric_month : "",
                                        billElectricPrevReading = readingElectric.readingelectric_prevreading.ToString(),
                                        billElectricRemarks = !string.IsNullOrEmpty(readingElectric.readingelectric_remarks) ? readingElectric.readingelectric_remarks : "",
                                        billElectricUnits = readingElectric.readingelectric_units.ToString(),
                                        billElectricWater = response.waterCharges.ToString(),
                                        billElectricTv = response.tvCharges.ToString(),
                                        energyCharges = totalEnergyCharges.ToString(),
                                        locationName = !string.IsNullOrEmpty(location.location_name) ? location.location_name : "",
                                        subAreaName = !string.IsNullOrEmpty(location.subarea_name) ? location.subarea_name : "",
                                        areaName = !string.IsNullOrEmpty(location.area_name) ? location.area_name : "",
                                        remarks = "Successfully Found",
                                        resultCode = "1100",
                                    };
                                }
                                else
                                {
                                    toReturn = new ApproveElectricResponseModel()
                                    {
                                        remarks = "No Resident Found",
                                        resultCode = "1200"
                                    };
                                }
                            }
                            else
                            {
                                toReturn = new ApproveElectricResponseModel()
                                {
                                    remarks = " No Location Found With Meter No " + model.readingElectricMeterNo,
                                    resultCode = "1200"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new ApproveElectricResponseModel()
                            {
                                remarks = "Please Provide Reading",
                                resultCode = "1300"
                            };
                        }
                    }
                }
                else
                {
                    toReturn = new ApproveElectricResponseModel()
                    {
                        remarks = "Please provide Logged In User",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new ApproveElectricResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public List<ReadingElectricResponseModel> getReadingsByUser(ReadingElectricRequestModel model)
        {
            List<ReadingElectricResponseModel> toRetrun = new List<ReadingElectricResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    int user = int.Parse(model.readingElectricAddedby);
                    var meters = (from x in db.tbl_userareas
                                  join y in db.tbl_location on x.fk_subarea equals y.fk_subarea
                                  join z in db.tbl_consummer_pool on y.location_id equals z.fk_location
                                  where x.fk_user == user
                                  select z.consummer_no).ToList();
                    var readings = (from x in db.tbl_readingelectric
                                    join z in db.tbl_users on x.readingelectric_addedby equals z.users_id
                                    join con in db.tbl_consummer_pool on x.readingelectric_meterno equals con.consummer_no
                                    join a in db.tbl_location on con.fk_location equals a.location_id
                                    join b in db.tbl_subarea on a.fk_subarea equals b.subarea_id
                                    join c in db.tbl_area on b.fk_area equals c.area_id
                                    join r in db.tbl_residents on x.fk_resident equals r.resident_id
                                    join y in db.tbl_readingpicture on x.fk_readingpicture equals y.readingpicture_id into pList
                                    from y in pList.DefaultIfEmpty()
                                    select new
                                    {
                                       x.fk_readingpicture,
                                       x.readingelectric_addedby,
                                       x.readingelectric_currentreading,
                                       x.readingelectric_datetime,
                                       x.readingelectric_id,
                                       x.readingelectric_meterno,
                                       x.readingelectric_month,
                                       x.readingelectric_prevreading,
                                       x.readingelectric_remarks,
                                       x.readingelectric_units,
                                       x.is_secondary,
                                       y.readingpicture_data,
                                       y.readingpicture_size,
                                       y.readingpicture_type,
                                       a.location_id,
                                       b.subarea_id,
                                       c.area_id,
                                       a.location_name,
                                       b.subarea_name,
                                       c.area_name,
                                       a.fk_subarea,
                                       z.users_fullname,
                                       r.resident_panumber,
                                       r.resident_id,
                                       r.resident_name,
                                       r.resident_pin_code,
                                       r.resident_rank,
                                       r.resident_unit
                                    }).Where(x => meters.Contains(x.readingelectric_meterno)).ToList();
                    if (readings.Count() > 0)
                    {
                        toRetrun = readings.Select(x => new ReadingElectricResponseModel()
                        {
                            area_id = x.area_id.ToString(),
                            area_name = x.area_name.ToString(),
                            location_id = x.location_id.ToString(),
                            location_name = x.location_name.ToString(),
                            subarea_id = x.subarea_id.ToString(),
                            subarea_name = x.subarea_name.ToString(),
                            fk_readingPicture = x.fk_readingpicture.ToString(),
                            readingElectricAddedby = x.users_fullname.ToString(),
                            readingElectricCurrentReading = x.readingelectric_currentreading.ToString(),
                            readingElectricDatetime = x.readingelectric_datetime.ToString(),
                            readingElectricId = x.readingelectric_id.ToString(),
                            readingElectricMeterNo = !string.IsNullOrEmpty(x.readingelectric_meterno) ? x.readingelectric_meterno : "",
                            readingElectricMonth = !string.IsNullOrEmpty(x.readingelectric_month) ? x.readingelectric_month : "",
                            readingElectricPrevReading = x.readingelectric_prevreading.ToString(),
                            readingElectricRemarks = !string.IsNullOrEmpty(x.readingelectric_remarks) ? x.readingelectric_remarks : "",
                            readingElectricUnits = x.readingelectric_units.ToString(),
                            readingpicture_data = x.readingpicture_data,
                            readingpicture_size = x.readingpicture_size.ToString(),
                            readingpicture_type = x.readingpicture_type,
                            residentId= x.resident_id.ToString(),
                            residentName= x.resident_name,
                            residentPANo= x.resident_panumber,
                            residentRank = x.resident_rank,
                            residentUnit= x.resident_unit,
                            residentPinCode= x.resident_pin_code.ToString(),
                            meterStatus = x.is_secondary.ToString(),
                            remarks = "Successfully Found",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toRetrun.Add(new ReadingElectricResponseModel()
                        {
                            remarks="No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch(Exception Ex)
            {
                toRetrun.Add(new ReadingElectricResponseModel()
                {
                    resultCode = "1000",
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                });
            }
            return toRetrun;
        }
        public List<ReadingElectricResponseModel> getApprovedReadingsByUser(ReadingElectricRequestModel model)
        {
            List<ReadingElectricResponseModel> toReturn = new List<ReadingElectricResponseModel>();
            List<ReadingElectricResponseModel> response = new List<ReadingElectricResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    int user = int.Parse(model.readingElectricAddedby);
                    var meters = (from x in db.tbl_userareas
                                  join y in db.tbl_location on x.fk_subarea equals y.fk_subarea
                                  join z in db.tbl_consummer_pool on y.location_id equals z.fk_location
                                  where x.fk_user == user
                                  select z.consummer_no).ToList();
                    var readings = (from x in db.tbl_approvereadings
                                    join z in db.tbl_users on x.reading_addedby equals z.users_id
                                    join con in db.tbl_consummer_pool on x.reading_meterno equals con.consummer_no
                                    join a in db.tbl_location on con.fk_location equals a.location_id
                                    join b in db.tbl_subarea on a.fk_subarea equals b.subarea_id
                                    join c in db.tbl_area on b.fk_area equals c.area_id
                                    join r in db.tbl_residents on x.fk_resident equals r.resident_id
                                    join y in db.tbl_readingpicture on x.fk_readingpicture equals y.readingpicture_id into pList
                                    from y in pList.DefaultIfEmpty()
                                    where  x.reading_meterno == model.readingElectricMeterNo && x.reading_month == model.readingElectricMonth
                                    select new
                                    {
                                        x.fk_readingpicture,
                                        x.reading_addedby,
                                        x.reading_currentreading,
                                        x.reading_datetime,
                                        x.reading_id,
                                        x.reading_meterno,
                                        x.reading_month,
                                        x.reading_prevreading,
                                        x.readinge_remarks,
                                        x.reading_units,
                                        x.is_secondary,
                                        x.bill_amount,
                                        x.bill_fpa,
                                        x.bill_gst,
                                        x.bill_meter_rent,
                                        x.bill_rebate,
                                        x.bill_tv,
                                        x.bill_water,
                                        y.readingpicture_data,
                                        y.readingpicture_size,
                                        y.readingpicture_type,
                                        a.location_id,
                                        b.subarea_id,
                                        c.area_id,
                                        a.location_name,
                                        b.subarea_name,
                                        c.area_name,
                                        a.fk_subarea,
                                        z.users_fullname,
                                        r.resident_panumber,
                                        r.resident_id,
                                        r.resident_name,
                                        r.resident_pin_code,
                                        r.resident_rank,
                                        r.resident_unit
                                    }).Where(x => meters.Contains(x.reading_meterno)).ToList();
                   
                    if (readings.Count() > 0)
                    {
                        toReturn = readings.Select(x => new ReadingElectricResponseModel()
                        {
                            area_id = x.area_id.ToString(),
                            area_name = x.area_name.ToString(),
                            location_id = x.location_id.ToString(),
                            location_name = x.location_name.ToString(),
                            subarea_id = x.subarea_id.ToString(),
                            subarea_name = x.subarea_name.ToString(),
                            fk_readingPicture = x.fk_readingpicture.ToString(),
                            readingElectricAddedby = x.users_fullname.ToString(),
                            readingElectricCurrentReading = x.reading_currentreading.ToString(),
                            readingElectricDatetime = x.reading_datetime.ToString(),
                            readingElectricId = x.reading_id.ToString(),
                            readingElectricMeterNo = !string.IsNullOrEmpty(x.reading_meterno) ? x.reading_meterno : "",
                            readingElectricMonth = !string.IsNullOrEmpty(x.reading_month) ? x.reading_month : "",
                            readingElectricPrevReading = x.reading_prevreading.ToString(),
                            readingElectricRemarks = !string.IsNullOrEmpty(x.readinge_remarks) ? x.readinge_remarks : "",
                            readingElectricUnits = x.reading_units.ToString(),
                            readingpicture_data = x.readingpicture_data,
                            readingpicture_size = x.readingpicture_size.ToString(),
                            readingpicture_type = x.readingpicture_type,
                            residentId = x.resident_id.ToString(),
                            residentName = x.resident_name,
                            residentPANo = x.resident_panumber,
                            residentRank = x.resident_rank,
                            residentUnit = x.resident_unit,
                            residentPinCode = x.resident_pin_code.ToString(),
                            meterStatus = x.is_secondary.ToString(),
                            billAmount = x.bill_amount.ToString(),
                            billFpa = x.bill_fpa.ToString(),
                            billGST = x.bill_gst.ToString(),
                            billRebate = x.bill_rebate.ToString(),
                            billTv= x.bill_tv.ToString(),
                            billWater = x.bill_water.ToString(),
                            remarks = "Successfully Found",
                            resultCode = "1100"
                        }).ToList();
                    }
                 
                    if (!string.IsNullOrEmpty(model.residentName))
                    {
                        toReturn = toReturn.Where(x => x.residentName.Contains(model.residentName)).ToList();
                    }
                    if(toReturn.Count()<1)                  
                    {
                        toReturn.Add(new ReadingElectricResponseModel()
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new ReadingElectricResponseModel()
                {
                    resultCode = "1000",
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                });
            }
            return toReturn;
        }
        public List<ReadingElectricResponseModel> getAllReadings(ReadingElectricRequestModel model)
        {
            List<ReadingElectricResponseModel> toRetrun = new List<ReadingElectricResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    int user = int.Parse(model.readingElectricAddedby);
                    var meters = (from x in db.tbl_userareas
                                  join y in db.tbl_location on x.fk_subarea equals y.fk_subarea
                                  join z in db.tbl_consummer_pool on y.location_id equals z.fk_location
                                  where x.fk_user == user
                                  select z.consummer_no).ToList();
                    var readings = (from x in db.tbl_readingelectric
                                    join z in db.tbl_users on x.readingelectric_addedby equals z.users_id
                                    join con in db.tbl_consummer_pool on x.readingelectric_meterno equals con.consummer_no
                                    join a in db.tbl_location on con.fk_location equals a.location_id
                                    join b in db.tbl_subarea on a.fk_subarea equals b.subarea_id
                                    join c in db.tbl_area on b.fk_area equals c.area_id
                                    join r in db.tbl_residents on x.fk_resident equals r.resident_id
                                    join y in db.tbl_readingpicture on x.fk_readingpicture equals y.readingpicture_id into pList
                                    from y in pList.DefaultIfEmpty()
                                    select new
                                    {
                                        x.fk_readingpicture,
                                        x.readingelectric_addedby,
                                        x.readingelectric_currentreading,
                                        x.readingelectric_datetime,
                                        x.readingelectric_id,
                                        x.readingelectric_meterno,
                                        x.readingelectric_month,
                                        x.readingelectric_prevreading,
                                        x.readingelectric_remarks,
                                        x.readingelectric_units,
                                        y.readingpicture_data,
                                        y.readingpicture_size,
                                        y.readingpicture_type,
                                        a.location_id,
                                        b.subarea_id,
                                        c.area_id,
                                        a.location_name,
                                        b.subarea_name,
                                        c.area_name,
                                        a.fk_subarea,
                                        z.users_fullname,
                                        r.resident_panumber,
                                        r.resident_id,
                                        r.resident_name,
                                        r.resident_pin_code,
                                        r.resident_rank,
                                        r.resident_unit
                                    }).ToList();
                    if (readings.Count() > 0)
                    {
                        toRetrun = readings.Select(x => new ReadingElectricResponseModel()
                        {
                            area_id = x.area_id.ToString(),
                            area_name = x.area_name.ToString(),
                            location_id = x.location_id.ToString(),
                            location_name = x.location_name.ToString(),
                            subarea_id = x.subarea_id.ToString(),
                            subarea_name = x.subarea_name.ToString(),
                            fk_readingPicture = x.fk_readingpicture.ToString(),
                            readingElectricAddedby = x.users_fullname.ToString(),
                            readingElectricCurrentReading = x.readingelectric_currentreading.ToString(),
                            readingElectricDatetime = x.readingelectric_datetime.ToString(),
                            readingElectricId = x.readingelectric_id.ToString(),
                            readingElectricMeterNo = !string.IsNullOrEmpty(x.readingelectric_meterno) ? x.readingelectric_meterno : "",
                            readingElectricMonth = !string.IsNullOrEmpty(x.readingelectric_month) ? x.readingelectric_month : "",
                            readingElectricPrevReading = x.readingelectric_prevreading.ToString(),
                            readingElectricRemarks = !string.IsNullOrEmpty(x.readingelectric_remarks) ? x.readingelectric_remarks : "",
                            readingElectricUnits = x.readingelectric_units.ToString(),
                            readingpicture_data = x.readingpicture_data,
                            readingpicture_size = x.readingpicture_size.ToString(),
                            readingpicture_type = x.readingpicture_type,
                            residentId = x.resident_id.ToString(),
                            residentName = x.resident_name,
                            residentPANo = x.resident_panumber,
                            residentRank = x.resident_rank,
                            residentUnit = x.resident_unit,
                            residentPinCode = x.resident_pin_code.ToString(),
                            remarks = "Successfully Found",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toRetrun.Add(new ReadingElectricResponseModel()
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toRetrun.Add(new ReadingElectricResponseModel()
                {
                    resultCode = "1000",
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                });
            }
            return toRetrun;
        }
        public LocationResponseModel getAllDetailByConsumerNo (LocationRequestModel model)
        {
            LocationResponseModel toReturn = new LocationResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(model.locationElectricMeter))
                {
                    using(db_bmsEntities db = new db_bmsEntities())
                    {
                        var location = (from x in db.tbl_location
                                        join y in db.tbl_subarea on x.fk_subarea equals y.subarea_id
                                        join a in db.tbl_area on y.fk_area equals a.area_id
                                        join z in db.tbl_consummer_pool on x.location_id equals z.fk_location
                                        join rb in db.tbl_residents on z.id equals rb.resident_consumer_no
                                        where z.consummer_no == model.locationElectricMeter
                                        select new
                                        {
                                            x.fk_subarea,
                                            x.location_id,
                                            x.location_name,
                                            a.area_id,
                                            a.area_name,
                                            rb.resident_id,
                                            y.subarea_name
                                        }).FirstOrDefault();
                        if (location != null)
                        {
                            toReturn = new LocationResponseModel()
                            {
                                areaName = location.area_name,
                                fk_area = location.area_id.ToString(),
                                subAreaName = location.subarea_name,
                                fk_subArea = location.fk_subarea.ToString(),
                                locationName = !string.IsNullOrEmpty(location.location_name) ? location.location_name : "",
                                locationId = location.location_id.ToString(),
                                residentId = location.resident_id.ToString(),
                                remarks = "Successfully Location Found",
                                resultCode = "1100"
                            };
                            var bill = (from x in db.tbl_billelectric where x.fk_location == location.location_id select x).OrderByDescending(x => x.billelectric_datetime).FirstOrDefault();
                            var billGas = (from x in db.tbl_billgas where x.fk_location == location.location_id select x).OrderByDescending(x => x.datetime).FirstOrDefault();
                            if (bill != null)
                            {
                                toReturn.totalElectricAmount = bill.billelectric_amount.ToString();
                                toReturn.previousReading = !String.IsNullOrEmpty(bill.billelectric_prevreading.ToString()) ? bill.billelectric_prevreading.ToString() : "";
                                toReturn.outstanding = !String.IsNullOrEmpty(bill.billelectric_outstanding.ToString()) ? bill.billelectric_outstanding.ToString() : "";
                                toReturn.billMonth = !string.IsNullOrEmpty(bill.billelectric_month) ? bill.billelectric_month : "";
                                toReturn.currentReading = bill.billelectric_currentreading.ToString();
                                toReturn.currentUnit = bill.billelectric_units.ToString();
                            }
                            else
                            {
                                toReturn.previousReading = "0";
                                toReturn.outstanding = "0";
                                toReturn.billMonth = "0";
                                toReturn.currentReading = "0";
                                toReturn.currentUnit = "0";

                            }
                            if (billGas != null)
                            {
                                toReturn.previousGasReading = billGas.prevreading.ToString();
                                toReturn.gasOutstanding = billGas.outstanding.ToString();
                                toReturn.billGasMonth = billGas.month.ToString();
                                toReturn.currentGasReading = billGas.currentreading.ToString();
                                toReturn.currentGasUnit = billGas.units.ToString();
                            }
                            else
                            {
                                toReturn.previousGasReading = "0";
                                toReturn.gasOutstanding = "0";
                                toReturn.billGasMonth = "0";
                                toReturn.currentGasReading = "0";
                                toReturn.currentGasUnit = "0";
                            }
                        }
                        else
                        {
                            toReturn = new LocationResponseModel()
                            {
                                resultCode = "1200",
                                remarks = "No Record Found"
                            };
                        }
                    }
                }
                else
                {
                    toReturn = new LocationResponseModel()
                    {
                        remarks = "Please Provide Consumer No",
                        resultCode ="1300"
                    };
                }
            }
            catch(Exception Ex)
            {
                toReturn = new LocationResponseModel()
                {
                    remarks = "There Was A Fatal Error "+Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public ReadingElectricDetailByConsumerNoResponseModel getAllDetailByConsumerNoForReadingElectric(ReadingElectricDetailByConsumerNoRequestModel model)
        {
            ReadingElectricDetailByConsumerNoResponseModel toReturn = new ReadingElectricDetailByConsumerNoResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(model.consummerNo))
                {
                    int meterStatus = int.Parse(model.meterStatus);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var location = (from x in db.tbl_location
                                        join y in db.tbl_subarea on x.fk_subarea equals y.subarea_id
                                        join a in db.tbl_area on y.fk_area equals a.area_id
                                        join z in db.tbl_consummer_pool on x.location_id equals z.fk_location
                                        join rb in db.tbl_residents on z.id equals rb.resident_consumer_no
                                        where z.consummer_no == model.consummerNo 
                                        select new
                                        {
                                            x.fk_subarea,
                                            x.location_id,
                                            x.location_name,
                                            a.area_id,
                                            a.area_name,
                                            rb.resident_id,
                                            y.subarea_name
                                        }).FirstOrDefault();
                        var reading = db.tbl_readingelectric.Where(x => x.readingelectric_meterno == model.consummerNo && x.is_secondary == meterStatus && x.fk_resident== location.resident_id).OrderByDescending(x => x.readingelectric_datetime).FirstOrDefault();
                        if (location != null)
                        {
                            toReturn = new ReadingElectricDetailByConsumerNoResponseModel()
                            {
                                locationId= location.location_id.ToString(),
                                previousReading = reading!=null?reading.readingelectric_currentreading.ToString():"0",
                                areaName = location.area_name,
                                subAreaName = location.subarea_name,
                                locationName = !string.IsNullOrEmpty(location.location_name) ? location.location_name : "",
                                remarks = "Successfully Location Found",
                                resultCode = "1100"
                            };

                        }
                        else
                        {
                            toReturn = new ReadingElectricDetailByConsumerNoResponseModel()
                            {
                                resultCode = "1200",
                                remarks = "No Record Found"
                            };
                        }
                    }
                }
                else
                {
                    toReturn = new ReadingElectricDetailByConsumerNoResponseModel()
                    {
                        remarks = "Please Provide Consumer No",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new ReadingElectricDetailByConsumerNoResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public BillDetailsByConsummerNoAndMonthResponseModel getBillDetailsByConsummerNoAndMonthForBillRecovery(BillDetailsByConsummerNoAndMonthRequestModel model)
        {
            BillDetailsByConsummerNoAndMonthResponseModel toReturn = new BillDetailsByConsummerNoAndMonthResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(model.locationElectricMeterNo))
                {
                    if (!string.IsNullOrEmpty(model.billingMonth))
                    {
                        if (!string.IsNullOrEmpty(model.residentId))
                        {
                            int residentId = int.Parse(model.residentId);
                            using (db_bmsEntities db = new db_bmsEntities())
                            {
                                var location = (from x in db.tbl_location
                                                join y in db.tbl_subarea on x.fk_subarea equals y.subarea_id
                                                join a in db.tbl_area on y.fk_area equals a.area_id
                                                join z in db.tbl_consummer_pool on x.location_id equals z.fk_location
                                                join b in db.tbl_ror on z.consummer_no equals b.consummer_no
                                                where (z.consummer_no == model.locationElectricMeterNo) && b.ror_month == model.billingMonth && b.fk_resident == residentId
                                                select new
                                                {
                                                    x.fk_subarea,
                                                    x.location_id,
                                                    x.location_name,
                                                    a.area_id,
                                                    a.area_name,
                                                    b.fk_resident,
                                                    b.ror_outstanding,
                                                    b.ror_amount,
                                                    y.subarea_name
                                                }).FirstOrDefault();
                                var approvedReadings = db.tbl_approvereadings.Where(x => x.reading_meterno == model.locationElectricMeterNo && x.reading_month == model.billingMonth && x.fk_resident == location.fk_resident ).ToList();
                                var approveElectricReadings = approvedReadings.Where(x => x.is_secondary != 2).ToList();
                                var approveGasReadings = approvedReadings.Where(x => x.is_secondary == 2).FirstOrDefault();
                                double units = 0.0;
                                string mmbtu = "";
                                if (approveElectricReadings.Count() > 0)
                                {
                                    foreach (var x in approvedReadings)
                                    {
                                        units = units + x.reading_units;
                                    };
                                }
                                if (approveGasReadings != null)
                                {
                                    mmbtu = approveGasReadings.reading_units.ToString();
                                }
                                var outstanding = (from x in db.tbl_outstanding where x.outstanding_month == model.billingMonth && x.fk_location == location.location_id && x.fk_resident == location.fk_resident select x).FirstOrDefault();
                                if (location != null)
                                {
                                    toReturn = new BillDetailsByConsummerNoAndMonthResponseModel()
                                    {
                                        amount = location.ror_amount.ToString(),
                                        area = location.area_name,
                                        location = location.location_name,
                                        subArea = location.subarea_name,
                                        outstandings = outstanding != null ? outstanding.outstanding_amount.ToString() : "0",
                                        mmbtu= mmbtu,
                                        units = units.ToString(),
                                        residentId = location.fk_resident.ToString(),
                                        remarks = "Success",
                                        resultCode = "1100"
                                    };

                                }
                                else
                                {
                                    toReturn = new BillDetailsByConsummerNoAndMonthResponseModel()
                                    {
                                        resultCode = "1200",
                                        remarks = "No Record Found"
                                    };
                                }
                            }
                        }
                        else
                        {
                            toReturn = new BillDetailsByConsummerNoAndMonthResponseModel()
                            {
                                resultCode = "1300",
                                remarks = "Please Provide Resident"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new BillDetailsByConsummerNoAndMonthResponseModel()
                        {
                            remarks = "Please Provide Billing Month",
                            resultCode = "1300"
                        };
                    }
                }
                else
                {
                    toReturn = new BillDetailsByConsummerNoAndMonthResponseModel()
                    {
                        remarks = "Please Provide Consumer No",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new BillDetailsByConsummerNoAndMonthResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }

        public List<BillDetailsByConsummerNoAndMonthResponseModel> getBillResidentsDetailsByConsummerNoAndMonthForBillRecovery(BillDetailsByConsummerNoAndMonthRequestModel model)
        {
           List< BillDetailsByConsummerNoAndMonthResponseModel> toReturn = new List<BillDetailsByConsummerNoAndMonthResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.locationElectricMeterNo))
                {
                    if (!string.IsNullOrEmpty(model.billingMonth))
                    {
                       
                            using (db_bmsEntities db = new db_bmsEntities())
                            {
                            var locationResidents = (from x in db.tbl_ror 
                                            join y in db.tbl_residents on x.fk_resident equals y.resident_id
                                            select new
                                            {
                                               
                                                y.resident_id,
                                                y.resident_name
                                            }).ToList();
                           
                                if (locationResidents.Count()>0)
                                {
                                    toReturn = locationResidents.Select(x=>new BillDetailsByConsummerNoAndMonthResponseModel()
                                    {
                                        residentId=x.resident_id.ToString(),
                                        residentName = x.resident_name,
                                        remarks = "Success",
                                        resultCode = "1100"
                                    }).ToList();

                                }
                                else
                                {
                                    toReturn.Add( new BillDetailsByConsummerNoAndMonthResponseModel()
                                    {
                                        resultCode = "1200",
                                        remarks = "No Record Found"
                                    });
                                }
                            }
                        
                    }
                    else
                    {
                        toReturn.Add( new BillDetailsByConsummerNoAndMonthResponseModel()
                        {
                            remarks = "Please Provide Billing Month",
                            resultCode = "1300"
                        });
                    }
                }
                else
                {
                    toReturn.Add(new BillDetailsByConsummerNoAndMonthResponseModel()
                    {
                        remarks = "Please Provide Consumer No",
                        resultCode = "1300"
                    });
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new BillDetailsByConsummerNoAndMonthResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public List<LocationHistoryResponseModel> getLocationHistoryByConsummerNo (LocationHistoryRequestModel model)
        {
            List<LocationHistoryResponseModel> toReturn = new List<LocationHistoryResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities()) 
                { 
                    if (!string.IsNullOrEmpty(model.consumerNo))
                    {
                        var _locationhistory = (from x in db.tbl_ror
                                               join r in db.tbl_residents on x.fk_resident equals r.resident_id
                                               join z in db.tbl_consummer_pool on x.consummer_no equals z.consummer_no
                                                join l in db.tbl_location on z.fk_location equals l.location_id
                                                where z.consummer_no == model.consumerNo
                                               select new
                                               {
                                                   x.id,
                                                   x.ror_status,
                                                   x.ror_amount,
                                                   x.ror_month,
                                                   x.ror_datetime,
                                                   l.location_id,
                                                   x.fk_resident,
                                                   r.resident_name,
                                                   r.resident_panumber,
                                                   r.resident_rank,
                                                   r.resident_unit,
                                                   r.resident_pin_code
                                               }).OrderByDescending(x=>x.ror_datetime).ToList();
                        var month = _locationhistory[0].ror_month;
                        var location = _locationhistory[0].location_id;
                        var outstandings = db.tbl_outstanding.Where(x => x.fk_location == location && x.outstanding_month==month ).ToList();
                        var payments = db.tbl_paymenthistory.Where(x => x.fk_location == location).ToList();
                        var locationHistory = (from x in _locationhistory
                                               join y in outstandings on x.fk_resident equals y.fk_resident
                                               join z in payments on x.id equals z.fk_billelectric into payment
                                               from z in payment.DefaultIfEmpty()
                                               select new
                                               {
                                                   x.id,
                                                   x.ror_status,
                                                   x.ror_amount,
                                                   x.ror_month,
                                                   x.location_id,
                                                   x.fk_resident,
                                                   x.resident_name,
                                                   x.resident_panumber,
                                                   x.resident_rank,
                                                   x.resident_unit,
                                                   x.resident_pin_code,
                                                   x.ror_datetime,
                                                   y.outstanding_amount,
                                                   paymentAmount = z!=null?z.payment_amount.ToString():"",
                                                   paymentMonth = z!=null?z.paymentmonth:"",
                                               }
                                               ).OrderByDescending(x => x.ror_datetime).ToList();
                        if (locationHistory.Count() > 0)
                        {
                            toReturn = locationHistory.Select(x => new LocationHistoryResponseModel()
                            {
                                billAmount = x.ror_amount.ToString(),
                                paNo=x.resident_panumber,
                                billStatus= x.ror_status.ToString(),
                                billId= x.id.ToString(),
                                residentStatus=x.resident_pin_code.ToString(),
                                billingMonth= x.ror_month,
                                OutstandingAmountOfMonth= x.outstanding_amount.ToString(),
                                paymentAmount = x.paymentAmount,
                                paymentMonth = x.paymentMonth,
                                residentName=x.resident_name,
                                residentRank=x.resident_rank,
                                residentUnit=x.resident_unit,
                                remarks="Success",
                                resultCode="1100"
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(new LocationHistoryResponseModel()
                            {
                                resultCode = "1200",
                                remarks = "No Record Found"
                            });
                        }


                    }
                    else
                    {
                        toReturn.Add( new LocationHistoryResponseModel()
                        {
                            resultCode = "1300",
                            remarks = "Please Provide Consummer No"
                        });
                    }
                }
            }
            catch(Exception Ex)
            {
                toReturn .Add(new LocationHistoryResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
    }
}