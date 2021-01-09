using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models.RequestModels;
using BillingManagementSystem.Models.ResponseModels;
using BillingManagementSystem.SubDataHelpers;
using BillingManagementSystem.Models;


namespace BillingManagementSystem.DataHelpers
{
    public class ConsummerPoolHelpers
    {
        public List<ConsummerPoolResponseModel> GetConsummerPool (ConsummerPoolRequestModel model)
        {
            List<ConsummerPoolResponseModel> toReturn = new List<ConsummerPoolResponseModel>();
            try
            {
                using (var db = new db_bmsEntities()) 
                {
                    var consummerPool = (from x in db.tbl_consummer_pool
                                         join y in db.tbl_location on x.fk_location equals y.location_id
                                         join z in db.tbl_subarea on y.fk_subarea equals z.subarea_id
                                         join a in db.tbl_area on z.fk_area equals a.area_id
                                         join r in db.tbl_residents on x.consummer_no equals r.resident_consumer_no into residents
                                         from r in residents.DefaultIfEmpty()
                                         select new
                                         {
                                             x.consummer_no,
                                             x.id,
                                             x.is_active,
                                             y.location_id,
                                             y.location_name,
                                             z.subarea_id,
                                             z.subarea_name,
                                             a.area_id,
                                             a.area_name,
                                             status = r != null ? "Occupied" : "Available",
                                         }).ToList();
                    if (consummerPool.Count() > 0)
                    {
                        toReturn = consummerPool.Select(x => new ConsummerPoolResponseModel()
                        {
                            areaId = x.area_id.ToString(),
                            areaName = !string.IsNullOrEmpty(x.area_name) ? x.area_name : "",
                            isActive = x.is_active.ToString(),
                            consummerId = x.id.ToString(),
                            consummerNo = x.consummer_no,
                            locationId = x.location_id.ToString(),
                            locationName = !string.IsNullOrEmpty(x.location_name) ? x.location_name : "",
                            subAreaId = x.subarea_id.ToString(),
                            subAreaName = !string.IsNullOrEmpty(x.subarea_name) ? x.subarea_name : "",
                            status = x.status,
                            remarks = "Success",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add( new ConsummerPoolResponseModel()
                        {
                            remarks = "No Record",
                            resultCode = "1200"
                        }); 
                    }
                }

            }
            catch(Exception Ex)
            {
                toReturn.Add( new ConsummerPoolResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public ConsummerPoolResponseModel AddConsummerNo(ConsummerPoolRequestModel model)
        {
            ConsummerPoolResponseModel toReturn = new ConsummerPoolResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.is_active))
                {
                    if (!string.IsNullOrEmpty(model.consummerNo))
                    {
                        if (new ModelsValidatorHelper().validateint(model.fk_location))
                        {
                            var newConsummerNo = new tbl_consummer_pool()
                            {
                                consummer_no = model.consummerNo,
                                fk_location = int.Parse(model.fk_location),
                                is_active = int.Parse(model.is_active)
                            };
                            using (db_bmsEntities db = new db_bmsEntities())
                            {
                                db.tbl_consummer_pool.Add(newConsummerNo);
                                db.SaveChanges();
                                toReturn = new ConsummerPoolResponseModel()
                                {
                                    remarks = "Success",
                                    resultCode = "1100"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new ConsummerPoolResponseModel()
                            {
                                resultCode = "1300",
                                remarks = "Please Provide Location"
                            };
                        }

                    }
                    else
                    {
                        toReturn = new ConsummerPoolResponseModel()
                        {
                            remarks = "Please Provide Consummer No",
                            resultCode = "1300"
                        };
                    }
                }
                else
                {
                    toReturn = new ConsummerPoolResponseModel()
                    {
                        resultCode = "1300",
                        remarks = "Please Provide Consummer No Status"
                    };
                }
            }
            catch(Exception Ex)
            {
                toReturn = new ConsummerPoolResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public ConsummerPoolResponseModel UpdateConsummerNo(ConsummerPoolRequestModel model)
        {
            ConsummerPoolResponseModel toReturn = new ConsummerPoolResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.consummerNoId))
                { 
                     int consummerNoId = int.Parse(model.consummerNoId);
                     using (db_bmsEntities db = new db_bmsEntities())
                                {
                                    var consummerNo = db.tbl_consummer_pool.Find(consummerNoId);
                                    consummerNo.consummer_no = !string.IsNullOrEmpty(model.consummerNo) ? model.consummerNo : consummerNo.consummer_no;
                                    consummerNo.fk_location = !string.IsNullOrEmpty(model.fk_location) ? int.Parse(model.fk_location) : consummerNo.fk_location;
                                    consummerNo.is_active = !string.IsNullOrEmpty(model.is_active) ? int.Parse(model.is_active) : consummerNo.is_active;
                                    db.SaveChanges();
                                    toReturn = new ConsummerPoolResponseModel()
                                    {
                                        remarks = "Success",
                                        resultCode = "1100"
                                    };
                                }
                }
                else
                {
                    toReturn = new ConsummerPoolResponseModel()
                    {
                        remarks = "Please Select Consummer No",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new ConsummerPoolResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public ConsummerPoolResponseModel DeleteConsummerNo(ConsummerPoolRequestModel model)
        {
            ConsummerPoolResponseModel toReturn = new ConsummerPoolResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.consummerNoId))
                {
                    int consummerNoId = int.Parse(model.consummerNoId);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var consummerNo = db.tbl_consummer_pool.Find(consummerNoId);
                        db.tbl_consummer_pool.Remove(consummerNo);
                        db.SaveChanges();
                        toReturn = new ConsummerPoolResponseModel()
                        {
                            remarks = "Success",
                            resultCode = "1100"
                        };
                    }
                }
                else
                {
                    toReturn = new ConsummerPoolResponseModel()
                    {
                        remarks = "Please Select Consummer No",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new ConsummerPoolResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public List<ConsummerPoolResponseModel> GetConsummerPoolByLocation(ConsummerPoolRequestModel model)
        {
            List<ConsummerPoolResponseModel> toReturn = new List<ConsummerPoolResponseModel>();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.fk_location))
                {
                    int locationId = int.Parse(model.fk_location);
                    using (var db = new db_bmsEntities())
                    {
                        var consummerPool = (from x in db.tbl_consummer_pool
                                             join y in db.tbl_location on x.fk_location equals y.location_id
                                             join z in db.tbl_subarea on y.fk_subarea equals z.subarea_id
                                             join a in db.tbl_area on z.fk_area equals a.area_id
                                             join r in db.tbl_residents on x.consummer_no equals r.resident_consumer_no into residents
                                             from r in residents.DefaultIfEmpty()
                                             where x.fk_location == locationId
                                             select new
                                             {
                                                 x.consummer_no,
                                                 x.id,
                                                 x.is_active,
                                                 y.location_id,
                                                 y.location_name,
                                                 z.subarea_id,
                                                 z.subarea_name,
                                                 a.area_id,
                                                 a.area_name,
                                                 status = r != null ? 1 : 0,
                                             }).ToList();
                        var availableConsummerPool = consummerPool.Where(x => x.status == 0).Select(x => new { x.consummer_no, x.id }).ToList();
                        if (consummerPool.Count() > 0)
                        {
                            toReturn = availableConsummerPool.Select(x => new ConsummerPoolResponseModel()
                            {
                                consummerNo= x.consummer_no,
                                consummerId=x.id.ToString(),
                                remarks = "Success",
                                resultCode = "1100"
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(new ConsummerPoolResponseModel()
                            {
                                remarks = "No Record",
                                resultCode = "1200"
                            });
                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                toReturn.Add(new ConsummerPoolResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
    }
}