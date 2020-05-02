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
                                fixedRate.fixedrates_amount = String.IsNullOrEmpty(model.fixedRatesAmount) ? double.Parse(model.fixedRatesAmount) : fixedRate.fixedrates_amount;
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
    }
}