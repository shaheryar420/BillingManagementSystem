using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.DataHelpers
{
    public class UserPermissionHelpers
    {
        public List<UserPermissionResponseModel> GetPermissionsByUser(UserPermissionRequestModel model)
        {
            List<UserPermissionResponseModel> toReturn = new List<UserPermissionResponseModel>();
            try
            {
                using(db_bmsEntities db = new db_bmsEntities())
                {
                    if(new ModelsValidatorHelper().validateint(model.fk_user))
                    {
                        int userId = int.Parse(model.fk_user);
                        var userPermissions = (from x in db.tbl_userpermissions where x.fk_user == userId select x).ToList();
                        if (userPermissions.Count() > 0)
                        {
                            toReturn = userPermissions.Select(userPermission => new UserPermissionResponseModel()
                            {
                                fk_action = userPermission.fk_action.ToString(),
                                fk_user = userPermission.fk_user.ToString(),
                                userpermissions_action = userPermission.userpermissions_action,
                                userpermissions_controller = userPermission.userpermissions_controller,
                                userpermissions_id = userPermission.userpermissions_id.ToString(),
                                remarks = "Successfully Found",
                                resultCode = "1100"
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(  new UserPermissionResponseModel()
                            {
                                remarks = "No Record",
                                resultCode = "1200"
                            });
                        }
                    }
                    else
                    {
                        toReturn .Add(new UserPermissionResponseModel()
                        {
                            remarks = "Please Provide User",
                            resultCode = "1300"
                        });
                    }
                }
            }
            catch(Exception Ex)
            {
                toReturn.Add( new UserPermissionResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public List<ControllerResponseModel> GetAllControllers()
        {
            List<ControllerResponseModel> toReturn = new List<ControllerResponseModel>();
            try
            {
                using(db_bmsEntities db = new db_bmsEntities())
                {
                    var Controllers = (from x in db.tbl_controller select x).ToList();
                    if (Controllers.Count() > 0) 
                    {
                        toReturn = Controllers.Select(Controller => new ControllerResponseModel()
                        {
                            controllerDescription = Controller.controller_description,
                            controllerId = Controller.controller_id.ToString(),
                            controllerName = Controller.controller_name,
                            remarks= "Succesfully Found",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add(new ControllerResponseModel
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch(Exception Ex)
            {
                toReturn.Add( new ControllerResponseModel()
                {
                    remarks ="There Was A Fatal Error "+Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public List<ActionResponseModel> GetAllActions()
        {
            List<ActionResponseModel> toReturn = new List<ActionResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var actions = (from x in db.tbl_action select x).ToList();
                    if (actions.Count() > 0)
                    {
                        toReturn = actions.Select(action => new ActionResponseModel()
                        {
                            actionDescription = action.action_description,
                            actionId = action.action_id.ToString(),
                            actionName = action.action_name,
                            remarks = "Succesfully Found",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add(new ActionResponseModel
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new ActionResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
    }
}