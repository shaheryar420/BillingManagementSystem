using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;


namespace BillingManagementSystem.DataHelpers
{
    public class FixedRatesHelpers
    {
        public FixedRatesResponseModel UpdateFixedRates(FixedRatesRequestModel model)
        {
            FixedRatesResponseModel toReturn = new FixedRatesResponseModel();
            try
            {
                if(new ModelsValidatorHelper().validateint(model.userId))
                {
                    if (new ModelsValidatorHelper().validateint(model.fixedRatesId))
                    {
                        int fixedRateId = int.Parse(model.fixedRatesId);
                        using (db_bmsEntities db = new db_bmsEntities())
                        {
                            var fixedRate = (from x in db.tbl_fixedrates where x.fixedrates_id == fixedRateId select x).FirstOrDefault();
                            if(fixedRate != null)
                            {
                                fixedRate.fixedrates_amount = !string.IsNullOrEmpty(model.fixedRatesAmount) ? double.Parse(model.fixedRatesAmount) : fixedRate.fixedrates_amount;
                                fixedRate.fixedrates_unit = !string.IsNullOrEmpty(model.fixedRatesUnit) ? int.Parse(model.fixedRatesUnit) : fixedRate.fixedrates_unit;
                                fixedRate.fixedrates_status = !string.IsNullOrEmpty(model.fixedRateStatus) ? int.Parse(model.fixedRateStatus) : fixedRate.fixedrates_status;
                                db.SaveChanges();
                                toReturn.remarks = " Successfully Updated";
                                toReturn.resultCode = "1100";
                            }
                            else
                            {
                                toReturn = new FixedRatesResponseModel()
                                {
                                    resultCode="1200",
                                    remarks = "No Record Found"
                                };
                            }
                        }
                    }
                    else
                    {
                        toReturn = new FixedRatesResponseModel()
                        {
                            remarks = "Please Select Fixed Rate",
                            resultCode = "1300"
                        };
                    }
                }
                else
                {
                    toReturn = new FixedRatesResponseModel()
                    {
                        remarks = " Please Provide User",
                        resultCode ="1300"
                    };
                }
            }
            catch(Exception Ex)
            {
                toReturn = new FixedRatesResponseModel()
                {
                    remarks = "There was a Fatal Error"+ Ex.ToString(),
                    resultCode = "1000"
                };   
            }
            return toReturn;
        }
        public FixedRatesResponseModel GetFixedRatesByName(FixedRatesRequestModel model)
        {
            FixedRatesResponseModel toReturn = new FixedRatesResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.userId))
                {
                    if (!string.IsNullOrEmpty(model.fixedRatesName))
                    {
                        using (db_bmsEntities db = new db_bmsEntities())
                        {
                            var fixedRate = (from x in db.tbl_fixedrates where x.fixedrates_name == model.fixedRatesName select x).FirstOrDefault();
                            if (fixedRate != null)
                            {
                                toReturn = new FixedRatesResponseModel()
                                {
                                    fixedRatesAmount = fixedRate.fixedrates_amount.ToString(),
                                    fixedRatesId = fixedRate.fixedrates_id.ToString(),
                                    fixedRatesName = fixedRate.fixedrates_name,
                                    fixedRatesUnit = fixedRate.fixedrates_unit!=null?fixedRate.fixedrates_unit.Value.ToString():"",
                                    remarks = " Successfully Updated",
                                    resultCode = "1100",
                                };
                                db.SaveChanges();
                              
                            }
                            else
                            {
                                toReturn = new FixedRatesResponseModel()
                                {
                                    resultCode = "1200",
                                    remarks = "No Record Found"
                                };
                            }
                        }
                    }
                    else
                    {
                        toReturn = new FixedRatesResponseModel()
                        {
                            remarks = "Please Select Fixed Rate",
                            resultCode = "1300"
                        };
                    }
                }
                else
                {
                    toReturn = new FixedRatesResponseModel()
                    {
                        remarks = " Please Provide User",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new FixedRatesResponseModel()
                {
                    remarks = "There was a Fatal Error" + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public List<FixedRatesResponseModel> GetListOfFixedRates()
        {
            List<FixedRatesResponseModel> toReturn = new List<FixedRatesResponseModel>();
            try
            {
                using(db_bmsEntities db = new db_bmsEntities())
                {
                    var fixedRates = db.tbl_fixedrates.ToList();
                    if (fixedRates.Count() > 0)
                    {
                        toReturn = fixedRates.Select(x => new FixedRatesResponseModel()
                        {
                            fixedRatesAmount = x.fixedrates_amount.ToString(),
                            fixedRatesId = x.fixedrates_id.ToString(),
                            fixedRatesName = x.fixedrates_name,
                            fixedRatesStatus = x.fixedrates_status.ToString(),
                            fixedRatesUnit = x.fixedrates_unit!=null?x.fixedrates_unit.ToString():"Not Neccessory",
                            remarks = "Success",
                            resultCode = "1100",

                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add(new FixedRatesResponseModel()
                        {
                            resultCode = "1200",
                            remarks = "No Record Found"
                        });
                    }
                    
                }
            }
            catch(Exception Ex)
            {
                toReturn.Add( new FixedRatesResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
    }
}