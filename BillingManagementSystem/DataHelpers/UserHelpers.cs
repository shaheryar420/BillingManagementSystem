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
                            user.fk_usertype = !string.IsNullOrEmpty(model.fk_userType)?int.Parse(model.fk_userType):user.fk_usertype;
                            user.users_fullname = !string.IsNullOrEmpty(model.usersFullName)?model.usersFullName : user.users_fullname;
                            user.users_username = !string.IsNullOrEmpty(model.usersUsername)?model.usersUsername:user.users_username;
                            user.users_password = !string.IsNullOrEmpty(model.usersPassword)?model.usersPassword:user.users_password;
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
    }
}