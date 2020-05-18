using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.DataHelpers
{
    public class UserHelpers
    {
        public UserResponseModel AddUser(UserRequestModel model)
        {
            UserResponseModel toReturn = new UserResponseModel();
            try
            {
                using(db_bmsEntities db = new db_bmsEntities())
                {
                    if (!string.IsNullOrEmpty(model.usersFullName))
                    {
                        if (!string.IsNullOrEmpty(model.usersUsername))
                        {
                            var existingUserName = (from x in db.tbl_users where x.users_username == model.usersUsername select x).FirstOrDefault();
                            if(existingUserName==null)
                            {
                                if (!string.IsNullOrEmpty(model.usersPassword))
                                {
                                    if (new ModelsValidatorHelper().validateint(model.fk_userType))
                                    {
                                        var user = new tbl_users()
                                        {
                                            fk_usertype = int.Parse(model.fk_userType),
                                            users_fullname = model.usersFullName,
                                            users_username = model.usersUsername,
                                            users_password = model.usersPassword,
                                            users_isActive = 1
                                        };
                                        db.tbl_users.Add(user);
                                        db.SaveChanges();
                                        toReturn = new UserResponseModel()
                                        {
                                            remarks = "User Added SuccessFully",
                                            resultCode = "1100"
                                        };
                                    }
                                    else
                                    {
                                        toReturn = new UserResponseModel()
                                        {
                                            remarks = "Please Provide User Type",
                                            resultCode = "1300"
                                        };
                                    }
                                }
                                else
                                {
                                    toReturn = new UserResponseModel()
                                    {
                                        remarks = "Please Provide Password",
                                        resultCode = "1300"
                                    };
                                }
                            }
                            else
                            {
                                toReturn = new UserResponseModel()
                                {
                                    remarks = "Username Already Exists",
                                    resultCode = "1400"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new UserResponseModel()
                            {
                                resultCode = "1300",
                                remarks = "Please Provide User Name"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new UserResponseModel()
                        {
                            remarks = "Please Provide Full Name",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch(Exception Ex)
            {
                toReturn = new UserResponseModel()
                {
                    remarks = "There Was A Fatal Error "+Ex.ToString(),
                    resultCode = "1000"
                };
            };
            return toReturn;
        }
        public UserResponseModel EditUser(UserRequestModel model)
        {
            UserResponseModel toReturn = new UserResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.usersId))
                    {
                        int userId = int.Parse(model.usersId);
                        var user = (from x in db.tbl_users where x.users_id == userId select x).FirstOrDefault(); 
                        if(user!= null)
                        {
                            var existingUserName = new tbl_users();
                            if(!string.IsNullOrEmpty(model.usersUsername))
                            {
                                existingUserName = (from x in db.tbl_users where x.users_username == model.usersUsername && x.users_id!= userId select x).FirstOrDefault();
                            }
                            else
                            {
                                existingUserName = null;
                            }
                            if (existingUserName == null)
                            {
                                user.fk_usertype = !string.IsNullOrEmpty(model.fk_userType) ? int.Parse(model.fk_userType) : user.fk_usertype;
                                user.users_fullname = !string.IsNullOrEmpty(model.usersFullName) ? model.usersFullName : user.users_fullname;
                                user.users_username = !string.IsNullOrEmpty(model.usersUsername) ? model.usersUsername : user.users_username;
                                user.users_password = !string.IsNullOrEmpty(model.usersPassword) ? model.usersPassword : user.users_password;
                                db.SaveChanges();
                                toReturn = new UserResponseModel()
                                {
                                    remarks = "User Updated SuccessFully",
                                    resultCode = "1100"
                                };
                            }
                            else
                            {
                                toReturn = new UserResponseModel()
                                {
                                    remarks = "User Name Already Exists",
                                    resultCode = "1400"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new UserResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new UserResponseModel()
                        {
                            remarks = "Please Select User",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new UserResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            };
            return toReturn;
        }
        public UserResponseModel DeleteUser(UserRequestModel model)
        {
            UserResponseModel toReturn = new UserResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.usersId))
                    {
                        int userId = int.Parse(model.usersId);
                        var user = (from x in db.tbl_users where x.users_id == userId select x).FirstOrDefault();
                        if (user != null)
                        {
                            user.users_isActive = 0;
                            db.SaveChanges();
                            toReturn = new UserResponseModel()
                            {
                                remarks = "User Deleted SuccessFully",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new UserResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new UserResponseModel()
                        {
                            remarks = "Please Select User",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new UserResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            };
            return toReturn;
        }
        public UserResponseModel GetUserById(UserRequestModel model)
        {
            UserResponseModel toReturn = new UserResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.usersId))
                    {
                        int userId = int.Parse(model.usersId);
                        var user = (from x in db.tbl_users
                                    join y in db.tbl_usertype on x.fk_usertype equals y.usertype_id 
                                    where x.users_id == userId 
                                    select new 
                                    {
                                        x.fk_usertype,
                                        x.users_fullname,
                                        x.users_username,
                                        x.users_password,
                                        y.usertype_name
                                    }).FirstOrDefault();
                        if (user != null)
                        {
                            toReturn = new UserResponseModel()
                            {
                                fk_userType = user.fk_usertype.ToString(),
                                userTypeName = !string.IsNullOrEmpty(user.usertype_name)?user.usertype_name:"",
                                usersFullName = !string.IsNullOrEmpty(user.users_fullname) ? user.users_fullname : "",
                                usersUsername = !string.IsNullOrEmpty(user.users_username) ? user.users_username : "",
                                usersPassword = !string.IsNullOrEmpty(user.users_password) ? user.users_password : "",
                                remarks = "User Found SuccessFully",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new UserResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new UserResponseModel()
                        {
                            remarks = "Please Select User",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new UserResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            };
            return toReturn;
        }
        public List<UserResponseModel> GetAllUsers(UserRequestModel model)
        {
            List<UserResponseModel> toReturn = new List<UserResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                { 
                    var users = (from x in db.tbl_users
                                join y in db.tbl_usertype on x.fk_usertype equals y.usertype_id
                                select new
                                {
                                    x.users_id,
                                    x.fk_usertype,
                                    x.users_fullname,
                                    x.users_username,
                                    x.users_password,
                                    y.usertype_name
                                }).ToList();
                    if (users.Count()>0)
                    {
                        toReturn= users.Select(user=>new UserResponseModel()
                        {
                            usersId = user.users_id.ToString(),
                            fk_userType = user.fk_usertype.ToString(),
                            userTypeName = !string.IsNullOrEmpty(user.usertype_name) ? user.usertype_name : "",
                            usersFullName = !string.IsNullOrEmpty(user.users_fullname) ? user.users_fullname : "",
                            usersUsername = !string.IsNullOrEmpty(user.users_username) ? user.users_username : "",
                            usersPassword = !string.IsNullOrEmpty(user.users_password) ? user.users_password : "",
                            remarks = "User Found SuccessFully",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add(new UserResponseModel()
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new UserResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            };
            return toReturn;
        }
        public List<UserResponseModel> GetAllUsersByUserType(UserRequestModel model)
        {
            List<UserResponseModel> toReturn = new List<UserResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.fk_userType))
                    {
                        int userTypeId = int.Parse(model.fk_userType);
                        var users = (from x in db.tbl_users
                                     join y in db.tbl_usertype on x.fk_usertype equals y.usertype_id
                                     where x.fk_usertype == userTypeId
                                     select new
                                     {
                                         x.fk_usertype,
                                         x.users_fullname,
                                         x.users_username,
                                         x.users_password,
                                         y.usertype_name
                                     }).ToList();
                        if (users.Count() > 0)
                        {
                            toReturn = users.Select(user => new UserResponseModel()
                            {
                                fk_userType = user.fk_usertype.ToString(),
                                userTypeName = !string.IsNullOrEmpty(user.usertype_name) ? user.usertype_name : "",
                                usersFullName = !string.IsNullOrEmpty(user.users_fullname) ? user.users_fullname : "",
                                usersUsername = !string.IsNullOrEmpty(user.users_username) ? user.users_username : "",
                                usersPassword = !string.IsNullOrEmpty(user.users_password) ? user.users_password : "",
                                remarks = "User Found SuccessFully",
                                resultCode = "1100"
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(new UserResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            });
                        }
                    }
                    else
                    {
                        toReturn.Add(new UserResponseModel()
                        {
                            remarks = "Please Provide User Type",
                            resultCode = "1300"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new UserResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            };
            return toReturn;
        }
        public List<UserResponseModel> GetUserPermissions(string userId)
        {
            List<UserResponseModel> toReturn = new List<UserResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    int _user = int.Parse(userId);
                    var users = (from x in db.tbl_userpermissions
                                 where x.fk_user == _user
                                 select new
                                 {
                                     x.userpermissions_action,
                                     x.userpermissions_controller
                                 }).ToList();
                    if (users.Count() > 0)
                    {
                        toReturn = users.Select(user => new UserResponseModel()
                        {
                            action = user.userpermissions_action,
                            controller = user.userpermissions_controller,
                            remarks = "Permissions Found Successfully",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add(new UserResponseModel()
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new UserResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            };
            return toReturn;
        }
        public UserSubAreasResponseModel AssignSubAreaToUser(UserSubAreasRequestModel model)
        {
            UserSubAreasResponseModel toReturn = new UserSubAreasResponseModel();
            try
            {
                if(new ModelsValidatorHelper().validateint(model.fk_user))
                {
                    int fkUser = int.Parse(model.fk_user);
                    if(new ModelsValidatorHelper().validateint(model.fk_subarea))
                    {
                        int fkSubArea = int.Parse(model.fk_subarea);
                        using(db_bmsEntities db = new db_bmsEntities())
                        {
                            var newAssignement = new tbl_userareas()
                            {
                                fk_subarea = fkSubArea,
                                fk_user = fkUser
                            };
                            var userAreas = (from x in db.tbl_userareas select x).ToList();
                            if (userAreas.Count() > 0)
                            {
                                var existingUserArea = (from x in userAreas where x.fk_subarea == fkSubArea && x.fk_user == fkUser select x).FirstOrDefault();
                                if (existingUserArea== null)
                                {
                                    db.tbl_userareas.Add(newAssignement);
                                    db.SaveChanges();
                                    toReturn = new UserSubAreasResponseModel()
                                    {
                                        remarks = "Sub Area Successfully Assigned",
                                        resultCode = "1100"
                                    };
                                }
                                else
                                {
                                    toReturn = new UserSubAreasResponseModel()
                                    {
                                        remarks = "Already Assigned",
                                        resultCode = "1400"
                                    };
                                }
                            }
                            else
                            {
                                db.tbl_userareas.Add(newAssignement);
                                db.SaveChanges();
                                toReturn = new UserSubAreasResponseModel()
                                {
                                    remarks = "Sub Area Successfully Assigned",
                                    resultCode = "1100"
                                };
                            }
                           
                        }
                    }
                    else
                    {
                        toReturn = new UserSubAreasResponseModel()
                        {
                            remarks = "Please Provide Sub Areas",
                            resultCode = "1300"
                        };
                    }
                }
                else
                {
                    toReturn = new UserSubAreasResponseModel()
                    {
                        resultCode = "1300",
                        remarks = "Please Provide User"
                    };
                }
            }
            catch(Exception Ex)
            {
                toReturn = new UserSubAreasResponseModel()
                {
                    remarks = "There was a Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public UserPermissionResponseModel AssignPermissionsToUser(UserPermissionRequestModel model)
        {
            UserPermissionResponseModel toReturn = new UserPermissionResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.fk_user))
                {
                    int fkUser = int.Parse(model.fk_user);
                    if (new ModelsValidatorHelper().validateint(model.fk_action))
                    {
                        int fkAction = int.Parse(model.fk_action);
                        using (db_bmsEntities db = new db_bmsEntities())
                        {
                            var newPermission = new tbl_userpermissions()
                            {
                                fk_action = fkAction,
                                fk_user = fkUser,
                                userpermissions_action = model.userpermissions_action,
                                userpermissions_controller = model.userpermissions_controller,
                            };
                            
                            var userPermission = (from x in db.tbl_userpermissions select x).ToList();
                            if (userPermission.Count() > 0)
                            {
                                var existingPermission = (from x in userPermission where x.fk_action == fkAction && x.fk_user == fkUser select x).FirstOrDefault();
                                if (existingPermission == null)
                                {
                                    db.tbl_userpermissions.Add(newPermission);
                                    db.SaveChanges();
                                    toReturn = new UserPermissionResponseModel()
                                    {
                                        remarks = "Permission Successfully Assigned",
                                        resultCode = "1100"
                                    };
                                }
                                else
                                {
                                    toReturn = new UserPermissionResponseModel()
                                    {
                                        remarks = "Already Assigned",
                                        resultCode = "1400"
                                    };
                                }
                            }
                            else
                            {
                                db.tbl_userpermissions.Add(newPermission);
                                db.SaveChanges();
                                toReturn = new UserPermissionResponseModel()
                                {
                                    remarks = "Permission Successfully Assigned",
                                    resultCode = "1100"
                                };
                            }

                        }
                    }
                    else
                    {
                        toReturn = new UserPermissionResponseModel()
                        {
                            remarks = "Please Provide Sub Areas",
                            resultCode = "1300"
                        };
                    }
                }
                else
                {
                    toReturn = new UserPermissionResponseModel()
                    {
                        resultCode = "1300",
                        remarks = "Please Provide User"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new UserPermissionResponseModel()
                {
                    remarks = "There was a Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public UserSubAreasResponseModel RemoveSubAreaFromUser(UserSubAreasRequestModel model)
        {
            UserSubAreasResponseModel toReturn = new UserSubAreasResponseModel();
            try
            {
                if(new ModelsValidatorHelper().validateint(model.userAreasId))
                {
                    int userAreasId = int.Parse(model.userAreasId);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var assignment = (from x in db.tbl_userareas where x.userareas_id == userAreasId select x).FirstOrDefault();
                        if(assignment != null)
                        {
                            db.tbl_userareas.Remove(assignment);
                            db.SaveChanges();
                            toReturn = new UserSubAreasResponseModel()
                            {
                                remarks = "Sub Area is No Longer Assigned",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new UserSubAreasResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                        
                    }
                }
                else
                {
                    toReturn = new UserSubAreasResponseModel()
                    {
                        remarks = "Please Select Assigned Area",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new UserSubAreasResponseModel()
                {
                    remarks = "There was a Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public UserPermissionResponseModel RemovePermissionFromUser(UserPermissionRequestModel model)
        {
            UserPermissionResponseModel toReturn = new UserPermissionResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.userpermissions_id))
                {
                    int userPermissionId = int.Parse(model.userpermissions_id);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var assignment = (from x in db.tbl_userpermissions where x.userpermissions_id == userPermissionId select x).FirstOrDefault();
                        if (assignment != null)
                        {
                            db.tbl_userpermissions.Remove(assignment);
                            db.SaveChanges();
                            toReturn = new UserPermissionResponseModel()
                            {
                                remarks = "User Permission is No Longer Assigned",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new UserPermissionResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }

                    }
                }
                else
                {
                    toReturn = new UserPermissionResponseModel()
                    {
                        remarks = "Please Select Assigned Permission",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new UserPermissionResponseModel()
                {
                    remarks = "There was a Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
    }
}