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
                    toReturn.totalOutstandings = (from x in db.tbl_billelectric select x.billelectric_outstanding).Sum().ToString();
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