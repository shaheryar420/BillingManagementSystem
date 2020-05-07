using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.DataHelpers
{
    public class PictureHelpers
    {
        #region Reading Picture
        public PictureResponseModel AddReadingPicture(PictureRequestModel model)
        {
            PictureResponseModel toReturn = new PictureResponseModel();
            try
            {
                if (String.IsNullOrEmpty(model.pictureData))
                {
                    if(String.IsNullOrEmpty(model.pictureSize))
                    {
                        if(string.IsNullOrEmpty(model.pictureType))
                        {
                            using (db_bmsEntities db = new db_bmsEntities())
                            {
                                var newReadingPicture = new tbl_readingpicture();
                                newReadingPicture.readingpicture_data = model.pictureData;
                                newReadingPicture.readingpicture_size = int.Parse(model.pictureSize);
                                newReadingPicture.readingpicture_type = model.pictureType;
                                db.tbl_readingpicture.Add(newReadingPicture);
                                db.SaveChanges();
                                toReturn = new PictureResponseModel()
                                {
                                    remarks = "Successfully Added",
                                    resultCode = "1100"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new PictureResponseModel()
                            {
                                remarks = "Please Provide Picture Type",
                                resultCode = "1300"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new PictureResponseModel()
                        {
                            remarks = "Please Provide Picture Size ",
                            resultCode = "1300"
                        };
                    }
                }
                else
                {
                    toReturn = new PictureResponseModel()
                    {
                        remarks = "Please Provide Picture Data",
                        resultCode = "1300"
                    };
                }
            }
            catch(Exception Ex)
            {
                toReturn = new PictureResponseModel()
                {
                    remarks = "There Was a Fatal Error "+Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public PictureResponseModel EditReadingPicture(PictureRequestModel model)
        {
            PictureResponseModel toReturn = new PictureResponseModel();
            try
            {
                if (String.IsNullOrEmpty(model.pictureData))
                {
                    if (String.IsNullOrEmpty(model.pictureSize))
                    {
                        if (string.IsNullOrEmpty(model.pictureType))
                        {
                            if(new ModelsValidatorHelper().validateint(model.pictiureId))
                            {
                                int pictureId = int.Parse(model.pictiureId);
                                using (db_bmsEntities db = new db_bmsEntities())
                                {
                                    var readingPicture = (from x in db.tbl_readingpicture where x.readingpicture_id == pictureId select x).FirstOrDefault();
                                    if(readingPicture!= null)
                                    {
                                        readingPicture.readingpicture_data = model.pictureData;
                                        readingPicture.readingpicture_size = int.Parse(model.pictureSize);
                                        readingPicture.readingpicture_type = model.pictureType;
                                        db.SaveChanges();
                                        toReturn = new PictureResponseModel()
                                        {
                                            remarks = "Successfully Updated",
                                            resultCode = "1100"
                                        };
                                    }
                                    else
                                    {
                                        toReturn = new PictureResponseModel()
                                        {
                                            remarks = "No record Found",
                                            resultCode = "1200"
                                        };
                                    }
                                }
                            }
                            else
                            {
                                toReturn = new PictureResponseModel()
                                {
                                    remarks ="Please Select Picture",
                                    resultCode = "1300"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new PictureResponseModel()
                            {
                                remarks = "Please Provide Picture Type",
                                resultCode = "1300"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new PictureResponseModel()
                        {
                            remarks = "Please Provide Picture Size ",
                            resultCode = "1300"
                        };
                    }
                }
                else
                {
                    toReturn = new PictureResponseModel()
                    {
                        remarks = "Please Provide Picture Data",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new PictureResponseModel()
                {
                    remarks = "There Was a Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public PictureResponseModel RemoveReadingPicture(PictureRequestModel model)
        {
            PictureResponseModel toReturn = new PictureResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.pictiureId))
                {
                    var pictureId = int.Parse(model.pictiureId);
                    using(db_bmsEntities db = new db_bmsEntities())
                    {
                        var readingPicture = (from x in db.tbl_readingpicture where x.readingpicture_id == pictureId select x).FirstOrDefault();
                        if(readingPicture != null)
                        {
                            db.tbl_readingpicture.Remove(readingPicture);
                            db.SaveChanges();
                            toReturn = new PictureResponseModel()
                            {
                                remarks = "Succesfully Deleted",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new PictureResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                }
                else
                {
                    toReturn = new PictureResponseModel()
                    {
                        remarks = "Please Select Picture",
                        resultCode = "1300"
                    };
                }     
            }
            catch (Exception Ex)
            {
                toReturn = new PictureResponseModel()
                {
                    remarks = "There Was a Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        #endregion
        #region Bill Picture
        public PictureResponseModel AddBillPicture(PictureRequestModel model)
        {
            PictureResponseModel toReturn = new PictureResponseModel();
            try
            {
                if (String.IsNullOrEmpty(model.pictureData))
                {
                    if (String.IsNullOrEmpty(model.pictureSize))
                    {
                        if (string.IsNullOrEmpty(model.pictureType))
                        {
                            using (db_bmsEntities db = new db_bmsEntities())
                            {
                                var newbillPicture = new tbl_billpicture();
                                newbillPicture.billpicture_date = model.pictureData;
                                newbillPicture.billpicture_size = int.Parse(model.pictureSize);
                                newbillPicture.billpicture_type = model.pictureType;
                                db.tbl_billpicture.Add(newbillPicture);
                                db.SaveChanges();
                                toReturn = new PictureResponseModel()
                                {
                                    remarks = "Successfully Added",
                                    resultCode = "1100"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new PictureResponseModel()
                            {
                                remarks = "Please Provide Picture Type",
                                resultCode = "1300"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new PictureResponseModel()
                        {
                            remarks = "Please Provide Picture Size ",
                            resultCode = "1300"
                        };
                    }
                }
                else
                {
                    toReturn = new PictureResponseModel()
                    {
                        remarks = "Please Provide Picture Data",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new PictureResponseModel()
                {
                    remarks = "There Was a Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public PictureResponseModel EditBillPicture(PictureRequestModel model)
        {
            PictureResponseModel toReturn = new PictureResponseModel();
            try
            {
                if (String.IsNullOrEmpty(model.pictureData))
                {
                    if (String.IsNullOrEmpty(model.pictureSize))
                    {
                        if (string.IsNullOrEmpty(model.pictureType))
                        {
                            if (new ModelsValidatorHelper().validateint(model.pictiureId))
                            {
                                int pictureId = int.Parse(model.pictiureId);
                                using (db_bmsEntities db = new db_bmsEntities())
                                {
                                    var billPicture = (from x in db.tbl_billpicture where x.billpicture_id == pictureId select x).FirstOrDefault();
                                    if (billPicture != null)
                                    {
                                        billPicture.billpicture_date = model.pictureData;
                                        billPicture.billpicture_size = int.Parse(model.pictureSize);
                                        billPicture.billpicture_type = model.pictureType;
                                        db.SaveChanges();
                                        toReturn = new PictureResponseModel()
                                        {
                                            remarks = "Successfully Updated",
                                            resultCode = "1100"
                                        };
                                    }
                                    else
                                    {
                                        toReturn = new PictureResponseModel()
                                        {
                                            remarks = "No record Found",
                                            resultCode = "1200"
                                        };
                                    }
                                }
                            }
                            else
                            {
                                toReturn = new PictureResponseModel()
                                {
                                    remarks = "Please Select Picture",
                                    resultCode = "1300"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new PictureResponseModel()
                            {
                                remarks = "Please Provide Picture Type",
                                resultCode = "1300"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new PictureResponseModel()
                        {
                            remarks = "Please Provide Picture Size ",
                            resultCode = "1300"
                        };
                    }
                }
                else
                {
                    toReturn = new PictureResponseModel()
                    {
                        remarks = "Please Provide Picture Data",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new PictureResponseModel()
                {
                    remarks = "There Was a Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public PictureResponseModel RemoveBillPicture(PictureRequestModel model)
        {
            PictureResponseModel toReturn = new PictureResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.pictiureId))
                {
                    var pictureId = int.Parse(model.pictiureId);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var billPicture = (from x in db.tbl_billpicture where x.billpicture_id == pictureId select x).FirstOrDefault();
                        if (billPicture != null)
                        {
                            db.tbl_billpicture.Remove(billPicture);
                            db.SaveChanges();
                            toReturn = new PictureResponseModel()
                            {
                                remarks = "Succesfully Deleted",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new PictureResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                }
                else
                {
                    toReturn = new PictureResponseModel()
                    {
                        remarks = "Please Select Picture",
                        resultCode = "1300"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new PictureResponseModel()
                {
                    remarks = "There Was a Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        #endregion
    }
}