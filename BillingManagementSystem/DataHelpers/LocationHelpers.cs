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
                    if(new ModelsValidatorHelper().validateint(model.fk_area))
                    {
                        if (!string.IsNullOrEmpty(model.locationName))
                        {
                            var existingLocation = (from x in db.tbl_location where x.location_electricmeter == model.locationElectricMeter select x).FirstOrDefault();
                            if (existingLocation == null)
                            {
                                var newLocation = new tbl_location()
                                {
                                    fk_area = int.Parse(model.fk_area),
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
                            location.fk_area = !string.IsNullOrEmpty(model.fk_area) ? int.Parse(model.fk_area) : location.fk_area;
                            location.location_electricmeter = !string.IsNullOrEmpty(model.locationElectricMeter)?model.locationElectricMeter:location.location_electricmeter;
                            location.location_gassmeter = !string.IsNullOrEmpty(model.locationGassMeter)?model.locationGassMeter:location.location_gassmeter;
                            location.location_name = !string.IsNullOrEmpty(model.locationName) ? model.locationName :location.location_name ;
                            location.location_wapdameter = !string.IsNullOrEmpty(model.locationWapdaMeter)?model.locationWapdaMeter:location.location_wapdameter;
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
                                        join y in db.tbl_area on x.fk_area equals y.area_id
                                        where x.location_id == locationId 
                                        select new 
                                        {
                                            x.fk_area,
                                            x.location_electricmeter,
                                            x.location_gassmeter,
                                            x.location_id,
                                            x.location_name,
                                            x.location_wapdameter,
                                            y.area_name
                                        }).FirstOrDefault();
                        if (location != null)
                        {
                            toReturn = new LocationResponseModel()
                            {
                                fk_area = location.fk_area.ToString(),
                                locationElectricMeter = !string.IsNullOrEmpty(location.location_electricmeter) ? location.location_electricmeter : "",
                                locationGassMeter = !string.IsNullOrEmpty(location.location_gassmeter) ? location.location_gassmeter : "",
                                locationName = !string.IsNullOrEmpty(location.location_name) ? location.location_name : "",
                                locationWapdaMeter = !string.IsNullOrEmpty(location.location_wapdameter) ? location.location_wapdameter : "",
                                locationId = location.location_id.ToString(),
                                remarks ="Successfully Location Found",
                                resultCode= "1100"
                            };
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
        public List<LocationResponseModel>  GetAllLocationsByArea(LocationRequestModel model)
        {
            List<LocationResponseModel>  toReturn = new List<LocationResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    if (new ModelsValidatorHelper().validateint(model.fk_area))
                    {
                        int areaId = int.Parse(model.fk_area);
                        var locations = (from x in db.tbl_location
                                        join y in db.tbl_area on x.fk_area equals y.area_id
                                        where x.location_id == areaId
                                        select new
                                        {
                                            x.fk_area,
                                            x.location_electricmeter,
                                            x.location_gassmeter,
                                            x.location_id,
                                            x.location_name,
                                            x.location_wapdameter,
                                            y.area_name
                                        }).ToList();
                        if (locations.Count()>0)
                        {
                            toReturn= locations.Select(location => new LocationResponseModel()
                            {
                                fk_area = location.fk_area.ToString(),
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
                                        join y in db.tbl_area on x.fk_area equals y.area_id
                                        select new
                                        {
                                            x.fk_area,
                                            x.location_electricmeter,
                                            x.location_gassmeter,
                                            x.location_id,
                                            x.location_name,
                                            x.location_wapdameter,
                                            y.area_name
                                        }).ToList();
                    if (locations.Count() > 0)
                    {
                        toReturn = locations.Select(location => new LocationResponseModel()
                        {
                            fk_area = location.fk_area.ToString(),
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
    }
}