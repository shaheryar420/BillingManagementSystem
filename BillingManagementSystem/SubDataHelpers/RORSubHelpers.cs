using BillingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.SubDataHelpers
{
    public class RORSubHelpers
    {
        public List<BillResponseModel> GetAllROR(BillRequestModel model)
        {
            List<BillResponseModel> toReturn = new List<BillResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var billsElectric = (from x in db.tbl_ror
                                         join p in db.tbl_paymentstatus on x.ror_status equals p.paymentstatus_id
                                         join z in db.tbl_residents on x.fk_resident equals z.resident_id
                                         join l in db.tbl_consummer_pool on x.consummer_no equals l.consummer_no
                                         join location in db.tbl_location on l.fk_location equals location.location_id
                                         join subArea in db.tbl_subarea on location.fk_subarea equals subArea.subarea_id
                                         join area in db.tbl_area on subArea.fk_area equals area.area_id
                                         select new
                                         {
                                             x.consummer_no,
                                             x.ror_amount,
                                             x.ror_datetime,
                                             p.paymentstatus_name,
                                             x.id,
                                             x.ror_month,
                                             x.ror_outstanding,
                                             x.fk_resident,
                                             x.ror_status,
                                             z.resident_name,
                                             z.resident_panumber,
                                             z.resident_rank,
                                             z.resident_remarks,
                                             z.resident_unit,
                                             location.location_name,
                                             location.location_id,
                                             area.area_id,
                                             area.area_name,
                                             
                                         }).ToList();
                    var primaryReading = (from x in db.tbl_approvereadings                       
                                          join y in db.tbl_readingpicture on x.fk_readingpicture equals y.readingpicture_id
                                          where x.is_secondary == 0 
                                          select new
                                          {
                                              x.fk_readingpicture,
                                              x.fk_ror,
                                              x.is_secondary,
                                              x.readinge_remarks,
                                              x.reading_addedby,
                                              x.reading_currentreading,
                                              x.reading_datetime,
                                              x.reading_id,
                                              x.reading_meterno,
                                              x.reading_month,
                                              x.reading_prevreading,
                                              x.reading_units,
                                              x.bill_amount,
                                              x.bill_fpa,
                                              x.bill_gst,
                                              x.bill_meter_rent,
                                              x.bill_rebate,
                                              x.bill_tv,
                                              x.bill_water,
                                              x.bill_fur_charge,
                                              y.readingpicture_id,
                                              y.readingpicture_data,
                                              y.readingpicture_size,
                                              y.readingpicture_type
                                          }).ToList();

                    var secondaryReading = (from x in db.tbl_approvereadings
                                            join y in db.tbl_readingpicture on x.fk_readingpicture equals y.readingpicture_id
                                            where x.is_secondary == 1 
                                            select new
                                            {
                                                x.fk_readingpicture,
                                                x.fk_ror,
                                                x.is_secondary,
                                                x.readinge_remarks,
                                                x.reading_addedby,
                                                x.reading_currentreading,
                                                x.reading_datetime,
                                                x.reading_id,
                                                x.reading_meterno,
                                                x.reading_month,
                                                x.reading_prevreading,
                                                x.reading_units,
                                                x.bill_amount,
                                                x.bill_fpa,
                                                x.bill_gst,
                                                x.bill_meter_rent,
                                                x.bill_rebate,
                                                x.bill_tv,
                                                x.bill_water,
                                                x.bill_fur_charge,
                                                y.readingpicture_id,
                                                y.readingpicture_data,
                                                y.readingpicture_size,
                                                y.readingpicture_type
                                            }).ToList();

                    var gasReading = (from x in db.tbl_approvereadings
                                      join y in db.tbl_readingpicture on x.fk_readingpicture equals y.readingpicture_id
                                      where x.is_secondary == 2 
                                      select new
                                      {
                                          x.fk_readingpicture,
                                          x.fk_ror,
                                          x.is_secondary,
                                          x.readinge_remarks,
                                          x.reading_addedby,
                                          x.reading_currentreading,
                                          x.reading_datetime,
                                          x.reading_id,
                                          x.reading_meterno,
                                          x.reading_month,
                                          x.reading_prevreading,
                                          x.reading_units,
                                          x.bill_amount,
                                          x.bill_fpa,
                                          x.bill_gst,
                                          x.bill_meter_rent,
                                          x.bill_rebate,
                                          x.bill_tv,
                                          x.bill_water,
                                          x.bill_fur_charge,
                                          y.readingpicture_id,
                                          y.readingpicture_data,
                                          y.readingpicture_size,
                                          y.readingpicture_type
                                      }).ToList();

                    var bill = (from x in billsElectric
                                join y in primaryReading on x.id equals y.fk_ror into primary
                                join z in secondaryReading on x.id equals z.fk_ror into secondary
                                join g in gasReading on x.id equals g.fk_ror into gas
                                from y in primary.DefaultIfEmpty()
                                from z in secondary.DefaultIfEmpty()
                                from g in gas.DefaultIfEmpty()
                                select new
                                {
                                    x.area_id,
                                    x.area_name,
                                    x.consummer_no,
                                    x.ror_amount,
                                    x.ror_datetime,
                                    x.paymentstatus_name,
                                    x.id,
                                    x.ror_month,
                                    x.ror_outstanding,
                                    x.fk_resident,
                                    x.ror_status,
                                    x.resident_name,
                                    x.resident_panumber,
                                    x.resident_rank,
                                    x.resident_remarks,
                                    x.resident_unit,
                                    x.location_name,
                                    x.location_id,
                                    primaryPreviuosReading = y != null ? y.reading_prevreading.ToString() : "",
                                    primaryCurrentReading = y != null ? y.reading_currentreading.ToString() : "",
                                    primaryUnits = y != null ? y.reading_units.ToString() : "",
                                    primaryAmount = y != null ? y.bill_amount.ToString() : "",
                                    primaryFPA = y != null ? y.bill_fpa.ToString() : "",
                                    primaryMeterRent = y != null ? y.bill_meter_rent.ToString() : "",
                                    primaryRebate = y != null ? y.bill_rebate.ToString() : "",
                                    primaryTv = y != null ? y.bill_tv.ToString() : "",
                                    primaryWater = y != null ? y.bill_water.ToString() : "",
                                    primaryReadingPictureId = y != null?y.readingpicture_id.ToString():"",
                                    primaryFurCharge= y!=null?y.bill_fur_charge.ToString():"",
                                    secondaryReadingPictureId = z != null ? z.readingpicture_id.ToString() : "",
                                    gasReadingPictureId = g != null ? g.readingpicture_id.ToString() : "",
                                    primaryReadingPictureData = y != null ? y.readingpicture_data.ToString() : "",
                                    primaryReadingPictureType = y != null ? y.readingpicture_type.ToString() : "",
                                    primaryReadingPictureSize = y != null ? y.readingpicture_size.ToString() : "",
                                    secondaryPreviuosReading = z != null ? z.reading_prevreading.ToString() : "",
                                    secondaryCurrentReading = z != null ? z.reading_currentreading.ToString() : "",
                                    secondaryUnits = z != null ? z.reading_units.ToString() : "",
                                    secondaryAmount = z != null ? z.bill_amount.ToString() : "",
                                    secondaryFPA = z != null ? z.bill_fpa.ToString() : "",
                                    secondaryMeterRent = z != null ? z.bill_meter_rent.ToString() : "",
                                    secondaryRebate = z != null ? z.bill_rebate.ToString() : "",
                                    secondaryTv = z != null ? z.bill_tv.ToString() : "",
                                    secondaryWater = z != null ? z.bill_water.ToString() : "",
                                    secondaryReadingPictureData = z != null ? z.readingpicture_data.ToString() : "",
                                    secondaryReadingPictureType = z != null ? z.readingpicture_type.ToString() : "",
                                    secondaryReadingPictureSize = z != null ? z.readingpicture_size.ToString() : "",
                                    secondaryFurCharge = z != null ? z.bill_fur_charge.ToString() : "",
                                    gasPreviuosReading = g != null ? g.reading_prevreading.ToString() : "",
                                    gasCurrentReading = g != null ? g.reading_currentreading.ToString() : "",
                                    gasMMBTU = g != null ? g.reading_units.ToString() : "",
                                    gasAmount = g != null ? g.bill_amount.ToString() : "",
                                    gasGST = g != null ? g.bill_gst.ToString() : "",
                                    gasMeterRent = g != null ? g.bill_meter_rent.ToString() : "",
                                    GasReadingPictureData = g != null ? g.readingpicture_data.ToString() : "",
                                    GasReadingPictureType = g != null ? g.readingpicture_type.ToString() : "",
                                    GasReadingPictureSize = g != null ? g.readingpicture_size.ToString() : "",


                                }).ToList();
                    if (bill.Count() > 0)
                    {
                        toReturn = bill.Select(x => new BillResponseModel()
                        {
                          
                            locationMeterNo = x.consummer_no,
                            paymentStatusName = x.paymentstatus_name,
                            billAmount = x.ror_amount.ToString(),
                            billDateTime = x.ror_datetime.ToString(),
                            billId = x.id.ToString(),
                            billMonth = !String.IsNullOrEmpty(x.ror_month) ? x.ror_month : "",
                            billOutstanding = x.ror_outstanding.ToString(),
                            residentName = !String.IsNullOrEmpty(x.resident_name) ? x.resident_name : "",
                            residentPaNo = !string.IsNullOrEmpty(x.resident_panumber) ? x.resident_panumber : "",
                            residentRank = !string.IsNullOrEmpty(x.resident_rank) ? x.resident_rank : "",
                            residentRemarks = !string.IsNullOrEmpty(x.resident_remarks) ? x.resident_remarks : "",
                            residentUnit = !string.IsNullOrEmpty(x.resident_unit) ? x.resident_unit : "",
                            locationName = !string.IsNullOrEmpty(x.location_name) ? x.location_name : "",
                            fk_location = x.location_id.ToString(),
                            fk_paymentStatus = x.ror_status.ToString(),
                            fk_resident = x.fk_resident.ToString(),
                            billPrimaryCurrentReading= x.primaryCurrentReading,
                            billPrimaryPrevReading = x.primaryPreviuosReading,
                            billPrimaryFPA= x.primaryFPA,
                            billPrimaryRebate = x.primaryRebate,
                            billPrimaryMeterRent =x.primaryMeterRent,
                            billPrimaryTv= x.primaryTv,
                            billPrimaryWater = x.primaryWater,
                            billPrimaryUnits= x.primaryUnits,
                            billPrimaryAmount = x.primaryAmount,
                            billGasAmount= x.gasAmount,
                            billSecondaryAmount= x.secondaryAmount,
                            billSecondaryCurrentReading = x.secondaryCurrentReading,
                            billSecondaryFPA = x.secondaryFPA,
                            billSecondaryMeterRent= x.secondaryMeterRent,
                            billSecondaryPrevReading = x.secondaryPreviuosReading,
                            billSecondaryRebate = x.secondaryRebate,
                            billSecondaryTv = x.secondaryTv,
                            billSecondaryUnits = x.secondaryUnits,
                            billSecondaryWater= x.secondaryWater,
                            billGasCurrentReading = x.gasCurrentReading,
                            billGasGST= x.gasGST,
                            billGasMeterRent= x.gasMeterRent,
                            billGasPrevReading= x.gasPreviuosReading,
                            billGasMMBTU=x.gasMMBTU,
                            billGasPictureData= x.GasReadingPictureData,
                            billGasPictureSize= x.GasReadingPictureSize,
                            billGasPictureType= x.GasReadingPictureType,
                            billPrimaryPictureData= x.primaryReadingPictureData,
                            billPrimaryPictureSize= x.primaryReadingPictureSize,
                            billPrimaryPictureType= x.primaryReadingPictureType,
                            billSecondaryPictureData= x.secondaryReadingPictureData,
                            billSecondaryPictureSize=x.secondaryReadingPictureSize,
                            billSecondaryPictureType= x.secondaryReadingPictureType,
                            billGasPictureId = x.gasReadingPictureId,
                            billPrimaryPictureId = x.primaryReadingPictureId,
                            billSecondaryPictureId = x.secondaryReadingPictureId,
                            billPrimaryFurCharges= x.primaryFurCharge,
                            billSecondaryFurCharges= x.secondaryFurCharge,
                            remarks = "Successfully Found",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add(new BillResponseModel()
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new BillResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public BillResponseModel GetAllRORById(int  id)
        {
            BillResponseModel toReturn = new BillResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var billsElectric = (from x in db.tbl_ror
                                         join p in db.tbl_paymentstatus on x.ror_status equals p.paymentstatus_id
                                         join z in db.tbl_residents on x.fk_resident equals z.resident_id
                                         join l in db.tbl_consummer_pool on x.consummer_no equals l.consummer_no
                                         join location in db.tbl_location on l.fk_location equals location.location_id
                                         join subArea in db.tbl_subarea on location.fk_subarea equals subArea.subarea_id
                                         join area in db.tbl_area on subArea.fk_area equals area.area_id

                                         where x.id == id
                                         select new
                                         {
                                             x.consummer_no,
                                             x.ror_amount,
                                             x.ror_datetime,
                                             p.paymentstatus_name,
                                             x.id,
                                             x.ror_month,
                                             x.ror_outstanding,
                                             x.fk_resident,
                                             x.ror_status,
                                             z.resident_name,
                                             z.resident_panumber,
                                             z.resident_rank,
                                             z.resident_remarks,
                                             z.resident_unit,
                                             location.location_name,
                                             location.location_id,
                                             area.area_id,
                                             area.area_name,

                                         }).ToList();
                    var primaryReading = (from x in db.tbl_approvereadings
                                          join y in db.tbl_readingpicture on x.fk_readingpicture equals y.readingpicture_id
                                          where x.is_secondary == 0 && x.fk_ror == id
                                          select new
                                          {
                                              x.fk_readingpicture,
                                              x.fk_ror,
                                              x.is_secondary,
                                              x.readinge_remarks,
                                              x.reading_addedby,
                                              x.reading_currentreading,
                                              x.reading_datetime,
                                              x.reading_id,
                                              x.reading_meterno,
                                              x.reading_month,
                                              x.reading_prevreading,
                                              x.reading_units,
                                              x.bill_amount,
                                              x.bill_fpa,
                                              x.bill_gst,
                                              x.bill_meter_rent,
                                              x.bill_rebate,
                                              x.bill_tv,
                                              x.bill_water,
                                              x.bill_fur_charge,
                                              y.readingpicture_id,
                                              y.readingpicture_data,
                                              y.readingpicture_size,
                                              y.readingpicture_type
                                          }).ToList();

                    var secondaryReading = (from x in db.tbl_approvereadings
                                            join y in db.tbl_readingpicture on x.fk_readingpicture equals y.readingpicture_id
                                            where x.is_secondary == 1 && x.fk_ror == id
                                            select new
                                            {
                                                x.fk_readingpicture,
                                                x.fk_ror,
                                                x.is_secondary,
                                                x.readinge_remarks,
                                                x.reading_addedby,
                                                x.reading_currentreading,
                                                x.reading_datetime,
                                                x.reading_id,
                                                x.reading_meterno,
                                                x.reading_month,
                                                x.reading_prevreading,
                                                x.reading_units,
                                                x.bill_amount,
                                                x.bill_fpa,
                                                x.bill_gst,
                                                x.bill_meter_rent,
                                                x.bill_rebate,
                                                x.bill_tv,
                                                x.bill_water,
                                                x.bill_fur_charge,
                                                y.readingpicture_id,
                                                y.readingpicture_data,
                                                y.readingpicture_size,
                                                y.readingpicture_type
                                            }).ToList();

                    var gasReading = (from x in db.tbl_approvereadings
                                      join y in db.tbl_readingpicture on x.fk_readingpicture equals y.readingpicture_id
                                      where x.is_secondary == 2 && x.fk_ror == id
                                      select new
                                      {
                                          x.fk_readingpicture,
                                          x.fk_ror,
                                          x.is_secondary,
                                          x.readinge_remarks,
                                          x.reading_addedby,
                                          x.reading_currentreading,
                                          x.reading_datetime,
                                          x.reading_id,
                                          x.reading_meterno,
                                          x.reading_month,
                                          x.reading_prevreading,
                                          x.reading_units,
                                          x.bill_amount,
                                          x.bill_fpa,
                                          x.bill_gst,
                                          x.bill_meter_rent,
                                          x.bill_rebate,
                                          x.bill_fur_charge,
                                          x.bill_tv,
                                          x.bill_water,
                                          y.readingpicture_id,
                                          y.readingpicture_data,
                                          y.readingpicture_size,
                                          y.readingpicture_type
                                      }).ToList();

                    var bill = (from x in billsElectric
                                join y in primaryReading on x.id equals y.fk_ror into primary
                                join z in secondaryReading on x.id equals z.fk_ror into secondary
                                join g in gasReading on x.id equals g.fk_ror into gas
                                from y in primary.DefaultIfEmpty()
                                from z in secondary.DefaultIfEmpty()
                                from g in gas.DefaultIfEmpty()

                                where x.id == id
                                select new
                                {
                                    x.area_id,
                                    x.area_name,
                                    x.consummer_no,
                                    x.ror_amount,
                                    x.ror_datetime,
                                    x.paymentstatus_name,
                                    x.id,
                                    x.ror_month,
                                    x.ror_outstanding,
                                    x.fk_resident,
                                    x.ror_status,
                                    x.resident_name,
                                    x.resident_panumber,
                                    x.resident_rank,
                                    x.resident_remarks,
                                    x.resident_unit,
                                    x.location_name,
                                    x.location_id,
                                    primaryPreviuosReading = y != null ? y.reading_prevreading.ToString() : "",
                                    primaryCurrentReading = y != null ? y.reading_currentreading.ToString() : "",
                                    primaryUnits = y != null ? y.reading_units.ToString() : "",
                                    primaryAmount = y != null ? y.bill_amount.ToString() : "",
                                    primaryFPA = y != null ? y.bill_fpa.ToString() : "",
                                    primaryMeterRent = y != null ? y.bill_meter_rent.ToString() : "",
                                    primaryRebate = y != null ? y.bill_rebate.ToString() : "",
                                    primaryTv = y != null ? y.bill_tv.ToString() : "",
                                    primaryWater = y != null ? y.bill_water.ToString() : "",
                                    primaryFurCharge = y != null ? y.bill_fur_charge.ToString() : "",
                                    primaryReadingPictureId = y != null ? y.readingpicture_id.ToString() : "",
                                    secondaryReadingPictureId = z != null ? z.readingpicture_id.ToString() : "",
                                    gasReadingPictureId = g != null ? g.readingpicture_id.ToString() : "",
                                    primaryReadingPictureData = y != null ? y.readingpicture_data.ToString() : "",
                                    primaryReadingPictureType = y != null ? y.readingpicture_type.ToString() : "",
                                    primaryReadingPictureSize = y != null ? y.readingpicture_size.ToString() : "",
                                    secondaryPreviuosReading = z != null ? z.reading_prevreading.ToString() : "",
                                    secondaryCurrentReading = z != null ? z.reading_currentreading.ToString() : "",
                                    secondaryUnits = z != null ? z.reading_units.ToString() : "",
                                    secondaryAmount = z != null ? z.bill_amount.ToString() : "",
                                    secondaryFPA = z != null ? z.bill_fpa.ToString() : "",
                                    secondaryMeterRent = z != null ? z.bill_meter_rent.ToString() : "",
                                    secondaryRebate = z != null ? z.bill_rebate.ToString() : "",
                                    secondaryTv = z != null ? z.bill_tv.ToString() : "",
                                    secondaryWater = z != null ? z.bill_water.ToString() : "",
                                    secondaryReadingPictureData = z != null ? z.readingpicture_data.ToString() : "",
                                    secondaryReadingPictureType = z != null ? z.readingpicture_type.ToString() : "",
                                    secondaryReadingPictureSize = z != null ? z.readingpicture_size.ToString() : "",
                                    secondaryFurCharge = z != null ? z.bill_fur_charge.ToString() : "",
                                    gasPreviuosReading = g != null ? g.reading_prevreading.ToString() : "",
                                    gasCurrentReading = g != null ? g.reading_currentreading.ToString() : "",
                                    gasMMBTU = g != null ? g.reading_units.ToString() : "",
                                    gasAmount = g != null ? g.bill_amount.ToString() : "",
                                    gasGST = g != null ? g.bill_gst.ToString() : "",
                                    gasMeterRent = g != null ? g.bill_meter_rent.ToString() : "",
                                    GasReadingPictureData = g != null ? g.readingpicture_data.ToString() : "",
                                    GasReadingPictureType = g != null ? g.readingpicture_type.ToString() : "",
                                    GasReadingPictureSize = g != null ? g.readingpicture_size.ToString() : "",


                                }).FirstOrDefault();
                    if (bill!= null)
                    {
                        toReturn = new BillResponseModel()
                        {

                            locationMeterNo = bill.consummer_no,
                            paymentStatusName = bill.paymentstatus_name,
                            billAmount = bill.ror_amount.ToString(),
                            billDateTime = bill.ror_datetime.ToString(),
                            billId = bill.id.ToString(),
                            billMonth = !String.IsNullOrEmpty(bill.ror_month) ? bill.ror_month : "",
                            billOutstanding = bill.ror_outstanding.ToString(),
                            residentName = !String.IsNullOrEmpty(bill.resident_name) ? bill.resident_name : "",
                            residentPaNo = !string.IsNullOrEmpty(bill.resident_panumber) ? bill.resident_panumber : "",
                            residentRank = !string.IsNullOrEmpty(bill.resident_rank) ? bill.resident_rank : "",
                            residentRemarks = !string.IsNullOrEmpty(bill.resident_remarks) ? bill.resident_remarks : "",
                            residentUnit = !string.IsNullOrEmpty(bill.resident_unit) ? bill.resident_unit : "",
                            locationName = !string.IsNullOrEmpty(bill.location_name) ? bill.location_name : "",
                            fk_location = bill.location_id.ToString(),
                            fk_paymentStatus = bill.ror_status.ToString(),
                            fk_resident = bill.fk_resident.ToString(),
                            billPrimaryCurrentReading = bill.primaryCurrentReading,
                            billPrimaryPrevReading = bill.primaryPreviuosReading,
                            billPrimaryFPA = bill.primaryFPA,
                            billPrimaryRebate = bill.primaryRebate,
                            billPrimaryMeterRent = bill.primaryMeterRent,
                            billPrimaryTv = bill.primaryTv,
                            billPrimaryWater = bill.primaryWater,
                            billPrimaryUnits = bill.primaryUnits,
                            billPrimaryAmount = bill.primaryAmount,
                            billGasAmount = bill.gasAmount,
                            billSecondaryAmount = bill.secondaryAmount,
                            billSecondaryCurrentReading = bill.secondaryCurrentReading,
                            billSecondaryFPA = bill.secondaryFPA,
                            billSecondaryMeterRent = bill.secondaryMeterRent,
                            billSecondaryPrevReading = bill.secondaryPreviuosReading,
                            billSecondaryRebate = bill.secondaryRebate,
                            billSecondaryTv = bill.secondaryTv,
                            billSecondaryUnits = bill.secondaryUnits,
                            billSecondaryWater = bill.secondaryWater,
                            billGasCurrentReading = bill.gasCurrentReading,
                            billGasGST = bill.gasGST,
                            billGasMeterRent = bill.gasMeterRent,
                            billGasPrevReading = bill.gasPreviuosReading,
                            billGasMMBTU = bill.gasMMBTU,
                            billGasPictureData = bill.GasReadingPictureData,
                            billGasPictureSize = bill.GasReadingPictureSize,
                            billGasPictureType = bill.GasReadingPictureType,
                            billPrimaryPictureData = bill.primaryReadingPictureData,
                            billPrimaryPictureSize = bill.primaryReadingPictureSize,
                            billPrimaryPictureType = bill.primaryReadingPictureType,
                            billSecondaryPictureData = bill.secondaryReadingPictureData,
                            billSecondaryPictureSize = bill.secondaryReadingPictureSize,
                            billSecondaryPictureType = bill.secondaryReadingPictureType,
                            billGasPictureId = bill.gasReadingPictureId,
                            billPrimaryPictureId = bill.primaryReadingPictureId,
                            billSecondaryPictureId = bill.secondaryReadingPictureId,
                            billPrimaryFurCharges = bill.primaryFurCharge,
                            billSecondaryFurCharges = bill.secondaryFurCharge,
                            remarks = "Successfully Found",
                            resultCode = "1100"
                        };
                    }
                    else
                    {
                        toReturn=new BillResponseModel()
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn=(new BillResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
        public List<BillResponseModel> GetAllRORByLocation(int locationId)
        {
            List<BillResponseModel> toReturn = new List<BillResponseModel>();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var billsElectric = (from x in db.tbl_ror
                                         join p in db.tbl_paymentstatus on x.ror_status equals p.paymentstatus_id
                                         join z in db.tbl_residents on x.fk_resident equals z.resident_id
                                         join l in db.tbl_consummer_pool on x.consummer_no equals l.consummer_no
                                         join location in db.tbl_location on l.fk_location equals location.location_id
                                         join subArea in db.tbl_subarea on location.fk_subarea equals subArea.subarea_id
                                         join area in db.tbl_area on subArea.fk_area equals area.area_id
                                         where location.location_id == locationId
                                         select new
                                         {
                                             x.consummer_no,
                                             x.ror_amount,
                                             x.ror_datetime,
                                             p.paymentstatus_name,
                                             x.id,
                                             x.ror_month,
                                             x.ror_outstanding,
                                             x.fk_resident,
                                             x.ror_status,
                                             z.resident_name,
                                             z.resident_panumber,
                                             z.resident_rank,
                                             z.resident_remarks,
                                             z.resident_unit,
                                             location.location_name,
                                             location.location_id,
                                             area.area_id,
                                             area.area_name,

                                         }).ToList();
                    var primaryReading = (from x in db.tbl_approvereadings
                                          join y in db.tbl_readingpicture on x.fk_readingpicture equals y.readingpicture_id
                                          where x.is_secondary == 0
                                          select new
                                          {
                                              x.fk_readingpicture,
                                              x.fk_ror,
                                              x.is_secondary,
                                              x.readinge_remarks,
                                              x.reading_addedby,
                                              x.reading_currentreading,
                                              x.reading_datetime,
                                              x.reading_id,
                                              x.reading_meterno,
                                              x.reading_month,
                                              x.reading_prevreading,
                                              x.reading_units,
                                              x.bill_amount,
                                              x.bill_fpa,
                                              x.bill_gst,
                                              x.bill_meter_rent,
                                              x.bill_rebate,
                                              x.bill_tv,
                                              x.bill_water,
                                              y.readingpicture_id,
                                              y.readingpicture_data,
                                              y.readingpicture_size,
                                              y.readingpicture_type
                                          }).ToList();

                    var secondaryReading = (from x in db.tbl_approvereadings
                                            join y in db.tbl_readingpicture on x.fk_readingpicture equals y.readingpicture_id
                                            where x.is_secondary == 1
                                            select new
                                            {
                                                x.fk_readingpicture,
                                                x.fk_ror,
                                                x.is_secondary,
                                                x.readinge_remarks,
                                                x.reading_addedby,
                                                x.reading_currentreading,
                                                x.reading_datetime,
                                                x.reading_id,
                                                x.reading_meterno,
                                                x.reading_month,
                                                x.reading_prevreading,
                                                x.reading_units,
                                                x.bill_amount,
                                                x.bill_fpa,
                                                x.bill_gst,
                                                x.bill_meter_rent,
                                                x.bill_rebate,
                                                x.bill_tv,
                                                x.bill_water,
                                                y.readingpicture_id,
                                                y.readingpicture_data,
                                                y.readingpicture_size,
                                                y.readingpicture_type
                                            }).ToList();

                    var gasReading = (from x in db.tbl_approvereadings
                                      join y in db.tbl_readingpicture on x.fk_readingpicture equals y.readingpicture_id
                                      where x.is_secondary == 2
                                      select new
                                      {
                                          x.fk_readingpicture,
                                          x.fk_ror,
                                          x.is_secondary,
                                          x.readinge_remarks,
                                          x.reading_addedby,
                                          x.reading_currentreading,
                                          x.reading_datetime,
                                          x.reading_id,
                                          x.reading_meterno,
                                          x.reading_month,
                                          x.reading_prevreading,
                                          x.reading_units,
                                          x.bill_amount,
                                          x.bill_fpa,
                                          x.bill_gst,
                                          x.bill_meter_rent,
                                          x.bill_rebate,
                                          x.bill_tv,
                                          x.bill_water,
                                          y.readingpicture_id,
                                          y.readingpicture_data,
                                          y.readingpicture_size,
                                          y.readingpicture_type
                                      }).ToList();

                    var bill = (from x in billsElectric
                                join y in primaryReading on x.id equals y.fk_ror into primary
                                join z in secondaryReading on x.id equals z.fk_ror into secondary
                                join g in gasReading on x.id equals g.fk_ror into gas
                                from y in primary.DefaultIfEmpty()
                                from z in secondary.DefaultIfEmpty()
                                from g in gas.DefaultIfEmpty()
                                select new
                                {
                                    x.area_id,
                                    x.area_name,
                                    x.consummer_no,
                                    x.ror_amount,
                                    x.ror_datetime,
                                    x.paymentstatus_name,
                                    x.id,
                                    x.ror_month,
                                    x.ror_outstanding,
                                    x.fk_resident,
                                    x.ror_status,
                                    x.resident_name,
                                    x.resident_panumber,
                                    x.resident_rank,
                                    x.resident_remarks,
                                    x.resident_unit,
                                    x.location_name,
                                    x.location_id,
                                    primaryPreviuosReading = y != null ? y.reading_prevreading.ToString() : "",
                                    primaryCurrentReading = y != null ? y.reading_currentreading.ToString() : "",
                                    primaryUnits = y != null ? y.reading_units.ToString() : "0",
                                    primaryAmount = y != null ? y.bill_amount.ToString() : "0",
                                    primaryFPA = y != null ? y.bill_fpa.ToString() : "",
                                    primaryMeterRent = y != null ? y.bill_meter_rent.ToString() : "",
                                    primaryRebate = y != null ? y.bill_rebate.ToString() : "",
                                    primaryTv = y != null ? y.bill_tv.ToString() : "",
                                    primaryWater = y != null ? y.bill_water.ToString() : "",
                                    primaryReadingPictureId = y != null ? y.readingpicture_id.ToString() : "",
                                    secondaryReadingPictureId = z != null ? z.readingpicture_id.ToString() : "",
                                    gasReadingPictureId = g != null ? g.readingpicture_id.ToString() : "",
                                    primaryReadingPictureData = y != null ? y.readingpicture_data.ToString() : "",
                                    primaryReadingPictureType = y != null ? y.readingpicture_type.ToString() : "",
                                    primaryReadingPictureSize = y != null ? y.readingpicture_size.ToString() : "",
                                    secondaryPreviuosReading = z != null ? z.reading_prevreading.ToString() : "",
                                    secondaryCurrentReading = z != null ? z.reading_currentreading.ToString() : "",
                                    secondaryUnits = z != null ? z.reading_units.ToString() : "0",
                                    secondaryAmount = z != null ? z.bill_amount.ToString() : "0",
                                    secondaryFPA = z != null ? z.bill_fpa.ToString() : "",
                                    secondaryMeterRent = z != null ? z.bill_meter_rent.ToString() : "",
                                    secondaryRebate = z != null ? z.bill_rebate.ToString() : "",
                                    secondaryTv = z != null ? z.bill_tv.ToString() : "",
                                    secondaryWater = z != null ? z.bill_water.ToString() : "",
                                    secondaryReadingPictureData = z != null ? z.readingpicture_data.ToString() : "",
                                    secondaryReadingPictureType = z != null ? z.readingpicture_type.ToString() : "",
                                    secondaryReadingPictureSize = z != null ? z.readingpicture_size.ToString() : "",
                                    gasPreviuosReading = g != null ? g.reading_prevreading.ToString() : "",
                                    gasCurrentReading = g != null ? g.reading_currentreading.ToString() : "",
                                    gasMMBTU = g != null ? g.reading_units.ToString() : "0",
                                    gasAmount = g != null ? g.bill_amount.ToString() : "0",
                                    gasGST = g != null ? g.bill_gst.ToString() : "",
                                    gasMeterRent = g != null ? g.bill_meter_rent.ToString() : "",
                                    GasReadingPictureData = g != null ? g.readingpicture_data.ToString() : "",
                                    GasReadingPictureType = g != null ? g.readingpicture_type.ToString() : "",
                                    GasReadingPictureSize = g != null ? g.readingpicture_size.ToString() : "",


                                }).ToList();
                    if (bill.Count() > 0)
                    {
                        toReturn = bill.Select(x => new BillResponseModel()
                        {

                            locationMeterNo = x.consummer_no,
                            paymentStatusName = x.paymentstatus_name,
                            billAmount = x.ror_amount.ToString(),
                            billDateTime = x.ror_datetime.ToString(),
                            billId = x.id.ToString(),
                            billMonth = !String.IsNullOrEmpty(x.ror_month) ? x.ror_month : "",
                            billOutstanding = x.ror_outstanding.ToString(),
                            residentName = !String.IsNullOrEmpty(x.resident_name) ? x.resident_name : "",
                            residentPaNo = !string.IsNullOrEmpty(x.resident_panumber) ? x.resident_panumber : "",
                            residentRank = !string.IsNullOrEmpty(x.resident_rank) ? x.resident_rank : "",
                            residentRemarks = !string.IsNullOrEmpty(x.resident_remarks) ? x.resident_remarks : "",
                            residentUnit = !string.IsNullOrEmpty(x.resident_unit) ? x.resident_unit : "",
                            locationName = !string.IsNullOrEmpty(x.location_name) ? x.location_name : "",
                            fk_location = x.location_id.ToString(),
                            fk_paymentStatus = x.ror_status.ToString(),
                            fk_resident = x.fk_resident.ToString(),
                            billPrimaryCurrentReading = x.primaryCurrentReading,
                            billPrimaryPrevReading = x.primaryPreviuosReading,
                            billPrimaryFPA = x.primaryFPA,
                            billPrimaryRebate = x.primaryRebate,
                            billPrimaryMeterRent = x.primaryMeterRent,
                            billPrimaryTv = x.primaryTv,
                            billPrimaryWater = x.primaryWater,
                            billPrimaryUnits = x.primaryUnits,
                            billPrimaryAmount = x.primaryAmount,
                            billGasAmount = x.gasAmount,
                            billSecondaryAmount = x.secondaryAmount,
                            billSecondaryCurrentReading = x.secondaryCurrentReading,
                            billSecondaryFPA = x.secondaryFPA,
                            billSecondaryMeterRent = x.secondaryMeterRent,
                            billSecondaryPrevReading = x.secondaryPreviuosReading,
                            billSecondaryRebate = x.secondaryRebate,
                            billSecondaryTv = x.secondaryTv,
                            billSecondaryUnits = x.secondaryUnits,
                            billSecondaryWater = x.secondaryWater,
                            billGasCurrentReading = x.gasCurrentReading,
                            billGasGST = x.gasGST,
                            billGasMeterRent = x.gasMeterRent,
                            billGasPrevReading = x.gasPreviuosReading,
                            billGasMMBTU = x.gasMMBTU,
                            billGasPictureData = x.GasReadingPictureData,
                            billGasPictureSize = x.GasReadingPictureSize,
                            billGasPictureType = x.GasReadingPictureType,
                            billPrimaryPictureData = x.primaryReadingPictureData,
                            billPrimaryPictureSize = x.primaryReadingPictureSize,
                            billPrimaryPictureType = x.primaryReadingPictureType,
                            billSecondaryPictureData = x.secondaryReadingPictureData,
                            billSecondaryPictureSize = x.secondaryReadingPictureSize,
                            billSecondaryPictureType = x.secondaryReadingPictureType,
                            billGasPictureId= x.gasReadingPictureId,
                            billPrimaryPictureId= x.primaryReadingPictureId,
                            billSecondaryPictureId= x.secondaryReadingPictureId,

                            remarks = "Successfully Found",
                            resultCode = "1100"
                        }).ToList();
                    }
                    else
                    {
                        toReturn.Add(new BillResponseModel()
                        {
                            remarks = "No Record Found",
                            resultCode = "1200"
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                toReturn.Add(new BillResponseModel()
                {
                    remarks = "There Was A Fatal Error " + Ex.ToString(),
                    resultCode = "1000"
                });
            }
            return toReturn;
        }
    }
}