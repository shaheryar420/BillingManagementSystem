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
                                    fk_paymenttype = 0,
                                };
                                db.tbl_paymenthistory.Add(newPayment);
                                db.SaveChanges();
                                var billPending = (from x in db.tbl_billelectric where x.fk_resident == residentId && x.fk_paymentstatus == 2 select x).FirstOrDefault();
                                if (billPending != null)
                                {
                                    if (billPending.billelectric_outstanding == int.Parse(model.paymentAmount))
                                    {
                                        billPending.fk_paymentstatus = 1;
                                        billPending.billelectric_outstanding = 0;
                                    }
                                    else
                                    {
                                        billPending.billelectric_outstanding = billPending.billelectric_outstanding - int.Parse(model.paymentAmount);
                                    }
                                   
                                    db.SaveChanges();
                                }
                                toReturn = new PaymentResponseModel()
                                {
                                    resultCode = "1100",
                                    remarks = "Succesfully Added"
                                };
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
        public PaymentResponseModel AddPaymentGas(PaymentRequestModel model)
        {
            PaymentResponseModel toReturn = new PaymentResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (!string.IsNullOrEmpty(model.paymentMonth))
                    {
                        if (!string.IsNullOrEmpty(model.paymentAmount))
                        {
                            if (new ModelsValidatorHelper().validateint(model.residentId))
                            {
                                int residentId = int.Parse(model.residentId);
                                var newPayment = new tbl_paymentgashistory()
                                {
                                    paymenthistory_datetime = DateTime.UtcNow.AddHours(5),
                                    paymentmonth = model.paymentMonth,
                                    payment_amount = int.Parse(model.paymentAmount),
                                    fk_paymenttype = 0
                                };
                                db.tbl_paymentgashistory.Add(newPayment);
                                db.SaveChanges();
                                var billPending = (from x in db.tbl_billgas where x.fk_resident == residentId && x.fk_paymentstatus == 2 select x).FirstOrDefault();
                                billPending.fk_paymentstatus = 1;
                                db.SaveChanges();
                                toReturn = new PaymentResponseModel()
                                {
                                    resultCode = "1100",
                                    remarks = "Succesfully Added"
                                };
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
                                remarks = "Please provide Amount Received",
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
            catch (Exception Ex)
            {
                toReturn = new PaymentResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public List<PaymentResponseModel> GetAllPayments()
        {
            List<PaymentResponseModel>  toReturn = new List<PaymentResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var payments = (from x in db.tbl_paymenthistory select x).ToList();
                    if(payments.Count() > 0)
                    {
                        toReturn = payments.Select(payment => new PaymentResponseModel()
                        {
                            paymentAmount = payment.payment_amount.ToString(),
                            paymenthistoryDatetime = payment.paymenthistory_datetime.ToString(),
                            paymenthistoryId = payment.paymenthistory_id.ToString(),
                            paymentMonth = !String.IsNullOrEmpty( payment.paymentmonth)?payment.paymentmonth:"",
                            remarks = "Successfully Found",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add(new PaymentResponseModel()
                        {
                            remarks ="No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add( new PaymentResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public List<PaymentResponseModel> GetAllGasPayments()
        {
            List<PaymentResponseModel> toReturn = new List<PaymentResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var payments = (from x in db.tbl_paymentgashistory select x).ToList();
                    if (payments.Count() > 0)
                    {
                        toReturn = payments.Select(payment => new PaymentResponseModel()
                        {
                            paymentAmount = payment.payment_amount.ToString(),
                            paymenthistoryDatetime = payment.paymenthistory_datetime.ToString(),
                            paymenthistoryId = payment.paymenthistory_id.ToString(),
                            paymentMonth = !String.IsNullOrEmpty(payment.paymentmonth) ? payment.paymentmonth : "",
                            remarks = "Successfully Found",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add(new PaymentResponseModel()
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new PaymentResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
    }
}