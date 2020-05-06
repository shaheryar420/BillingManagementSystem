using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.DataHelpers
{
    public class MonthFinderHelpers
    {
        public string GetPreviousMonth(string x)
        {
            string[] monthYear = x.Split('_');
            int month = int.Parse(monthYear[0]);
            int year = int.Parse(monthYear[1]);
            if(month == 1)
            {
                month = 12;
                year = year - 1;
            }
            else
            {
                month = month - 1;
            }
            return month > 9 ? month + "-" + year : "0" + month + "-" + year;
        }
        public string GetCurrentMonth()
        {
            DateTime currentDate = DateTime.UtcNow.AddHours(5);
            int month = currentDate.Month;
            int year = currentDate.Year;
            return month > 9 ? month + "-" + year : "0" + month + "-" + year;
        }
    }
}