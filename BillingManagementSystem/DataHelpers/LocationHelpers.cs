using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.DataHelpers
{
    public class LocationHelpers
    {
        public LocationResponseModel AddLocation(LocationRequestModel model)
        {
            LocationResponseModel toReturn = new LocationResponseModel();
            try
            {
                using(db_bmsEntities db = new db_bmsEntities())
                {
                    if(new ModelsValidatorHelper().validateint(model.fk_subArea))
                    {
                        if (!string.IsNullOrEmpty(model.locationName))
                        {
                            var existingLocation = (from x in db.tbl_location where x.location_electricmeter == model.locationElectricMeter select x).FirstOrDefault();
                            if (existingLocation == null)
                            {
                                var newLocation = new tbl_location()
                                {
                                    fk_subarea = int.Parse(model.fk_subArea),
                                    location_electricmeter = !string.IsNullOrEmpty(model.locationElectricMeter) ? model.locationElectricMeter : "",
                                    location_gassmeter = !string.IsNullOrEmpty(model.locationGassMeter) ? model.locationGassMeter : "",
                                    location_name = model.locationName,
                                    location_wapdameter = !string.IsNullOrEmpty(model.locationWapdaMeter) ? model.locationWapdaMeter : "",
                                };
                                db.tbl_location.Add(newLocation);
                                db.SaveChanges();
                                toReturn = new LocationResponseModel()
                                {
                                    remarks = "Location Added Successfully",
                                    resultCode = "1100"
                                };
                            }
                            else
                            {
                                toReturn = new LocationResponseModel()
                                {
                                    remarks = "Meter No Already Exist",
                                    resultCode = "1400 "
                                };
                            }
                        }
                        else
                        {
                            toReturn = new LocationResponseModel()
                            {
                                remarks="Please Enter Location Name",
                                resultCode="1300"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new LocationResponseModel()
                        {
                            resultCode="1300",
                            remarks="Please Provide Area"
                        };
                    }

                }
            }
            catch(Exception Ex)
            {
                toReturn = new LocationResponseModel()
                {
                    remarks = "There Was a Fatal Error " + Ex.ToString(),
                    resultCode ="1000"
                };
            }
            return toReturn;
        }
        public LocationResponseModel EditLocation(LocationRequestModel model)
        {
            LocationResponseModel toReturn = new LocationResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.locationId))
                    {
                        int locationId = int.Parse(model.locationId);
                        var location = (from x in db.tbl_location where x.location_id == locationId select x).FirstOrDefault();
                        if(location != null)
                        {
                            var existingLocation = new tbl_location();
                            if (!string.IsNullOrEmpty(model.locationElectricMeter))
                            {
                                existingLocation = (from x in db.tbl_location where x.location_electricmeter == model.locationElectricMeter && x.location_id != locationId select x).FirstOrDefault(); 
                            }
                            else
                            {
                                existingLocation = null;
                            }
                            if (existingLocation == null)
                            {
                                location.fk_subarea = !string.IsNullOrEmpty(model.fk_subArea) ? int.Parse(model.fk_subArea) : location.fk_subarea;
                                location.location_electricmeter = !string.IsNullOrEmpty(model.locationElectricMeter) ? model.locationElectricMeter : location.location_electricmeter;
                                location.location_gassmeter = !string.IsNullOrEmpty(model.locationGassMeter) ? model.locationGassMeter : location.location_gassmeter;
                                location.location_name = !string.IsNullOrEmpty(model.locationName) ? model.locationName : location.location_name;
                                location.location_wapdameter = !string.IsNullOrEmpty(model.locationWapdaMeter) ? model.locationWapdaMeter : location.location_wapdameter;
                                db.SaveChanges();
                                toReturn = new LocationResponseModel()
                                {
                                    remarks = "Location Updated Successfully",
                                    resultCode = "1100"
                                };
                            }
                            else
                            {
                                toReturn = new LocationResponseModel()
                                {
                                    resultCode = "1400",
                                    remarks = "Location Name Already Exists"
                                };                            
                            }
                        }
                        else
                        {
                            toReturn = new LocationResponseModel()
                            {
                                remarks="No Record Found",
                                resultCode="1200"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new LocationResponseModel()
                        {
                            resultCode = "1300",
                            remarks = "Please Provide Location"
                        };
                    }

                }
            }
            catch (Exception Ex)
            {
                toReturn = new LocationResponseModel()
                {
                    remarks = "There Was a Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public LocationResponseModel DeleteLocation(LocationRequestModel model)
        {
            LocationResponseModel toReturn = new LocationResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.locationId))
                    {
                        int locationId = int.Parse(model.locationId);
                        var location = (from x in db.tbl_location where x.location_id == locationId select x).FirstOrDefault();
                        if (location != null)
                        {
                            db.tbl_location.Remove(location);
                            db.SaveChanges();
                            toReturn = new LocationResponseModel()
                            {
                                remarks = "Location Deleted Successfully",
                                resultCode = "1100"
                            };
                        }
                        else
                        {
                            toReturn = new LocationResponseModel()
                            {
                                remarks="No Record Found",
                                resultCode="1200"
                            };
                        }

                    }
                    else
                    {
                        toReturn = new LocationResponseModel()
                        {
                            resultCode = "1300",
                            remarks = "Please Provide Location"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new LocationResponseModel()
                {
                    remarks = "There Was a Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public LocationResponseModel GetLocationById(LocationRequestModel model)
        {
            LocationResponseModel toReturn = new LocationResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.locationId))
                    {
                        int locationId = int.Parse(model.locationId);
                        var location = (from x in db.tbl_location
                                        join y in db.tbl_subarea on x.fk_subarea equals y.subarea_id
                                        join rb in db.tbl_residentbuilding on x.location_id equals rb.fk_building
                                        where x.location_id == locationId 
                                        select new 
                                        {
                                            x.fk_subarea,
                                            x.location_electricmeter,
                                            x.location_gassmeter,
                                            x.location_id,
                                            x.location_name,
                                            x.location_wapdameter,
                                            rb.fk_resident,
                                            y.subarea_name
                                        }).FirstOrDefault();
                        if (location != null)
                        {
                            toReturn = new LocationResponseModel()
                            {
                                fk_subArea = location.fk_subarea.ToString(),
                                locationElectricMeter = !string.IsNullOrEmpty(location.location_electricmeter) ? location.location_electricmeter : "",
                                locationGassMeter = !string.IsNullOrEmpty(location.location_gassmeter) ? location.location_gassmeter : "",
                                locationName = !string.IsNullOrEmpty(location.location_name) ? location.location_name : "",
                                locationWapdaMeter = !string.IsNullOrEmpty(location.location_wapdameter) ? location.location_wapdameter : "",
                                locationId = location.location_id.ToString(),
                                residentId = location.fk_resident.ToString(),
                                remarks ="Successfully Location Found",
                                resultCode= "1100"
                            };
                            var bill = (from x in db.tbl_billelectric where x.fk_location == locationId select x).OrderByDescending(x => x.billelectric_datetime).FirstOrDefault();
                            var billGas = (from x in db.tbl_billgas where x.fk_location == locationId select x).OrderByDescending(x => x.datetime).FirstOrDefault();
                            if (bill != null)
                            {
                                toReturn.previousReading = !String.IsNullOrEmpty(bill.billelectric_prevreading.ToString()) ? bill.billelectric_prevreading.ToString() : "";
                                toReturn.outstanding = !String.IsNullOrEmpty(bill.billelectric_outstanding.ToString()) ? bill.billelectric_outstanding.ToString() : "";
                                toReturn.billMonth = !string.IsNullOrEmpty(bill.billelectric_month) ? bill.billelectric_month : "";
                                toReturn.currentReading = bill.billelectric_currentreading.ToString();
                                toReturn.currentUnit = bill.billelectric_units.ToString();
                            }
                            else
                            {
                                toReturn.previousReading = "0";
                                toReturn.outstanding = "0";
                                toReturn.billMonth = "0";
                                toReturn.currentReading = "0";
                                toReturn.currentUnit = "0";

                            }
                            if(billGas!= null)
                            {
                                toReturn.previousGasReading = billGas.prevreading.ToString();
                                toReturn.gasOutstanding = billGas.outstanding.ToString();
                                toReturn.billGasMonth = billGas.month.ToString();
                                toReturn.currentGasReading = billGas.currentreading.ToString();
                                toReturn.currentGasUnit = billGas.units.ToString();
                            }
                            else
                            {
                                toReturn.previousGasReading = "0";
                                toReturn.gasOutstanding = "0";
                                toReturn.billGasMonth = "0";
                                toReturn.currentGasReading = "0";
                                toReturn.currentGasUnit = "0";
                            }
                        }
                        else 
                        {
                            toReturn = new LocationResponseModel()
                            {
                                resultCode = "1200",
                                remarks = "No Record Found"
                            };
                        }
                    }
                    else
                    {
                        toReturn = new LocationResponseModel()
                        {
                            resultCode = "1300",
                            remarks = "Please Provide Location"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn = new LocationResponseModel()
                {
                    remarks = "There Was a Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public List<LocationResponseModel>  GetAllLocationsBySubArea(LocationRequestModel model)
        {
            List<LocationResponseModel>  toReturn = new List<LocationResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.fk_subArea))
                    {
                        int areaId = int.Parse(model.fk_subArea);
                        var locations = (from x in db.tbl_location
                                        join y in db.tbl_subarea on x.fk_subarea equals y.subarea_id
                                        where x.fk_subarea == areaId
                                        select new
                                        {
                                            x.fk_subarea,
                                            x.location_electricmeter,
                                            x.location_gassmeter,
                                            x.location_id,
                                            x.location_name,
                                            x.location_wapdameter,
                                            y.subarea_name
                                        }).ToList();
                        if (locations.Count()>0)
                        {
                            toReturn= locations.Select(location => new LocationResponseModel()
                            {
                                fk_subArea = location.fk_subarea.ToString(),
                                locationElectricMeter = !string.IsNullOrEmpty(location.location_electricmeter) ? location.location_electricmeter : "",
                                locationGassMeter = !string.IsNullOrEmpty(location.location_gassmeter) ? location.location_gassmeter : "",
                                locationName = !string.IsNullOrEmpty(location.location_name) ? location.location_name : "",
                                locationWapdaMeter = !string.IsNullOrEmpty(location.location_wapdameter) ? location.location_wapdameter : "",
                                locationId = location.location_id.ToString(),
                                remarks = "Successfully Location Found",
                                resultCode = "1100"
                            }).ToList();
                        }
                        else
                        {
                            toReturn.Add(new LocationResponseModel()
                            {
                                resultCode = "1200",
                                remarks = "No Record Found"
                            });
                        }
                    }
                    else
                    {
                        toReturn.Add( new LocationResponseModel()
                        {
                            resultCode = "1300",
                            remarks = "Please Provide Area"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new LocationResponseModel()
                {
                    remarks = "There Was a Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public List<LocationResponseModel> GetAllLocations(LocationRequestModel model)
        {
            List<LocationResponseModel>  toReturn = new List<LocationResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var locations = (from x in db.tbl_location
                                        join y in db.tbl_subarea on x.fk_subarea equals y.subarea_id
                                        join z in db.tbl_area on y.fk_area equals z.area_id
                                        select new
                                        {
                                            x.fk_subarea,
                                            x.location_electricmeter,
                                            x.location_gassmeter,
                                            x.location_id,
                                            x.location_name,
                                            x.location_wapdameter,
                                            y.subarea_name,
                                            z.area_id,
                                            z.area_name
                                        }).ToList();
                    if (locations.Count() > 0)
                    {
                        toReturn = locations.Select(location => new LocationResponseModel()
                        {
                            fk_area = location.area_id.ToString(),
                            areaName = !string.IsNullOrEmpty(location.area_name)?location.area_name:"",
                            fk_subArea = location.fk_subarea.ToString(),
                            locationElectricMeter = !string.IsNullOrEmpty(location.location_electricmeter) ? location.location_electricmeter : "",
                            locationGassMeter = !string.IsNullOrEmpty(location.location_gassmeter) ? location.location_gassmeter : "",
                            locationName = !string.IsNullOrEmpty(location.location_name) ? location.location_name : "",
                            locationWapdaMeter = !string.IsNullOrEmpty(location.location_wapdameter) ? location.location_wapdameter : "",
                            locationId = location.location_id.ToString(),
                            remarks = "Successfully Location Found",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add(new LocationResponseModel()
                        {
                            resultCode = "1200",
                            remarks = "No Record Found"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new LocationResponseModel()
                {
                    remarks = "There Was a Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public List<LocationResponseModel> GetAllAvailableLocationsBySubArea(LocationRequestModel model)
        {
            List<LocationResponseModel> toReturn = new List<LocationResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.fk_subArea))
                    {
                        int areaId = int.Parse(model.fk_subArea);
                        var residentBuildings = (from x in db.tbl_residentbuilding select x).ToList();
                        var locations = (from x in db.tbl_location
                                         join y in db.tbl_subarea on x.fk_subarea equals y.subarea_id
                                         where x.fk_subarea == areaId
                                         select new
                                         {
                                             x.fk_subarea,
                                             x.location_electricmeter,
                                             x.location_gassmeter,
                                             x.location_id,
                                             x.location_name,
                                             x.location_wapdameter,
                                             y.subarea_name
                                         }).ToList();
                        if (locations.Count() > 0)
                        {
                            toReturn = locations.Select(location => new LocationResponseModel()
                            {
                                fk_subArea = location.fk_subarea.ToString(),
                                locationElectricMeter = !string.IsNullOrEmpty(location.location_electricmeter) ? location.location_electricmeter : "",
                                locationGassMeter = !string.IsNullOrEmpty(location.location_gassmeter) ? location.location_gassmeter : "",
                                locationName = !string.IsNullOrEmpty(location.location_name) ? location.location_name : "",
                                locationWapdaMeter = !string.IsNullOrEmpty(location.location_wapdameter) ? location.location_wapdameter : "",
                                locationId = location.location_id.ToString(),
                                remarks = "Successfully Location Found",
                                resultCode = "1100"
                            }).ToList();
                            //foreach (var location in locations)
                            //{
                            //    if (residentBuildings.Contains(new tbl_residentbuilding() { fk_resident = location.location_id });
                            //    foreach (var residentBuilding in residentBuildings)
                            //    {
                            //        if (residentBuilding.fk_building)
                            //        {
                            //            toReturn.Remove(location);
                            //        }
                            //    }
                            //}
                            if (toReturn.Count == 0)
                            {
                                toReturn.Add(new LocationResponseModel()
                                {
                                    remarks = "No Available location Found",
                                    resultCode = "1200"
                                });
                            }
                        }
                        else
                        {
                            toReturn.Add(new LocationResponseModel()
                            {
                                resultCode = "1200",
                                remarks = "No Record Found"
                            });
                        }
                    }
                    else
                    {
                        toReturn.Add(new LocationResponseModel()
                        {
                            resultCode = "1300",
                            remarks = "Please Provide Sub Area"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new LocationResponseModel()
                {
                    remarks = "There Was a Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
    }
}