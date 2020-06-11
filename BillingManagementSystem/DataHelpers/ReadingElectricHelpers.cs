using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                    if(new ModelsValidatorHelper().validateint(model.readingElectricAddedby))
                    {
                        if(!string.IsNullOrEmpty(model.readingElectricMonth))
                        {
                            if (new ModelsValidatorHelper().validateint(model.readingElectricPrevReading))
                            {
                                using (db_bmsEntities db = new db_bmsEntities())
                                {
                                    var existingReading = (from x in db.tbl_readingelectric where x.readingelectric_month == model.readingElectricMonth select x).FirstOrDefault();
                                    if (existingReading == null)
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
                                                readingelectric_currentreading = double.Parse(model.readingElectricCurrentReading),
                                                readingelectric_datetime = DateTime.UtcNow.AddHours(5),
                                                readingelectric_prevreading = double.Parse(model.readingElectricPrevReading),
                                                readingelectric_meterno = !string.IsNullOrEmpty(model.readingElectricMeterNo) ? model.readingElectricMeterNo : "",
                                                readingelectric_remarks = !string.IsNullOrEmpty(model.readingElectricRemarks) ? model.readingElectricRemarks : "",
                                                readingelectric_tv = !string.IsNullOrEmpty(model.readingElectricTv) ? double.Parse(model.readingElectricTv) : 0,
                                                readingelectric_fpa = !string.IsNullOrEmpty(model.readingElectricFpa) ? double.Parse(model.readingElectricFpa) : 0,
                                                readingelectric_water = !string.IsNullOrEmpty(model.readingElectircWater) ? double.Parse(model.readingElectircWater) : 0,
                                            };
                                            db.tbl_readingelectric.Add(newReadingElectric);
                                            db.SaveChanges();
                                        }
                                        else
                                        {
                                            var newReadingElectric = new tbl_readingelectric()
                                            {
                                                fk_readingpicture = 0,
                                                readingelectric_addedby = int.Parse(model.readingElectricAddedby),
                                                readingelectric_units = int.Parse(model.readingElectricUnits),
                                                readingelectric_month = model.readingElectricMonth,
                                                readingelectric_currentreading = double.Parse(model.readingElectricCurrentReading),
                                                readingelectric_datetime = DateTime.UtcNow.AddHours(5),
                                                readingelectric_prevreading = double.Parse(model.readingElectricPrevReading),
                                                readingelectric_meterno = !string.IsNullOrEmpty(model.readingElectricMeterNo) ? model.readingElectricMeterNo : "",
                                                readingelectric_remarks = !string.IsNullOrEmpty(model.readingElectricRemarks) ? model.readingElectricRemarks : "",
                                                readingelectric_tv = !string.IsNullOrEmpty(model.readingElectricTv) ? double.Parse(model.readingElectricTv) : 0,
                                                readingelectric_fpa = !string.IsNullOrEmpty(model.readingElectricFpa) ? double.Parse(model.readingElectricFpa) : 0,
                                                readingelectric_water = !string.IsNullOrEmpty(model.readingElectircWater) ? double.Parse(model.readingElectircWater) : 0,
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
                                resultCode ="1300"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new ReadingElectricResponseModel()
                        {
                            remarks="Please Provide Added By",
                            resultCode="1300"
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
                            readingElectric.readingelectric_tv = !string.IsNullOrEmpty(model.readingElectricTv) ? double.Parse(model.readingElectricTv) : 0;
                            readingElectric.readingelectric_fpa = !string.IsNullOrEmpty(model.readingElectricFpa) ? double.Parse(model.readingElectricFpa) : 0;
                            readingElectric.readingelectric_water = !string.IsNullOrEmpty(model.readingElectircWater) ? double.Parse(model.readingElectircWater) : 0;
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
                            var location = (from x in db.tbl_location where x.location_electricmeter == readingElectric.readingelectric_meterno select x).FirstOrDefault();
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
                                        totalAmount = totalAmount + (totalFPA + readingElectric.readingelectric_tv + meterRent);
                                        totalAmount = (totalAmount - totalEnergyCharges) ;
                                        totalAmount = totalAmount + readingElectric.readingelectric_water;
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
                                            billelectric_tv = readingElectric.readingelectric_tv,
                                            billelectric_water = readingElectric.readingelectric_water,
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
                                            where x.location_electricmeter == readingElectric.readingelectric_meterno
                                            select new
                                            {
                                                x.location_id,
                                                x.location_gassmeter,
                                                x.location_name,
                                                y.subarea_name,
                                                z.area_name
                                            }).FirstOrDefault();
                            if (location != null)
                            {
                                var residentBuilding = (from x in db.tbl_residentbuilding where x.fk_building == location.location_id select x).FirstOrDefault();
                                if (residentBuilding != null)
                                {
                                    var previousPendingBill = (from x in db.tbl_billelectric where x.fk_paymentstatus == 3 && x.fk_location == residentBuilding.fk_building select x).FirstOrDefault();
                                    double outstanding = 0.0;
                                    if (previousPendingBill != null)
                                    {
                                        outstanding = previousPendingBill.billelectric_outstanding.Value;
                                    }
                                    else
                                    {
                                        outstanding = 0.0;
                                    }
                                    //Calculating Bill Amount & Adding Bill
                                    var fpa = (from x in db.tbl_fixedrates where x.fixedrates_id == 1 select x.fixedrates_amount).FirstOrDefault();
                                    var meterRent = (from x in db.tbl_fixedrates where x.fixedrates_id == 2 select x.fixedrates_amount).FirstOrDefault();
                                    var slabs = (from x in db.tbl_slabs select x).ToList();
                                    //Calculations
                                    double totalAmount = 0.0;
                                    double totalEnergyCharges = 0.0;
                                    double totalFPA = 0.0;
                                    for (int i = 0; i < slabs.Count; i++)
                                    {
                                        if (totalEnergyCharges == 0)
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
                                                    totalAmount = (slabs[i - 1].slab_net_rate.Value * _units) + extraTotalAmount;
                                                    totalEnergyCharges = (slabs[i - 1].slab_energy_charges.Value * _units) + extraEnergy;
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
                                    totalAmount = totalAmount + (totalFPA + meterRent);
                                    totalAmount = (totalAmount - totalEnergyCharges);
                                    outstanding = outstanding + totalAmount;

                                    /// Entry
                                    toReturn = new ApproveElectricResponseModel()
                                    {
                                        billElectricAmount = totalAmount.ToString(),
                                        billElectricCurrentReading = readingElectric.readingelectric_currentreading.ToString(),
                                        billElectricMonth = !String.IsNullOrEmpty(readingElectric.readingelectric_month) ? readingElectric.readingelectric_month : "",
                                        billElectricOutstanding = outstanding.ToString(),
                                        billElectricPrevReading = readingElectric.readingelectric_prevreading.ToString(),
                                        billElectricRemarks = !string.IsNullOrEmpty(readingElectric.readingelectric_remarks) ? readingElectric.readingelectric_remarks : "",
                                        billElectricUnits = readingElectric.readingelectric_units.ToString(),
                                        billElectricWater = readingElectric.readingelectric_water.ToString(),
                                        billElectricTv = readingElectric.readingelectric_tv.ToString(),
                                        energyCharges = totalEnergyCharges.ToString(),
                                        locationName = !string.IsNullOrEmpty(location.location_name) ? location.location_name : "",
                                        locationMeterNo = !string.IsNullOrEmpty(location.location_gassmeter) ? location.location_gassmeter : "",
                                        subAreaName=!string.IsNullOrEmpty(location.subarea_name)?location.subarea_name:"",
                                        areaName = !string.IsNullOrEmpty(location.area_name)?location.area_name:"",
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
                                    remarks = " No Meter Found With Meter No " + model.readingElectricMeterNo,
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
                using(db_bmsEntities db = new db_bmsEntities())
                {
                    int user = int.Parse(model.readingElectricAddedby);
                    var meters = (from x in db.tbl_userareas
                                  join y in db.tbl_location on x.fk_subarea equals y.fk_subarea
                                  where x.fk_user == user
                                  select y.location_electricmeter).ToList();
                    var readings = (from x in db.tbl_readingelectric
                                    join z in db.tbl_users on x.readingelectric_addedby equals z.users_id
                                    join a in db.tbl_location on x.readingelectric_meterno equals a.location_electricmeter
                                    join b in db.tbl_subarea on a.fk_subarea equals b.subarea_id
                                    join c in db.tbl_area on b.fk_area equals c.area_id
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
                                       x.readingelectric_fpa,
                                       x.readingelectric_tv,
                                       x.readingelectric_water,
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
                                       z.users_fullname
                                    }).Where(x=> meters.Contains(x.readingelectric_meterno)).ToList();
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
                            readingElectricFpa = x.readingelectric_fpa.ToString(),
                            readingElectricTv = x.readingelectric_tv.ToString(),
                            readingElectricWater = x.readingelectric_water.ToString(),
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
                                        join rb in db.tbl_residentbuilding on x.location_id equals rb.fk_building
                                        where x.location_electricmeter == model.locationElectricMeter
                                        select new
                                        {
                                            x.fk_subarea,
                                            x.location_electricmeter,
                                            x.location_gassmeter,
                                            x.location_id,
                                            x.location_name,
                                            x.location_wapdameter,
                                            a.area_id,
                                            a.area_name,
                                            rb.fk_resident,
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
                                locationElectricMeter = !string.IsNullOrEmpty(location.location_electricmeter) ? location.location_electricmeter : "",
                                locationGassMeter = !string.IsNullOrEmpty(location.location_gassmeter) ? location.location_gassmeter : "",
                                locationName = !string.IsNullOrEmpty(location.location_name) ? location.location_name : "",
                                locationWapdaMeter = !string.IsNullOrEmpty(location.location_wapdameter) ? location.location_wapdameter : "",
                                locationId = location.location_id.ToString(),
                                residentId = location.fk_resident.ToString(),
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
    }
}