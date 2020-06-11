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
                                if (!string.IsNullOrEmpty(model.readingpicture_type))
                                { 
                                    if (!string.IsNullOrEmpty(model.readingpicture_data))
                                    {
                                        if (new ModelsValidatorHelper().validateint(model.readingpicture_size))
                                        {
                                            var newReadingPicture = new tbl_readingpicture()
                                            {
                                                readingpicture_data = model.readingpicture_data,
                                                readingpicture_size = int.Parse(model.readingpicture_size),
                                                readingpicture_type = model.readingpicture_type
                                            };
                                            db.tbl_readingpicture.Add(newReadingPicture);
                                            db.SaveChanges();
                                            int residentId = int.Parse(model.residentId);
                                            var meterNo = (from x in db.tbl_location
                                                           join rb in db.tbl_residentbuilding on x.location_id equals rb.fk_building
                                                           where rb.fk_resident == residentId
                                                           select x.location_electricmeter).FirstOrDefault();
                                            var newPayment = new tbl_paymenthistory()
                                            {
                                                meter_no = meterNo,
                                                fk_resident = residentId,
                                                fk_picture = newReadingPicture.readingpicture_id,
                                                paymenthistory_datetime = DateTime.UtcNow.AddHours(5),
                                                paymentmonth = model.paymentMonth,
                                                payment_amount = int.Parse(model.paymentAmount),
                                                fk_paymenttype = 0,
                                            };
                                            db.tbl_paymenthistory.Add(newPayment);
                                            db.SaveChanges();
                                            var newApprovedPayment = new tbl_residentpayments()
                                            {
                                                fk_resident = newPayment.fk_resident,
                                                fk_paymenttype = 0,
                                                fk_picture = newReadingPicture.readingpicture_id,
                                                paymentmonth = newPayment.paymentmonth,
                                                payment_amount = newPayment.payment_amount,
                                                payment_datetime = newPayment.paymenthistory_datetime,
                                                meter_no = newPayment.meter_no
                                            };
                                            db.tbl_residentpayments.Add(newApprovedPayment);
                                            db.SaveChanges();
                                            toReturn = new PaymentResponseModel()
                                            {
                                                remarks = "Payment Successfully Approved",
                                                resultCode = "1100"
                                            };
                                            var billPending = (from x in db.tbl_billelectric where x.fk_resident == newPayment.fk_resident && x.fk_paymentstatus == 2 select x).FirstOrDefault();
                                            if (billPending != null)
                                            {
                                                if (billPending.billelectric_outstanding == newPayment.payment_amount)
                                                {
                                                    billPending.fk_paymentstatus = 1;
                                                    billPending.billelectric_outstanding = 0;
                                                }
                                                else
                                                {
                                                    billPending.billelectric_outstanding = billPending.billelectric_outstanding - newPayment.payment_amount;
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
                                                remarks = "Please Provide Picture Size",
                                                resultCode = "1300"
                                            };
                                        }
                                    }
                                    else
                                    {
                                        toReturn = new PaymentResponseModel()
                                        {
                                            remarks = "Please Provide Picture Data",
                                            resultCode = "1300"
                                        };
                                    }
                                }
                                else
                                {
                                    toReturn = new PaymentResponseModel()
                                    {
                                        remarks = "Please Provide Picture Type",
                                        resultCode = "1300"
                                    };
                                }
                            }
                            else
                            {
                                toReturn = new PaymentResponseModel()
                                {
                                    remarks = "Please Provide Resident",
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
                                if (!string.IsNullOrEmpty(model.readingpicture_type))
                                {
                                    if (!string.IsNullOrEmpty(model.readingpicture_data))
                                    {
                                        if (new ModelsValidatorHelper().validateint(model.readingpicture_size))
                                        {
                                            var newReadingPicture = new tbl_readingpicture()
                                            {
                                                readingpicture_data = model.readingpicture_data,
                                                readingpicture_size = int.Parse(model.readingpicture_size),
                                                readingpicture_type = model.readingpicture_type
                                            };
                                            db.tbl_readingpicture.Add(newReadingPicture);
                                            db.SaveChanges();
                                            int residentId = int.Parse(model.residentId);
                                            var meterNo = (from x in db.tbl_location
                                                           join rb in db.tbl_residentbuilding on x.location_id equals rb.fk_building
                                                           where rb.fk_resident == residentId
                                                           select x.location_gassmeter).FirstOrDefault();
                                            var newPayment = new tbl_paymentgashistory()
                                            {
                                                meter_no = meterNo,
                                                fk_resident = residentId,
                                                paymenthistory_datetime = DateTime.UtcNow.AddHours(5),
                                                paymentmonth = model.paymentMonth,
                                                fk_picture = newReadingPicture.readingpicture_id,
                                                payment_amount = int.Parse(model.paymentAmount),
                                                fk_paymenttype = 0
                                            };
                                            db.tbl_paymentgashistory.Add(newPayment);
                                            db.SaveChanges();
                                            var newApprovedPayment = new tbl_residentpayments()
                                            {
                                                fk_resident = newPayment.fk_resident,
                                                fk_paymenttype = 0,
                                                fk_picture = newReadingPicture.readingpicture_id,
                                                paymentmonth = newPayment.paymentmonth,
                                                payment_amount = newPayment.payment_amount,
                                                payment_datetime = newPayment.paymenthistory_datetime,
                                                meter_no = newPayment.meter_no
                                            };
                                            db.tbl_residentpayments.Add(newApprovedPayment);
                                            db.SaveChanges();
                                            toReturn = new PaymentResponseModel()
                                            {
                                                remarks = "Payment Successfully Approved",
                                                resultCode = "1100"
                                            };
                                            var billPending = (from x in db.tbl_billgas where x.fk_resident == newPayment.fk_resident && x.fk_paymentstatus == 2 select x).FirstOrDefault();
                                            if (billPending != null)
                                            {
                                                if (billPending.outstanding == newPayment.payment_amount)
                                                {
                                                    billPending.fk_paymentstatus = 1;
                                                    billPending.outstanding = 0;
                                                }
                                                else
                                                {
                                                    billPending.outstanding = billPending.outstanding - newPayment.payment_amount;
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
                                                remarks = "Please Provide Picture Size",
                                                resultCode = "1300"
                                            };
                                        }
                                    }
                                    else
                                    {
                                        toReturn = new PaymentResponseModel()
                                        {
                                            remarks = "Please Provide Picture Data",
                                            resultCode = "1300"
                                        };
                                    } 
                                }
                                else
                                {
                                    toReturn = new PaymentResponseModel()
                                    {
                                        remarks = "Please Provide Picture Type",
                                        resultCode = "1300"
                                    };
                                }
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
                    var payments = (from x in db.tbl_paymenthistory 
                                    join y in db.tbl_location on x.meter_no equals y.location_electricmeter
                                    join r in db.tbl_residents on x.fk_resident equals r.resident_id
                                    join p in db.tbl_readingpicture on x.fk_picture equals p.readingpicture_id
                                    join s in db.tbl_subarea on y.fk_subarea equals s.subarea_id
                                    join a in db.tbl_area on s.fk_area equals a.area_id
                                    select new 
                                    {
                                        p.readingpicture_data,
                                        p.readingpicture_id,
                                        p.readingpicture_size,
                                        p.readingpicture_type,
                                        r.resident_name,
                                        y.location_id,
                                        y.location_name,
                                        s.subarea_name,
                                        a.area_name,
                                        x.paymentmonth,
                                        x.paymenthistory_id,
                                        x.payment_amount,
                                        x.paymenthistory_datetime

                                        
                                    }).ToList();
                    if(payments.Count() > 0)
                    {
                        toReturn = payments.Select(payment => new PaymentResponseModel()
                        {
                            picturedata = payment.readingpicture_data,
                            pictureSize = payment.readingpicture_size.ToString(),
                            pictureId = payment.readingpicture_id.ToString(),
                            pictureType = payment.readingpicture_type,
                            locationId = payment.location_id.ToString(),
                            paymentAmount = payment.payment_amount.ToString(),
                            paymenthistoryDatetime = payment.paymenthistory_datetime.ToString(),
                            paymenthistoryId = payment.paymenthistory_id.ToString(),
                            paymentMonth = !String.IsNullOrEmpty( payment.paymentmonth)?payment.paymentmonth:"",
                            residentName = payment.resident_name,
                            locationName = payment.location_name,
                            subAreaName = payment.subarea_name,
                            areaName = payment.area_name,
                            remarks = "Successfully Found",
                            resultCode = "1100"
                        }).ToList();
                        foreach (var payment in toReturn)
                        {
                            int locationId = int.Parse(payment.locationId);
                            var bill = (from x in db.tbl_billelectric where x.fk_location == locationId select x).OrderByDescending(x => x.billelectric_datetime).FirstOrDefault();
                            payment.outstanding = bill.billelectric_outstanding.ToString();
                        }
                        
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
                           
                            var newApprovedPayment = new tbl_residentpayments()
                            {
                                fk_resident =payment.fk_resident,
                                fk_paymenttype = 0,
                                paymentmonth = payment.paymentmonth,
                                payment_amount = payment.payment_amount,
                                payment_datetime = payment.paymenthistory_datetime,
                                meter_no = payment.meter_no
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
                            var newApprovedPayment = new tbl_residentpayments()
                            {
                                fk_resident = payment.fk_resident,
                                fk_paymenttype = 0,
                                paymentmonth = payment.paymentmonth,
                                payment_amount = payment.payment_amount,
                                payment_datetime = payment.paymenthistory_datetime,
                                meter_no = payment.meter_no
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
                    var payments = (from x in db.tbl_paymentgashistory
                                    join y in db.tbl_location on x.meter_no equals y.location_gassmeter
                                    join r in db.tbl_residents on x.fk_resident equals r.resident_id
                                    join s in db.tbl_subarea on y.fk_subarea equals s.subarea_id
                                    join p in db.tbl_readingpicture on x.fk_picture equals p.readingpicture_id
                                    join a in db.tbl_area on s.fk_area equals a.area_id
                                    select new
                                    {
                                        p.readingpicture_id,
                                        p.readingpicture_data,
                                        p.readingpicture_size,
                                        p.readingpicture_type,
                                        r.resident_name,
                                        y.location_id,
                                        y.location_name,
                                        s.subarea_name,
                                        a.area_name,
                                        x.paymentmonth,
                                        x.paymenthistory_id,
                                        x.payment_amount,
                                        x.paymenthistory_datetime


                                    }).ToList();
                    if (payments.Count() > 0)
                    {
                        toReturn = payments.Select(payment => new PaymentResponseModel()
                        {
                            picturedata = payment.readingpicture_data,
                            pictureSize = payment.readingpicture_size.ToString(),
                            pictureId = payment.readingpicture_id.ToString(),
                            pictureType = payment.readingpicture_type,
                            locationId = payment.location_id.ToString(),
                            paymentAmount = payment.payment_amount.ToString(),
                            paymenthistoryDatetime = payment.paymenthistory_datetime.ToString(),
                            paymenthistoryId = payment.paymenthistory_id.ToString(),
                            paymentMonth = !String.IsNullOrEmpty(payment.paymentmonth) ? payment.paymentmonth : "",
                            residentName = payment.resident_name,
                            locationName = payment.location_name,
                            subAreaName = payment.subarea_name,
                            areaName = payment.area_name,
                            remarks = "Successfully Found",
                            resultCode = "1100"
                        }).ToList();
                        foreach (var payment in toReturn)
                        {
                            int locationId = int.Parse(payment.locationId);
                            var bill = (from x in db.tbl_billgas where x.fk_location == locationId select x).OrderByDescending(x => x.datetime).FirstOrDefault();
                            payment.outstanding = bill.outstanding.ToString();
                        }
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