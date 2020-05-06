using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.DataHelpers
{
    public class DashBoardHelpers
    {
        public DashBoardResponseModel DashBoardStats()
        {
            DashBoardResponseModel toReturn = new DashBoardResponseModel();
            try
            {
                using(db_bmsEntities db = new db_bmsEntities())
                {
                    toReturn.totalResidents = (from x in db.tbl_residents select x).Count().ToString();
                    var totalOutstanding = (from x in db.tbl_billelectric select x.billelectric_outstanding).Sum().ToString();
                    toReturn.totalOutstandings = !string.IsNullOrEmpty(totalOutstanding) ? totalOutstanding : "0";
                    var currentMonth= new MonthFinderHelpers().GetCurrentMonth();
                    var monthlyOutstandings = (from x in db.tbl_billelectric where x.billelectric_month == currentMonth select x.billelectric_outstanding).Sum();
                    var monthlyRecovery = (from x in db.tbl_paymenthistory where x.paymentmonth == currentMonth select x.payment_amount).ToList();
                    toReturn.monthlyOutstandings = monthlyOutstandings != null ? monthlyOutstandings.ToString() : "0";
                    if (monthlyRecovery.Count > 0)
                    {
                        toReturn.monthlyRecovery = monthlyRecovery.Sum().ToString();
                    }
                    else
                    {
                        toReturn.monthlyRecovery = "0";
                    }
                    
                    toReturn.remarks = "Stats Initiated Successfully ";
                    toReturn.resultCode = "1100";
                }
            }
            catch(Exception Ex)
            {
                toReturn = new DashBoardResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
    }
}