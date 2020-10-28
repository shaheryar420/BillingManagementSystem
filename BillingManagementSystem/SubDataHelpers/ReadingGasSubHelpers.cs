using BillingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingManagementSystem.SubDataHelpers
{
    public class ReadingGasSubHelpers
    {
        public BillGasCalculationResponseModel CalculateBill(double _units, int locationId)
        {
            BillGasCalculationResponseModel toReturn = new BillGasCalculationResponseModel();
            try
            {
                using (db_bmsEntities db = new db_bmsEntities())
                {
                    var location = db.tbl_location.Where(x => x.location_id == locationId).FirstOrDefault();
                    //Calculating Bill Amount & Adding Bill
           
                    var meterRent = (from x in db.tbl_fixedrates where x.fixedrates_id == 5 select x.fixedrates_amount).FirstOrDefault();
                    var gst = (from x in db.tbl_fixedrates where x.fixedrates_id == 6 select x.fixedrates_amount).FirstOrDefault();
                    
                    var slabs = (from x in db.tbl_gas_slab select x).ToList();
                   
                    //Calculations
                    double totalAmount = 0.0;
                    double GST = gst;
                    double MeterRent = meterRent;
                    _units = _units / 100000;
                    for (int i = 0; i < slabs.Count; i++)
                    {
                        if (slabs[i].slab_tariff.Contains("<"))
                        {
                            var limit = double.Parse(slabs[i].slab_tariff.Replace("<=", " ").Trim());
                            if (_units <= limit)
                            {
                                var mmbtu = (_units * 1046) / 281.7385;
                                totalAmount = slabs[i].slab_rate.Value * mmbtu;
                                totalAmount = Math.Round(totalAmount);
                            }

                        }
                        else if (slabs[i].slab_tariff.Contains("-"))
                        {
                            var limitLow = double.Parse(slabs[i].slab_tariff.Split('-')[0].Trim());
                            var limitHigh = double.Parse(slabs[i].slab_tariff.Split('-')[1].Trim());
                            if (_units > limitLow && _units <= limitHigh)
                            {
                                double extraUnits = _units - limitLow + 1;
                                _units = _units - extraUnits;
                                var mmbtu = (_units * 1046) / 281.7385;
                                var extraMmbtu = (extraUnits * 1046) / 281.7385;
                                double extraTotalAmount = slabs[i].slab_rate.Value * extraMmbtu;
                                totalAmount = (slabs[i - 1].slab_rate.Value * mmbtu) + extraTotalAmount;
                                totalAmount = Math.Round(totalAmount);

                            }
                        }
                        else if (slabs[i].slab_tariff.Contains(">"))
                        {
                            var limit = double.Parse(slabs[i].slab_tariff.Replace(">", " ").Trim());
                            if (_units > limit)
                            {
                                double extraUnits = _units - limit+1;
                                _units = _units - extraUnits;
                                var mmbtu = (_units * 1046) / 281.7385;
                                var extraMmbtu = (extraUnits * 1046) / 281.7385;
                                double extraTotalAmount = slabs[i].slab_rate.Value * extraMmbtu;
                                totalAmount = (slabs[i - 1].slab_rate.Value * mmbtu) + extraTotalAmount;
                                totalAmount = Math.Round(totalAmount);
                            }
                        }
                    }
                    totalAmount = totalAmount + (meterRent);
                    GST = (totalAmount * (GST / 100));
                    totalAmount = totalAmount+GST;
                    totalAmount = Math.Round(totalAmount);
                    GST = Math.Round(GST);

                    toReturn = new BillGasCalculationResponseModel()
                    {
                        totalAmount = totalAmount.ToString(),
                        meterRent = meterRent.ToString(),
                        GST = GST.ToString(),                        
                        resultCode = "1100",
                        remarks = "Success"
                    };
                }
            }
            catch (Exception Ex)
            {
                toReturn = new BillGasCalculationResponseModel()
                {
                    remarks = "Exception " + Ex.ToString(),
                    resultCode = "1000"
                };
            }
            return toReturn;
        }
    }
}