using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.DataHelpers
{
    public class SubAreaHelpers
    {
        public SubAreaResponseModel AddSubArea(SubAreaRequestModel model)
        {
            SubAreaResponseModel toReturn = new SubAreaResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.fk_area))
                {
                    int fk_area = int.Parse(model.fk_area);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        if (!string.IsNullOrEmpty(model.subAreaName))
                        {
                            var existingArea = (from x in db.tbl_subarea where x.subarea_name == model.subAreaName select x).FirstOrDefault();
                            if (existingArea == null)
                            {
                                var area = new tbl_subarea()
                                {
                                    fk_area = fk_area,
                                    subarea_name = model.subAreaName
                                };
                                db.tbl_subarea.Add(area);
                                db.SaveChanges();
                                toReturn = new SubAreaResponseModel()
                                {
                                    remarks = "Sub Area Added SuccessFully",
                                    resultCode = "1100"
                                };
                            }
                            else
                            {
                                toReturn = new SubAreaResponseModel()
                                {
                                    remarks = "Sub Area Name Already Exists",
                                    resultCode = "1400"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new SubAreaResponseModel()
                            {
                                remarks = "Please Provide Sub Area Name",
                                resultCode = "1300"
                            };
                        }
                    }
                }
                else
                {
                    toReturn = new SubAreaResponseModel()
                    {
                        remarks = "Please Provide Area",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new SubAreaResponseModel()
                {
                    remarks = "There Was A Fatal Error" + Ex.ToString(),
                    resultCode = "1000"
                };
            };
            return toReturn;
        }
        public SubAreaResponseModel EditSubArea(SubAreaRequestModel model)
        {
            SubAreaResponseModel toReturn = new SubAreaResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.subAreaId))
                    {
                        int subAreaId = int.Parse(model.subAreaId);
                        var subArea = (from x in db.tbl_subarea where x.subarea_id == subAreaId select x).FirstOrDefault();
                        if (subArea != null)
                        {
                            var existingSubArea = new tbl_subarea();
                            if (!string.IsNullOrEmpty(model.subAreaName))
                            {
                                existingSubArea = (from x in db.tbl_subarea where x.subarea_name == model.subAreaName && x.subarea_id != subAreaId select x).FirstOrDefault();
                            }
                            else
                            {
                                existingSubArea = null;
                            }
                            if (existingSubArea == null)
                            {
                                subArea.subarea_name = !string.IsNullOrEmpty(model.subAreaName) ? model.subAreaName : subArea.subarea_name;
                                subArea.fk_area = !string.IsNullOrEmpty(model.fk_area) ? int.Parse(model.fk_area) : subArea.fk_area;
                                db.SaveChanges();
                                toReturn = new SubAreaResponseModel()
                                {
                                    remarks = "Sub Area Updated SuccessFully",
                                    resultCode = "1100"
                                };
                            }
                            else
                            {
                                toReturn = new SubAreaResponseModel()
                                {
                                    remarks = "Sub Area Name Already Exists",
                                    resultCode = "1400"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new SubAreaResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new SubAreaResponseModel()
                        {
                            remarks = "Please Select Sub Area",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new SubAreaResponseModel()
                {
                    remarks = "There Was A Fatal Error" + Ex.ToString(),
                    resultCode = "1000"
                };
            };
            return toReturn;
        }
        public SubAreaResponseModel DeleteSubArea(SubAreaRequestModel model)
        {
            SubAreaResponseModel toReturn = new SubAreaResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.subAreaId))
                    {
                        int subAreaId = int.Parse(model.subAreaId);
                        var subArea = (from x in db.tbl_subarea where x.subarea_id == subAreaId select x).FirstOrDefault();
                        if (subArea != null)
                        {
                            db.tbl_subarea.Remove(subArea);
                            db.SaveChanges();
                            toReturn = new SubAreaResponseModel()
                            {
                                remarks = "Sub Area Deleted SuccessFully",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new SubAreaResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new SubAreaResponseModel()
                        {
                            remarks = "Please Select Sub Area",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new SubAreaResponseModel()
                {
                    remarks = "There Was A Fatal Error" + Ex.ToString(),
                    resultCode = "1000"
                };
            };
            return toReturn;
        }
        public SubAreaResponseModel GetSubAreaById(SubAreaRequestModel model)
        {
            SubAreaResponseModel toReturn = new SubAreaResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.subAreaId))
                    {
                        int subAreaId = int.Parse(model.subAreaId);
                        var subArea = (from x in db.tbl_subarea
                                       join y in db.tbl_area on x.fk_area equals y.area_id
                                    where x.subarea_id == subAreaId
                                    select new 
                                    {
                                        x.fk_area,
                                        x.subarea_id,
                                        x.subarea_name,
                                        y.area_name
                                    }
                                    ).FirstOrDefault();
                        if (subArea != null)
                        {
                            toReturn = new SubAreaResponseModel()
                            {
                                subAreaId = subArea.subarea_id.ToString(),
                                subAreaName = !string.IsNullOrEmpty(subArea.subarea_name) ? subArea.subarea_name : "",
                                fk_area =  subArea.fk_area.ToString(),
                                areaName = subArea.area_name,
                                remarks = "Sub Area Found SuccessFully",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new SubAreaResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new SubAreaResponseModel()
                        {
                            remarks = "Please Select Sub Area",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new SubAreaResponseModel()
                {
                    remarks = "There Was A Fatal Error" + Ex.ToString(),
                    resultCode = "1000"
                };
            };
            return toReturn;
        }
        public List<SubAreaResponseModel> GetAllSubAreas()
        {
            List<SubAreaResponseModel> toReturn = new List<SubAreaResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var subAreas = (from x in db.tbl_subarea 
                                    join y in db.tbl_area on x.fk_area equals y.area_id
                                    select new 
                                    {
                                        x.fk_area,
                                        x.subarea_id,
                                        x.subarea_name,
                                        y.area_name
                                    }).ToList();
                    if (subAreas.Count() > 0)
                    {
                        toReturn = subAreas.Select(subArea => new SubAreaResponseModel()
                        {
                            subAreaId = subArea.subarea_id.ToString(),
                            subAreaName = !string.IsNullOrEmpty(subArea.subarea_name) ? subArea.subarea_name : "",
                            fk_area = subArea.fk_area.ToString(),
                            areaName = subArea.area_name,
                            remarks = "Area Found SuccessFully",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add(new SubAreaResponseModel()
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new SubAreaResponseModel()
                {
                    remarks = "There Was A Fatal Error" + Ex.ToString(),
                    resultCode = "1000"
                });
            };
            return toReturn;
        }
        public List<SubAreaResponseModel> GetAllSubAreasByUser(SubAreaRequestModel model)
        {
            List<SubAreaResponseModel> toReturn = new List<SubAreaResponseModel>();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.userId))
                {
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        int userId = int.Parse(model.userId);
                        var subAreas = (from x in db.tbl_subarea
                                        join y in db.tbl_userareas on x.subarea_id equals y.fk_subarea
                                        join z in db.tbl_area on x.fk_area equals z.area_id
                                        where y.fk_user == userId
                                        select new
                                        {
                                            x.fk_area,
                                            x.subarea_id,
                                            x.subarea_name,
                                            z.area_name
                                        }).ToList();
                        if (subAreas.Count() > 0)
                        {
                            toReturn = subAreas.Select(subArea => new SubAreaResponseModel()
                            {
                                subAreaId = subArea.subarea_id.ToString(),
                                areaName = !string.IsNullOrEmpty(subArea.area_name) ? subArea.area_name : "",
                                subAreaName = subArea.subarea_name,
                                fk_area = subArea.fk_area.ToString(),
                                remarks = "Sub Area Found SuccessFully",
                                resultCode = "1100"
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(new SubAreaResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new SubAreaResponseModel()
                    {
                        remarks = "Please Provide User",
                        resultCode = "1300"
                    });
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new SubAreaResponseModel()
                {
                    remarks = "There Was A Fatal Error" + Ex.ToString(),
                    resultCode = "1000"
                });
            };
            return toReturn;
        }
        public List<SubAreaResponseModel> GetAllSubAreasByArea(SubAreaRequestModel model)
        {
            List<SubAreaResponseModel> toReturn = new List<SubAreaResponseModel>();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.userId))
                {
                    int userId = int.Parse(model.userId);
                    if (new ModelsValidatorHelper().validateint(model.fk_area))
                    {
                        using (db_bmsEntities db = new db_bmsEntities())
                        {
                            int fk_area = int.Parse(model.fk_area);
                            var subAreas = (from x in db.tbl_subarea
                                            join y in db.tbl_userareas on x.subarea_id equals y.fk_subarea
                                            join z in db.tbl_area on x.fk_area equals z.area_id
                                            where z.area_id == fk_area && y.fk_user == userId
                                            select new
                                        {
                                            x.fk_area,
                                            x.subarea_id,
                                            x.subarea_name,
                                            z.area_name
                                        }).ToList();
                            if (subAreas.Count() > 0)
                            {
                                toReturn = subAreas.Select(subArea => new SubAreaResponseModel()
                                {
                                    subAreaId = subArea.subarea_id.ToString(),
                                    areaName = !string.IsNullOrEmpty(subArea.area_name) ? subArea.area_name : "",
                                    subAreaName = subArea.subarea_name,
                                    fk_area = subArea.fk_area.ToString(),
                                    remarks = "Sub Area Found SuccessFully",
                                    resultCode = "1100"
                                }).ToList();
                            }
                            else
                            {
                                toReturn.Add(new SubAreaResponseModel()
                                {
                                    remarks = "No Record Found",
                                    resultCode = "1200"
                                });
                            }
                        }
                    }
                    else
                    {
                        toReturn.Add(new SubAreaResponseModel()
                        {
                            remarks = "Please Provide User",
                            resultCode = "1300"
                        });
                    } 
                }
                else
                {
                    toReturn.Add(new SubAreaResponseModel()
                    {
                        resultCode = "1300",
                        remarks = "Please Provide UserId"
                    });
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new SubAreaResponseModel()
                {
                    remarks = "There Was A Fatal Error" + Ex.ToString(),
                    resultCode = "1000"
                });
            };
            return toReturn;
        }
    }
}