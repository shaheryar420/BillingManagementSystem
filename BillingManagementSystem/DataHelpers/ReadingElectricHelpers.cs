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
                if(!string.IsNullOrEmpty(model.readingpicture_data))
                {
                    if (!string.IsNullOrEmpty(model.readingpicture_type)) 
                    {
                        if(new ModelsValidatorHelper().validateint(model.readingpicture_size))
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
                                                    readingelectric_units = double.Parse(model.readingElectricPrevReading)-double.Parse(model.readingElectricCurrentReading),
                                                    readingelectric_month = model.readingElectricMonth,
                                                    readingelectric_currentreading = double.Parse(model.readingElectricCurrentReading),
                                                    readingelectric_datetime = DateTime.UtcNow.AddHours(5),
                                                    readingelectric_prevreading = double.Parse(model.readingElectricPrevReading),
                                                    readingelectric_meterno = !string.IsNullOrEmpty(model.readingElectricMeterNo)?model.readingElectricMeterNo:"",
                                                    readingelectric_remarks = !string.IsNullOrEmpty(model.readingElectricRemarks)?model.readingElectricRemarks:"",
                                                };
                                                db.tbl_readingelectric.Add(newReadingElectric);
                                                db.SaveChanges();
                                                toReturn = new ReadingElectricResponseModel()
                                                {
                                                    remarks = "Successfully Added",
                                                    resultCode = "1100"
                                                };
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
                        else
                        {
                            toReturn = new ReadingElectricResponseModel()
                            {
                                remarks="Please Provide Picture Size",
                                resultCode="1300"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new ReadingElectricResponseModel()
                        {
                            remarks="Please Provide Picture Type",
                            resultCode="1300"
                        };
                    }
                }
                else
                {
                    toReturn = new ReadingElectricResponseModel()
                    {
                        remarks="Please Provide Picture Data",
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
        public ReadingElectricResponseModel ApproveReadingElectric(ReadingElectricRequestModel model)
        {
            ReadingElectricResponseModel toReturn = new ReadingElectricResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.readingElectricId))
                    {
                        int readingElectricId = int.Parse(model.readingElectricId);
                        var readingElectric = (from x in db.tbl_readingelectric where x.readingelectric_id == readingElectricId select x).FirstOrDefault();
                        var location = (from x in db.tbl_location where x.location_electricmeter == readingElectric.readingelectric_meterno select x).FirstOrDefault();
                        if(location != null)
                        {
                            var residentBuilding = (from x in db.tbl_residentbuilding where x.fk_building == location.location_id select x).FirstOrDefault();
                            if(residentBuilding != null)
                            {
                                var fixedRatesElectric = (from x in db.tbl_fixedrates where x.fixedrates_name == "Electric Charges" select x).FirstOrDefault();
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
                                    var newBill = new tbl_billelectric()
                                    {
                                        fk_billpicture = newBillPicture.billpicture_id,
                                        fk_location = residentBuilding.fk_building,
                                        fk_resident = residentBuilding.fk_resident,
                                        fk_paymentstatus = 0,
                                        billelectric_amount = readingElectric.readingelectric_units* fixedRatesElectric.fixedrates_amount,
                                        billelectric_currentreading = readingElectric.readingelectric_currentreading,
                                        billelectric_month = readingElectric.readingelectric_month,
                                        billelectric_outstanding = 0,
                                        billelectric_prevreading = readingElectric.readingelectric_prevreading,
                                        billelectric_units = readingElectric.readingelectric_units,
                                        billelectric_tv = 0,
                                        billelectric_water = 0,
                                        billelectric_datetime = readingElectric.readingelectric_datetime,
                                        billelectric_remarks = readingElectric.readingelectric_remarks
                                    };
                                    db.tbl_billelectric.Add(newBill);
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
                                        remarks="No Reading Picture Found",
                                        resultCode="1200"
                                    };
                                }
                            }
                            else
                            {
                                toReturn = new ReadingElectricResponseModel()
                                {
                                    remarks = "No Resident Found",
                                    resultCode="1200"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new ReadingElectricResponseModel()
                            {
                                remarks = " No Meter Found With Meter No "+ model.readingElectricMeterNo,
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
        public List<ReadingElectricResponseModel> getAllReadings(ReadingElectricRequestModel model)
        {
            List<ReadingElectricResponseModel> toRetrun = new List<ReadingElectricResponseModel>();
            try
            {
                using(db_bmsEntities db = new db_bmsEntities())
                {
                    var readings = (from x in db.tbl_readingelectric
                                    join y in db.tbl_readingpicture on x.fk_readingpicture equals y.readingpicture_id
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
                                       y.readingpicture_type
                                    }).ToList();
                    if (readings.Count() > 0)
                    {
                        toRetrun = readings.Select(x => new ReadingElectricResponseModel()
                        {
                            fk_readingPicture= x.fk_readingpicture.ToString(),
                            readingElectricAddedby= x.readingelectric_addedby.ToString(),
                            readingElectricCurrentReading= x.readingelectric_currentreading.ToString(),
                            readingElectricDatetime=x.readingelectric_datetime.ToString(),
                            readingElectricId= x.readingelectric_id.ToString(),
                            readingElectricMeterNo=!string.IsNullOrEmpty(x.readingelectric_meterno)? x.readingelectric_meterno:"",
                            readingElectricMonth=!string.IsNullOrEmpty(x.readingelectric_month)?x.readingelectric_month:"",
                            readingElectricPrevReading=x.readingelectric_prevreading.ToString(),
                            readingElectricRemarks=!string.IsNullOrEmpty(x.readingelectric_remarks)?x.readingelectric_remarks:"",
                            readingElectricUnits= x.readingelectric_units.ToString(),
                            readingpicture_data= x.readingpicture_data,
                            readingpicture_size= x.readingpicture_size.ToString(),
                            readingpicture_type= x.readingpicture_type,
                            remarks="Successfully Found",
                            resultCode="1100"
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

    }
}