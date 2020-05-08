using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;
	

namespace BillingManagementSystem.DataHelpers
{
    public class LoginHelpers
    {
       public LoginResponseModel LoginUser(String userName, String password)
        {
            LoginResponseModel toReturn = new LoginResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        using (db_bmsEntities db = new db_bmsEntities())
                        {
                            var loginUser = (from x in db.tbl_users where x.users_username == userName select x).FirstOrDefault();
                            if(loginUser!= null)
                            {
                                if(loginUser.users_password == password)
                                {
                                    var userType = (from x in db.tbl_usertype where x.usertype_id == loginUser.fk_usertype select x).FirstOrDefault();
                                    toReturn = new LoginResponseModel()
                                    {
                                        userFullName = loginUser.users_fullname,
                                        userId = loginUser.users_id.ToString(),
                                        userName =loginUser.users_username,
                                        userType = userType.usertype_name,
                                        userTypeId = userType.usertype_id.ToString(),
                                        resultCode ="1100",
                                        remarks ="Successfully Logged In"

                                    };
                                }
                                else
                                {
                                    toReturn = new LoginResponseModel()
                                    {
                                        remarks = "Password Incorrect",
                                        resultCode = "1500"
                                    };
                                }
                            }
                            else
                            {
                                toReturn = new LoginResponseModel()
                                {
                                    resultCode = "1200",
                                    remarks = "User Not Found"
                                };
                            }
                        }
                    }
                    else
                    {
                        toReturn = new LoginResponseModel()
                        {
                            remarks = "Please Provide Password",
                            resultCode = "1300"
                        };
                    }
                }
                else
                {
                    toReturn = new LoginResponseModel()
                    {
                        remarks = "Please Provide Username",
                        resultCode = "1300"
                    };
                }
            }
            catch(Exception Ex)
            {
                toReturn = new LoginResponseModel()
                {
                    remarks = "There was a Fatal Error "+ Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
    }
}