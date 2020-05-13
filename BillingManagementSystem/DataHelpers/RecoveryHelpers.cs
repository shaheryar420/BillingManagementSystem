﻿using System;
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
                                    fk_resident = residentId,
                                    paymenthistory_datetime = DateTime.UtcNow.AddHours(5),
                                    paymentmonth = model.paymentMonth,
                                    payment_amount = int.Parse(model.paymentAmount),
                                    fk_paymenttype = 0,
                                };
                                db.tbl_paymenthistory.Add(newPayment);
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
        public PaymentResponseModel EditPayment(PaymentRequestModel model)
        {
            PaymentResponseModel toReturn = new PaymentResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.paymenthistoryId))
                    {
                        int paymentId = int.Parse(model.paymenthistoryId);
                        var payment = (from x in db.tbl_paymenthistory where x.paymenthistory_id == paymentId select x).FirstOrDefault();
                        if(payment!= null)
                        {
                            payment.fk_resident = !string.IsNullOrEmpty(model.residentId)?int.Parse(model.residentId):payment.fk_resident;
                            payment.paymentmonth = !string.IsNullOrEmpty(model.paymentMonth)?model.paymentMonth :payment.paymentmonth;
                            payment.payment_amount = !string.IsNullOrEmpty(model.paymentAmount)?int.Parse(model.paymentAmount):payment.payment_amount;
                            payment.fk_paymenttype = 0;
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
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new PaymentResponseModel()
                        {
                            remarks = "Please Select Payment",
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
        public PaymentResponseModel EditGasPayment(PaymentRequestModel model)
        {
            PaymentResponseModel toReturn = new PaymentResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.paymenthistoryId))
                    {
                        int paymentId = int.Parse(model.paymenthistoryId);
                        var payment = (from x in db.tbl_paymentgashistory where x.paymenthistory_id == paymentId select x).FirstOrDefault();
                        if (payment != null)
                        {
                            payment.fk_resident = !string.IsNullOrEmpty(model.residentId) ? int.Parse(model.residentId) : payment.fk_resident;
                            payment.paymentmonth = !string.IsNullOrEmpty(model.paymentMonth) ? model.paymentMonth : payment.paymentmonth;
                            payment.payment_amount = !string.IsNullOrEmpty(model.paymentAmount) ? int.Parse(model.paymentAmount) : payment.payment_amount;
                            payment.fk_paymenttype = 0;
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
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new PaymentResponseModel()
                        {
                            remarks = "Please Select Payment",
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
                                    fk_resident = residentId,
                                    paymenthistory_datetime = DateTime.UtcNow.AddHours(5),
                                    paymentmonth = model.paymentMonth,
                                    payment_amount = int.Parse(model.paymentAmount),
                                    fk_paymenttype = 0
                                };
                                db.tbl_paymentgashistory.Add(newPayment);
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
        public PaymentResponseModel ApproveElectricPayment(PaymentRequestModel model)
        {
            PaymentResponseModel toReturn = new PaymentResponseModel();
            try
            {
                if(new ModelsValidatorHelper().validateint(model.paymenthistoryId))
                {
                    int paymentId = int.Parse(model.paymenthistoryId);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var payment = (from x in db.tbl_paymenthistory where x.paymenthistory_id == paymentId select x).FirstOrDefault();
                        if(payment!= null)
                        {
                            var meterNo = (from x in db.tbl_location
                                           join rb in db.tbl_residentbuilding on x.location_id equals rb.fk_building
                                           where rb.fk_resident == payment.fk_resident
                                           select x.location_electricmeter).FirstOrDefault();
                            var newApprovedPayment = new tbl_residentpayments()
                            {
                                fk_resident =payment.fk_resident,
                                fk_paymenttype = 0,
                                paymentmonth = payment.paymentmonth,
                                payment_amount = payment.payment_amount,
                                payment_datetime = payment.paymenthistory_datetime,
                                meter_no = meterNo
                            };
                            db.tbl_residentpayments.Add(newApprovedPayment);
                            db.SaveChanges();
                            toReturn = new PaymentResponseModel()
                            {
                                remarks = "Payment Successfully Approved",
                                resultCode = "1100"
                            };
                            var billPending = (from x in db.tbl_billelectric where x.fk_resident == payment.fk_resident && x.fk_paymentstatus == 2 select x).FirstOrDefault();
                            if (billPending != null)
                            {
                                if (billPending.billelectric_outstanding == payment.payment_amount)
                                {
                                    billPending.fk_paymentstatus = 1;
                                    billPending.billelectric_outstanding = 0;
                                }
                                else
                                {
                                    billPending.billelectric_outstanding = billPending.billelectric_outstanding - payment.payment_amount;
                                }

                                db.SaveChanges();
                            }
                            db.tbl_paymenthistory.Remove(payment);
                            db.SaveChanges();
                        }
                        else
                        {
                            toReturn = new PaymentResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                }
                else
                {
                    toReturn = new PaymentResponseModel()
                    {
                        remarks = "Please Provide Payment",
                        resultCode = "1300"
                    };
                }
            }
            catch(Exception Ex)
            {
                toReturn = new PaymentResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            };
            return toReturn;
        }
        public PaymentResponseModel ApproveGasPayment(PaymentRequestModel model)
        {
            PaymentResponseModel toReturn = new PaymentResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.paymenthistoryId))
                {
                    int paymentId = int.Parse(model.paymenthistoryId);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var payment = (from x in db.tbl_paymentgashistory where x.paymenthistory_id == paymentId select x).FirstOrDefault();
                        if (payment != null)
                        {
                            var meterNo = (from x in db.tbl_location
                                           join rb in db.tbl_residentbuilding on x.location_id equals rb.fk_building
                                           where rb.fk_resident == payment.fk_resident
                                           select x.location_gassmeter).FirstOrDefault();
                            var newApprovedPayment = new tbl_residentpayments()
                            {
                                fk_resident = payment.fk_resident,
                                fk_paymenttype = 0,
                                paymentmonth = payment.paymentmonth,
                                payment_amount = payment.payment_amount,
                                payment_datetime = payment.paymenthistory_datetime,
                                meter_no = meterNo
                            };
                            db.tbl_residentpayments.Add(newApprovedPayment);
                            db.SaveChanges();
                            toReturn = new PaymentResponseModel()
                            {
                                remarks = "Payment Successfully Approved",
                                resultCode = "1100"
                            };
                            var billPending = (from x in db.tbl_billgas where x.fk_resident == payment.fk_resident && x.fk_paymentstatus == 2 select x).FirstOrDefault();
                            if (billPending != null)
                            {
                                if (billPending.outstanding == payment.payment_amount)
                                {
                                    billPending.fk_paymentstatus = 1;
                                    billPending.outstanding = 0;
                                }
                                else
                                {
                                    billPending.outstanding = billPending.outstanding - payment.payment_amount;
                                }

                                db.SaveChanges();
                            }
                            db.tbl_paymentgashistory.Remove(payment);
                            db.SaveChanges();
                        }
                        else
                        {
                            toReturn = new PaymentResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                }
                else
                {
                    toReturn = new PaymentResponseModel()
                    {
                        remarks = "Please Provide Payment",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new PaymentResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            };
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