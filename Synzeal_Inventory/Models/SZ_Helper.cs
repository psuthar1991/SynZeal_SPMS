using Newtonsoft.Json;
using Synzeal_Inventory.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using static Synzeal_Inventory.Models.EnumList;

namespace Synzeal_Inventory.Models
{
    public class SZ_Helper
    {
        public static SynzealLiveEntities db = new SynzealLiveEntities();

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static void InsertLog(string action, string message)
        {
            SZ_Log objlog = new SZ_Log();
            objlog.Action = action;
            objlog.CreatedDate = DateTime.Now;
            objlog.Message = message;
            objlog.UserId = SessionCookieManagement.UserId;
            objlog.Id = Guid.NewGuid();
            db.Entry(objlog).State = EntityState.Added;
            db.SaveChanges();
        }

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetWeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static string GetStringDateDiffrance(DateTime startdate, DateTime enddate)
        {
            DayOfWeek startOfWeek = DayOfWeek.Monday;
            var diff = enddate.Subtract(startdate);

            var weeks = (int)diff.Days / 7;

            // need to check if there's an extra week to count
            var remainingDays = diff.Days % 7;
            var cal = CultureInfo.InvariantCulture.Calendar;
            var d1WeekNo = cal.GetWeekOfYear(startdate, CalendarWeekRule.FirstFullWeek, startOfWeek);
            var d1PlusRemainingWeekNo = cal.GetWeekOfYear(startdate.AddDays(remainingDays), CalendarWeekRule.FirstFullWeek, startOfWeek);

            if (d1WeekNo != d1PlusRemainingWeekNo)
                weeks++;

            return weeks + " week";


            //const int SECOND = 1;
            //const int MINUTE = 60 * SECOND;
            //const int HOUR = 60 * MINUTE;
            //const int DAY = 24 * HOUR;
            //const int WEEK = 7 * DAY;
            //const int MONTH = 30 * DAY;

            //var ts = new TimeSpan(enddate.Ticks - startdate.Ticks);
            //double delta = Math.Abs(ts.TotalSeconds);

            //if (delta < 1 * MINUTE)
            //    return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            //if (delta < 2 * MINUTE)
            //    return "a minute ago";

            //if (delta < 45 * MINUTE)
            //    return ts.Minutes + " minutes ago";

            //if (delta < 90 * MINUTE)
            //    return "an hour ago";

            //if (delta < 24 * HOUR)
            //    return ts.Hours + " hours ago";

            //if (delta < 48 * HOUR)
            //    return "yesterday";

            //if (delta < 30 * DAY)
            //    return ts.Days + " days ago";

            //if (delta < 12 * MONTH)
            //{
            //    int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
            //    return months <= 1 ? "one month ago" : months + " months ago";
            //}
            //else
            //{
            //    int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            //    return years <= 1 ? "one year ago" : years + " years ago";
            //}
        }

        public static string ToJson(object input)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(input);
        }
        public static int calculatePerc(string price, decimal discount)
        {

            var divide = (Convert.ToDecimal(price) * discount / 100);

            var calcPrice = Convert.ToInt32((Convert.ToDecimal(price) - divide));
            return calcPrice;
        }
        public static List<PriceQtyModel> PreparePriceModel(string pricevalue, decimal? itemDiscount = null)
        {
            var model = new List<PriceQtyModel>();
            if(string.IsNullOrEmpty(pricevalue))
            {
                return model;
            }
            var allpricedata = pricevalue.Split(',');

            if (allpricedata != null)
            {

                if (allpricedata.Count() == 1)
                {
                    var submodel = new PriceQtyModel();
                    var qty = "";
                    var price = "";
                    if (allpricedata[0].IndexOf("X") != -1)
                    {
                        string packs = allpricedata[0].Split('X')[1];
                        int packsize = Convert.ToInt32(System.Text.RegularExpressions.Regex.Match(packs, @"\d+").Value);
                        qty = Convert.ToString(Convert.ToInt32(allpricedata[0].Split(' ')[0]) * Convert.ToInt32(packsize));
                        if (allpricedata[0].IndexOf("=") != -1)
                        {
                            try
                            {
                                price = System.Text.RegularExpressions.Regex.Match(allpricedata[0].Split('@')[1].Split('=')[1], @"\d+").Value;
                            }
                            catch (Exception)
                            {
                                price = System.Text.RegularExpressions.Regex.Match(allpricedata[0].Split('=')[1], @"\d+").Value;
                            }
                        }
                        else
                        {
                            price = System.Text.RegularExpressions.Regex.Match(allpricedata[0].Split('@')[1], @"\d+").Value;
                        }
                    }
                    else
                    {
                        qty = allpricedata[0].Split(' ')[0];
                        try
                        {
                            price = System.Text.RegularExpressions.Regex.Match(allpricedata[0].Split('@')[1], @"\d+").Value;
                        }
                        catch (Exception)
                        {

                        }
                    }

                    if (!string.IsNullOrEmpty(qty))
                    {
                        submodel.MG = Convert.ToString(qty.Replace(".000", ""));
                        if (string.IsNullOrEmpty(price))
                        {
                            submodel.Price = "0";
                        }
                        else
                        {
                            if(itemDiscount.HasValue && itemDiscount.Value > 0)
                            {
                                submodel.Price = Convert.ToString(calculatePerc(Convert.ToString(price), itemDiscount.Value));
                            }
                            else
                            {
                                submodel.Price = Convert.ToString(price);
                            }
                        }
                        model.Add(submodel);
                    }
                }
                else
                {
                    var cnt = new List<string>();
                    foreach (var item in allpricedata)
                    {
                        try
                        {
                            var submodel = new PriceQtyModel();
                            var qty = System.Text.RegularExpressions.Regex.Match(item.Split('@')[0], @"\d+").Value;
                            var price = System.Text.RegularExpressions.Regex.Match(item.Split('@')[1], @"\d+").Value;
                            if (item.Split('X').Count() > 1)
                            {
                                if (item.Split('X')[1].Contains("="))
                                {
                                    qty = Convert.ToString(Convert.ToInt32(qty) * Convert.ToInt32(item.Split('X')[1].Split('=')[0]));
                                }
                                else
                                {
                                    qty = Convert.ToString(Convert.ToInt32(qty) * Convert.ToInt32(item.Split('X')[1]));
                                }
                            }
                            if (!string.IsNullOrEmpty(price))
                            {
                                submodel.MG = Convert.ToString(qty);
                                submodel.Price = Convert.ToString(price);
                                model.Add(submodel);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

            }
            return model;
        }
        public static string GetCurrancy(SZ_Quotation quote)
        {
            if (!string.IsNullOrEmpty(quote.Currency))
            {
                return quote.Currency;
            }

            if (quote.CountryType == "Export")
            {
                return "USD";
            }

            return "INR";
        }

        public static int CalculateQty(string requiredQty)
        {
            if (!string.IsNullOrEmpty(requiredQty))
            {
                try
                {
                    var rqty = Regex.Match(requiredQty, @"^\D*?((-?(\d+(\.\d+)?))|(-?\.\d+)).*").Value;
                    if (!string.IsNullOrEmpty(rqty))
                    {
                        return Convert.ToInt32(rqty);
                    }
                    return 0;
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }
        public static string GetProjectType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return "";
            }
            string str = "";
            foreach (ProjectType r in Enum.GetValues(typeof(ProjectType)))
            {
                var item = Enum.GetName(typeof(ProjectType), r);
                var test = r.ToString();
                string text = GetEnumDescription((ProjectType)(int)r);
                int val = (int)r;
                string selected = "";
                if (Convert.ToString(val) == type)
                {
                    str = text;
                }
            }
            return str;
        }

        public static decimal CalculateBeforePriceData(SZ_QuotationDetail quoteDetail)
        {
            if (string.IsNullOrEmpty(quoteDetail.RequiredQty))
            {
                return 0;
            }

            if (!string.IsNullOrEmpty(quoteDetail.Price))
            {
                //100 mg@225 USD
                var mainPriceModel = new List<QTYModel>();
                var pricedata = quoteDetail.Price.Split(',');
                if (pricedata != null && pricedata.Count() > 0)
                {
                    foreach (var item in pricedata)
                    {
                        var objModel = new QTYModel();
                        if (item.ToLower().Contains("x"))
                        {
                            try
                            {
                                var pri = item.Split('@');
                                objModel.Qty = Convert.ToDecimal(Regex.Match(pri[0], @"\d+").Value) * Convert.ToInt32(pri[1].Split('=')[0].Split('X')[1]);
                                objModel.Price = Regex.Match(pri[1].Split('=')[1], @"\d+.+\d").Value;
                                mainPriceModel.Add(objModel);
                            }
                            catch (Exception)
                            {
                                objModel.Qty = 0;
                                objModel.Price = "0";
                                mainPriceModel.Add(objModel);
                            }
                        }
                        else
                        {
                            try
                            {
                                var pri = item.Split('@');
                                objModel.Qty = Convert.ToDecimal(Regex.Match(pri[0], @"\d+").Value);
                                objModel.Price = Regex.Match(pri[1], @"\d+").Value;
                                mainPriceModel.Add(objModel);
                            }
                            catch (Exception ex)
                            {
                                objModel.Qty = 0;
                                objModel.Price = "0";
                                mainPriceModel.Add(objModel);
                            }
                        }
                    }

                    try
                    {
                        int qid = Convert.ToInt32(quoteDetail.RequiredQty);
                        var mdata = mainPriceModel.Where(x => x.Qty == qid).Select(x => x.Price).FirstOrDefault();
                        if (!string.IsNullOrEmpty(mdata))
                        {
                            return Convert.ToInt32(mdata);
                        }
                    }
                    catch (Exception)
                    {
                        return 0;
                    }
                }
                return 0;
            }

            return 0;
        }

        public static int CalculatePackSizeFromOrderConfirmation(string orderRemark)
        {
            try
            {
                if (string.IsNullOrEmpty(orderRemark))
                {
                    return 1;
                }
                // 25 mg * 1 vial
                var pri = orderRemark.Split('*');
                return Convert.ToInt32(Regex.Match(pri[0], @"\d+").Value);
            }
            catch (Exception)
            {
                return 1;
            }
        }
        public static int CalculateNoofPackFromOrderConfirmation(string orderRemark)
        {
            try
            {
                if (string.IsNullOrEmpty(orderRemark))
                {
                    return 1;
                }
                // 25 mg * 1 vial
                var pri = orderRemark.Split('*');
                return Convert.ToInt32(Regex.Match(pri[1], @"\d+").Value);
            }
            catch (Exception)
            {
                return 1;
            }
        }
        public static decimal GetUnitPerRate(MovetoProjectSAPModel model)
        {
            if (model.QuantityPO > 0 && model.PriceBfrDiscount > 0)
            {
                return (model.PriceBfrDiscount / model.QuantityPO);
            }
            return 0;
        }
        public static string CheckBatch(List<SZ_Inventory> inventoryData, SZ_QuotationDetail quoteDetail)
        {
            var invData = inventoryData.Where(x => x.Id == quoteDetail.AdditionalBatchNo).Select(x => x.BatchNo).FirstOrDefault();
            if (string.IsNullOrEmpty(invData))
            {
                return "";
            }

            return invData;
        }
        public static decimal CalculateAftPriceData(SZ_QuotationDetail quoteDetail, MovetoProjectSAPModel model)
        {
            if (!string.IsNullOrEmpty(quoteDetail.FinalPrice))
            {
                if (!string.IsNullOrEmpty(Regex.Match(quoteDetail.FinalPrice, @"\d+.+\d").Value))
                {
                    try
                    {
                        if (Regex.Match(quoteDetail.FinalPrice, @"\d+.+\d").Value.Contains("+"))
                        {
                            var splt = Regex.Match(quoteDetail.FinalPrice, @"\d+.+\d").Value.Split('+');
                            if (splt != null && splt.Count() > 0)
                            {
                                decimal tlt = 0;
                                foreach (var item in splt)
                                {
                                    if (!string.IsNullOrEmpty(item))
                                    {
                                        tlt += Convert.ToDecimal(Regex.Match(item, @"\d+.+\d").Value);
                                    }
                                }
                                return tlt;
                            }
                        }
                        return Convert.ToDecimal(Regex.Match(quoteDetail.FinalPrice, @"\d+.+\d").Value);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                return 0;
            }

            if (string.IsNullOrEmpty(quoteDetail.RequiredQty))
            {
                return 0;
            }

            if (!string.IsNullOrEmpty(quoteDetail.Price))
            {
                //100 mg@225 USD
                var mainPriceModel = new List<QTYModel>();
                var pricedata = quoteDetail.Price.Split(',');
                if (pricedata != null && pricedata.Count() > 0)
                {
                    foreach (var item in pricedata)
                    {
                        var objModel = new QTYModel();
                        if (item.ToLower().Contains("x"))
                        {
                            try
                            {
                                var pri = item.Split('@');
                                objModel.Qty = Convert.ToDecimal(Regex.Match(pri[0], @"\d+.+\d").Value) * Convert.ToInt32(pri[1].Split('=')[0].Split('X')[1]);
                                objModel.Price = Regex.Match(pri[1].Split('=')[1].Split('X')[0], @"\d+.+\d").Value;
                                mainPriceModel.Add(objModel);
                            }
                            catch (Exception)
                            {
                                objModel.Qty = 0;
                                objModel.Price = "0";
                                mainPriceModel.Add(objModel);
                            }
                        }
                        else
                        {
                            try
                            {
                                var pri = item.Split('@');
                                objModel.Qty = Convert.ToDecimal(Regex.Match(pri[0], @"\d+.+\d").Value);
                                objModel.Price = Regex.Match(pri[1], @"\d+.+\d").Value;
                                mainPriceModel.Add(objModel);
                            }
                            catch (Exception ex)
                            {
                                objModel.Qty = 0;
                                objModel.Price = "0";
                                mainPriceModel.Add(objModel);
                            }
                        }
                    }
                    try
                    {
                        int qid = Convert.ToInt32(quoteDetail.RequiredQty);
                        var mdata = mainPriceModel.Where(x => x.Qty == qid).Select(x => x.Price).FirstOrDefault();
                        if (!string.IsNullOrEmpty(mdata))
                        {
                            return Convert.ToInt32(mdata);
                        }
                    }
                    catch (Exception)
                    {
                        return 0;
                    }
                }
                return 0;
            }
            return 0;
        }

    }
}