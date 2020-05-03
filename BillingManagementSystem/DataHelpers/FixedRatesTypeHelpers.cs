using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.DataHelpers
{
    public class FixedRatesTypeHelpers
    {
        public FixedRatesTypeResponseModel AddFixedRateType(FixedRatesTypeRequestModel model)
        {
            FixedRatesTypeResponseModel toReturn = new FixedRatesTypeResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (!string.IsNullOrEmpty(model.fixedRateTypeName))
                    {
                        var existingFixedRateType = (from x in db.tbl_fixedratetype where x.fixedratetype_name == model.fixedRateTypeName select x).ToList();
                        if(existingFixedRateType == null)
                        {
                            var newfixedRateType = new tbl_fixedratetype()
                            {
                                fixedratetype_name = model.fixedRateTypeName
                            };
                            db.tbl_fixedratetype.Add(newfixedRateType);
                            db.SaveChanges();
                            toReturn = new FixedRatesTypeResponseModel()
                            {
                                remarks = "Successfully Added",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new FixedRatesTypeResponseModel()
                            {
                                remarks = " Type Already Exists",
                                resultCode = "1400"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new FixedRatesTypeResponseModel()
                        {
                            resultCode = "1300",
                            remarks = "Please Enter Name"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new FixedRatesTypeResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public FixedRatesTypeResponseModel EditFixedRateType(FixedRatesTypeRequestModel model)
        {
            FixedRatesTypeResponseModel toReturn = new FixedRatesTypeResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.fixedRateTypeId))
                    {
                        var fixedRateTypeId = int.Parse(model.fixedRateTypeId);
                        var fixedRateType = (from x in db.tbl_fixedratetype where x.fixedratetype_id == fixedRateTypeId select x).FirstOrDefault();
                        if (fixedRateType != null)
                        {
                            fixedRateType.fixedratetype_name = !string.IsNullOrEmpty(model.fixedRateTypeName) ? model.fixedRateTypeName : fixedRateType.fixedratetype_name;
                            db.SaveChanges();
                            toReturn = new FixedRatesTypeResponseModel()
                            {
                                remarks = "Successfully Updated",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new FixedRatesTypeResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new FixedRatesTypeResponseModel()
                        {
                            resultCode = "1300",
                            remarks = "Please Provide fixedRate Type"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new FixedRatesTypeResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public FixedRatesTypeResponseModel DeleteFixedRateType(FixedRatesTypeRequestModel model)
        {
            FixedRatesTypeResponseModel toReturn = new FixedRatesTypeResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.fixedRateTypeId))
                    {
                        var fixedRateTypeId = int.Parse(model.fixedRateTypeId);
                        var fixedRateType = (from x in db.tbl_fixedratetype where x.fixedratetype_id == fixedRateTypeId select x).FirstOrDefault();
                        if (fixedRateType != null)
                        {
                            db.tbl_fixedratetype.Remove(fixedRateType);
                            db.SaveChanges();
                            toReturn = new FixedRatesTypeResponseModel()
                            {
                                remarks = "Successfully Deleted",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new FixedRatesTypeResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new FixedRatesTypeResponseModel()
                        {
                            resultCode = "1300",
                            remarks = "Please Provide fixedRate Type"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new FixedRatesTypeResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public FixedRatesTypeResponseModel GetFixedRateTypeById(FixedRatesTypeRequestModel model)
        {
            FixedRatesTypeResponseModel toReturn = new FixedRatesTypeResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.fixedRateTypeId))
                    {
                        var fixedRateTypeId = int.Parse(model.fixedRateTypeId);
                        var fixedRateType = (from x in db.tbl_fixedratetype where x.fixedratetype_id == fixedRateTypeId select x).FirstOrDefault();
                        if (fixedRateType != null)
                        {
                            toReturn = new FixedRatesTypeResponseModel()
                            {
                                fixedRateTypeName = fixedRateType.fixedratetype_name,
                                fixedRateTypeId = fixedRateType.fixedratetype_id.ToString(),
                                remarks = "Successfully Found",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new FixedRatesTypeResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new FixedRatesTypeResponseModel()
                        {
                            resultCode = "1300",
                            remarks = "Please Provide fixedRate Type"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new FixedRatesTypeResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public List<FixedRatesTypeResponseModel> GetAllFixedRateTypes(FixedRatesTypeRequestModel model)
        {
            List<FixedRatesTypeResponseModel> toReturn = new List<FixedRatesTypeResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var fixedRateTypes = (from x in db.tbl_fixedratetype select x).ToList();
                    if (fixedRateTypes.Count() > 0)
                    {
                        toReturn = fixedRateTypes.Select(fixedRateType => new FixedRatesTypeResponseModel()
                        {
                            fixedRateTypeName = fixedRateType.fixedratetype_name,
                            fixedRateTypeId = fixedRateType.fixedratetype_id.ToString(),
                            remarks = "Successfully Found",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add(new FixedRatesTypeResponseModel()
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new FixedRatesTypeResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
    }
}