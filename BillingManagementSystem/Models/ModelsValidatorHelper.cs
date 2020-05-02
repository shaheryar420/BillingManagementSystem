using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.Models
{
    public class ModelsValidatorHelper
    {
        public bool validateint(string x)
        {
            if (!string.IsNullOrEmpty(x))
            {
                bool isvalid = x.All(char.IsDigit);
                return isvalid;
            }
            else
            {
                return false;
            }
        }
    }
}