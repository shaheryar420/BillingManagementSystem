using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;
using BillingManagementSystem.Models.RequestModels;

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
                            var existingLocation = (from x in db.tbl_location where x.location_name == model.locationName select x).FirstOrDefault();
                            if (existingLocation == null)
                            {
                                var newLocation = new tbl_location()
                                {
                                    fk_subarea = int.Parse(model.fk_subArea),
                                    location_name = model.locationName,
                                    location_tv_charges= !string.IsNullOrEmpty(model.locationTvStatus)?int.Parse(model.locationTvStatus):0,
                                    location_water_charges= !string.IsNullOrEmpty(model.locationWaterStatus)?int.Parse(model.locationWaterStatus):0,
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
                            if (!string.IsNullOrEmpty(model.locationName))
                            {
                                existingLocation = (from x in db.tbl_location where x.location_name == model.locationName && x.location_id != locationId select x).FirstOrDefault(); 
                            }
                            else
                            {
                                existingLocation = null;
                            }
                            if (existingLocation == null)
                            {
                                location.fk_subarea = !string.IsNullOrEmpty(model.fk_subArea) ? int.Parse(model.fk_subArea) : location.fk_subarea;
                                location.location_name = !string.IsNullOrEmpty(model.locationName) ? model.locationName : location.location_name;
                                location.location_tv_charges = !string.IsNullOrEmpty(model.locationTvStatus) ? int.Parse(model.locationTvStatus) : location.location_tv_charges;
                                location.location_water_charges = !string.IsNullOrEmpty(model.locationWaterStatus) ? int.Parse(model.locationWaterStatus) : location.location_water_charges;
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
                                            x.location_id,
                                            x.location_name,
                                            rb.fk_resident,
                                            y.subarea_name
                                        }).FirstOrDefault();
                        if (location != null)
                        {
                            toReturn = new LocationResponseModel()
                            {
                                subAreaName = location.subarea_name,
                                fk_subArea = location.fk_subarea.ToString(),
                                locationName = !string.IsNullOrEmpty(location.location_name) ? location.location_name : "",
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
                                        join z in db.tbl_area on y.fk_area equals z.area_id
                                        where x.fk_subarea == areaId
                                        select new
                                        {

                                            x.fk_subarea,
                                            x.location_id,
                                            x.location_name,
                                            y.subarea_name,
                                            z.area_name,
                                            z.area_id
                                        }).ToList();
                        if (locations.Count()>0)
                        {
                            foreach (var location in locations)
                            {
                                var Consumers = (from z in db.tbl_location 
                                                 join r in db.tbl_residentbuilding on z.location_id equals r.fk_building
                                                 join re in db.tbl_residents on r.fk_resident equals re.resident_id
                                                 where z.location_id == location.location_id
                                                 select new
                                                 {
                                                     re.resident_id
                                                 }
                                              ).ToList();
                                var noOfConsumers = Consumers.Count();
                                double totalUnits = 0;
                                double totalAmount = 0;
                                double gasUnits = 0;
                                double gasAmount = 0;
                                var bills = new SubDataHelpers.RORSubHelpers().GetAllRORByLocation(location.location_id);

                                if (bills[0].resultCode == "1100")
                                {
                                    foreach (var bill in bills)
                                    {
                                        totalAmount = totalAmount + double.Parse(bill.billAmount) + double.Parse(bill.billSecondaryAmount);
                                        totalUnits = totalUnits + double.Parse(bill.billPrimaryUnits) + double.Parse(bill.billSecondaryUnits);
                                        gasAmount = gasAmount + double.Parse(bill.billGasAmount);
                                        gasUnits = gasUnits + double.Parse(bill.billGasMMBTU);
                                    }
                                }
                                var _locoation = new LocationResponseModel()
                                {
                                    totalGasAmount = gasAmount.ToString(),
                                    totalGasUnits = gasUnits.ToString(),
                                    totalElectricAmount = totalAmount.ToString(),
                                    totalElectricUnits = totalUnits.ToString(),
                                    noOfConsumers = noOfConsumers.ToString(),
                                    fk_subArea = location.fk_subarea.ToString(),
                                    areaName = !string.IsNullOrEmpty(location.area_name) ? location.area_name : "",
                                    subAreaName = location.subarea_name,
                                    fk_area = location.area_id.ToString(),
                                    locationName = !string.IsNullOrEmpty(location.location_name) ? location.location_name : "",
                                    locationId = location.location_id.ToString(),
                                    remarks = "Locations Found SuccessFully",
                                    resultCode = "1100"
                                };
                                toReturn.Add(_locoation) ;
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
                        toReturn.Add( new LocationResponseModel()
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
                                            x.location_id,
                                            x.location_name,
                                            x.location_tv_charges,
                                            x.location_water_charges,
                                            y.subarea_name,
                                            z.area_id,
                                            z.area_name
                                        }).ToList();
                    if (locations.Count() > 0)
                    {
                        toReturn = locations.Select(location => new LocationResponseModel()
                        {
                            subAreaName = location.subarea_name,
                            fk_area = location.area_id.ToString(),
                            tvChargesStatus= location.location_tv_charges.ToString(),
                            waterChargesStatus= location.location_water_charges.ToString(),
                            areaName = !string.IsNullOrEmpty(location.area_name)?location.area_name:"",
                            fk_subArea = location.fk_subarea.ToString(),
                            locationName = !string.IsNullOrEmpty(location.location_name) ? location.location_name : "",
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
                        int subAreaId = int.Parse(model.fk_subArea);
                        var residentBuildings = (from x in db.tbl_residentbuilding select x).ToList();
                        var consummerPool = db.tbl_consummer_pool.ToList();
                        var locations = (from x in db.tbl_location
                                         join y in db.tbl_subarea on x.fk_subarea equals y.subarea_id
                                         where x.fk_subarea == subAreaId
                                         select new
                                         {
                                             x.fk_subarea,
                                             x.location_id,
                                             x.location_name,
                                             y.subarea_name
                                         }).ToList();
                        if (locations.Count() > 0)
                        {
                            toReturn = locations.Select(location => new LocationResponseModel()
                            {
                                subAreaName = location.subarea_name,
                                fk_subArea = location.fk_subarea.ToString(),
                                locationName = !string.IsNullOrEmpty(location.location_name) ? location.location_name : "",
                                locationId = location.location_id.ToString(),
                                remarks = "Successfully Location Found",
                                resultCode = "1100"
                            }).ToList();
                            var alreadyAssignedLocations = new List<LocationResponseModel>();
                            foreach (var location in toReturn)
                            {
                                int locationId = int.Parse(location.locationId);
                                foreach (var residentBuilding in residentBuildings)
                                {
                                    if (residentBuilding.fk_building== locationId)
                                    {
                                        var helper = new ConsummerPoolHelpers();
                                        var request = new ConsummerPoolRequestModel()
                                        {
                                            fk_location = residentBuilding.fk_building.ToString(),
                                        };
                                        var response = helper.GetConsummerPoolByLocation(request);
                                        if (response[0].resultCode == "1200")
                                        {
                                            alreadyAssignedLocations.Add(location);
                                        }
                                    }
                                }
                            }
                            foreach(var location in alreadyAssignedLocations)
                            {
                                if (toReturn.Contains(location))
                                {
                                    toReturn.Remove(location);
                                }
                            }
                            if (toReturn.Count == 0)
                            {
                                toReturn.Add(new LocationResponseModel()
                                {
                                    remarks = "No Available Building Found",
                                    resultCode = "1200"
                                });
                            }
                        }
                        else
                        {
                            toReturn.Add(new LocationResponseModel()
                            {
                                resultCode = "1200",
                                remarks = "No Building Found"
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