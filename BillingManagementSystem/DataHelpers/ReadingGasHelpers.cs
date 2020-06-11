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
                            double _units = readingGas.reading_units;
                            var location = (from x in db.tbl_location where x.location_gassmeter == readingGas.reading_meterno select x).FirstOrDefault();
                            if (location != null)
                            {
                                var residentBuilding = (from x in db.tbl_residentbuilding where x.fk_building == location.location_id select x).FirstOrDefault();
                                if (residentBuilding != null)
                                {
                                    var previousPendingBill = (from x in db.tbl_billgas where x.fk_paymentstatus == 3 && x.fk_location == residentBuilding.fk_building select x).FirstOrDefault();
                                    var outstanding = "";
                                    if (previousPendingBill != null)
                                    {
                                        outstanding = previousPendingBill.outstanding.ToString();
                                        previousPendingBill.fk_paymentstatus = 2;
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
                                        totalAmount = totalAmount + (totalFPA  + meterRent);
                                        totalAmount = (totalAmount - totalEnergyCharges);
                                        
                                        /// Entry
                                        var newBill = new tbl_billgas()
                                        {
                                            fk_billpicture = newBillPicture.billpicture_id,
                                            fk_location = residentBuilding.fk_building,
                                            fk_resident = residentBuilding.fk_resident,
                                            fk_paymentstatus = 3,
                                            amount = totalAmount,
                                            currentreading = readingGas.reading_currentreading,
                                            month = readingGas.reading_month,
                                            outstanding = 0,
                                            prevreading = readingGas.reading_prevreading,
                                            units = readingGas.reading_units,
                                            fpa = totalFPA,
                                            rebate = totalEnergyCharges,
                                            datetime = readingGas.reading_datetime,
                                            remarks = readingGas.reading_remarks
                                        };
                                        db.tbl_billgas.Add(newBill);
                                        db.SaveChanges();
                                        newBill.outstanding = newBill.amount + double.Parse(outstanding);
                                        var newreadingGasLog = new tbl_readinggaslog()
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
                                        db.tbl_readinggaslog.Add(newreadingGasLog);
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
                                  join y in db.tbl_location on x.fk_subarea equals y.fk_subarea
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
                                        join rb in db.tbl_residentbuilding on x.location_id equals rb.fk_building
                                        where x.location_gassmeter == model.locationGassMeter
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
                            if (billGas != null)
                            {
                                toReturn.previousReading = !String.IsNullOrEmpty(billGas.prevreading.ToString()) ? billGas.prevreading.ToString() : "";
                                toReturn.outstanding = !String.IsNullOrEmpty(billGas.outstanding.ToString()) ? billGas.outstanding.ToString() : "";
                                toReturn.billMonth = !string.IsNullOrEmpty(billGas.month) ? billGas.month : "";
                                toReturn.currentReading = billGas.currentreading.ToString();
                                toReturn.currentUnit = billGas.units.ToString();
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