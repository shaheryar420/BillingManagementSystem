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
                if(String.IsNullOrEmpty(model.readingpicture_data))
                {
                    if (String.IsNullOrEmpty(model.readingpicture_type)) 
                    {
                        if(new ModelsValidatorHelper().validateint(model.readingpicture_size))
                        {
                            if(new ModelsValidatorHelper().validateint(model.readingElectricCurrentReading))
                            {
                                if(new ModelsValidatorHelper().validateint(model.readingElectricUnits))
                                {
                                    if(new ModelsValidatorHelper().validateint(model.readingElectricAddedby))
                                    {
                                        if(String.IsNullOrEmpty(model.readingElectricMonth))
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
                                                        readingelectric_units = double.Parse(model.readingElectricUnits),
                                                        readingelectric_month = model.readingElectricMonth,
                                                        readingelectric_currentreading = double.Parse(model.readingElectricCurrentReading),
                                                        readingelectric_datetime = DateTime.UtcNow.AddHours(5),
                                                        readingelectric_prevreading = double.Parse(model.readingElectricPrevReading),
                                                        readingelectric_meterno = String.IsNullOrEmpty(model.readingElectricMeterNo)?model.readingElectricMeterNo:"",
                                                        readingelectric_remarks = String.IsNullOrEmpty(model.readingElectricRemarks)?model.readingElectricRemarks:""
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
                                        remarks = "Please Provide Electric Units",
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
                                readingPicture.readingpicture_data = String.IsNullOrEmpty(model.readingpicture_data)?model.readingpicture_data :readingPicture.readingpicture_data;
                                readingPicture.readingpicture_size = String.IsNullOrEmpty(model.readingpicture_size)?int.Parse(model.readingpicture_size):readingPicture.readingpicture_size;
                                readingPicture.readingpicture_type = String.IsNullOrEmpty(model.readingpicture_type)?model.readingpicture_type :readingPicture.readingpicture_type;
                                db.SaveChanges();
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(model.readingpicture_data))
                                {
                                    if (String.IsNullOrEmpty(model.readingpicture_type))
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
                            readingElectric.readingelectric_addedby = String.IsNullOrEmpty(model.readingElectricAddedby)?int.Parse(model.readingElectricAddedby):readingElectric.readingelectric_addedby;
                            readingElectric.readingelectric_units = String.IsNullOrEmpty(model.readingElectricUnits)?double.Parse(model.readingElectricUnits):readingElectric.readingelectric_units;
                            readingElectric.readingelectric_month = String.IsNullOrEmpty(model.readingElectricMonth)?model.readingElectricMonth : readingElectric.readingelectric_month;
                            readingElectric.readingelectric_currentreading = String.IsNullOrEmpty(model.readingElectricCurrentReading)?double.Parse(model.readingElectricCurrentReading):readingElectric.readingelectric_currentreading;
                            readingElectric.readingelectric_datetime = DateTime.UtcNow.AddHours(5);
                            readingElectric.readingelectric_prevreading = String.IsNullOrEmpty(model.readingElectricPrevReading)?double.Parse(model.readingElectricPrevReading):readingElectric.readingelectric_prevreading;
                            readingElectric.readingelectric_meterno = String.IsNullOrEmpty(model.readingElectricMeterNo) ? model.readingElectricMeterNo : readingElectric.readingelectric_meterno;
                            readingElectric.readingelectric_remarks = String.IsNullOrEmpty(model.readingElectricRemarks) ? model.readingElectricRemarks : readingElectric.readingelectric_remarks;
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
    }
}