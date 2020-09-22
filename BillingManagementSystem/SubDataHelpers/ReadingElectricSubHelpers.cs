using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingManagementSystem.Models;

namespace BillingManagementSystem.SubDataHelpers
{
    public class ReadingElectricSubHelpers
    {
        public BillCalculationResponseModel CalculateBill(double _units, int resident_pin_code)
        {
            BillCalculationResponseModel toReturn = new BillCalculationResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    
                    //Calculating Bill Amount & Adding Bill
                    var fparate = (from x in db.tbl_fixedrates where x.fixedrates_id == 1 select x).FirstOrDefault();
                    var meterRent = (from x in db.tbl_fixedrates where x.fixedrates_id == 2 select x.fixedrates_amount).FirstOrDefault();
                    var tvRate = (from x in db.tbl_fixedrates where x.fixedrates_id == 3 select x).FirstOrDefault();
                    var waterRate = (from x in db.tbl_fixedrates where x.fixedrates_id == 4 select x).FirstOrDefault();
                    var slabs = (from x in db.tbl_slabs select x).ToList();
                    if (resident_pin_code == 5)
                    {
                        slabs = slabs.Where(x => x.fk_tarrifcategory == 2).ToList();
                    }
                    else if(resident_pin_code == 3 || resident_pin_code == 2)
                    {
                        _units = _units - CalculateAuthUnits(resident_pin_code);
                    }
                    //Calculations
                    double totalAmount = 0.0;
                    double totalEnergyCharges = 0.0;
                    double totalFPA = 0.0;
                    double fpa = fparate.fixedrates_amount;
                    double waterCharges = waterRate.fixedrates_amount;
                    double tvCharges = tvRate.fixedrates_amount;
                    for (int i = 0; i < slabs.Count; i++)
                    {
                        if (totalEnergyCharges == 0)
                        {

                            if (slabs[i].slab_tariff.Contains(">"))
                            {
                                var limit = double.Parse(slabs[i].slab_tariff.Replace(">", " ").Trim());
                                if (_units <= limit)
                                {
                                    totalAmount = slabs[i].slab_net_rate.Value * _units;
                                    totalEnergyCharges = slabs[i].slab_energy_charges.Value * _units;
                                    totalFPA = fpa * _units;
                                }

                            }
                            else if (slabs[i].slab_tariff.Contains("-"))
                            {
                                var limitLow = double.Parse(slabs[i].slab_tariff.Split('-')[0].Trim());
                                var limitHigh = double.Parse(slabs[i].slab_tariff.Split('-')[1].Trim());
                                if (_units >= limitLow && _units <= limitHigh)
                                {
                                    double extraUnits = _units - limitLow + 1;
                                    _units = _units - extraUnits;
                                    double extraTotalAmount = slabs[i].slab_net_rate.Value * extraUnits;
                                    double extraEnergy = slabs[i].slab_energy_charges.Value * extraUnits;
                                    double extraFPA = fpa * extraUnits;
                                    totalAmount = (slabs[i - 1].slab_net_rate.Value * _units) + extraTotalAmount;
                                    totalEnergyCharges = (slabs[i - 1].slab_energy_charges.Value * _units) + extraEnergy;
                                    totalFPA = (fpa * _units) + extraFPA;
                                }
                            }
                            else if (slabs[i].slab_tariff.Contains("<"))
                            {
                                var limit = double.Parse(slabs[i].slab_tariff.Replace("<", " ").Trim());
                                if (_units > limit)
                                {
                                    double extraUnits = _units - limit;
                                    _units = _units - extraUnits;
                                    double extraTotalAmount = slabs[i].slab_net_rate.Value * extraUnits;
                                    double extraEnergy = slabs[i].slab_energy_charges.Value * extraUnits;
                                    double extraFPA = fpa * extraUnits;
                                    totalAmount = (slabs[i - 1].slab_net_rate.Value * _units) + extraTotalAmount;
                                    totalEnergyCharges = (slabs[i - 1].slab_energy_charges.Value * _units) + extraEnergy;
                                    totalFPA = (fpa * _units) + extraFPA;
                                }
                            }
                            else
                            {
                                totalAmount = slabs[i].slab_net_rate.Value * _units;
                                totalEnergyCharges = slabs[i].slab_energy_charges.Value * _units;
                                totalFPA = fpa * _units;
                            }
                        }
                    }
                    totalEnergyCharges = totalEnergyCharges / 2;
                    if (_units >= fparate.fixedrates_unit)
                    {
                        if (fparate.fixedrates_status == 0)
                        {
                            totalAmount = totalAmount + (totalFPA  + meterRent);
                        }
                        else
                        {
                            totalAmount = totalAmount + ( meterRent - totalFPA);
                        }
                    }
                    else
                    {
                        totalAmount = totalAmount + ( meterRent);
                    }
                    if (_units >= tvRate.fixedrates_unit)
                    {
                        if (tvRate.fixedrates_status == 0)
                        {
                            totalAmount = totalAmount + tvCharges;
                        }
                        else
                        {
                            totalAmount = totalAmount + tvCharges;
                        }
                    }
                    if (_units >= waterRate.fixedrates_unit)
                    {
                        if (waterRate.fixedrates_status == 0)
                        {
                            totalAmount = totalAmount + waterCharges;
                        }
                        else
                        {
                            totalAmount = totalAmount + waterCharges;
                        }
                    }
                    if (resident_pin_code == 1)
                    {
                        totalAmount = (totalAmount - totalEnergyCharges);
                    }
                    else if(resident_pin_code == 4)
                    {
                        totalEnergyCharges = 0.0;
                    }

                    toReturn = new BillCalculationResponseModel()
                    {
                        totalAmount = totalAmount,
                        totalEnergyCharges = totalEnergyCharges,
                        totalFPA = totalFPA,
                        tvCharges= tvCharges,
                        waterCharges= waterCharges,
                        resultCode="1100",
                        remarks="Success"
                    };
                }
            }
            catch(Exception Ex)
            {
                toReturn = new BillCalculationResponseModel()
                {
                    remarks = "Exception " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
        public double CalculateAuthUnits(int resident_pin_code)
        {
            double unit = 0.0;
            string[] summerMonths= { "05", "06", "07", "08","09","10" };
            string[] winterMonths = { "04", "03", "02", "01", "11", "12" };
            string currentMonth = new DataHelpers.MonthFinderHelpers().GetCurrentMonthWithOutYear();
            if (summerMonths.Contains(currentMonth))
            {
                if (resident_pin_code == 2)
                {
                    unit = 23;
                }
                if(resident_pin_code == 3)
                {
                    unit = 30;
                }
            }
            if (winterMonths.Contains(currentMonth))
            {
                if (resident_pin_code == 2)
                {
                    unit = 18;
                }
                if (resident_pin_code == 3)
                {
                    unit = 50;
                }
            }
           
            return unit;
        }
    }
}