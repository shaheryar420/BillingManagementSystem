using System;
using System.Collections.Generic;
using System.Globalization;
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
                    if (new ModelsValidatorHelper().validateint(model.locationId))
                    {
                        int locationId = int.Parse(model.locationId);
                        if (!string.IsNullOrEmpty(model.residentName))
                        {
                            if (!string.IsNullOrEmpty(model.residentPaNumber))
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
                                    var residentExistingBuilding = (from x in db.tbl_residentbuilding where x.fk_building == locationId select x).FirstOrDefault();
                                    if(residentExistingBuilding == null)
                                    {
                                        db.tbl_residents.Add(resident);
                                        db.SaveChanges();
                                        toReturn = new ResidentResponseModel()
                                        {
                                            residentId = resident.resident_id.ToString(),
                                            remarks = "Successfully Added",
                                            resultCode = "1100"
                                        };
                                        var resdidentBuilding = new tbl_residentbuilding()
                                        {
                                            fk_building = locationId,
                                            fk_resident = resident.resident_id
                                        };
                                        db.tbl_residentbuilding.Add(resdidentBuilding);
                                        db.SaveChanges();
                                       
                                    }
                                    else
                                    {
                                        toReturn = new ResidentResponseModel()
                                        {
                                            remarks = "Please select Different building as it is already occupied",
                                            resultCode = "1300"
                                        };
                                    }
                                   
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
                                    remarks = "Please Provide Pa Number",
                                    resultCode = "1300"
                                };
                            }
                        }
                        else
                        {
                            toReturn = new ResidentResponseModel()
                            {
                                remarks = "Please Provide Name",
                                resultCode = "1300"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new ResidentResponseModel()
                        {
                            remarks = "Please Provide Building",
                            resultCode = "1300"
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
                            var existingResident = new tbl_residents();
                            if (!string.IsNullOrEmpty(model.residentPaNumber))
                            {
                                existingResident = (from x in db.tbl_residents where x.resident_panumber == model.residentPaNumber && x.resident_id != residentId select x).FirstOrDefault();
                            }
                            else
                            {
                                existingResident = null;
                            }
                            if (existingResident == null)
                            {
                                resident.resident_name = !string.IsNullOrEmpty(model.residentName) ? model.residentName : resident.resident_name;
                                resident.resident_panumber = !string.IsNullOrEmpty(model.residentPaNumber) ? model.residentPaNumber : resident.resident_panumber;
                                resident.resident_rank = !string.IsNullOrEmpty(model.residentRank) ? model.residentRank : resident.resident_rank;
                                resident.resident_remarks = !string.IsNullOrEmpty(model.residentRemarks) ? model.residentRemarks : resident.resident_remarks;
                                resident.resident_unit = !string.IsNullOrEmpty(model.residentUnit) ? model.residentUnit : resident.resident_unit;
                                var residentBuilding = (from x in db.tbl_residentbuilding where x.fk_resident == resident.resident_id select x).FirstOrDefault();
                                residentBuilding.fk_building = int.Parse(model.locationId);
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
                                    remarks = "Resident Already Exists",
                                    resultCode = "1400"
                                };
                            }
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
                        var residentBuilding = (from x in db.tbl_residentbuilding where x.fk_resident == residentId select x).FirstOrDefault();
                        if (resident != null)
                        {
                            if (residentBuilding != null)
                            {
                                db.tbl_residentbuilding.Remove(residentBuilding);
                                db.SaveChanges();
                            }
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
                        var resident = (from x in db.tbl_residents 
                                                  join y in db.tbl_residentbuilding on x.resident_id equals y.fk_resident 
                                                  join z in db.tbl_location on y.fk_building equals z.location_id
                                                  join a in db.tbl_area on z.fk_area equals a.area_id
                                                  where x.resident_id == residentId
                                                  select new 
                                                  {
                                                     x.resident_id,
                                                     x.resident_name,
                                                     x.resident_panumber,
                                                     x.resident_rank,
                                                     x.resident_remarks,
                                                     x.resident_unit,
                                                     z.location_id,
                                                     z.location_name,
                                                     a.area_id,
                                                     a.area_name
                                                  }).FirstOrDefault();
                        if (resident != null)
                        {
                            toReturn = new ResidentResponseModel()
                            {
                                areaId = resident.area_id.ToString(),
                                areaName = resident.area_name,
                                locationName = resident.location_name,
                                loactionId = resident.location_id.ToString(),
                                residentName = resident.resident_name,
                                residentPaNumber = resident.resident_panumber,
                                residentRank = !string.IsNullOrEmpty(resident.resident_rank)?resident.resident_rank:"",
                                residentId = resident.resident_id.ToString(),
                                residentRemarks = !string.IsNullOrEmpty(resident.resident_remarks)?resident.resident_remarks:"",
                                residentUnit = !string.IsNullOrEmpty(resident.resident_unit)?resident.resident_unit:"",
                                remarks = "Successfully Found",
                                resultCode = "1100"
                            };
                            var billElectric = (from x in db.tbl_billelectric where x.fk_paymentstatus == 2 select x).FirstOrDefault();
                            var billGas = (from x in db.tbl_billgas where x.fk_paymentstatus == 2 select x).FirstOrDefault();
                            toReturn.totatOutStandings = "0";
                            double totalOutStandings =0;
                            if ( billElectric != null)
                            {
                                var electricOutstanding = billElectric.billelectric_outstanding.ToString();
                                 totalOutStandings = totalOutStandings + double.Parse(electricOutstanding);
                            }
                            if( billGas != null)
                            {
                                var gasOutstanding = billGas.outstanding.ToString();
                                totalOutStandings = totalOutStandings + double.Parse(gasOutstanding);
                            }
                            toReturn.totatOutStandings = totalOutStandings.ToString();
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
                    var residents = (from x in db.tbl_residents
                                     join y in db.tbl_residentbuilding on x.resident_id equals y.fk_resident
                                     join z in db.tbl_location on y.fk_building equals z.location_id
                                     join a in db.tbl_area on z.fk_area equals a.area_id
                                     select new
                                     {
                                         x.resident_id,
                                         x.resident_name,
                                         x.resident_panumber,
                                         x.resident_rank,
                                         x.resident_remarks,
                                         x.resident_unit,
                                         z.location_id,
                                         z.location_name,
                                         a.area_id,
                                         a.area_name
                                     }).ToList();
                    if (residents.Count()>0)
                    {
                        toReturn =residents.Select(resident=> new ResidentResponseModel()
                        {
                            areaId = resident.area_id.ToString(),
                            areaName = resident.area_name,
                            locationName = resident.location_name,
                            loactionId = resident.location_id.ToString(),
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
        public List<ResidentResponseModel> GetAllResidentsForSearch(ResidentRequestModel model)
        {
            List<ResidentResponseModel> toReturn = new List<ResidentResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var residents = (from x in db.tbl_residents
                                     join y in db.tbl_residentbuilding on x.resident_id equals y.fk_resident
                                     join z in db.tbl_location on y.fk_building equals z.location_id
                                     join a in db.tbl_area on z.fk_area equals a.area_id
                                     select new 
                                     {
                                         x.resident_id,
                                         x.resident_name,
                                         x.resident_panumber,
                                         x.resident_rank,
                                         x.resident_remarks,
                                         x.resident_unit,
                                         z.location_id,
                                         z.location_name,
                                         a.area_id,
                                         a.area_name

                                     }).ToList();
                    if (residents.Count() > 0)
                    {
                        toReturn = residents.Select(resident => new ResidentResponseModel()
                        {
                            areaId = resident.area_id.ToString(),
                            areaName = resident.area_name,
                            locationName = resident.location_name,
                            loactionId = resident.location_id.ToString(),
                            residentName = resident.resident_name,
                            residentPaNumber = resident.resident_panumber,
                            residentRank = !string.IsNullOrEmpty(resident.resident_rank) ? resident.resident_rank : "",
                            residentId = resident.resident_id.ToString(),
                            residentRemarks = !string.IsNullOrEmpty(resident.resident_remarks) ? resident.resident_remarks : "",
                            residentUnit = !string.IsNullOrEmpty(resident.resident_unit) ? resident.resident_unit : "",
                            remarks = "Successfully Found",
                            resultCode = "1100"
                        }).ToList();
                        foreach(var resident in residents)
                        {
                            var outstanding = (from x in db.tbl_billelectric where x.fk_resident == resident.resident_id && x.fk_paymentstatus== 2 select x.billelectric_outstanding).FirstOrDefault();
                            var _resdient = (from x in toReturn where x.residentId == resident.resident_id.ToString() select x).FirstOrDefault();
                            _resdient.totatOutStandings = outstanding.ToString();
                        }
                    }
                    else
                    {
                        toReturn.Add(new ResidentResponseModel()
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
        public List<ResidentResponseModel> GetAllResidentsForSearchByPaNo(ResidentRequestModel model)
        {
            List<ResidentResponseModel> toReturn = new List<ResidentResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.residentPaNumber))
                {
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var residents = (from x in db.tbl_residents
                                         join y in db.tbl_residentbuilding on x.resident_id equals y.fk_resident
                                         join z in db.tbl_location on y.fk_building equals z.location_id
                                         join a in db.tbl_area on z.fk_area equals a.area_id
                                         where x.resident_panumber == model.residentPaNumber
                                         select new
                                         {
                                             x.resident_id,
                                             x.resident_name,
                                             x.resident_panumber,
                                             x.resident_rank,
                                             x.resident_remarks,
                                             x.resident_unit,
                                             z.location_id,
                                             z.location_name,
                                             a.area_id,
                                             a.area_name

                                         }).ToList();
                        if (residents.Count() > 0)
                        {
                            toReturn = residents.Select(resident => new ResidentResponseModel()
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
                            foreach (var resident in residents)
                            {
                                var outstanding = (from x in db.tbl_billelectric where x.fk_resident == resident.resident_id && x.fk_paymentstatus == 2 select x.billelectric_outstanding).FirstOrDefault();
                                var _resdient = (from x in toReturn where x.residentId == resident.resident_id.ToString() select x).FirstOrDefault();
                                _resdient.totatOutStandings = outstanding.ToString();
                            }
                        }
                        else
                        {
                            toReturn.Add(new ResidentResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new ResidentResponseModel()
                    {
                        remarks="Please Provide Pa No",
                        resultCode = "1300"
                    });
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
        public List<ResidentResponseModel> GetAllResidentsForSearchByRank(ResidentRequestModel model)
        {
            List<ResidentResponseModel> toReturn = new List<ResidentResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.residentRank))
                {
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var residents = (from x in db.tbl_residents
                                         join y in db.tbl_residentbuilding on x.resident_id equals y.fk_resident
                                         join z in db.tbl_location on y.fk_building equals z.location_id
                                         join a in db.tbl_area on z.fk_area equals a.area_id
                                         where x.resident_rank == model.residentRank
                                         select new
                                         {
                                             x.resident_id,
                                             x.resident_name,
                                             x.resident_panumber,
                                             x.resident_rank,
                                             x.resident_remarks,
                                             x.resident_unit,
                                             z.location_id,
                                             z.location_name,
                                             a.area_id,
                                             a.area_name

                                         }).ToList();
                        if (residents.Count() > 0)
                        {
                            toReturn = residents.Select(resident => new ResidentResponseModel()
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
                            foreach (var resident in residents)
                            {
                                var outstanding = (from x in db.tbl_billelectric where x.fk_resident == resident.resident_id && x.fk_paymentstatus == 2 select x.billelectric_outstanding).FirstOrDefault();
                                var _resdient = (from x in toReturn where x.residentId == resident.resident_id.ToString() select x).FirstOrDefault();
                                _resdient.totatOutStandings = outstanding.ToString();
                            }
                        }
                        else
                        {
                            toReturn.Add(new ResidentResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new ResidentResponseModel()
                    {
                        remarks = "Please Provide Rank",
                        resultCode = "1300"
                    });
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
        public List<ResidentResponseModel> GetAllResidentsForSearchByUnit(ResidentRequestModel model)
        {
            List<ResidentResponseModel> toReturn = new List<ResidentResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.residentUnit))
                {
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var residents = (from x in db.tbl_residents
                                         join y in db.tbl_residentbuilding on x.resident_id equals y.fk_resident
                                         join z in db.tbl_location on y.fk_building equals z.location_id
                                         join a in db.tbl_area on z.fk_area equals a.area_id
                                         where x.resident_unit == model.residentUnit
                                         select new
                                         {
                                             x.resident_id,
                                             x.resident_name,
                                             x.resident_panumber,
                                             x.resident_rank,
                                             x.resident_remarks,
                                             x.resident_unit,
                                             z.location_id,
                                             z.location_name,
                                             a.area_id,
                                             a.area_name

                                         }).ToList();
                        if (residents.Count() > 0)
                        {
                            toReturn = residents.Select(resident => new ResidentResponseModel()
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
                            foreach (var resident in residents)
                            {
                                var outstanding = (from x in db.tbl_billelectric where x.fk_resident == resident.resident_id && x.fk_paymentstatus == 2 select x.billelectric_outstanding).FirstOrDefault();
                                var _resdient = (from x in toReturn where x.residentId == resident.resident_id.ToString() select x).FirstOrDefault();
                                _resdient.totatOutStandings = outstanding.ToString();
                            }
                        }
                        else
                        {
                            toReturn.Add(new ResidentResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new ResidentResponseModel()
                    {
                        remarks = "Please Provide Unit",
                        resultCode = "1300"
                    });
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
        public List<ResidentResponseModel> GetAllResidentsForSearchByLastPayment(ResidentRequestModel model)
        {
            List<ResidentResponseModel> toReturn = new List<ResidentResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.paymentMonth))
                {
                    var date = DateTime.ParseExact(model.paymentMonth, "dd/MM/yyyy H:mm:ss", CultureInfo.InvariantCulture);
                    var month = new MonthFinderHelpers().GetMonth(date);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var residents = (from x in db.tbl_residents
                                         join y in db.tbl_residentbuilding on x.resident_id equals y.fk_resident
                                         join z in db.tbl_location on y.fk_building equals z.location_id
                                         join a in db.tbl_area on z.fk_area equals a.area_id
                                         where x.resident_unit == model.residentUnit
                                         select new
                                         {
                                             x.resident_id,
                                             x.resident_name,
                                             x.resident_panumber,
                                             x.resident_rank,
                                             x.resident_remarks,
                                             x.resident_unit,
                                             z.location_id,
                                             z.location_name,
                                             a.area_id,
                                             a.area_name,
                                         }).ToList();
                        if (residents.Count() > 0)
                        {
                            foreach (var resident in residents)
                            {
                                var residentLastPayment = (from x in db.tbl_residentpayments
                                                           where x.fk_resident == resident.resident_id
                                                           select x).OrderByDescending(x => x.payment_datetime).FirstOrDefault();
                                if (residentLastPayment.paymentmonth == month)
                                {
                                    toReturn.Add(new ResidentResponseModel()
                                    {
                                        residentName = resident.resident_name,
                                        residentPaNumber = resident.resident_panumber,
                                        residentRank = !string.IsNullOrEmpty(resident.resident_rank) ? resident.resident_rank : "",
                                        residentId = resident.resident_id.ToString(),
                                        residentRemarks = !string.IsNullOrEmpty(resident.resident_remarks) ? resident.resident_remarks : "",
                                        residentUnit = !string.IsNullOrEmpty(resident.resident_unit) ? resident.resident_unit : "",
                                        remarks = "Successfully Found",
                                        resultCode = "1100"
                                    });
                                }
                            }
                            foreach (var resident in residents)
                            {
                                var outstanding = (from x in db.tbl_billelectric where x.fk_resident == resident.resident_id && x.fk_paymentstatus == 2 select x.billelectric_outstanding).FirstOrDefault();
                                var _resdient = (from x in toReturn where x.residentId == resident.resident_id.ToString() select x).FirstOrDefault();
                                _resdient.totatOutStandings = outstanding.ToString();
                            }
                        }
                        else
                        {
                            toReturn.Add(new ResidentResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new ResidentResponseModel()
                    {
                        remarks = "Please Provide Month",
                        resultCode = "1300"
                    });
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
        public List<ResidentResponseModel> GetAllResidentsForSearchByAmount(ResidentRequestModel model)
        {
            List<ResidentResponseModel> toReturn = new List<ResidentResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.paymentAmount))
                {
                    double amount = double.Parse(model.paymentAmount);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var residents = (from x in db.tbl_residents
                                         join y in db.tbl_residentbuilding on x.resident_id equals y.fk_resident
                                         join z in db.tbl_location on y.fk_building equals z.location_id
                                         join a in db.tbl_area on z.fk_area equals a.area_id
                                         where x.resident_unit == model.residentUnit
                                         select new
                                         {
                                             x.resident_id,
                                             x.resident_name,
                                             x.resident_panumber,
                                             x.resident_rank,
                                             x.resident_remarks,
                                             x.resident_unit,
                                             z.location_id,
                                             z.location_name,
                                             a.area_id,
                                             a.area_name,
                                         }).ToList();
                        if (residents.Count() > 0)
                        {
                            foreach (var resident in residents)
                            {
                                var residentLastPayment = (from x in db.tbl_residentpayments
                                                           where x.fk_resident == resident.resident_id
                                                           select x).OrderByDescending(x => x.payment_datetime).FirstOrDefault();
                                if (residentLastPayment.payment_amount == amount)
                                {
                                    toReturn.Add(new ResidentResponseModel()
                                    {
                                        residentName = resident.resident_name,
                                        residentPaNumber = resident.resident_panumber,
                                        residentRank = !string.IsNullOrEmpty(resident.resident_rank) ? resident.resident_rank : "",
                                        residentId = resident.resident_id.ToString(),
                                        residentRemarks = !string.IsNullOrEmpty(resident.resident_remarks) ? resident.resident_remarks : "",
                                        residentUnit = !string.IsNullOrEmpty(resident.resident_unit) ? resident.resident_unit : "",
                                        remarks = "Successfully Found",
                                        resultCode = "1100"
                                    });
                                }
                            }
                            foreach (var resident in residents)
                            {
                                var outstanding = (from x in db.tbl_billelectric where x.fk_resident == resident.resident_id && x.fk_paymentstatus == 2 select x.billelectric_outstanding).FirstOrDefault();
                                var _resdient = (from x in toReturn where x.residentId == resident.resident_id.ToString() select x).FirstOrDefault();
                                _resdient.totatOutStandings = outstanding.ToString();
                            }
                        }
                        else
                        {
                            toReturn.Add(new ResidentResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new ResidentResponseModel()
                    {
                        remarks = "Please Provide Month",
                        resultCode = "1300"
                    });
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
        public List<ResidentResponseModel> GetAllResidentsForSearchByName(ResidentRequestModel model)
        {
            List<ResidentResponseModel> toReturn = new List<ResidentResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.residentName))
                {
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var residents = (from x in db.tbl_residents
                                         join y in db.tbl_residentbuilding on x.resident_id equals y.fk_resident
                                         join z in db.tbl_location on y.fk_building equals z.location_id
                                         join a in db.tbl_area on z.fk_area equals a.area_id
                                         where x.resident_panumber == model.residentName
                                         select new
                                         {
                                             x.resident_id,
                                             x.resident_name,
                                             x.resident_panumber,
                                             x.resident_rank,
                                             x.resident_remarks,
                                             x.resident_unit,
                                             z.location_id,
                                             z.location_name,
                                             a.area_id,
                                             a.area_name

                                         }).ToList();
                        if (residents.Count() > 0)
                        {
                            toReturn = residents.Select(resident => new ResidentResponseModel()
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
                            foreach (var resident in residents)
                            {
                                var outstanding = (from x in db.tbl_billelectric where x.fk_resident == resident.resident_id && x.fk_paymentstatus == 2 select x.billelectric_outstanding).FirstOrDefault();
                                var _resdient = (from x in toReturn where x.residentId == resident.resident_id.ToString() select x).FirstOrDefault();
                                _resdient.totatOutStandings = outstanding.ToString();
                            }
                        }
                        else
                        {
                            toReturn.Add(new ResidentResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new ResidentResponseModel()
                    {
                        remarks = "Please Provide Name",
                        resultCode = "1300"
                    });
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
        public List<ResidentResponseModel> GetAllResidentsForSearchByElectricMeterNo(ResidentRequestModel model)
        {
            List<ResidentResponseModel> toReturn = new List<ResidentResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.meterNo))
                {
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var residents = (from x in db.tbl_residents
                                         join y in db.tbl_residentbuilding on x.resident_id equals y.fk_resident
                                         join z in db.tbl_location on y.fk_building equals z.location_id
                                         join a in db.tbl_area on z.fk_area equals a.area_id
                                         where z.location_electricmeter == model.meterNo
                                         select new
                                         {
                                             x.resident_id,
                                             x.resident_name,
                                             x.resident_panumber,
                                             x.resident_rank,
                                             x.resident_remarks,
                                             x.resident_unit,
                                             z.location_id,
                                             z.location_name,
                                             a.area_id,
                                             a.area_name

                                         }).ToList();
                        if (residents.Count() > 0)
                        {
                            toReturn = residents.Select(resident => new ResidentResponseModel()
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
                            foreach (var resident in residents)
                            {
                                var outstanding = (from x in db.tbl_billelectric where x.fk_resident == resident.resident_id && x.fk_paymentstatus == 2 select x.billelectric_outstanding).FirstOrDefault();
                                var _resdient = (from x in toReturn where x.residentId == resident.resident_id.ToString() select x).FirstOrDefault();
                                _resdient.totatOutStandings = outstanding.ToString();
                            }
                        }
                        else
                        {
                            toReturn.Add(new ResidentResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new ResidentResponseModel()
                    {
                        remarks = "Please Provide Name",
                        resultCode = "1300"
                    });
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
        public List<ResidentResponseModel> GetAllResidentsForSearchByGasMeterNo(ResidentRequestModel model)
        {
            List<ResidentResponseModel> toReturn = new List<ResidentResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(model.meterNo))
                {
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var residents = (from x in db.tbl_residents
                                         join y in db.tbl_residentbuilding on x.resident_id equals y.fk_resident
                                         join z in db.tbl_location on y.fk_building equals z.location_id
                                         join a in db.tbl_area on z.fk_area equals a.area_id
                                         where z.location_gassmeter == model.meterNo
                                         select new
                                         {
                                             x.resident_id,
                                             x.resident_name,
                                             x.resident_panumber,
                                             x.resident_rank,
                                             x.resident_remarks,
                                             x.resident_unit,
                                             z.location_id,
                                             z.location_name,
                                             a.area_id,
                                             a.area_name

                                         }).ToList();
                        if (residents.Count() > 0)
                        {
                            toReturn = residents.Select(resident => new ResidentResponseModel()
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
                            foreach (var resident in residents)
                            {
                                var outstanding = (from x in db.tbl_billelectric where x.fk_resident == resident.resident_id && x.fk_paymentstatus == 2 select x.billelectric_outstanding).FirstOrDefault();
                                var _resdient = (from x in toReturn where x.residentId == resident.resident_id.ToString() select x).FirstOrDefault();
                                _resdient.totatOutStandings = outstanding.ToString();
                            }
                        }
                        else
                        {
                            toReturn.Add(new ResidentResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            });
                        }
                    }
                }
                else
                {
                    toReturn.Add(new ResidentResponseModel()
                    {
                        remarks = "Please Provide Name",
                        resultCode = "1300"
                    });
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
        public List<ResidentResponseModel> GetAllResidentsByArea(ResidentRequestModel model)
        {
            List<ResidentResponseModel> toReturn = new List<ResidentResponseModel>();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.areaId))
                {
                    var areaId = int.Parse(model.areaId);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var residents = (from l in db.tbl_location
                                         join a in db.tbl_area on l.fk_area equals a.area_id
                                         join rl in db.tbl_residentbuilding on l.location_id equals rl.fk_building
                                         join r in db.tbl_residents on rl.fk_resident equals r.resident_id
                                         where a.area_id == areaId
                                         select new 
                                         {
                                             r.resident_id,
                                             r.resident_name,
                                             r.resident_panumber,
                                             r.resident_rank,
                                             r.resident_remarks,
                                             r.resident_unit,
                                             l.location_id,
                                             l.location_name,
                                             a.area_id,
                                             a.area_name

                                         }).ToList();

                        if (residents.Count() > 0)
                        {
                            toReturn = residents.Select(resident => new ResidentResponseModel()
                            {
                                residentName = resident.resident_name,
                                residentPaNumber = resident.resident_panumber,
                                residentRank = !string.IsNullOrEmpty(resident.resident_rank) ? resident.resident_rank : "",
                                residentId = resident.resident_id.ToString(),
                                residentRemarks = !string.IsNullOrEmpty(resident.resident_remarks) ? resident.resident_remarks : "",
                                residentUnit = !string.IsNullOrEmpty(resident.resident_unit) ? resident.resident_unit : "",
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(new ResidentResponseModel()
                            {
                                remarks = "No Record Found",
                                resultCode = "1200"
                            });
                        }
                    } 
                }
                else
                {
                    toReturn.Add(new ResidentResponseModel()
                    {
                        remarks="Please Provide Area",
                        resultCode= "1300"
                    });
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
        public ResidentResponseModel SelectDetailInfoByResident(ResidentRequestModel model)
        {
            ResidentResponseModel toReturn = new ResidentResponseModel();
            try
            {
                if (new ModelsValidatorHelper().validateint(model.areaId))
                {
                    var areaId = int.Parse(model.residentId);
                    using (db_bmsEntities db = new db_bmsEntities())
                    {
                        var resident = (from l in db.tbl_location
                                         join a in db.tbl_area on l.fk_area equals a.area_id
                                         join rl in db.tbl_residentbuilding on l.location_id equals rl.fk_building
                                         join r in db.tbl_residents on rl.fk_resident equals r.resident_id
                                         where a.area_id == areaId
                                         select new
                                         {
                                             r.resident_name,
                                             r.resident_id,
                                             l.location_electricmeter,
                                         }).FirstOrDefault();

                        if (resident!=null)
                        {
                            toReturn = new ResidentResponseModel()
                            {
                                residentName = resident.resident_name,
                                residentId = resident.resident_id.ToString(),
                                meterNo = resident.location_electricmeter,
                                remarks = "Successfully Found",
                                resultCode = "1100"
                            };
                            var bill = (from x in db.tbl_billelectric where x.fk_resident == resident.resident_id select x).OrderByDescending(x => x.billelectric_datetime).FirstOrDefault();
                            toReturn.previousReading = bill.billelectric_prevreading.ToString();
                            toReturn.outstanding = bill.billelectric_outstanding.ToString();
                           
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
                }
                else
                {
                    toReturn = new ResidentResponseModel()
                    {
                        remarks = "Please Provide Area",
                        resultCode = "1300"
                    };
                }

            }
            catch (Exception Ex)
            {
                toReturn =new ResidentResponseModel()
                {
                    remarks = "There Was A fatal Error" + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public List<ResidentResponseModel>  GetAllResidentsDetailInfo(ResidentRequestModel model)
        {
            List<ResidentResponseModel> toReturn = new List<ResidentResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var residents = (from l in db.tbl_location
                                    join a in db.tbl_area on l.fk_area equals a.area_id
                                    join rl in db.tbl_residentbuilding on l.location_id equals rl.fk_building
                                    join r in db.tbl_residents on rl.fk_resident equals r.resident_id
                                    select new
                                    {
                                        r.resident_name,
                                        r.resident_id,
                                        l.location_electricmeter,
                                        r.resident_panumber,
                                        r.resident_rank,
                                        r.resident_remarks,
                                        r.resident_unit
                                    }).ToList();

                    if (residents.Count()> 0)
                    {
                        toReturn = residents.Select(resident => new ResidentResponseModel()
                        {
                            residentName = resident.resident_name,
                            residentId = resident.resident_id.ToString(),
                            meterNo = resident.location_electricmeter,
                            residentPaNumber = !string.IsNullOrEmpty(resident.resident_panumber)?resident.resident_panumber:"",
                            residentRank = !string.IsNullOrEmpty(resident.resident_rank)? resident.resident_rank:"",
                            residentRemarks = !string.IsNullOrEmpty(resident.resident_remarks)?resident.resident_remarks:"",
                            residentUnit = !string.IsNullOrEmpty(resident.resident_unit)?resident.resident_unit:"",
                            remarks = "Successfully Found",
                            resultCode = "1100"
                        }).ToList();
                        foreach (var _resident in toReturn)
                        {
                            var residentId = int.Parse(_resident.residentId);
                            var bill = (from x in db.tbl_billelectric where x.fk_resident == residentId select x).OrderByDescending(x => x.billelectric_datetime).FirstOrDefault();
                            _resident.previousReading = bill.billelectric_prevreading!=  null?bill.billelectric_prevreading.ToString():"0";
                            _resident.outstanding = bill.billelectric_outstanding!= null ? bill.billelectric_outstanding.ToString():"0";
                        }

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
                toReturn.Add( new ResidentResponseModel()
                {
                    remarks = "There Was A fatal Error" + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
    }
}