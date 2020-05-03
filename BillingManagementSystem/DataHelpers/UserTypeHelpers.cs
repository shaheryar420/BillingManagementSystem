using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.DataHelpers
{
    public class UserTypeHelpers
    {
        public UserTypeResponseModel AddUserType(UserTypeRequestModel model)
        {
            UserTypeResponseModel toReturn = new UserTypeResponseModel();
            try
            {
                using(db_bmsEntities db = new db_bmsEntities())
                {
                    if (String.IsNullOrEmpty(model.userTypeName))
                    {
                        var newUserType = new tbl_usertype()
                        {
                            usertype_name = model.userTypeName
                        };
                        db.tbl_usertype.Add(newUserType);
                        db.SaveChanges();
                        toReturn = new UserTypeResponseModel()
                        {
                            remarks = "Successfully Added",
                            resultCode = "1100"
                        };
                    }
                    else
                    {
                        toReturn = new UserTypeResponseModel()
                        {
                            resultCode = "1300",
                            remarks = "Please Enter Name"
                        };
                    }
                }
            }
            catch(Exception Ex)
            {
                toReturn = new UserTypeResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public UserTypeResponseModel EditUserType(UserTypeRequestModel model)
        {
            UserTypeResponseModel toReturn = new UserTypeResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.userTypeId))
                    {
                        var userTypeId = int.Parse(model.userTypeId);
                        var userType = (from x in db.tbl_usertype where x.usertype_id == userTypeId select x).FirstOrDefault();
                        if(userType!= null)
                        {
                            userType.usertype_name = String.IsNullOrEmpty(model.userTypeName) ? model.userTypeName : userType.usertype_name;
                            db.SaveChanges();
                            toReturn = new UserTypeResponseModel()
                            {
                                remarks = "Successfully Updated",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new UserTypeResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new UserTypeResponseModel()
                        {
                            resultCode = "1300",
                            remarks = "Please Provide User Type"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new UserTypeResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public UserTypeResponseModel DeleteUserType(UserTypeRequestModel model)
        {
            UserTypeResponseModel toReturn = new UserTypeResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.userTypeId))
                    {
                        var userTypeId = int.Parse(model.userTypeId);
                        var userType = (from x in db.tbl_usertype where x.usertype_id == userTypeId select x).FirstOrDefault();
                        if (userType != null)
                        {
                            db.tbl_usertype.Remove(userType);
                            db.SaveChanges();
                            toReturn = new UserTypeResponseModel()
                            {
                                remarks = "Successfully Deleted",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new UserTypeResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new UserTypeResponseModel()
                        {
                            resultCode = "1300",
                            remarks = "Please Provide User Type"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new UserTypeResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public UserTypeResponseModel GetUserTypeById(UserTypeRequestModel model)
        {
            UserTypeResponseModel toReturn = new UserTypeResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.userTypeId))
                    {
                        var userTypeId = int.Parse(model.userTypeId);
                        var userType = (from x in db.tbl_usertype where x.usertype_id == userTypeId select x).FirstOrDefault();
                        if (userType != null)
                        {  
                            toReturn = new UserTypeResponseModel()
                            {
                                userTypeName = userType.usertype_name,
                                userTypeId= userType.usertype_id.ToString(),
                                remarks = "Successfully Found",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new UserTypeResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new UserTypeResponseModel()
                        {
                            resultCode = "1300",
                            remarks = "Please Provide User Type"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new UserTypeResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public List<UserTypeResponseModel> GetAllUserTypes(UserTypeRequestModel model)
        {
            List<UserTypeResponseModel> toReturn = new List<UserTypeResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var userTypeId = int.Parse(model.userTypeId);
                    var userTypes = (from x in db.tbl_usertype select x).ToList();
                    if (userTypes.Count()>0)
                    {
                        toReturn = userTypes.Select(userType => new UserTypeResponseModel()
                        {
                            userTypeName = userType.usertype_name,
                            userTypeId = userType.usertype_id.ToString(),
                            remarks = "Successfully Found",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add(new UserTypeResponseModel()
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add( new UserTypeResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
    }
}