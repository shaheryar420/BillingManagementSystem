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
                            area.area_name = !string.IsNullOrEmpty(model.areaName) ? model.areaName: area.area_name;
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
                        toReturn = areas.Select(area => new AreaResponseModel()
                        {
                            areaId = area.area_id.ToString(),
                            areaName = !string.IsNullOrEmpty(area.area_name) ? area.area_name : "",
                            remarks = "Area Found SuccessFully",
                            resultCode = "1100"
                        }).ToList();
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