using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;


namespace BillingManagementSystem.DataHelpers
{
    public class ApprovePaymentHelpers
    {
        public List<ApprovePaymentResponseModel> GetAllApprovePayments()
        {
            List<ApprovePaymentResponseModel> toReturn = new List<ApprovePaymentResponseModel>();
            try
            {
                using(db_bmsEntities db = new db_bmsEntities())
                {
                    var payments = (from x in db.tbl_residentpayments 
                                    join y in db.tbl_residents on x.fk_resident equals y.resident_id
                                    select new 
                                    {
                                        x.fk_resident,
                                        x.meter_no,
                                        x.paymentmonth,
                                        x.payment_amount,
                                        x.payment_datetime,
                                        x.payment_id,
                                        y.resident_name,
                                        y.resident_panumber,
                                        y.resident_rank
                                    }).ToList();
                    if (payments.Count() > 0)
                    {
                        toReturn = payments.Select(payment => new ApprovePaymentResponseModel()
                        {
                            fk_resident = payment.fk_resident.ToString(),
                            meterNo = payment.meter_no,
                            paymentMonth = payment.paymentmonth,
                            paymentAmount = payment.payment_amount.ToString(),
                            paymentDatetime = payment.payment_datetime.ToString(),
                            paymentId = payment.payment_id.ToString(),
                            residentName = payment.resident_name,
                            residentPaNo = !string.IsNullOrEmpty(payment.resident_panumber)?payment.resident_panumber:"",
                            residentRank = !string.IsNullOrEmpty(payment.resident_rank)?payment.resident_rank :"",
                            resultCode = "1100",
                            remarks ="Found Succeessfully"
                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add(new ApprovePaymentResponseModel()
                        {
                            resultCode="1200",
                            remarks ="No Record Found"
                        });
                    }
                }
            }
            catch(Exception Ex)
            {
                toReturn.Add( new ApprovePaymentResponseModel()
                {
                    remarks = "There was Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
    }
}