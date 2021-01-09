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
                    if (!string.IsNullOrEmpty(model.fk_paymentType))
                    {
                        if (!string.IsNullOrEmpty(model.billingMonth))
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
                                                    var bill = (from x in db.tbl_ror
                                                                join y in db.tbl_consummer_pool on x.consummer_no equals y.consummer_no
                                                                join l in db.tbl_location on y.fk_location equals l.location_id
                                                                where x.fk_resident == residentId && x.ror_month == model.billingMonth
                                                                select new
                                                                {
                                                                    x.id,
                                                                    l.location_id,
                                                                    y.consummer_no,
                                                                    x.ror_status,
                                                                    x.ror_amount
                                                                }).FirstOrDefault();
                                                    var existingPayment = (from x in db.tbl_paymententryelectric where x.fk_resident == residentId && x.billingmonth == model.billingMonth && x.fk_location == bill.location_id  select x).FirstOrDefault();
                                                    if (existingPayment == null)
                                                    {
                                                        var newPayment = new tbl_paymententryelectric()
                                                        {
                                                            meter_no = bill.consummer_no,
                                                            fk_resident = residentId,
                                                            fk_location = bill.location_id,
                                                            billingmonth = model.billingMonth,
                                                            fk_picture = newReadingPicture.readingpicture_id,
                                                            fk_billelectric = bill.id,
                                                            payment_datetime = DateTime.UtcNow.AddHours(5),
                                                            paymentmonth = model.paymentMonth,
                                                            payment_amount = double.Parse(model.paymentAmount),
                                                            fk_paymenttype = int.Parse(model.fk_paymentType),
                                                        };

                                                        db.tbl_paymententryelectric.Add(newPayment);
                                                        db.SaveChanges();
                                                        toReturn = new PaymentResponseModel()
                                                        {
                                                            resultCode = "1100",
                                                            remarks = "Succesfully Added"
                                                        };
                                                       
                                                    }
                                                    else
                                                    {
                                                        var outstanding = db.tbl_outstanding.Where(x => x.fk_consummer_no == bill.consummer_no && x.outstanding_month == model.billingMonth && x.fk_resident == residentId).FirstOrDefault();
                                                        if (outstanding.outstanding_amount == existingPayment.payment_amount)
                                                        {
                                                            toReturn = new PaymentResponseModel()
                                                            {
                                                                resultCode = "1400",
                                                                remarks = "Already Paid"
                                                            };
                                                        }
                                                        else
                                                        {
                                                            existingPayment.payment_amount = existingPayment.payment_amount + double.Parse(model.paymentAmount);
                                                            db.SaveChanges();
                                                        }
                                                    }



                                                  
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
                        else
                        {
                            toReturn = new PaymentResponseModel()
                            {
                                remarks = "Please Provide Billing Month",
                                resultCode = "1300"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new PaymentResponseModel()
                        {
                            remarks = "Please Provide Payment Source",
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
        //public PaymentResponseModel AddPaymentGas(PaymentRequestModel model)
        //{
        //    PaymentResponseModel toReturn = new PaymentResponseModel();
        //    try
        //    {
        //        using (db_bmsEntities db = new db_bmsEntities())
        //        {
        //            if (!string.IsNullOrEmpty(model.paymentMonth))
        //            {
        //                if (!string.IsNullOrEmpty(model.paymentAmount))
        //                {
        //                    if (new ModelsValidatorHelper().validateint(model.residentId))
        //                    {
        //                        if (!string.IsNullOrEmpty(model.readingpicture_type))
        //                        {
        //                            if (!string.IsNullOrEmpty(model.readingpicture_data))
        //                            {
        //                                if (new ModelsValidatorHelper().validateint(model.readingpicture_size))
        //                                {
        //                                    var newReadingPicture = new tbl_readingpicture()
        //                                    {
        //                                        readingpicture_data = model.readingpicture_data,
        //                                        readingpicture_size = int.Parse(model.readingpicture_size),
        //                                        readingpicture_type = model.readingpicture_type
        //                                    };
        //                                    db.tbl_readingpicture.Add(newReadingPicture);
        //                                    db.SaveChanges();
        //                                    int residentId = int.Parse(model.residentId);
        //                                    var meterNo = (from x in db.tbl_location
        //                                                   join rb in db.tbl_residentbuilding on x.location_id equals rb.fk_building
        //                                                   join z in  db.tbl_consummer_pool on ,
        //                                                   where rb.fk_resident == residentId
        //                                                   select x.location_gassmeter).FirstOrDefault();
        //                                    var newPayment = new tbl_paymentgashistory()
        //                                    {
        //                                        meter_no = meterNo,
        //                                        fk_resident = residentId,
        //                                        paymenthistory_datetime = DateTime.UtcNow.AddHours(5),
        //                                        paymentmonth = model.paymentMonth,
        //                                        fk_billgas = 0,
        //                                        fk_picture = newReadingPicture.readingpicture_id,
        //                                        payment_amount = double.Parse(model.paymentAmount),
        //                                        fk_paymenttype = 0
        //                                    };
        //                                    db.tbl_paymentgashistory.Add(newPayment);
        //                                    db.SaveChanges();
        //                                    //var newApprovedPayment = new tbl_residentpayments()
        //                                    //{
        //                                    //    fk_resident = newPayment.fk_resident,
        //                                    //    fk_paymenttype = 0,
        //                                    //    fk_picture = newReadingPicture.readingpicture_id,
        //                                    //    paymentmonth = newPayment.paymentmonth,
        //                                    //    payment_amount = newPayment.payment_amount,
        //                                    //    payment_datetime = newPayment.paymenthistory_datetime,
        //                                    //    meter_no = newPayment.meter_no
        //                                    //};
        //                                    //db.tbl_residentpayments.Add(newApprovedPayment);
        //                                    //db.SaveChanges();
        //                                    toReturn = new PaymentResponseModel()
        //                                    {
        //                                        remarks = "Payment Successfully Approved",
        //                                        resultCode = "1100"
        //                                    };
        //                                    var billPending = (from x in db.tbl_billgas where x.fk_resident == newPayment.fk_resident && x.fk_paymentstatus == 3 select x).OrderByDescending(x=>x.datetime).FirstOrDefault();
        //                                    if (billPending != null)
        //                                    {
        //                                        newPayment.fk_billgas = billPending.id;
        //                                        if (billPending.outstanding == newPayment.payment_amount)
        //                                        {
        //                                            billPending.fk_paymentstatus = 1;
        //                                            billPending.outstanding = 0;
        //                                            billPending.paymentAmount= newPayment.payment_amount;
        //                                            billPending.paymentDate = newPayment.paymenthistory_datetime;
        //                                            billPending.paymentMonth = newPayment.paymentmonth;
        //                                        }
        //                                        else
        //                                        {
        //                                            billPending.outstanding = billPending.outstanding - newPayment.payment_amount;
        //                                            billPending.paymentAmount = newPayment.payment_amount;
        //                                            billPending.paymentDate = newPayment.paymenthistory_datetime;
        //                                            billPending.paymentMonth = newPayment.paymentmonth;
        //                                        }

        //                                        db.SaveChanges();
        //                                    }
        //                                    toReturn = new PaymentResponseModel()
        //                                    {
        //                                        resultCode = "1100",
        //                                        remarks = "Succesfully Added"
        //                                    };
        //                                }
        //                                else
        //                                {
        //                                    toReturn = new PaymentResponseModel()
        //                                    {
        //                                        remarks = "Please Provide Picture Size",
        //                                        resultCode = "1300"
        //                                    };
        //                                }
        //                            }
        //                            else
        //                            {
        //                                toReturn = new PaymentResponseModel()
        //                                {
        //                                    remarks = "Please Provide Picture Data",
        //                                    resultCode = "1300"
        //                                };
        //                            } 
        //                        }
        //                        else
        //                        {
        //                            toReturn = new PaymentResponseModel()
        //                            {
        //                                remarks = "Please Provide Picture Type",
        //                                resultCode = "1300"
        //                            };
        //                        }
        //                    }
        //                    else
        //                    {
        //                        toReturn = new PaymentResponseModel()
        //                        {
        //                            remarks = "Please Provide ResidentId",
        //                            resultCode = "1300"
        //                        };
        //                    }
        //                }
        //                else
        //                {
        //                    toReturn = new PaymentResponseModel()
        //                    {
        //                        remarks = "Please provide Amount Received",
        //                        resultCode = "1300"
        //                    };
        //                }
        //            }
        //            else
        //            {
        //                toReturn = new PaymentResponseModel()
        //                {
        //                    remarks = "Please Provide Payment Month",
        //                    resultCode = "1300"
        //                };
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        toReturn = new PaymentResponseModel()
        //        {
        //            remarks = "There Was A Fatal Error " + Ex.ToString(),
        //            resultCode = "1000"
        //        };
        //    }
        //    return toReturn;
        //}
        public PaymentResponseModel removePaymentElectric(PaymentRequestModel model)
        {
            PaymentResponseModel toReturn = new PaymentResponseModel();
            try
            {
                if(new ModelsValidatorHelper().validateint(model.paymenthistoryId))
                {
                    int paymentId = int.Parse(model.paymenthistoryId);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var payment = (from x in db.tbl_paymententryelectric where x.payment_id == paymentId select x).FirstOrDefault();
                        if (payment != null)
                        {
                           
                            db.tbl_paymententryelectric.Remove(payment);
                            db.SaveChanges();
                            toReturn = new PaymentResponseModel()
                            {
                                remarks = "SuccessFully Removed",
                                resultCode = "1100"
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
                }
                else
                {
                    toReturn = new PaymentResponseModel()
                    {
                        remarks ="Please select Payment To Delete",
                        resultCode = "1300"
                    };
                }
            }
            catch(Exception Ex)
            {
                toReturn = new PaymentResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public PaymentResponseModel removePaymentGas(PaymentRequestModel model)
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
                        if(payment!= null)
                        {
                            var bill = (from x in db.tbl_billgas where x.id == payment.fk_billgas select x).FirstOrDefault();
                            if (bill != null)
                            {
                                bill.paymentAmount = 0;
                                bill.paymentDate = null;
                                bill.paymentMonth = "";
                                bill.fk_paymentstatus = 3;
                                bill.outstanding = bill.amount;
                            }
                            db.tbl_paymentgashistory.Remove(payment);
                            db.SaveChanges();
                            toReturn = new PaymentResponseModel()
                            {
                                remarks = "SuccessFully Removed",
                                resultCode = "1100"
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
                }
                else
                {
                    toReturn = new PaymentResponseModel()
                    {
                        remarks = "Please select Payment To Delete",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new PaymentResponseModel()
                {
                    remarks = "There was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public List<PaymentResponseModel> GetAllPaymentsEntrys()
        {
            List<PaymentResponseModel> toReturn = new List<PaymentResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var payments = (from x in db.tbl_paymententryelectric
                                    join z in db.tbl_consummer_pool on x.meter_no equals z.consummer_no
                                    join y in db.tbl_location on z.fk_location equals y.location_id
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
                                        r.resident_panumber,
                                        r.resident_unit,
                                        r.resident_rank,
                                        r.resident_pin_code,
                                        y.location_id,
                                        y.location_name,
                                        s.subarea_name,
                                        a.area_name,
                                        x.paymentmonth,
                                        x.payment_id,
                                        x.payment_amount,
                                        x.payment_datetime


                                    }).ToList();
                    if (payments.Count() > 0)
                    {
                        toReturn = payments.Select(payment => new PaymentResponseModel()
                        {
                            residentPaNo = payment.resident_panumber,
                            residentRank = payment.resident_rank,
                            residentStatus = payment.resident_pin_code.ToString(),                            
                            residentUnit = payment.resident_unit,
                            picturedata = payment.readingpicture_data,
                            pictureSize = payment.readingpicture_size.ToString(),
                            pictureId = payment.readingpicture_id.ToString(),
                            pictureType = payment.readingpicture_type,
                            locationId = payment.location_id.ToString(),
                            paymentAmount = payment.payment_amount.ToString(),
                            paymenthistoryDatetime = payment.payment_datetime.ToString(),
                            paymenthistoryId = payment.payment_id.ToString(),
                            paymentMonth = !String.IsNullOrEmpty(payment.paymentmonth) ? payment.paymentmonth : "",
                            residentName = payment.resident_name,
                            locationName = payment.location_name,
                            subAreaName = payment.subarea_name,
                            areaName = payment.area_name,

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
        public List<PaymentResponseModel> GetAllPayments()
        {
            List<PaymentResponseModel>  toReturn = new List<PaymentResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var payments = (from x in db.tbl_paymenthistory
                                    join z in db.tbl_consummer_pool on x.meter_no equals z.consummer_no
                                    join y in db.tbl_location on z.fk_location equals y.location_id
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
                                        r.resident_panumber,
                                        r.resident_unit,
                                        r.resident_rank,
                                        r.resident_pin_code,
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
                            residentPaNo= payment.resident_panumber,
                            residentRank= payment.resident_rank,
                            residentStatus= payment.resident_pin_code.ToString(),
                            residentUnit=payment.resident_unit,
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
                        var payment = (from x in db.tbl_paymententryelectric where x.payment_id == paymentId select x).FirstOrDefault();
                        if(payment!= null)
                        {
                           
                            var newApprovedPayment = new tbl_paymenthistory()
                            {
                                fk_resident =payment.fk_resident,
                                fk_paymenttype = 0,
                                paymentmonth = payment.paymentmonth,
                                payment_amount = payment.payment_amount,
                                paymenthistory_datetime = payment.payment_datetime,
                                billingmonth= payment.billingmonth,
                                fk_billelectric=payment.fk_billelectric,
                                fk_location= payment.fk_location,
                                fk_picture= payment.fk_picture,
                                meter_no = payment.meter_no
                            };
                            db.tbl_paymenthistory.Add(newApprovedPayment);
                            db.SaveChanges();
                            toReturn = new PaymentResponseModel()
                            {
                                remarks = "Payment Successfully Approved",
                                resultCode = "1100"
                            };
                            var outstanding = (from x in db.tbl_outstanding where x.outstanding_month == payment.billingmonth && x.fk_resident == newApprovedPayment.fk_resident && x.fk_location == newApprovedPayment.fk_location select x).FirstOrDefault();
                            if (outstanding != null)
                            {
                                outstanding.outstanding_amount = outstanding.outstanding_amount - newApprovedPayment.payment_amount;
                                db.SaveChanges();
                            }
                            var currentbill = (from x in db.tbl_ror where x.fk_resident == newApprovedPayment.fk_resident && x.ror_month == newApprovedPayment.billingmonth && x.consummer_no == newApprovedPayment.meter_no select x).FirstOrDefault();
                            if (currentbill != null)
                            {
                                if (currentbill.ror_outstanding == newApprovedPayment.payment_amount)
                                {
                                    currentbill.ror_status = 1;
                                    currentbill.ror_outstanding = currentbill.ror_outstanding- newApprovedPayment.payment_amount;
                             
                                    db.SaveChanges();
                                }
                                else
                                {
                                    currentbill.ror_status = 4;
                                    currentbill.ror_outstanding = currentbill.ror_outstanding - newApprovedPayment.payment_amount;

                                    db.SaveChanges();
                                }
                            }
                            db.tbl_paymententryelectric.Remove(payment);
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
        //public List<PaymentResponseModel> GetAllGasPayments()
        //{
        //    List<PaymentResponseModel> toReturn = new List<PaymentResponseModel>();
        //    try
        //    {
        //        using (db_bmsEntities db = new db_bmsEntities())
        //        {
        //            var payments = (from x in db.tbl_paymentgashistory
        //                            join y in db.tbl_location on x.meter_no equals y.location_gassmeter
        //                            join r in db.tbl_residents on x.fk_resident equals r.resident_id
        //                            join s in db.tbl_subarea on y.fk_subarea equals s.subarea_id
        //                            join p in db.tbl_readingpicture on x.fk_picture equals p.readingpicture_id
        //                            join a in db.tbl_area on s.fk_area equals a.area_id
        //                            select new
        //                            {
        //                                p.readingpicture_id,
        //                                p.readingpicture_data,
        //                                p.readingpicture_size,
        //                                p.readingpicture_type,
        //                                r.resident_name,
        //                                y.location_id,
        //                                y.location_name,
        //                                s.subarea_name,
        //                                a.area_name,
        //                                x.paymentmonth,
        //                                x.paymenthistory_id,
        //                                x.payment_amount,
        //                                x.paymenthistory_datetime


        //                            }).ToList();
        //            if (payments.Count() > 0)
        //            {
        //                toReturn = payments.Select(payment => new PaymentResponseModel()
        //                {
        //                    picturedata = payment.readingpicture_data,
        //                    pictureSize = payment.readingpicture_size.ToString(),
        //                    pictureId = payment.readingpicture_id.ToString(),
        //                    pictureType = payment.readingpicture_type,
        //                    locationId = payment.location_id.ToString(),
        //                    paymentAmount = payment.payment_amount.ToString(),
        //                    paymenthistoryDatetime = payment.paymenthistory_datetime.ToString(),
        //                    paymenthistoryId = payment.paymenthistory_id.ToString(),
        //                    paymentMonth = !String.IsNullOrEmpty(payment.paymentmonth) ? payment.paymentmonth : "",
        //                    residentName = payment.resident_name,
        //                    locationName = payment.location_name,
        //                    subAreaName = payment.subarea_name,
        //                    areaName = payment.area_name,
        //                    remarks = "Successfully Found",
        //                    resultCode = "1100"
        //                }).ToList();
        //                foreach (var payment in toReturn)
        //                {
        //                    int locationId = int.Parse(payment.locationId);
        //                    var bill = (from x in db.tbl_billgas where x.fk_location == locationId select x).OrderByDescending(x => x.datetime).FirstOrDefault();
        //                    payment.outstanding = bill.outstanding.ToString();
        //                }
        //            }
        //            else
        //            {
        //                toReturn.Add(new PaymentResponseModel()
        //                {
        //                    remarks = "No Record Found",
        //                    resultCode = "1200"
        //                });
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        toReturn.Add(new PaymentResponseModel()
        //        {
        //            remarks = "There Was A Fatal Error " + Ex.ToString(),
        //            resultCode = "1000"
        //        });
        //    }
        //    return toReturn;
        //}
    }
}