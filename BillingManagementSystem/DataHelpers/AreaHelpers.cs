using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.DataHelpers
{
    public class AreaHelpers
    {
        public AreaResponseModel AddArea(AreaRequestModel model)
        {
            AreaResponseModel toReturn = new AreaResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (!string.IsNullOrEmpty(model.areaName))
                    {
                        var existingArea = (from x in db.tbl_area where x.area_name == model.areaName select x).FirstOrDefault();
                        if (existingArea == null)
                        {
                            var area = new tbl_area()
                            {
                                area_name = model.areaName
                            };
                            db.tbl_area.Add(area);
                            db.SaveChanges();
                            toReturn = new AreaResponseModel()
                            {
                                remarks = "Area Added SuccessFully",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new AreaResponseModel()
                            {
                                remarks = "Area Name Already Exists",
                                resultCode = "1400"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new AreaResponseModel()
                        {
                            remarks = "Please Provide Area Name",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new AreaResponseModel()
                {
                    remarks = "There Was A Fatal Error"+Ex.ToString(),
                    resultCode = "1000"
                };
            };
            return toReturn;
        }
        public AreaResponseModel EditArea(AreaRequestModel model)
        {
            AreaResponseModel toReturn = new AreaResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.areaId))
                    {
                        int areaId = int.Parse(model.areaId);
                        var area = (from x in db.tbl_area where x.area_id == areaId select x).FirstOrDefault();
                        if (area != null)
                        {
                            var existingArea = new tbl_area();
                            if(!string.IsNullOrEmpty(model.areaName))
                            {
                                existingArea = (from x in db.tbl_area where x.area_name == model.areaName && x.area_id!= areaId select x).FirstOrDefault(); 
                            }
                            else
                            {
                                existingArea = null;
                            }
                            if (existingArea == null)
                            {
                                area.area_name = !string.IsNullOrEmpty(model.areaName) ? model.areaName : area.area_name;
                                db.SaveChanges();
                                toReturn = new AreaResponseModel()
                                {
                                    remarks = "Area Updated SuccessFully",
                                    resultCode = "1100"
                                };
                            }
                            else
                            {
                                toReturn = new AreaResponseModel()
                                {
                                    remarks = "Area Name Already Exists",
                                    resultCode = "1400"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new AreaResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new AreaResponseModel()
                        {
                            remarks = "Please Select Area",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new AreaResponseModel()
                {
                    remarks = "There Was A Fatal Error"+Ex.ToString(),
                    resultCode = "1000"
                };
            };
            return toReturn;
        }
        public AreaResponseModel DeleteArea(AreaRequestModel model)
        {
            AreaResponseModel toReturn = new AreaResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.areaId))
                    {
                        int areaId = int.Parse(model.areaId);
                        var area = (from x in db.tbl_area where x.area_id == areaId select x).FirstOrDefault();
                        if (area != null)
                        {
                            db.tbl_area.Remove(area);
                            db.SaveChanges();
                            toReturn = new AreaResponseModel()
                            {
                                remarks = "Area Deleted SuccessFully",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new AreaResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new AreaResponseModel()
                        {
                            remarks = "Please Select Area",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new AreaResponseModel()
                {
                    remarks = "There Was A Fatal Error" + Ex.ToString(),
                    resultCode = "1000"
                };
            };
            return toReturn;
        }
        public AreaResponseModel GetAreaById(AreaRequestModel model)
        {
            AreaResponseModel toReturn = new AreaResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.areaId))
                    {
                        int areaId = int.Parse(model.areaId);
                        var area = (from x in db.tbl_area
                                    where x.area_id == areaId
                                    select x
                                    ).FirstOrDefault();
                        if (area != null)
                        {
                            toReturn = new AreaResponseModel()
                            {
                                areaId = area.area_id.ToString(),
                                areaName = !string.IsNullOrEmpty(area.area_name)?area.area_name:"",
                                remarks = "Area Found SuccessFully",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new AreaResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new AreaResponseModel()
                        {
                            remarks = "Please Select Area",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new AreaResponseModel()
                {
                    remarks = "There Was A Fatal Error"+ Ex.ToString(),
                    resultCode = "1000"
                };
            };
            return toReturn;
        }
        public List<AreaResponseModel> GetAllAreas(AreaRequestModel model)
        {
            List<AreaResponseModel> toReturn = new List<AreaResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var areas = (from x in db.tbl_area select x).ToList();
                    if (areas.Count() > 0)
                    {
                        foreach(var area in areas)
                        {
                            var Consumers = (from x in db.tbl_area
                                             join y in db.tbl_subarea on x.area_id equals y.fk_area
                                             join z in db.tbl_location on y.subarea_id equals z.fk_subarea
                                             join r in db.tbl_residentbuilding on z.location_id equals r.fk_building
                                             join re in db.tbl_residents on r.fk_resident equals re.resident_id
                                             where x.area_id == area.area_id
                                             select new
                                             {
                                                 re.resident_id
                                             }
                                          ).ToList();
                            var noOfConsumers = Consumers.Count();
                            double totalUnits = 0;
                            double totalAmount = 0;
                            foreach (var Consumer in Consumers)
                            {
                                var Units = (from x in db.tbl_billelectric
                                             where x.fk_resident == Consumer.resident_id
                                             select x).FirstOrDefault();
                                if (Units != null)
                                {
                                    totalAmount = totalAmount + Units.billelectric_amount;
                                    totalUnits = totalUnits + Units.billelectric_units;
                                }
                            }
                            var _area = new AreaResponseModel()
                            {
                                totalAmount = totalAmount.ToString(),
                                areaId = area.area_id.ToString(),
                                totalUnits= totalUnits.ToString(),
                                areaName = !string.IsNullOrEmpty(area.area_name) ? area.area_name : "",
                                noOfConsumers = noOfConsumers.ToString(),
                                remarks = "Area Found SuccessFully",
                                resultCode = "1100"
                            };
                            toReturn.Add(_area);
                        }
                    }
                    else
                    {
                        toReturn.Add(new AreaResponseModel()
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new AreaResponseModel()
                {
                    remarks = "There Was A Fatal Error"+Ex.ToString(),
                    resultCode = "1000"
                });
            };
            return toReturn;
        }
        
    }
}