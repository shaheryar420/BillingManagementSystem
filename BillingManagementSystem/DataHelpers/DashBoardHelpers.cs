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
                    var currentMonth = new MonthFinderHelpers().GetCurrentMonth();
                    toReturn.totalResidents = (from x in db.tbl_residents where x.resident_status==0 select x).Count().ToString();
                    var billElectric = (from x in db.tbl_billelectric select x).Count();
                    var billgas = (from x in db.tbl_billgas select x).Count();
                    var payElectric = (from x in db.tbl_paymenthistory where x.paymentmonth == currentMonth select x).Count();
                    var payGas = (from x in db.tbl_paymentgashistory where x.paymentmonth == currentMonth select x).Count();
                    toReturn.totalOutstandings = db.tbl_outstanding.Sum(x => x.outstanding_amount).ToString();
                    var outstanding = db.tbl_outstanding.Where(x => x.outstanding_month == currentMonth).ToList();
                    if (outstanding != null)
                    {
                        toReturn.monthlyOutstandings = outstanding.Sum(x => x.outstanding_amount).ToString();
                    }
                    else
                    {
                        toReturn.monthlyOutstandings = "0";
                    }
                    if(payElectric >0 && payGas > 0)
                    {
                        var monthlyRecovery = ((from x in db.tbl_paymenthistory where x.paymentmonth == currentMonth select x.payment_amount).Sum() + (from x in db.tbl_paymentgashistory where x.paymentmonth == currentMonth select x.payment_amount).Sum()).ToString();
                        toReturn.monthlyRecovery = monthlyRecovery != null ? monthlyRecovery.ToString() : "0";
                    }
                    else if (payElectric > 0)
                    {
                        var monthlyRecovery = ((from x in db.tbl_paymenthistory where x.paymentmonth == currentMonth select x.payment_amount).Sum() ).ToString();
                        toReturn.monthlyRecovery = monthlyRecovery != null ? monthlyRecovery.ToString() : "0";
                    }
                    else if (payGas > 0)
                    {
                        var monthlyRecovery = ((from x in db.tbl_paymentgashistory where x.paymentmonth == currentMonth select x.payment_amount).Sum()).ToString();
                        toReturn.monthlyRecovery = monthlyRecovery != null ? monthlyRecovery.ToString() : "0";
                    }
                    else
                    {
                        toReturn.monthlyRecovery =  "0";
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