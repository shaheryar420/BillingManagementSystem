using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.DataHelpers
{
    public class UserAreaHelpers
    {
        public List<UserSubAreasResponseModel> GetAssignedSubAreasByUser(UserSubAreasRequestModel model)
        {
            List<UserSubAreasResponseModel> toReturn = new List<UserSubAreasResponseModel>();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.fk_user))
                {
                    int userId = int.Parse(model.fk_user);
                    using(db_bmsEntities db =  new db_bmsEntities())
                    {
                        var userAreas = (from x in db.tbl_userareas
                                         join y in db.tbl_users on x.fk_user equals y.users_id
                                         join z in db.tbl_subarea on x.fk_subarea equals z.fk_area
                                         select new
                                         {
                                             x.userareas_id,
                                             x.fk_subarea,
                                             x.fk_user,
                                             y.users_fullname,
                                             y.users_username,
                                             z.subarea_name,
                                         }).ToList();
                        if (userAreas.Count() > 0)
                        {
                            toReturn = userAreas.Select(userArea => new UserSubAreasResponseModel()
                            {
                                userAreasId = userArea.userareas_id.ToString(),
                                fk_subarea = userArea.fk_subarea.ToString(),
                                fk_user = userArea.fk_user.ToString(),
                                userUserName = userArea.users_username,
                                userName = userArea.users_fullname,
                                subAreaName = userArea.subarea_name,
                                remarks = "Successfully Found",
                                resultCode = "1100"
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(new UserSubAreasResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode="1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add( new UserSubAreasResponseModel()
                    {
                        remarks = "Please Provide User",
                        resultCode = "1300"
                    });
                }
            }
            catch(Exception Ex)
            {
                toReturn.Add( new UserSubAreasResponseModel()
                {
                    remarks = "There Was A Fatal Error "+Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
    }
}