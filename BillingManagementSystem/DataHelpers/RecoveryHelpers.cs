using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.DataHelpers
{
    public class RecoveryHelpers
    {
        public PaymentResponseModel AddPayment(PaymentRequestModel model)
        {
            PaymentResponseModel toReturn = new PaymentResponseModel();
            try
            {
                using(db_bmsEntities db = new db_bmsEntities())
                {
                    if(!string.IsNullOrEmpty(model.paymentMonth))
                    {
                        if(!string.IsNullOrEmpty(model.paymentAmount))
                        {
                            if (new ModelsValidatorHelper().validateint(model.residentId))
                            {
                                int residentId = int.Parse(model.residentId);
                                var newPayment = new tbl_paymenthistory()
                                {
                                    paymenthistory_datetime = DateTime.UtcNow.AddHours(5),
                                    paymentmonth = model.paymentMonth,
                                    payment_amount = int.Parse(model.paymentAmount),
                                    fk_paymenttype = 0
                                };
                                db.tbl_paymenthistory.Add(newPayment);
                                db.SaveChanges();
                                var billPending = (from x in db.tbl_billelectric where x.fk_resident == residentId && x.fk_paymentstatus == 2 select x).FirstOrDefault();
                                billPending.fk_paymentstatus = 1;
                                db.SaveChanges();
                            }
                            else
                            {
                                toReturn = new PaymentResponseModel()
                                {
                                    remarks = "Please Provide ResidentId",
                                    resultCode = "1300"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new PaymentResponseModel()
                            {
                                remarks ="Please provide Amount Received",
                                resultCode = "1300"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new PaymentResponseModel()
                        {
                            remarks = "Please Provide Payment Month",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch(Exception Ex)
            {
                toReturn = new PaymentResponseModel()
                {
                    remarks ="There Was A Fatal Error "+Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
    }
}