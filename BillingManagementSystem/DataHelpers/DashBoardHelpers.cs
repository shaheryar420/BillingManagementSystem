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
                    toReturn.totalResidents = (from x in db.tbl_residents select x).Count().ToString();
                    var billElectric = (from x in db.tbl_billelectric select x).Count();
                    var billgas = (from x in db.tbl_billgas select x).Count();
                    var payElectric = (from x in db.tbl_paymenthistory where x.paymentmonth == currentMonth select x).Count();
                    var payGas = (from x in db.tbl_paymentgashistory where x.paymentmonth == currentMonth select x).Count();
                    if(billElectric>0&& billgas > 0) 
                    {
                        var totalOutstanding = ((from x in db.tbl_billelectric select x.billelectric_outstanding).Sum() + (from x in db.tbl_billgas select x.outstanding).Sum()).ToString();
                        var monthlyOutstandings = ((from x in db.tbl_billelectric where x.billelectric_month == currentMonth select x.billelectric_outstanding).Sum() + (from x in db.tbl_billgas where x.month == currentMonth select x.outstanding).Sum()).ToString();
                        toReturn.totalOutstandings = !string.IsNullOrEmpty(totalOutstanding) ? totalOutstanding : "0";
                        toReturn.monthlyOutstandings = monthlyOutstandings != "" ? monthlyOutstandings : "0";
                    }
                    else if (billElectric > 0)
                    {
                        var totalOutstanding = ((from x in db.tbl_billelectric select x.billelectric_outstanding).Sum() ).ToString();
                        var monthlyOutstandings = ((from x in db.tbl_billelectric where x.billelectric_month == currentMonth select x.billelectric_outstanding).Sum()).ToString();
                        toReturn.totalOutstandings = !string.IsNullOrEmpty(totalOutstanding) ? totalOutstanding : "0";
                        toReturn.monthlyOutstandings = monthlyOutstandings != "" ? monthlyOutstandings : "0";
                    }
                    else if(billgas > 0)
                    {
                        var totalOutstanding = ((from x in db.tbl_billgas select x.outstanding).Sum()).ToString();
                        var monthlyOutstandings = ((from x in db.tbl_billgas where x.month == currentMonth select x.outstanding).Sum()).ToString();
                        toReturn.totalOutstandings = !string.IsNullOrEmpty(totalOutstanding) ? totalOutstanding : "0";
                        toReturn.monthlyOutstandings = monthlyOutstandings != "" ? monthlyOutstandings : "0";
                    }
                    else
                    {
                        toReturn.totalOutstandings =  "0";
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