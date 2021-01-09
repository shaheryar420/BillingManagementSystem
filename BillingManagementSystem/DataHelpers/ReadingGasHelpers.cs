using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;
using BillingManagementSystem.SubDataHelpers;

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
                                                var existingReading = (from x in db.tbl_readinggas where x.reading_month == model.readingGasMonth select x).FirstOrDefault();
                                                if (existingReading == null)
                                                {
                                                    var newReadingPicture = new tbl_readingpicture()
                                                    {
                                                        readingpicture_data = model.readingpicture_data,
                                                        readingpicture_size = int.Parse(model.readingpicture_size),
                                                        readingpicture_type = model.readingpicture_type
                                                    };
                                                    db.tbl_readingpicture.Add(newReadingPicture);
                                                    db.SaveChanges();
                                                    var resident = (from x in db.tbl_location
                                                                    join z in db.tbl_consummer_pool on x.location_id equals z.fk_location
                                                                    join y in db.tbl_residents on z.consummer_no equals y.resident_consumer_no
                                                                    where z.consummer_no == model.readingGasMeterNo
                                                                    select new
                                                                    {
                                                                        y.resident_id,
                                                                        x.location_id
                                                                    }).FirstOrDefault();
                                                    var units = double.Parse(model.readingGasCurrentReading) - double.Parse(model.readingGasPrevReading);
                                                    var MMBTU  = units / 100000;
                                                    MMBTU = (MMBTU * 1046) / 281.7385;
                                                    var newReadingGas = new tbl_readinggas()
                                                    {
                                                        fk_readingpicture = newReadingPicture.readingpicture_id,
                                                        reading_addedby = int.Parse(model.readingGasAddedby),
                                                        reading_units = units,
                                                        reading_month = model.readingGasMonth,
                                                        MMBTU= MMBTU,
                                                        fk_location= resident.location_id,
                                                        fk_resident= resident.resident_id,
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
                                                else
                                                {
                                                    toReturn = new ReadingGasResponseModel()
                                                    {
                                                        remarks = "Reading Already Exists for This Month",
                                                        resultCode = "1200"
                                                    };
                                                }
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
                            double _units = readingGas.reading_units;
                            var location = (from x in db.tbl_location
                                            join y in db.tbl_consummer_pool on x.location_id equals y.fk_location
                                            where y.consummer_no == readingGas.reading_meterno
                                            select x).FirstOrDefault();
                            if (location != null)
                            {
                                var resident = db.tbl_residents.Where(x => x.resident_id == readingGas.fk_resident).FirstOrDefault();
                                if (resident != null)
                                {
                                    var readingPicture = (from x in db.tbl_readingpicture where x.readingpicture_id == readingGas.fk_readingpicture select x).FirstOrDefault();
                                    if (readingPicture != null)
                                    {

                                        var response = new SubDataHelpers.ReadingGasSubHelpers().CalculateBill(_units, location.location_id);
                                        double totalAmount = Math.Round(double.Parse(response.totalAmount));
                                        double GST = Math.Round(double.Parse(response.GST));
                                        double meterRent = Math.Round(double.Parse(response.meterRent));
                                        /// Entry
                                        var newBill = new tbl_approvereadings()
                                        {
                                            reading_addedby = readingGas.reading_addedby,
                                            reading_meterno = readingGas.reading_meterno,
                                            fk_readingpicture = readingPicture.readingpicture_id,
                                            fk_location = location.location_id,
                                            fk_resident = resident.resident_id,
                                            bill_amount = totalAmount,
                                            reading_currentreading = readingGas.reading_currentreading,
                                            reading_month = readingGas.reading_month,
                                            reading_prevreading = readingGas.reading_prevreading,
                                            reading_units = readingGas.reading_units,
                                            bill_gst = GST,
                                            is_secondary = 2,
                                            reading_datetime = readingGas.reading_datetime,
                                            readinge_remarks = readingGas.reading_remarks,
                                            bill_meter_rent = meterRent,

                                        };
                                        db.tbl_approvereadings.Add(newBill);
                                        db.tbl_readinggas.Remove(readingGas);
                                        db.SaveChanges();
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
        public ApproveGasResponseModel calculateReadingGas(ReadingGasRequestModel model)
        {
            ApproveGasResponseModel toReturn = new ApproveGasResponseModel();
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
                            double units = readingGas.reading_units;
                            var location = (from x in db.tbl_location
                                            join y in db.tbl_subarea on x.fk_subarea equals y.subarea_id
                                            join z in db.tbl_area on y.fk_area equals z.area_id
                                            join c in db.tbl_consummer_pool on x.location_id equals c.fk_location
                                            where c.consummer_no == readingGas.reading_meterno
                                            select new
                                            {
                                                x.location_id,
                                                c.consummer_no,
                                                x.location_name,
                                                y.subarea_name,
                                                z.area_name
                                            }).FirstOrDefault();
                            if (location != null)
                            {
                                var resident = db.tbl_residents.Where(x => x.resident_id == readingGas.fk_resident).FirstOrDefault();
                                if (resident != null)
                                {

                                    var response = new SubDataHelpers.ReadingGasSubHelpers().CalculateBill(units, location.location_id);                                    

                                    /// Entry
                                    toReturn = new ApproveGasResponseModel()
                                    {
                                         billGasAmount = response.totalAmount.ToString(),
                                         billGasCurrentReading = readingGas.reading_currentreading.ToString(),
                                         billGasMonth = !String.IsNullOrEmpty(readingGas.reading_month) ? readingGas.reading_month : "",
                                         billGasPrevReading = readingGas.reading_prevreading.ToString(),
                                         gasCharges = response.GST.ToString(),
                                         billGasRemarks = !string.IsNullOrEmpty(readingGas.reading_remarks) ? readingGas.reading_remarks : "",
                                         billGasUnits = readingGas.reading_units.ToString(),
                                         areaName = !string.IsNullOrEmpty(location.subarea_name)?location.subarea_name:"",
                                         subAreaName = !string.IsNullOrEmpty(location.area_name)?location.area_name:"",                                         
                                         locationName = !string.IsNullOrEmpty(location.location_name) ? location.location_name : "",
                                         locationMeterNo = !string.IsNullOrEmpty(location.consummer_no) ? location.consummer_no : "",
                                         remarks = "Successfully Found",
                                         resultCode = "1100",
                                     };
                                }
                                else
                                {
                                    toReturn = new ApproveGasResponseModel()
                                    {
                                        remarks = "No Resident Found",
                                        resultCode = "1200"
                                    };
                                }
                            }
                            else
                            {
                                toReturn = new ApproveGasResponseModel()
                                {
                                    remarks = " No Meter Found With Meter No " + model.readingGasMeterNo,
                                    resultCode = "1200"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new ApproveGasResponseModel()
                            {
                                remarks = "Please Provide Reading",
                                resultCode = "1300"
                            };
                        }
                    }
                }
                else
                {
                    toReturn = new ApproveGasResponseModel()
                    {
                        remarks = "Please provide Logged In User",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new ApproveGasResponseModel()
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
                                  join y in db.tbl_location on x.fk_subarea equals y.fk_subarea
                                  join z in db.tbl_consummer_pool on y.location_id equals z.fk_location
                                  where x.fk_user == user
                                  select z.consummer_no).ToList();
                    var readings = (from x in db.tbl_readinggas
                                    join y in db.tbl_readingpicture on x.fk_readingpicture equals y.readingpicture_id
                                    join r in db.tbl_residents on x.fk_resident equals r.resident_id
                                    join con in db.tbl_consummer_pool on x.reading_meterno equals con.consummer_no
                                    join a in db.tbl_location on con.fk_location equals a.location_id
                                    join b in db.tbl_subarea on a.fk_subarea equals b.subarea_id
                                    join c in db.tbl_area on b.fk_area equals c.area_id
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
                                        z.users_fullname,
                                        a.location_id,
                                        b.subarea_id,
                                        c.area_id,
                                        a.location_name,
                                        b.subarea_name,
                                        c.area_name,
                                        a.fk_subarea,
                                        r.resident_panumber,
                                        r.resident_id,
                                        r.resident_name,
                                        r.resident_pin_code,
                                        r.resident_rank,
                                        r.resident_unit
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
                            area_id = x.area_id.ToString(),
                            area_name = x.area_name.ToString(),
                            location_id = x.location_id.ToString(),
                            location_name = x.location_name.ToString(),
                            subarea_id = x.subarea_id.ToString(),
                            subarea_name = x.subarea_name.ToString(),
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
      
        public LocationResponseModel getAllDetailByConsumerNo(LocationRequestModel model)
        {
            LocationResponseModel toReturn = new LocationResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(model.locationGassMeter))
                {
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var location = (from x in db.tbl_location
                                        join y in db.tbl_subarea on x.fk_subarea equals y.subarea_id
                                        join a in db.tbl_area on y.fk_area equals a.area_id
                                        join z in db.tbl_consummer_pool on x.location_id equals z.fk_location
                                        join rb in db.tbl_residents on z.consummer_no equals rb.resident_consumer_no
                                        where z.consummer_no == model.locationGassMeter
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
                            var reading = db.tbl_readinggas.Where(x => x.reading_meterno == model.locationGassMeter  && x.fk_resident == location.resident_id).OrderByDescending(x => x.reading_datetime).FirstOrDefault();
                            var approveReading = db.tbl_approvereadings.Where(x => x.reading_meterno == model.locationGassMeter && x.is_secondary == 2 && x.fk_resident == location.resident_id).OrderByDescending(x => x.reading_datetime).FirstOrDefault();
                            toReturn = new LocationResponseModel()
                            {
                                areaName = location.area_name,
                                fk_area = location.area_id.ToString(),
                                subAreaName = location.subarea_name,
                                previousReading = reading != null ? reading.reading_currentreading.ToString():approveReading.reading_currentreading.ToString(),
                                fk_subArea = location.fk_subarea.ToString(),
                                locationName = !string.IsNullOrEmpty(location.location_name) ? location.location_name : "",
                                locationId = location.location_id.ToString(),
                                residentId = location.resident_id.ToString(),
                                remarks = "Successfully Location Found",
                                resultCode = "1100"
                            };
                         
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
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new LocationResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
      
    }
}