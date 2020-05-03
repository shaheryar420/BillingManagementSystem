using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.DataHelpers
{
    public class RORHelpers
    {
        public List<BillElectricResponseModel> GetAllRORElectric(BillElectricRequestModel model)
        {
            List<BillElectricResponseModel> toReturn = new List<BillElectricResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var billsElectric = (from x in db.tbl_billelectric
                                        join y in db.tbl_billpicture on x.fk_billpicture equals y.billpicture_id
                                        join z in db.tbl_residents on x.fk_resident equals z.resident_id
                                        join l in db.tbl_location on x.fk_location equals l.location_id
                                        select new
                                        {
                                            x.billelectric_amount,
                                            x.billelectric_currentreading,
                                            x.billelectric_datetime,
                                            x.billelectric_id,
                                            x.billelectric_month,
                                            x.billelectric_outstanding,
                                            x.billelectric_prevreading,
                                            x.billelectric_remarks,
                                            x.billelectric_tv,
                                            x.billelectric_units,
                                            x.billelectric_water,
                                            x.fk_location,
                                            x.fk_paymentstatus,
                                            x.fk_resident,
                                            x.fk_billpicture,
                                            y.billpicture_date,
                                            y.billpicture_size,
                                            y.billpicture_type,
                                            z.resident_name,
                                            z.resident_panumber,
                                            z.resident_rank,
                                            z.resident_remarks,
                                            z.resident_unit,
                                            l.location_name,
                                            l.location_electricmeter
                                        }).ToList();
                    if (billsElectric.Count() > 0)
                    {
                        toReturn = billsElectric.Select(billElectric => new BillElectricResponseModel()
                        {
                            billElectricAmount = billElectric.billelectric_amount.ToString(),
                            billElectricCurrentReading = billElectric.billelectric_currentreading.ToString(),
                            billElectricDateTime = billElectric.billelectric_datetime.ToString(),
                            billElectricId = billElectric.billelectric_id.ToString(),
                            billElectricMonth = !String.IsNullOrEmpty(billElectric.billelectric_month)?billElectric.billelectric_month:"",
                            billElectricOutstanding = billElectric.billelectric_outstanding.ToString(),
                            billElectricPrevReading = billElectric.billelectric_prevreading.ToString(),
                            billElectricRemarks = !string.IsNullOrEmpty(billElectric.billelectric_remarks)?billElectric.billelectric_remarks:"",
                            billElectricTv = billElectric.billelectric_tv.ToString(),
                            billElectricUnits = billElectric.billelectric_units.ToString(),
                            billElectricWater = billElectric.billelectric_water.ToString(),
                            pictureData = billElectric.billpicture_date,
                            pictureSize = billElectric.billpicture_size.ToString(),
                            pictureType = billElectric.billpicture_type,
                            residentName =!String.IsNullOrEmpty(billElectric.resident_name)? billElectric.resident_name:"",
                            residentPaNo = !string.IsNullOrEmpty(billElectric.resident_panumber)?billElectric.resident_panumber:"",
                            residentRank = !string.IsNullOrEmpty(billElectric.resident_rank)?billElectric.resident_rank:"",
                            residentRemarks = !string.IsNullOrEmpty(billElectric.resident_remarks)? billElectric.resident_remarks:"",
                            residentUnit = !string.IsNullOrEmpty(billElectric.resident_unit)? billElectric.resident_unit:"",
                            locationName = !string.IsNullOrEmpty(billElectric.location_name)? billElectric.location_name:"",
                            locationMeterNo = !string.IsNullOrEmpty(billElectric.location_electricmeter)? billElectric.location_electricmeter:"",
                            fk_billPicture = billElectric.fk_billpicture.ToString(),
                            fk_location = billElectric.fk_location.ToString(),
                            fk_paymentStatus= billElectric.fk_paymentstatus.ToString(),
                            fk_resident = billElectric.fk_resident.ToString(),
                            remarks = "Successfully Found",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add(new BillElectricResponseModel()
                        {
                            resultCode = "No Record Found",
                            remarks="1200"
                        });
                    }
                }
            }
            catch(Exception Ex)
            {
                toReturn.Add( new BillElectricResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public List<BillElectricResponseModel> GetAllRORElectricByMonth(BillElectricRequestModel model)
        {
            List<BillElectricResponseModel> toReturn = new List<BillElectricResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.billElectricMonth))
                {
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var billsElectric = (from x in db.tbl_billelectric
                                             join y in db.tbl_billpicture on x.fk_billpicture equals y.billpicture_id
                                             join z in db.tbl_residents on x.fk_resident equals z.resident_id
                                             join l in db.tbl_location on x.fk_location equals l.location_id
                                             where x.billelectric_month == model.billElectricMonth
                                             select new
                                             {
                                                 x.billelectric_amount,
                                                 x.billelectric_currentreading,
                                                 x.billelectric_datetime,
                                                 x.billelectric_id,
                                                 x.billelectric_month,
                                                 x.billelectric_outstanding,
                                                 x.billelectric_prevreading,
                                                 x.billelectric_remarks,
                                                 x.billelectric_tv,
                                                 x.billelectric_units,
                                                 x.billelectric_water,
                                                 x.fk_location,
                                                 x.fk_paymentstatus,
                                                 x.fk_resident,
                                                 x.fk_billpicture,
                                                 y.billpicture_date,
                                                 y.billpicture_size,
                                                 y.billpicture_type,
                                                 z.resident_name,
                                                 z.resident_panumber,
                                                 z.resident_rank,
                                                 z.resident_remarks,
                                                 z.resident_unit,
                                                 l.location_name,
                                                 l.location_electricmeter
                                             }).ToList();
                        if (billsElectric.Count() > 0)
                        {
                            toReturn = billsElectric.Select(billElectric => new BillElectricResponseModel()
                            {
                                billElectricAmount = billElectric.billelectric_amount.ToString(),
                                billElectricCurrentReading = billElectric.billelectric_currentreading.ToString(),
                                billElectricDateTime = billElectric.billelectric_datetime.ToString(),
                                billElectricId = billElectric.billelectric_id.ToString(),
                                billElectricMonth = !String.IsNullOrEmpty(billElectric.billelectric_month) ? billElectric.billelectric_month : "",
                                billElectricOutstanding = billElectric.billelectric_outstanding.ToString(),
                                billElectricPrevReading = billElectric.billelectric_prevreading.ToString(),
                                billElectricRemarks = !string.IsNullOrEmpty(billElectric.billelectric_remarks) ? billElectric.billelectric_remarks : "",
                                billElectricTv = billElectric.billelectric_tv.ToString(),
                                billElectricUnits = billElectric.billelectric_units.ToString(),
                                billElectricWater = billElectric.billelectric_water.ToString(),
                                pictureData = billElectric.billpicture_date,
                                pictureSize = billElectric.billpicture_size.ToString(),
                                pictureType = billElectric.billpicture_type,
                                residentName = !String.IsNullOrEmpty(billElectric.resident_name) ? billElectric.resident_name : "",
                                residentPaNo = !string.IsNullOrEmpty(billElectric.resident_panumber) ? billElectric.resident_panumber : "",
                                residentRank = !string.IsNullOrEmpty(billElectric.resident_rank) ? billElectric.resident_rank : "",
                                residentRemarks = !string.IsNullOrEmpty(billElectric.resident_remarks) ? billElectric.resident_remarks : "",
                                residentUnit = !string.IsNullOrEmpty(billElectric.resident_unit) ? billElectric.resident_unit : "",
                                locationName = !string.IsNullOrEmpty(billElectric.location_name) ? billElectric.location_name : "",
                                locationMeterNo = !string.IsNullOrEmpty(billElectric.location_electricmeter) ? billElectric.location_electricmeter : "",
                                fk_billPicture = billElectric.fk_billpicture.ToString(),
                                fk_location = billElectric.fk_location.ToString(),
                                fk_paymentStatus = billElectric.fk_paymentstatus.ToString(),
                                fk_resident = billElectric.fk_resident.ToString(),
                                remarks = "Successfully Found",
                                resultCode = "1100"
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(new BillElectricResponseModel()
                            {
                                resultCode = "No Record Found",
                                remarks = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new BillElectricResponseModel()
                    {
                        remarks="Please Provide Month",
                        resultCode ="1300"
                    });
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new BillElectricResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public List<BillElectricResponseModel> GetAllRORElectricByAmount(BillElectricRequestModel model)
        {
            List<BillElectricResponseModel> toReturn = new List<BillElectricResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.billElectricAmount))
                {
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var amount = double.Parse(model.billElectricAmount);
                        var billsElectric = (from x in db.tbl_billelectric
                                             join y in db.tbl_billpicture on x.fk_billpicture equals y.billpicture_id
                                             join z in db.tbl_residents on x.fk_resident equals z.resident_id
                                             join l in db.tbl_location on x.fk_location equals l.location_id
                                             where x.billelectric_amount == amount
                                             select new
                                             {
                                                 x.billelectric_amount,
                                                 x.billelectric_currentreading,
                                                 x.billelectric_datetime,
                                                 x.billelectric_id,
                                                 x.billelectric_month,
                                                 x.billelectric_outstanding,
                                                 x.billelectric_prevreading,
                                                 x.billelectric_remarks,
                                                 x.billelectric_tv,
                                                 x.billelectric_units,
                                                 x.billelectric_water,
                                                 x.fk_location,
                                                 x.fk_paymentstatus,
                                                 x.fk_resident,
                                                 x.fk_billpicture,
                                                 y.billpicture_date,
                                                 y.billpicture_size,
                                                 y.billpicture_type,
                                                 z.resident_name,
                                                 z.resident_panumber,
                                                 z.resident_rank,
                                                 z.resident_remarks,
                                                 z.resident_unit,
                                                 l.location_name,
                                                 l.location_electricmeter
                                             }).ToList();
                        if (billsElectric.Count() > 0)
                        {
                            toReturn = billsElectric.Select(billElectric => new BillElectricResponseModel()
                            {
                                billElectricAmount = billElectric.billelectric_amount.ToString(),
                                billElectricCurrentReading = billElectric.billelectric_currentreading.ToString(),
                                billElectricDateTime = billElectric.billelectric_datetime.ToString(),
                                billElectricId = billElectric.billelectric_id.ToString(),
                                billElectricMonth = !String.IsNullOrEmpty(billElectric.billelectric_month) ? billElectric.billelectric_month : "",
                                billElectricOutstanding = billElectric.billelectric_outstanding.ToString(),
                                billElectricPrevReading = billElectric.billelectric_prevreading.ToString(),
                                billElectricRemarks = !string.IsNullOrEmpty(billElectric.billelectric_remarks) ? billElectric.billelectric_remarks : "",
                                billElectricTv = billElectric.billelectric_tv.ToString(),
                                billElectricUnits = billElectric.billelectric_units.ToString(),
                                billElectricWater = billElectric.billelectric_water.ToString(),
                                pictureData = billElectric.billpicture_date,
                                pictureSize = billElectric.billpicture_size.ToString(),
                                pictureType = billElectric.billpicture_type,
                                residentName = !String.IsNullOrEmpty(billElectric.resident_name) ? billElectric.resident_name : "",
                                residentPaNo = !string.IsNullOrEmpty(billElectric.resident_panumber) ? billElectric.resident_panumber : "",
                                residentRank = !string.IsNullOrEmpty(billElectric.resident_rank) ? billElectric.resident_rank : "",
                                residentRemarks = !string.IsNullOrEmpty(billElectric.resident_remarks) ? billElectric.resident_remarks : "",
                                residentUnit = !string.IsNullOrEmpty(billElectric.resident_unit) ? billElectric.resident_unit : "",
                                locationName = !string.IsNullOrEmpty(billElectric.location_name) ? billElectric.location_name : "",
                                locationMeterNo = !string.IsNullOrEmpty(billElectric.location_electricmeter) ? billElectric.location_electricmeter : "",
                                fk_billPicture = billElectric.fk_billpicture.ToString(),
                                fk_location = billElectric.fk_location.ToString(),
                                fk_paymentStatus = billElectric.fk_paymentstatus.ToString(),
                                fk_resident = billElectric.fk_resident.ToString(),
                                remarks = "Successfully Found",
                                resultCode = "1100"
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(new BillElectricResponseModel()
                            {
                                resultCode = "No Record Found",
                                remarks = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new BillElectricResponseModel()
                    {
                        remarks = "Please Provide Amount",
                        resultCode = "1300"
                    });
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new BillElectricResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public List<BillElectricResponseModel> GetAllRORElectricByMeterNo(BillElectricRequestModel model)
        {
            List<BillElectricResponseModel> toReturn = new List<BillElectricResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.meterNo))
                {
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var billsElectric = (from x in db.tbl_billelectric
                                             join y in db.tbl_billpicture on x.fk_billpicture equals y.billpicture_id
                                             join z in db.tbl_residents on x.fk_resident equals z.resident_id
                                             join l in db.tbl_location on x.fk_location equals l.location_id
                                             where l.location_electricmeter == model.meterNo
                                             select new
                                             {
                                                 x.billelectric_amount,
                                                 x.billelectric_currentreading,
                                                 x.billelectric_datetime,
                                                 x.billelectric_id,
                                                 x.billelectric_month,
                                                 x.billelectric_outstanding,
                                                 x.billelectric_prevreading,
                                                 x.billelectric_remarks,
                                                 x.billelectric_tv,
                                                 x.billelectric_units,
                                                 x.billelectric_water,
                                                 x.fk_location,
                                                 x.fk_paymentstatus,
                                                 x.fk_resident,
                                                 x.fk_billpicture,
                                                 y.billpicture_date,
                                                 y.billpicture_size,
                                                 y.billpicture_type,
                                                 z.resident_name,
                                                 z.resident_panumber,
                                                 z.resident_rank,
                                                 z.resident_remarks,
                                                 z.resident_unit,
                                                 l.location_name,
                                                 l.location_electricmeter
                                             }).ToList();
                        if (billsElectric.Count() > 0)
                        {
                            toReturn = billsElectric.Select(billElectric => new BillElectricResponseModel()
                            {
                                billElectricAmount = billElectric.billelectric_amount.ToString(),
                                billElectricCurrentReading = billElectric.billelectric_currentreading.ToString(),
                                billElectricDateTime = billElectric.billelectric_datetime.ToString(),
                                billElectricId = billElectric.billelectric_id.ToString(),
                                billElectricMonth = !String.IsNullOrEmpty(billElectric.billelectric_month) ? billElectric.billelectric_month : "",
                                billElectricOutstanding = billElectric.billelectric_outstanding.ToString(),
                                billElectricPrevReading = billElectric.billelectric_prevreading.ToString(),
                                billElectricRemarks = !string.IsNullOrEmpty(billElectric.billelectric_remarks) ? billElectric.billelectric_remarks : "",
                                billElectricTv = billElectric.billelectric_tv.ToString(),
                                billElectricUnits = billElectric.billelectric_units.ToString(),
                                billElectricWater = billElectric.billelectric_water.ToString(),
                                pictureData = billElectric.billpicture_date,
                                pictureSize = billElectric.billpicture_size.ToString(),
                                pictureType = billElectric.billpicture_type,
                                residentName = !String.IsNullOrEmpty(billElectric.resident_name) ? billElectric.resident_name : "",
                                residentPaNo = !string.IsNullOrEmpty(billElectric.resident_panumber) ? billElectric.resident_panumber : "",
                                residentRank = !string.IsNullOrEmpty(billElectric.resident_rank) ? billElectric.resident_rank : "",
                                residentRemarks = !string.IsNullOrEmpty(billElectric.resident_remarks) ? billElectric.resident_remarks : "",
                                residentUnit = !string.IsNullOrEmpty(billElectric.resident_unit) ? billElectric.resident_unit : "",
                                locationName = !string.IsNullOrEmpty(billElectric.location_name) ? billElectric.location_name : "",
                                locationMeterNo = !string.IsNullOrEmpty(billElectric.location_electricmeter) ? billElectric.location_electricmeter : "",
                                fk_billPicture = billElectric.fk_billpicture.ToString(),
                                fk_location = billElectric.fk_location.ToString(),
                                fk_paymentStatus = billElectric.fk_paymentstatus.ToString(),
                                fk_resident = billElectric.fk_resident.ToString(),
                                remarks = "Successfully Found",
                                resultCode = "1100"
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(new BillElectricResponseModel()
                            {
                                resultCode = "No Record Found",
                                remarks = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new BillElectricResponseModel()
                    {
                        remarks = "Please Provide MeterNo",
                        resultCode = "1300"
                    });
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new BillElectricResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public List<BillElectricResponseModel> GetAllRORElectricByArea(BillElectricRequestModel model)
        {
            List<BillElectricResponseModel> toReturn = new List<BillElectricResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.areaName))
                {
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var billsElectric = (from x in db.tbl_billelectric
                                             join y in db.tbl_billpicture on x.fk_billpicture equals y.billpicture_id
                                             join z in db.tbl_residents on x.fk_resident equals z.resident_id
                                             join l in db.tbl_location on x.fk_location equals l.location_id
                                             join a in db.tbl_area on l.fk_area equals a.area_id
                                             where a.area_name == model.areaName
                                             select new
                                             {
                                                 x.billelectric_amount,
                                                 x.billelectric_currentreading,
                                                 x.billelectric_datetime,
                                                 x.billelectric_id,
                                                 x.billelectric_month,
                                                 x.billelectric_outstanding,
                                                 x.billelectric_prevreading,
                                                 x.billelectric_remarks,
                                                 x.billelectric_tv,
                                                 x.billelectric_units,
                                                 x.billelectric_water,
                                                 x.fk_location,
                                                 x.fk_paymentstatus,
                                                 x.fk_resident,
                                                 x.fk_billpicture,
                                                 y.billpicture_date,
                                                 y.billpicture_size,
                                                 y.billpicture_type,
                                                 z.resident_name,
                                                 z.resident_panumber,
                                                 z.resident_rank,
                                                 z.resident_remarks,
                                                 z.resident_unit,
                                                 l.location_name,
                                                 l.location_electricmeter
                                             }).ToList();
                        if (billsElectric.Count() > 0)
                        {
                            toReturn = billsElectric.Select(billElectric => new BillElectricResponseModel()
                            {
                                billElectricAmount = billElectric.billelectric_amount.ToString(),
                                billElectricCurrentReading = billElectric.billelectric_currentreading.ToString(),
                                billElectricDateTime = billElectric.billelectric_datetime.ToString(),
                                billElectricId = billElectric.billelectric_id.ToString(),
                                billElectricMonth = !String.IsNullOrEmpty(billElectric.billelectric_month) ? billElectric.billelectric_month : "",
                                billElectricOutstanding = billElectric.billelectric_outstanding.ToString(),
                                billElectricPrevReading = billElectric.billelectric_prevreading.ToString(),
                                billElectricRemarks = !string.IsNullOrEmpty(billElectric.billelectric_remarks) ? billElectric.billelectric_remarks : "",
                                billElectricTv = billElectric.billelectric_tv.ToString(),
                                billElectricUnits = billElectric.billelectric_units.ToString(),
                                billElectricWater = billElectric.billelectric_water.ToString(),
                                pictureData = billElectric.billpicture_date,
                                pictureSize = billElectric.billpicture_size.ToString(),
                                pictureType = billElectric.billpicture_type,
                                residentName = !String.IsNullOrEmpty(billElectric.resident_name) ? billElectric.resident_name : "",
                                residentPaNo = !string.IsNullOrEmpty(billElectric.resident_panumber) ? billElectric.resident_panumber : "",
                                residentRank = !string.IsNullOrEmpty(billElectric.resident_rank) ? billElectric.resident_rank : "",
                                residentRemarks = !string.IsNullOrEmpty(billElectric.resident_remarks) ? billElectric.resident_remarks : "",
                                residentUnit = !string.IsNullOrEmpty(billElectric.resident_unit) ? billElectric.resident_unit : "",
                                locationName = !string.IsNullOrEmpty(billElectric.location_name) ? billElectric.location_name : "",
                                locationMeterNo = !string.IsNullOrEmpty(billElectric.location_electricmeter) ? billElectric.location_electricmeter : "",
                                fk_billPicture = billElectric.fk_billpicture.ToString(),
                                fk_location = billElectric.fk_location.ToString(),
                                fk_paymentStatus = billElectric.fk_paymentstatus.ToString(),
                                fk_resident = billElectric.fk_resident.ToString(),
                                remarks = "Successfully Found",
                                resultCode = "1100"
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(new BillElectricResponseModel()
                            {
                                resultCode = "No Record Found",
                                remarks = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new BillElectricResponseModel()
                    {
                        remarks = "Please Provide Area Name",
                        resultCode = "1300"
                    });
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new BillElectricResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public List<BillElectricResponseModel> GetAllRORElectricByResident(BillElectricRequestModel model)
        {
            List<BillElectricResponseModel> toReturn = new List<BillElectricResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.fk_resident))
                {
                    int residentId = int.Parse(model.fk_resident);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var billsElectric = (from x in db.tbl_billelectric
                                             join y in db.tbl_billpicture on x.fk_billpicture equals y.billpicture_id
                                             join z in db.tbl_residents on x.fk_resident equals z.resident_id
                                             join l in db.tbl_location on x.fk_location equals l.location_id
                                             where x.fk_resident == residentId
                                             select new
                                             {
                                                 x.billelectric_amount,
                                                 x.billelectric_currentreading,
                                                 x.billelectric_datetime,
                                                 x.billelectric_id,
                                                 x.billelectric_month,
                                                 x.billelectric_outstanding,
                                                 x.billelectric_prevreading,
                                                 x.billelectric_remarks,
                                                 x.billelectric_tv,
                                                 x.billelectric_units,
                                                 x.billelectric_water,
                                                 x.fk_location,
                                                 x.fk_paymentstatus,
                                                 x.fk_resident,
                                                 x.fk_billpicture,
                                                 y.billpicture_date,
                                                 y.billpicture_size,
                                                 y.billpicture_type,
                                                 z.resident_name,
                                                 z.resident_panumber,
                                                 z.resident_rank,
                                                 z.resident_remarks,
                                                 z.resident_unit,
                                                 l.location_name,
                                                 l.location_electricmeter
                                             }).ToList();
                        if (billsElectric.Count() > 0)
                        {
                            toReturn = billsElectric.Select(billElectric => new BillElectricResponseModel()
                            {
                                billElectricAmount = billElectric.billelectric_amount.ToString(),
                                billElectricCurrentReading = billElectric.billelectric_currentreading.ToString(),
                                billElectricDateTime = billElectric.billelectric_datetime.ToString(),
                                billElectricId = billElectric.billelectric_id.ToString(),
                                billElectricMonth = !String.IsNullOrEmpty(billElectric.billelectric_month) ? billElectric.billelectric_month : "",
                                billElectricOutstanding = billElectric.billelectric_outstanding.ToString(),
                                billElectricPrevReading = billElectric.billelectric_prevreading.ToString(),
                                billElectricRemarks = !string.IsNullOrEmpty(billElectric.billelectric_remarks) ? billElectric.billelectric_remarks : "",
                                billElectricTv = billElectric.billelectric_tv.ToString(),
                                billElectricUnits = billElectric.billelectric_units.ToString(),
                                billElectricWater = billElectric.billelectric_water.ToString(),
                                pictureData = billElectric.billpicture_date,
                                pictureSize = billElectric.billpicture_size.ToString(),
                                pictureType = billElectric.billpicture_type,
                                residentName = !String.IsNullOrEmpty(billElectric.resident_name) ? billElectric.resident_name : "",
                                residentPaNo = !string.IsNullOrEmpty(billElectric.resident_panumber) ? billElectric.resident_panumber : "",
                                residentRank = !string.IsNullOrEmpty(billElectric.resident_rank) ? billElectric.resident_rank : "",
                                residentRemarks = !string.IsNullOrEmpty(billElectric.resident_remarks) ? billElectric.resident_remarks : "",
                                residentUnit = !string.IsNullOrEmpty(billElectric.resident_unit) ? billElectric.resident_unit : "",
                                locationName = !string.IsNullOrEmpty(billElectric.location_name) ? billElectric.location_name : "",
                                locationMeterNo = !string.IsNullOrEmpty(billElectric.location_electricmeter) ? billElectric.location_electricmeter : "",
                                fk_billPicture = billElectric.fk_billpicture.ToString(),
                                fk_location = billElectric.fk_location.ToString(),
                                fk_paymentStatus = billElectric.fk_paymentstatus.ToString(),
                                fk_resident = billElectric.fk_resident.ToString(),
                                remarks = "Successfully Found",
                                resultCode = "1100"
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(new BillElectricResponseModel()
                            {
                                resultCode = "No Record Found",
                                remarks = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new BillElectricResponseModel()
                    {
                        remarks = "Please Provide Resident",
                        resultCode = "1300"
                    });
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new BillElectricResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public List<BillElectricResponseModel> GetAllRORElectricByPaNo(BillElectricRequestModel model)
        {
            List<BillElectricResponseModel> toReturn = new List<BillElectricResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.paNo))
                {
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var billsElectric = (from x in db.tbl_billelectric
                                             join y in db.tbl_billpicture on x.fk_billpicture equals y.billpicture_id
                                             join z in db.tbl_residents on x.fk_resident equals z.resident_id
                                             join l in db.tbl_location on x.fk_location equals l.location_id
                                             where z.resident_panumber == model.paNo
                                             select new
                                             {
                                                 x.billelectric_amount,
                                                 x.billelectric_currentreading,
                                                 x.billelectric_datetime,
                                                 x.billelectric_id,
                                                 x.billelectric_month,
                                                 x.billelectric_outstanding,
                                                 x.billelectric_prevreading,
                                                 x.billelectric_remarks,
                                                 x.billelectric_tv,
                                                 x.billelectric_units,
                                                 x.billelectric_water,
                                                 x.fk_location,
                                                 x.fk_paymentstatus,
                                                 x.fk_resident,
                                                 x.fk_billpicture,
                                                 y.billpicture_date,
                                                 y.billpicture_size,
                                                 y.billpicture_type,
                                                 z.resident_name,
                                                 z.resident_panumber,
                                                 z.resident_rank,
                                                 z.resident_remarks,
                                                 z.resident_unit,
                                                 l.location_name,
                                                 l.location_electricmeter
                                             }).ToList();
                        if (billsElectric.Count() > 0)
                        {
                            toReturn = billsElectric.Select(billElectric => new BillElectricResponseModel()
                            {
                                billElectricAmount = billElectric.billelectric_amount.ToString(),
                                billElectricCurrentReading = billElectric.billelectric_currentreading.ToString(),
                                billElectricDateTime = billElectric.billelectric_datetime.ToString(),
                                billElectricId = billElectric.billelectric_id.ToString(),
                                billElectricMonth = !String.IsNullOrEmpty(billElectric.billelectric_month) ? billElectric.billelectric_month : "",
                                billElectricOutstanding = billElectric.billelectric_outstanding.ToString(),
                                billElectricPrevReading = billElectric.billelectric_prevreading.ToString(),
                                billElectricRemarks = !string.IsNullOrEmpty(billElectric.billelectric_remarks) ? billElectric.billelectric_remarks : "",
                                billElectricTv = billElectric.billelectric_tv.ToString(),
                                billElectricUnits = billElectric.billelectric_units.ToString(),
                                billElectricWater = billElectric.billelectric_water.ToString(),
                                pictureData = billElectric.billpicture_date,
                                pictureSize = billElectric.billpicture_size.ToString(),
                                pictureType = billElectric.billpicture_type,
                                residentName = !String.IsNullOrEmpty(billElectric.resident_name) ? billElectric.resident_name : "",
                                residentPaNo = !string.IsNullOrEmpty(billElectric.resident_panumber) ? billElectric.resident_panumber : "",
                                residentRank = !string.IsNullOrEmpty(billElectric.resident_rank) ? billElectric.resident_rank : "",
                                residentRemarks = !string.IsNullOrEmpty(billElectric.resident_remarks) ? billElectric.resident_remarks : "",
                                residentUnit = !string.IsNullOrEmpty(billElectric.resident_unit) ? billElectric.resident_unit : "",
                                locationName = !string.IsNullOrEmpty(billElectric.location_name) ? billElectric.location_name : "",
                                locationMeterNo = !string.IsNullOrEmpty(billElectric.location_electricmeter) ? billElectric.location_electricmeter : "",
                                fk_billPicture = billElectric.fk_billpicture.ToString(),
                                fk_location = billElectric.fk_location.ToString(),
                                fk_paymentStatus = billElectric.fk_paymentstatus.ToString(),
                                fk_resident = billElectric.fk_resident.ToString(),
                                remarks = "Successfully Found",
                                resultCode = "1100"
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(new BillElectricResponseModel()
                            {
                                resultCode = "No Record Found",
                                remarks = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new BillElectricResponseModel()
                    {
                        remarks = "Please Provide Pa No",
                        resultCode = "1300"
                    });
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new BillElectricResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public List<BillElectricResponseModel> GetAllRORElectricByUnit(BillElectricRequestModel model)
        {
            List<BillElectricResponseModel> toReturn = new List<BillElectricResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.unit))
                {
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var billsElectric = (from x in db.tbl_billelectric
                                             join y in db.tbl_billpicture on x.fk_billpicture equals y.billpicture_id
                                             join z in db.tbl_residents on x.fk_resident equals z.resident_id
                                             join l in db.tbl_location on x.fk_location equals l.location_id
                                             where z.resident_unit == model.unit
                                             select new
                                             {
                                                 x.billelectric_amount,
                                                 x.billelectric_currentreading,
                                                 x.billelectric_datetime,
                                                 x.billelectric_id,
                                                 x.billelectric_month,
                                                 x.billelectric_outstanding,
                                                 x.billelectric_prevreading,
                                                 x.billelectric_remarks,
                                                 x.billelectric_tv,
                                                 x.billelectric_units,
                                                 x.billelectric_water,
                                                 x.fk_location,
                                                 x.fk_paymentstatus,
                                                 x.fk_resident,
                                                 x.fk_billpicture,
                                                 y.billpicture_date,
                                                 y.billpicture_size,
                                                 y.billpicture_type,
                                                 z.resident_name,
                                                 z.resident_panumber,
                                                 z.resident_rank,
                                                 z.resident_remarks,
                                                 z.resident_unit,
                                                 l.location_name,
                                                 l.location_electricmeter
                                             }).ToList();
                        if (billsElectric.Count() > 0)
                        {
                            toReturn = billsElectric.Select(billElectric => new BillElectricResponseModel()
                            {
                                billElectricAmount = billElectric.billelectric_amount.ToString(),
                                billElectricCurrentReading = billElectric.billelectric_currentreading.ToString(),
                                billElectricDateTime = billElectric.billelectric_datetime.ToString(),
                                billElectricId = billElectric.billelectric_id.ToString(),
                                billElectricMonth = !String.IsNullOrEmpty(billElectric.billelectric_month) ? billElectric.billelectric_month : "",
                                billElectricOutstanding = billElectric.billelectric_outstanding.ToString(),
                                billElectricPrevReading = billElectric.billelectric_prevreading.ToString(),
                                billElectricRemarks = !string.IsNullOrEmpty(billElectric.billelectric_remarks) ? billElectric.billelectric_remarks : "",
                                billElectricTv = billElectric.billelectric_tv.ToString(),
                                billElectricUnits = billElectric.billelectric_units.ToString(),
                                billElectricWater = billElectric.billelectric_water.ToString(),
                                pictureData = billElectric.billpicture_date,
                                pictureSize = billElectric.billpicture_size.ToString(),
                                pictureType = billElectric.billpicture_type,
                                residentName = !String.IsNullOrEmpty(billElectric.resident_name) ? billElectric.resident_name : "",
                                residentPaNo = !string.IsNullOrEmpty(billElectric.resident_panumber) ? billElectric.resident_panumber : "",
                                residentRank = !string.IsNullOrEmpty(billElectric.resident_rank) ? billElectric.resident_rank : "",
                                residentRemarks = !string.IsNullOrEmpty(billElectric.resident_remarks) ? billElectric.resident_remarks : "",
                                residentUnit = !string.IsNullOrEmpty(billElectric.resident_unit) ? billElectric.resident_unit : "",
                                locationName = !string.IsNullOrEmpty(billElectric.location_name) ? billElectric.location_name : "",
                                locationMeterNo = !string.IsNullOrEmpty(billElectric.location_electricmeter) ? billElectric.location_electricmeter : "",
                                fk_billPicture = billElectric.fk_billpicture.ToString(),
                                fk_location = billElectric.fk_location.ToString(),
                                fk_paymentStatus = billElectric.fk_paymentstatus.ToString(),
                                fk_resident = billElectric.fk_resident.ToString(),
                                remarks = "Successfully Found",
                                resultCode = "1100"
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(new BillElectricResponseModel()
                            {
                                resultCode = "No Record Found",
                                remarks = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new BillElectricResponseModel()
                    {
                        remarks = "Please Provide Unit",
                        resultCode = "1300"
                    });
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new BillElectricResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public List<BillElectricResponseModel> GetAllRORElectricByRank(BillElectricRequestModel model)
        {
            List<BillElectricResponseModel> toReturn = new List<BillElectricResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.rank))
                {
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var billsElectric = (from x in db.tbl_billelectric
                                             join y in db.tbl_billpicture on x.fk_billpicture equals y.billpicture_id
                                             join z in db.tbl_residents on x.fk_resident equals z.resident_id
                                             join l in db.tbl_location on x.fk_location equals l.location_id
                                             where z.resident_rank == model.rank
                                             select new
                                             {
                                                 x.billelectric_amount,
                                                 x.billelectric_currentreading,
                                                 x.billelectric_datetime,
                                                 x.billelectric_id,
                                                 x.billelectric_month,
                                                 x.billelectric_outstanding,
                                                 x.billelectric_prevreading,
                                                 x.billelectric_remarks,
                                                 x.billelectric_tv,
                                                 x.billelectric_units,
                                                 x.billelectric_water,
                                                 x.fk_location,
                                                 x.fk_paymentstatus,
                                                 x.fk_resident,
                                                 x.fk_billpicture,
                                                 y.billpicture_date,
                                                 y.billpicture_size,
                                                 y.billpicture_type,
                                                 z.resident_name,
                                                 z.resident_panumber,
                                                 z.resident_rank,
                                                 z.resident_remarks,
                                                 z.resident_unit,
                                                 l.location_name,
                                                 l.location_electricmeter
                                             }).ToList();
                        if (billsElectric.Count() > 0)
                        {
                            toReturn = billsElectric.Select(billElectric => new BillElectricResponseModel()
                            {
                                billElectricAmount = billElectric.billelectric_amount.ToString(),
                                billElectricCurrentReading = billElectric.billelectric_currentreading.ToString(),
                                billElectricDateTime = billElectric.billelectric_datetime.ToString(),
                                billElectricId = billElectric.billelectric_id.ToString(),
                                billElectricMonth = !String.IsNullOrEmpty(billElectric.billelectric_month) ? billElectric.billelectric_month : "",
                                billElectricOutstanding = billElectric.billelectric_outstanding.ToString(),
                                billElectricPrevReading = billElectric.billelectric_prevreading.ToString(),
                                billElectricRemarks = !string.IsNullOrEmpty(billElectric.billelectric_remarks) ? billElectric.billelectric_remarks : "",
                                billElectricTv = billElectric.billelectric_tv.ToString(),
                                billElectricUnits = billElectric.billelectric_units.ToString(),
                                billElectricWater = billElectric.billelectric_water.ToString(),
                                pictureData = billElectric.billpicture_date,
                                pictureSize = billElectric.billpicture_size.ToString(),
                                pictureType = billElectric.billpicture_type,
                                residentName = !String.IsNullOrEmpty(billElectric.resident_name) ? billElectric.resident_name : "",
                                residentPaNo = !string.IsNullOrEmpty(billElectric.resident_panumber) ? billElectric.resident_panumber : "",
                                residentRank = !string.IsNullOrEmpty(billElectric.resident_rank) ? billElectric.resident_rank : "",
                                residentRemarks = !string.IsNullOrEmpty(billElectric.resident_remarks) ? billElectric.resident_remarks : "",
                                residentUnit = !string.IsNullOrEmpty(billElectric.resident_unit) ? billElectric.resident_unit : "",
                                locationName = !string.IsNullOrEmpty(billElectric.location_name) ? billElectric.location_name : "",
                                locationMeterNo = !string.IsNullOrEmpty(billElectric.location_electricmeter) ? billElectric.location_electricmeter : "",
                                fk_billPicture = billElectric.fk_billpicture.ToString(),
                                fk_location = billElectric.fk_location.ToString(),
                                fk_paymentStatus = billElectric.fk_paymentstatus.ToString(),
                                fk_resident = billElectric.fk_resident.ToString(),
                                remarks = "Successfully Found",
                                resultCode = "1100"
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(new BillElectricResponseModel()
                            {
                                resultCode = "No Record Found",
                                remarks = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new BillElectricResponseModel()
                    {
                        remarks = "Please Provide Rank",
                        resultCode = "1300"
                    });
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new BillElectricResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public List<BillElectricResponseModel> GetAllRORElectricByDate(BillElectricRequestModel model)
        {
            List<BillElectricResponseModel> toReturn = new List<BillElectricResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.billElectricDateTime))
                {
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var dateEntered = DateTime.ParseExact(model.billElectricDateTime, "dd/MM/yyyy H:mm:ss", CultureInfo.InvariantCulture);
                        var billsElectric = (from x in db.tbl_billelectric
                                             join y in db.tbl_billpicture on x.fk_billpicture equals y.billpicture_id
                                             join z in db.tbl_residents on x.fk_resident equals z.resident_id
                                             join l in db.tbl_location on x.fk_location equals l.location_id
                                             where x.billelectric_datetime == dateEntered
                                             select new
                                             {
                                                 x.billelectric_amount,
                                                 x.billelectric_currentreading,
                                                 x.billelectric_datetime,
                                                 x.billelectric_id,
                                                 x.billelectric_month,
                                                 x.billelectric_outstanding,
                                                 x.billelectric_prevreading,
                                                 x.billelectric_remarks,
                                                 x.billelectric_tv,
                                                 x.billelectric_units,
                                                 x.billelectric_water,
                                                 x.fk_location,
                                                 x.fk_paymentstatus,
                                                 x.fk_resident,
                                                 x.fk_billpicture,
                                                 y.billpicture_date,
                                                 y.billpicture_size,
                                                 y.billpicture_type,
                                                 z.resident_name,
                                                 z.resident_panumber,
                                                 z.resident_rank,
                                                 z.resident_remarks,
                                                 z.resident_unit,
                                                 l.location_name,
                                                 l.location_electricmeter
                                             }).ToList();
                        if (billsElectric.Count() > 0)
                        {
                            toReturn = billsElectric.Select(billElectric => new BillElectricResponseModel()
                            {
                                billElectricAmount = billElectric.billelectric_amount.ToString(),
                                billElectricCurrentReading = billElectric.billelectric_currentreading.ToString(),
                                billElectricDateTime = billElectric.billelectric_datetime.ToString(),
                                billElectricId = billElectric.billelectric_id.ToString(),
                                billElectricMonth = !String.IsNullOrEmpty(billElectric.billelectric_month) ? billElectric.billelectric_month : "",
                                billElectricOutstanding = billElectric.billelectric_outstanding.ToString(),
                                billElectricPrevReading = billElectric.billelectric_prevreading.ToString(),
                                billElectricRemarks = !string.IsNullOrEmpty(billElectric.billelectric_remarks) ? billElectric.billelectric_remarks : "",
                                billElectricTv = billElectric.billelectric_tv.ToString(),
                                billElectricUnits = billElectric.billelectric_units.ToString(),
                                billElectricWater = billElectric.billelectric_water.ToString(),
                                pictureData = billElectric.billpicture_date,
                                pictureSize = billElectric.billpicture_size.ToString(),
                                pictureType = billElectric.billpicture_type,
                                residentName = !String.IsNullOrEmpty(billElectric.resident_name) ? billElectric.resident_name : "",
                                residentPaNo = !string.IsNullOrEmpty(billElectric.resident_panumber) ? billElectric.resident_panumber : "",
                                residentRank = !string.IsNullOrEmpty(billElectric.resident_rank) ? billElectric.resident_rank : "",
                                residentRemarks = !string.IsNullOrEmpty(billElectric.resident_remarks) ? billElectric.resident_remarks : "",
                                residentUnit = !string.IsNullOrEmpty(billElectric.resident_unit) ? billElectric.resident_unit : "",
                                locationName = !string.IsNullOrEmpty(billElectric.location_name) ? billElectric.location_name : "",
                                locationMeterNo = !string.IsNullOrEmpty(billElectric.location_electricmeter) ? billElectric.location_electricmeter : "",
                                fk_billPicture = billElectric.fk_billpicture.ToString(),
                                fk_location = billElectric.fk_location.ToString(),
                                fk_paymentStatus = billElectric.fk_paymentstatus.ToString(),
                                fk_resident = billElectric.fk_resident.ToString(),
                                remarks = "Successfully Found",
                                resultCode = "1100"
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(new BillElectricResponseModel()
                            {
                                resultCode = "No Record Found",
                                remarks = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new BillElectricResponseModel()
                    {
                        remarks = "Please Provide Date",
                        resultCode = "1300"
                    });
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new BillElectricResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
    }
}