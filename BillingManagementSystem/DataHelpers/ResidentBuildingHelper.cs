using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.DataHelpers
{
    public class ResidentBuildingHelper
    {
        public ResidentBuildingResponseModel EditResidentBuilding(ResidentBuildingRequestModel model)
        {
            ResidentBuildingResponseModel toReturn = new ResidentBuildingResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if(new ModelsValidatorHelper().validateint(model.fk_building))
                    {
                        if (new ModelsValidatorHelper().validateint(model.fk_resident))
                        {
                            int residentId = int.Parse(model.fk_resident);
                            var ResidentBuliding = (from x in db.tbl_residentbuilding where x.fk_resident == residentId select x).FirstOrDefault();
                            ResidentBuliding.fk_building = int.Parse(model.fk_building);
                            db.SaveChanges();
                            toReturn.remarks = "SuccessFully Updated";
                            toReturn.resultCode = "1100";

                        }
                        else
                        {
                            toReturn = new ResidentBuildingResponseModel()
                            {
                                remarks = "Please Provide Resident",
                                resultCode = "1300"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new ResidentBuildingResponseModel()
                        {
                            remarks = "Please Provide Building",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch(Exception Ex)
            {
                toReturn = new ResidentBuildingResponseModel()
                {
                    remarks = "There was A Fatal Error "+Ex.ToString(),
                    resultCode = " 1000"
                };
            }
            return toReturn;
        }
    }
}