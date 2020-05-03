using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.DataHelpers
{
    public class ResidentHelpers
    {
        public ResidentResponseModel AddResident(ResidentRequestModel model)
        {
            ResidentResponseModel toReturn = new ResidentResponseModel();
            try
            {
                using(db_bmsEntities db = new db_bmsEntities())
                {
                    if (!string.IsNullOrEmpty(model.residentName))
                    {
                        if(!string.IsNullOrEmpty(model.residentPaNumber))
                        {
                            var existingResident = (from x in db.tbl_residents where x.resident_panumber == model.residentPaNumber select x).FirstOrDefault();
                            if (existingResident == null)
                            {
                                tbl_residents resident = new tbl_residents()
                                {
                                    resident_name = model.residentName,
                                    resident_panumber = model.residentPaNumber,
                                    resident_rank = !string.IsNullOrEmpty(model.residentRank) ? model.residentRank : "",
                                    resident_remarks = !string.IsNullOrEmpty(model.residentRemarks) ? model.residentRemarks : "",
                                    resident_unit = !string.IsNullOrEmpty(model.residentUnit) ? model.residentUnit : ""
                                };
                                db.tbl_residents.Add(resident);
                                db.SaveChanges();
                                toReturn = new ResidentResponseModel()
                                {
                                    remarks = "Successfully Added",
                                    resultCode = "1100"
                                };
                            }
                            else
                            {
                                toReturn = new ResidentResponseModel()
                                {
                                    remarks = "Resident Already Exists",
                                    resultCode = "1400"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new ResidentResponseModel()
                            {
                                remarks="Please Provide Pa Number",
                                resultCode="1300"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new ResidentResponseModel()
                        {
                            remarks="Please Provide Name",
                            resultCode="1300"
                        };
                    }
                }
            }
            catch(Exception Ex)
            {
                toReturn = new ResidentResponseModel()
                {
                    remarks= "There Was A fatal Error"+ Ex.ToString(),
                    resultCode="1000"
                };
            }
            return toReturn;
        }
        public ResidentResponseModel EditResident(ResidentRequestModel model)
        {
            ResidentResponseModel toReturn = new ResidentResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.residentId))
                    {
                        var residentId = int.Parse(model.residentId);
                        tbl_residents resident = (from x in db.tbl_residents where x.resident_id== residentId select x ).FirstOrDefault();
                        if(resident!= null)
                        {
                            resident.resident_name = !string.IsNullOrEmpty(model.residentName) ? model.residentName : resident.resident_name;
                            resident.resident_panumber = !string.IsNullOrEmpty(model.residentPaNumber) ? model.residentPaNumber : resident.resident_panumber;
                            resident.resident_rank = !string.IsNullOrEmpty(model.residentRank) ? model.residentRank : resident.resident_rank;
                            resident.resident_remarks = !string.IsNullOrEmpty(model.residentRemarks) ? model.residentRemarks : resident.resident_remarks;
                            resident.resident_unit = !string.IsNullOrEmpty(model.residentUnit) ? model.residentUnit : resident.resident_unit;
                            db.SaveChanges();
                            toReturn = new ResidentResponseModel()
                            {
                                remarks = "Successfully Updated",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new ResidentResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode="1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new ResidentResponseModel()
                        {
                            remarks = "Please Select Resident",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new ResidentResponseModel()
                {
                    remarks = "There Was A fatal Error" + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public ResidentResponseModel DeleteResident(ResidentRequestModel model)
        {
            ResidentResponseModel toReturn = new ResidentResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.residentId))
                    {
                        var residentId = int.Parse(model.residentId);
                        tbl_residents resident = (from x in db.tbl_residents where x.resident_id == residentId select x).FirstOrDefault();
                        if (resident != null)
                        {
                            db.tbl_residents.Remove(resident);
                            db.SaveChanges();
                            toReturn = new ResidentResponseModel()
                            {
                                remarks = "Successfully Deleted",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new ResidentResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new ResidentResponseModel()
                        {
                            remarks = "Please Select Resident",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new ResidentResponseModel()
                {
                    remarks = "There Was A fatal Error" + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public ResidentResponseModel GetResidentById (ResidentRequestModel model)
        {
            ResidentResponseModel toReturn = new ResidentResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.residentId))
                    {
                        var residentId = int.Parse(model.residentId);
                        tbl_residents resident = (from x in db.tbl_residents where x.resident_id == residentId select x).FirstOrDefault();
                        if (resident != null)
                        {
                            toReturn = new ResidentResponseModel()
                            {
                                residentName = resident.resident_name,
                                residentPaNumber = resident.resident_panumber,
                                residentRank = !string.IsNullOrEmpty(resident.resident_rank)?resident.resident_rank:"",
                                residentId = resident.resident_id.ToString(),
                                residentRemarks = !string.IsNullOrEmpty(resident.resident_remarks)?resident.resident_remarks:"",
                                residentUnit = !string.IsNullOrEmpty(resident.resident_unit)?resident.resident_unit:"",
                                remarks = "Successfully Found",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new ResidentResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new ResidentResponseModel()
                        {
                            remarks = "Please Select Resident",
                            resultCode = "1300"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new ResidentResponseModel()
                {
                    remarks = "There Was A fatal Error" + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public List<ResidentResponseModel> GetAllResidents(ResidentRequestModel model)
        {
            List<ResidentResponseModel> toReturn = new List<ResidentResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var residents = (from x in db.tbl_residents select x).ToList();
                    if (residents.Count()>0)
                    {
                        toReturn =residents.Select(resident=> new ResidentResponseModel()
                        {
                            residentName = resident.resident_name,
                            residentPaNumber = resident.resident_panumber,
                            residentRank = !string.IsNullOrEmpty(resident.resident_rank) ? resident.resident_rank : "",
                            residentId = resident.resident_id.ToString(),
                            residentRemarks = !string.IsNullOrEmpty(resident.resident_remarks) ? resident.resident_remarks : "",
                            residentUnit = !string.IsNullOrEmpty(resident.resident_unit) ? resident.resident_unit : "",
                            remarks = "Successfully Found",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add( new ResidentResponseModel()
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new ResidentResponseModel()
                {
                    remarks = "There Was A fatal Error" + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
    }
}