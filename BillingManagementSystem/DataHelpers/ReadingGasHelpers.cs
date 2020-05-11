using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.DataHelpers
{
    public class ReadingGasHelpers
    {
        public ReadingGasResponseModel AddReadingGas(ReadingGasRequestModel model)
        {
            ReadingGasResponseModel toReturn = new ReadingGasResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(model.readingpicture_data))
                {
                    if (!string.IsNullOrEmpty(model.readingpicture_type))
                    {
                        if (new ModelsValidatorHelper().validateint(model.readingpicture_size))
                        {
                            if (new ModelsValidatorHelper().validateint(model.readingGasCurrentReading))
                            {
                                if (new ModelsValidatorHelper().validateint(model.readingGasAddedby))
                                {
                                    if (!string.IsNullOrEmpty(model.readingGasMonth))
                                    {
                                        if (new ModelsValidatorHelper().validateint(model.readingGasPrevReading))
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
                                                var newReadingGas = new tbl_readinggas()
                                                {
                                                    fk_readingpicture = newReadingPicture.readingpicture_id,
                                                    reading_addedby = int.Parse(model.readingGasAddedby),
                                                    reading_units = double.Parse(model.readingGasCurrentReading) - double.Parse(model.readingGasPrevReading),
                                                    reading_month = model.readingGasMonth,
                                                    reading_currentreading = double.Parse(model.readingGasCurrentReading),
                                                    reading_datetime = DateTime.UtcNow.AddHours(5),
                                                    reading_prevreading = double.Parse(model.readingGasPrevReading),
                                                    reading_meterno = !string.IsNullOrEmpty(model.readingGasMeterNo) ? model.readingGasMeterNo : "",
                                                    reading_remarks = !string.IsNullOrEmpty(model.readingGasRemarks) ? model.readingGasRemarks : "",
                                                };
                                                db.tbl_readinggas.Add(newReadingGas);
                                                db.SaveChanges();
                                                toReturn = new ReadingGasResponseModel()
                                                {
                                                    remarks = "Successfully Added",
                                                    resultCode = "1100"
                                                };
                                            }
                                        }
                                        else
                                        {
                                            toReturn = new ReadingGasResponseModel()
                                            {
                                                remarks = "Please Provide Previous Reading",
                                                resultCode = "1300"
                                            };
                                        }
                                    }
                                    else
                                    {
                                        toReturn = new ReadingGasResponseModel()
                                        {
                                            remarks = "Please Provide Reading Month",
                                            resultCode = "1300"
                                        };
                                    }
                                }
                                else
                                {
                                    toReturn = new ReadingGasResponseModel()
                                    {
                                        remarks = "Please Provide Added By",
                                        resultCode = "1300"
                                    };
                                }
                            }
                            else
                            {
                                toReturn = new ReadingGasResponseModel()
                                {
                                    remarks = "Please Provide Current Reading",
                                    resultCode = "1300"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new ReadingGasResponseModel()
                            {
                                remarks = "Please Provide Picture Size",
                                resultCode = "1300"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new ReadingGasResponseModel()
                        {
                            remarks = "Please Provide Picture Type",
                            resultCode = "1300"
                        };
                    }
                }
                else
                {
                    toReturn = new ReadingGasResponseModel()
                    {
                        remarks = "Please Provide Picture Data",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new ReadingGasResponseModel()
                {
                    remarks = "There Was Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public ReadingGasResponseModel EditReadingGas(ReadingGasRequestModel model)
        {
            ReadingGasResponseModel toReturn = new ReadingGasResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.readingGasId))
                {
                    var readingGasId = int.Parse(model.readingGasId);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var readingGas = (from x in db.tbl_readinggas where x.reading_id == readingGasId select x).FirstOrDefault();
                        if (readingGas != null)
                        {
                            var readingPicture = (from x in db.tbl_readingpicture where x.readingpicture_id == readingGas.fk_readingpicture select x).FirstOrDefault();
                            if (readingPicture != null)
                            {
                                readingPicture.readingpicture_data = !string.IsNullOrEmpty(model.readingpicture_data) ? model.readingpicture_data : readingPicture.readingpicture_data;
                                readingPicture.readingpicture_size = !string.IsNullOrEmpty(model.readingpicture_size) ? int.Parse(model.readingpicture_size) : readingPicture.readingpicture_size;
                                readingPicture.readingpicture_type = !string.IsNullOrEmpty(model.readingpicture_type) ? model.readingpicture_type : readingPicture.readingpicture_type;
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
                                            readingGas.fk_readingpicture = newReadingPicture.readingpicture_id;
                                        }
                                        else
                                        {
                                            toReturn = new ReadingGasResponseModel()
                                            {
                                                remarks = "Please Provide Picture Size",
                                                resultCode = "1300"
                                            };
                                        }
                                    }
                                    else
                                    {
                                        toReturn = new ReadingGasResponseModel()
                                        {
                                            remarks = "Please Provide Picture Type",
                                            resultCode = "1300"
                                        };
                                    }
                                }
                                else
                                {
                                    toReturn = new ReadingGasResponseModel()
                                    {
                                        remarks = "Please Provide Picture Data",
                                        resultCode = "1300"
                                    };
                                }
                            }
                            readingGas.reading_addedby = !string.IsNullOrEmpty(model.readingGasAddedby) ? int.Parse(model.readingGasAddedby) : readingGas.reading_addedby;
                            readingGas.reading_units = !string.IsNullOrEmpty(model.readingGasUnits) ? double.Parse(model.readingGasUnits) : readingGas.reading_units;
                            readingGas.reading_month = !string.IsNullOrEmpty(model.readingGasMonth) ? model.readingGasMonth : readingGas.reading_month;
                            readingGas.reading_currentreading = !string.IsNullOrEmpty(model.readingGasCurrentReading) ? double.Parse(model.readingGasCurrentReading) : readingGas.reading_currentreading;
                            readingGas.reading_datetime = DateTime.UtcNow.AddHours(5);
                            readingGas.reading_prevreading = !string.IsNullOrEmpty(model.readingGasPrevReading) ? double.Parse(model.readingGasPrevReading) : readingGas.reading_prevreading;
                            readingGas.reading_meterno = !string.IsNullOrEmpty(model.readingGasMeterNo) ? model.readingGasMeterNo : readingGas.reading_meterno;
                            readingGas.reading_remarks = !string.IsNullOrEmpty(model.readingGasRemarks) ? model.readingGasRemarks : readingGas.reading_remarks;
                            db.SaveChanges();
                            toReturn = new ReadingGasResponseModel()
                            {
                                remarks = "Successfully Updated",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new ReadingGasResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                }
                else
                {
                    toReturn = new ReadingGasResponseModel()
                    {
                        remarks = "Please Select Gas Reading",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new ReadingGasResponseModel()
                {
                    remarks = "There Was Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public ReadingGasResponseModel DeleteReadingGas(ReadingGasRequestModel model)
        {
            ReadingGasResponseModel toReturn = new ReadingGasResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.readingGasId))
                {
                    var readingGasId = int.Parse(model.readingGasId);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var readingGas = (from x in db.tbl_readinggas where x.reading_id == readingGasId select x).FirstOrDefault();
                        if (readingGas != null)
                        {
                            var readingPicture = (from x in db.tbl_readingpicture where x.readingpicture_id == readingGas.fk_readingpicture select x).FirstOrDefault();
                            if (readingPicture != null)
                            {
                                db.tbl_readingpicture.Remove(readingPicture);
                                db.SaveChanges();
                            }
                            db.tbl_readinggas.Remove(readingGas);
                            db.SaveChanges();
                            toReturn = new ReadingGasResponseModel()
                            {
                                remarks = "Successfully Deleted",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new ReadingGasResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                }
                else
                {
                    toReturn = new ReadingGasResponseModel()
                    {
                        remarks = "Please Select Gas Reading",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new ReadingGasResponseModel()
                {
                    remarks = "There Was Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public ReadingGasResponseModel ApproveReadingGas(ReadingGasRequestModel model)
        {
            ReadingGasResponseModel toReturn = new ReadingGasResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.userId))
                {
                    int userId = int.Parse(model.userId);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        if (new ModelsValidatorHelper().validateint(model.readingGasId))
                        {
                            int readingGasId = int.Parse(model.readingGasId);
                            var readingGas = (from x in db.tbl_readinggas where x.reading_id == readingGasId select x).FirstOrDefault();
                            var location = (from x in db.tbl_location where x.location_gassmeter == readingGas.reading_meterno select x).FirstOrDefault();
                            if (location != null)
                            {
                                var residentBuilding = (from x in db.tbl_residentbuilding where x.fk_building == location.location_id select x).FirstOrDefault();
                                if (residentBuilding != null)
                                {
                                    var previousPendingBill = (from x in db.tbl_billgas where x.fk_paymentstatus == 2 && x.fk_resident == residentBuilding.fk_resident select x).FirstOrDefault();
                                    var outstanding = "";
                                    if (previousPendingBill != null)
                                    {
                                        outstanding = previousPendingBill.prevreading.ToString();
                                        previousPendingBill.fk_paymentstatus = 3;
                                        db.SaveChanges();
                                        var newPaymentHistory = new tbl_paymenthistory();
                                        newPaymentHistory.paymentmonth = new MonthFinderHelpers().GetPreviousMonth(readingGas.reading_month);
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
                                    var fixedRatesWater = (from x in db.tbl_fixedrates where x.fixedrates_name == "Water Charges" select x).FirstOrDefault();
                                    var fixedRatesTv = (from x in db.tbl_fixedrates where x.fixedrates_name == "Tv Charges" select x).FirstOrDefault();
                                    var readingPicture = (from x in db.tbl_readingpicture where x.readingpicture_id == readingGas.fk_readingpicture select x).FirstOrDefault();
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
                                        var newBill = new tbl_billgas()
                                        {
                                            fk_billpicture = newBillPicture.billpicture_id,
                                            fk_location = residentBuilding.fk_building,
                                            fk_resident = residentBuilding.fk_resident,
                                            fk_paymentstatus = 2,
                                            amount = double.Parse(model.amount),
                                            currentreading = readingGas.reading_currentreading,
                                            month = readingGas.reading_month,
                                            outstanding = 0,
                                            tv = fixedRatesTv.fixedrates_amount,
                                            water = fixedRatesWater.fixedrates_amount,
                                            prevreading = readingGas.reading_prevreading,
                                            units = readingGas.reading_units,
                                            datetime = readingGas.reading_datetime,
                                            remarks = readingGas.reading_remarks
                                        };
                                        db.tbl_billgas.Add(newBill);
                                        db.SaveChanges();
                                        newBill.outstanding = newBill.amount + double.Parse(outstanding);
                                        var newReadingGasLog = new tbl_readinggaslog()
                                        {
                                            readinglog_addedby = readingGas.reading_addedby,
                                            readinglog_archivedby = userId,
                                            readinglog_archivedon = DateTime.UtcNow.AddHours(5),
                                            readinglog_currentreading = readingGas.reading_currentreading,
                                            readinglog_datetime = readingGas.reading_datetime,
                                            readinglog_meterno = readingGas.reading_meterno,
                                            readinglog_month = readingGas.reading_month,
                                            readinglog_prevreading = readingGas.reading_prevreading,
                                            readinglog_remarks = readingGas.reading_remarks,
                                            readinglog_units = readingGas.reading_units,
                                        };
                                        db.tbl_readinggaslog.Add(newReadingGasLog);
                                        db.SaveChanges();
                                        db.tbl_readinggas.Remove(readingGas);
                                        db.tbl_readingpicture.Remove(readingPicture);
                                        db.SaveChanges();
                                        toReturn = new ReadingGasResponseModel()
                                        {
                                            remarks = "Reading Successfully Approved",
                                            resultCode = "1100"
                                        };
                                    }
                                    else
                                    {
                                        toReturn = new ReadingGasResponseModel()
                                        {
                                            remarks = "No Reading Picture Found",
                                            resultCode = "1200"
                                        };
                                    }
                                }
                                else
                                {
                                    toReturn = new ReadingGasResponseModel()
                                    {
                                        remarks = "No Resident Found",
                                        resultCode = "1200"
                                    };
                                }
                            }
                            else
                            {
                                toReturn = new ReadingGasResponseModel()
                                {
                                    remarks = " No Meter Found With Meter No " + model.readingGasMeterNo,
                                    resultCode = "1200"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new ReadingGasResponseModel()
                            {
                                remarks = "Please Provide Reading",
                                resultCode = "1300"
                            };
                        }
                    }
                }
                else
                {
                    toReturn = new ReadingGasResponseModel()
                    {
                        remarks = "Please provide Logged In User",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new ReadingGasResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public List<ReadingGasResponseModel> getAllReadings(ReadingGasRequestModel model)
        {
            List<ReadingGasResponseModel> toRetrun = new List<ReadingGasResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    int user = int.Parse(model.readingGasAddedby);
                    var meters = (from x in db.tbl_userareas
                                  join y in db.tbl_location on x.fk_area equals y.fk_area
                                  where x.fk_user == user
                                  select y.location_gassmeter).ToList();
                    var readings = (from x in db.tbl_readinggas
                                    join y in db.tbl_readingpicture on x.fk_readingpicture equals y.readingpicture_id
                                    join z in db.tbl_users on x.reading_addedby equals z.users_id
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
                                        x.reading_remarks,
                                        x.reading_units,
                                        y.readingpicture_data,
                                        y.readingpicture_size,
                                        y.readingpicture_type,
                                        z.users_fullname
                                    }).Where(x => meters.Contains(x.reading_meterno)).ToList();
                    if (readings.Count() > 0)
                    {
                        toRetrun = readings.Select(x => new ReadingGasResponseModel()
                        {
                            fk_readingPicture = x.fk_readingpicture.ToString(),
                            readingGasAddedby = x.users_fullname.ToString(),
                            readingGasCurrentReading = x.reading_currentreading.ToString(),
                            readingGasDatetime = x.reading_datetime.ToString(),
                            readingGasId = x.reading_id.ToString(),
                            readingGasMeterNo = !string.IsNullOrEmpty(x.reading_meterno) ? x.reading_meterno : "",
                            readingGasMonth = !string.IsNullOrEmpty(x.reading_month) ? x.reading_month : "",
                            readingGasPrevReading = x.reading_prevreading.ToString(),
                            readingGasRemarks = !string.IsNullOrEmpty(x.reading_remarks) ? x.reading_remarks : "",
                            readingGasUnits = x.reading_units.ToString(),
                            readingpicture_data = x.readingpicture_data,
                            readingpicture_size = x.readingpicture_size.ToString(),
                            readingpicture_type = x.readingpicture_type,
                            remarks = "Successfully Found",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toRetrun.Add(new ReadingGasResponseModel()
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toRetrun.Add(new ReadingGasResponseModel()
                {
                    resultCode = "1000",
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                });
            }
            return toRetrun;
        }
    }
}