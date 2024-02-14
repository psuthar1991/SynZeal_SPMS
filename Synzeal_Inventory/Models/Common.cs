using DocumentFormat.OpenXml.Office2019.Excel.RichData;
using Synzeal_Inventory.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public static class Common
    {
        public static SynzealLiveEntities db = new SynzealLiveEntities();

        public static string GetFullName(this Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");
            var firstName = db.GenericAttributes.Where(x => x.EntityId == customer.Id && x.KeyGroup == "Customer" && x.Key == "FirstName").Select(x => x.Value).FirstOrDefault();
            var lastName = db.GenericAttributes.Where(x => x.EntityId == customer.Id && x.KeyGroup == "Customer" && x.Key == "LastName").Select(x => x.Value).FirstOrDefault();

            string fullName = "";
            if (!String.IsNullOrWhiteSpace(firstName) && !String.IsNullOrWhiteSpace(lastName))
                fullName = string.Format("{0} {1}", firstName, lastName);
            else
            {
                if (!String.IsNullOrWhiteSpace(firstName))
                    fullName = firstName;

                if (!String.IsNullOrWhiteSpace(lastName))
                    fullName = lastName;
            }
            return fullName;
        }
        public static string GetFullName(this Customer customer, SynzealLiveEntities _context)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");
            var firstName = _context.GenericAttributes.Where(x => x.EntityId == customer.Id && x.KeyGroup == "Customer" && x.Key == "FirstName").Select(x => x.Value).FirstOrDefault();
            var lastName = _context.GenericAttributes.Where(x => x.EntityId == customer.Id && x.KeyGroup == "Customer" && x.Key == "LastName").Select(x => x.Value).FirstOrDefault();

            string fullName = "";
            if (!String.IsNullOrWhiteSpace(firstName) && !String.IsNullOrWhiteSpace(lastName))
                fullName = string.Format("{0} {1}", firstName, lastName);
            else
            {
                if (!String.IsNullOrWhiteSpace(firstName))
                    fullName = firstName;

                if (!String.IsNullOrWhiteSpace(lastName))
                    fullName = lastName;
            }
            return fullName;
        }
        public static string ReplaceData(this string value, string replace, string replaceby)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value.Replace(replace, replaceby);
            }
            return value;
        }
        public static DateTime StartOfDay(this DateTime theDate)
        {
            return theDate.Date;
        }

        public static DateTime EndOfDay(this DateTime theDate)
        {
            return theDate.Date.AddDays(1).AddTicks(-1);
        }
        public static int GetMasterCategoryPriceId(int? mg, int? value, string type, List<SZ_CategoryMaster> Model)
        {
            int selectedCategoryid = 0;
            if (mg.HasValue && !string.IsNullOrEmpty(type) && type == "Export")
            {
                if (mg == 10)
                {
                    selectedCategoryid = Model.Where(x => x.TenUSD == value).Select(x => x.Id).FirstOrDefault();
                }
                if (mg == 25)
                {
                    selectedCategoryid = Model.Where(x => x.TwentyfiveUSD == value).Select(x => x.Id).FirstOrDefault();
                }
                if (mg == 50)
                {
                    selectedCategoryid = Model.Where(x => x.FiftyUSD == value).Select(x => x.Id).FirstOrDefault();
                }
                if (mg == 100)
                {
                    selectedCategoryid = Model.Where(x => x.OnehundredUSD == value).Select(x => x.Id).FirstOrDefault();
                }
                if (mg == 250)
                {
                    selectedCategoryid = Model.Where(x => x.TwohundredFiftyUSD == value).Select(x => x.Id).FirstOrDefault();
                }
                if (mg == 500)
                {
                    selectedCategoryid = Model.Where(x => x.FivehundredUSD == value).Select(x => x.Id).FirstOrDefault();
                }
                if (mg == 1000)
                {
                    selectedCategoryid = Model.Where(x => x.OneThousandUSD == value).Select(x => x.Id).FirstOrDefault();
                }
            }
            if (mg.HasValue && !string.IsNullOrEmpty(type) && type == "Domestic")
            {
                if (mg == 10)
                {
                    selectedCategoryid = Model.Where(x => x.Ten == value).Select(x => x.Id).FirstOrDefault();
                }
                if (mg == 25)
                {
                    selectedCategoryid = Model.Where(x => x.Twentyfive == value).Select(x => x.Id).FirstOrDefault();
                }
                if (mg == 50)
                {
                    selectedCategoryid = Model.Where(x => x.Fifty == value).Select(x => x.Id).FirstOrDefault();
                }
                if (mg == 100)
                {
                    selectedCategoryid = Model.Where(x => x.Onehundred == value).Select(x => x.Id).FirstOrDefault();
                }
                if (mg == 250)
                {
                    selectedCategoryid = Model.Where(x => x.TwohundredFifty == value).Select(x => x.Id).FirstOrDefault();
                }
                if (mg == 500)
                {
                    selectedCategoryid = Model.Where(x => x.Fivehundred == value).Select(x => x.Id).FirstOrDefault();
                }
                if (mg == 1000)
                {
                    selectedCategoryid = Model.Where(x => x.OneThousand == value).Select(x => x.Id).FirstOrDefault();
                }
            }
            return selectedCategoryid;
        }

        public static readonly Random rand = new Random();

        public static Color GetRandomColour()
        {
            return Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
        }
        public static int Distance(int x, int y)
        {
            return Math.Abs(x - y);
        }
        public static T Closest<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, TKey pivot) where TKey : IComparable<TKey>
        {
            return source.Where(x => pivot.CompareTo(keySelector(x)) <= 0).OrderBy(keySelector).FirstOrDefault();
        }
        public static int GetClosestMGValue(int? mg)
        {
            int findingMG = 0;
            if (mg.HasValue)
            {
                findingMG = mg.Value;
            }
            List<int> numbers = new List<int>() { 10, 25, 50, 100, 250, 500, 1000 };
            return numbers.Aggregate((x, y) => Math.Abs(x - findingMG) < Math.Abs(y - findingMG) ? x : y);
        }
        public static int GetClosestMasterCategoryPriceId(int? mg, int? value, string type, List<SZ_CategoryMaster> Model)
        {
            int findingNumber = 0;
            if (value.HasValue)
            {
                findingNumber = value.Value;
            }
            int findingMG = 0;
            if (mg.HasValue)
            {
                findingMG = mg.Value;
            }
            int selectedCategoryid = 0;
            if (mg.HasValue && !string.IsNullOrEmpty(type) && type == "Export")
            {
                if (mg == 10)
                {
                    List<int> numbers = Model.Select(x => (int)x.TenUSD).ToList();
                    int closest = numbers.Aggregate((x, y) => Math.Abs(x - findingNumber) < Math.Abs(y - findingNumber) ? x : y);
                    selectedCategoryid = Model.Where(x => x.TenUSD == closest).Select(x => x.Id).FirstOrDefault();
                }
                else if (mg == 25)
                {
                    List<int> numbers = Model.Select(x => (int)x.TwentyfiveUSD).ToList();
                    int closest = numbers.Aggregate((x, y) => Math.Abs(x - findingNumber) < Math.Abs(y - findingNumber) ? x : y);
                    selectedCategoryid = Model.Where(x => x.TwentyfiveUSD == closest).Select(x => x.Id).FirstOrDefault();
                }
                else if (mg == 50)
                {
                    List<int> numbers = Model.Select(x => (int)x.FiftyUSD).ToList();
                    int closest = numbers.Aggregate((x, y) => Math.Abs(x - findingNumber) < Math.Abs(y - findingNumber) ? x : y);
                    selectedCategoryid = Model.Where(x => x.FiftyUSD == closest).Select(x => x.Id).FirstOrDefault();
                }
                else if (mg == 100)
                {
                    List<int> numbers = Model.Select(x => (int)x.OnehundredUSD).ToList();
                    int closest = numbers.Aggregate((x, y) => Math.Abs(x - findingNumber) < Math.Abs(y - findingNumber) ? x : y);
                    selectedCategoryid = Model.Where(x => x.OnehundredUSD == closest).Select(x => x.Id).FirstOrDefault();
                }
                else if (mg == 250)
                {
                    List<int> numbers = Model.Select(x => (int)x.TwohundredFiftyUSD).ToList();
                    int closest = numbers.Aggregate((x, y) => Math.Abs(x - findingNumber) < Math.Abs(y - findingNumber) ? x : y);
                    selectedCategoryid = Model.Where(x => x.TwohundredFiftyUSD == closest).Select(x => x.Id).FirstOrDefault();
                }
                else if (mg == 500)
                {
                    List<int> numbers = Model.Select(x => (int)x.FivehundredUSD).ToList();
                    int closest = numbers.Aggregate((x, y) => Math.Abs(x - findingNumber) < Math.Abs(y - findingNumber) ? x : y);
                    selectedCategoryid = Model.Where(x => x.FivehundredUSD == closest).Select(x => x.Id).FirstOrDefault();
                }
                else if (mg == 1000)
                {
                    List<int> numbers = Model.Select(x => (int)x.OneThousandUSD).ToList();
                    int closest = numbers.Aggregate((x, y) => Math.Abs(x - findingNumber) < Math.Abs(y - findingNumber) ? x : y);
                    selectedCategoryid = Model.Where(x => x.OneThousandUSD == closest).Select(x => x.Id).FirstOrDefault();
                }
                else
                {
                    List<int> numbers = new List<int>() { 10, 25, 50, 100, 250, 500, 1000 };
                    int closest = numbers.Aggregate((x, y) => Math.Abs(x - findingMG) < Math.Abs(y - findingMG) ? x : y);
                    selectedCategoryid = GetClosestMasterCategoryPriceId(closest, value, type, Model);
                }
            }
            if (mg.HasValue && !string.IsNullOrEmpty(type) && type == "Domestic")
            {
                if (mg == 10)
                {
                    List<int> numbers = Model.Select(x => (int)x.Ten).ToList();
                    int closest = numbers.Aggregate((x, y) => Math.Abs(x - findingNumber) < Math.Abs(y - findingNumber) ? x : y);
                    selectedCategoryid = Model.Where(x => x.Ten == closest).Select(x => x.Id).FirstOrDefault();
                }
                else if (mg == 25)
                {
                    List<int> numbers = Model.Select(x => (int)x.Twentyfive).ToList();
                    int closest = numbers.Aggregate((x, y) => Math.Abs(x - findingNumber) < Math.Abs(y - findingNumber) ? x : y);
                    selectedCategoryid = Model.Where(x => x.Twentyfive == closest).Select(x => x.Id).FirstOrDefault();
                }
                else if (mg == 50)
                {
                    List<int> numbers = Model.Select(x => (int)x.Fifty).ToList();
                    int closest = numbers.Aggregate((x, y) => Math.Abs(x - findingNumber) < Math.Abs(y - findingNumber) ? x : y);
                    selectedCategoryid = Model.Where(x => x.Fifty == closest).Select(x => x.Id).FirstOrDefault();
                }
                else if (mg == 100)
                {
                    List<int> numbers = Model.Select(x => (int)x.Onehundred).ToList();
                    int closest = numbers.Aggregate((x, y) => Math.Abs(x - findingNumber) < Math.Abs(y - findingNumber) ? x : y);
                    selectedCategoryid = Model.Where(x => x.Onehundred == closest).Select(x => x.Id).FirstOrDefault();
                }
                else if (mg == 250)
                {
                    List<int> numbers = Model.Select(x => (int)x.TwohundredFifty).ToList();
                    int closest = numbers.Aggregate((x, y) => Math.Abs(x - findingNumber) < Math.Abs(y - findingNumber) ? x : y);
                    selectedCategoryid = Model.Where(x => x.TwohundredFifty == closest).Select(x => x.Id).FirstOrDefault();
                }
                else if (mg == 500)
                {
                    List<int> numbers = Model.Select(x => (int)x.Fivehundred).ToList();
                    int closest = numbers.Aggregate((x, y) => Math.Abs(x - findingNumber) < Math.Abs(y - findingNumber) ? x : y);
                    selectedCategoryid = Model.Where(x => x.Fivehundred == closest).Select(x => x.Id).FirstOrDefault();
                }
                else if (mg == 1000)
                {
                    List<int> numbers = Model.Select(x => (int)x.OneThousand).ToList();
                    int closest = numbers.Aggregate((x, y) => Math.Abs(x - findingNumber) < Math.Abs(y - findingNumber) ? x : y);
                    selectedCategoryid = Model.Where(x => x.OneThousand == closest).Select(x => x.Id).FirstOrDefault();
                }
                else
                {
                    List<int> numbers = new List<int>() { 10, 25, 50, 100, 250, 500, 1000 };
                    int closest = numbers.Aggregate((x, y) => Math.Abs(x - findingMG) < Math.Abs(y - findingMG) ? x : y);
                    selectedCategoryid = GetClosestMasterCategoryPriceId(closest, value, type, Model);
                }
            }
            return selectedCategoryid;
        }
        public static string ToShortMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);
        }
        public static string GetOrdinalDate(this DateTime value, bool isSup = false)
        {
            string day = value.Day.Ordinal(isSup);
            string month = value.ToString("MMM");
            string year = value.ToString("yy");
            return day + " " + month + " " + year;
        }
        public static string Ordinal(this int number, bool isSup = false)
        {
            string suffix = String.Empty;

            int ones = number % 10;
            int tens = (int)Math.Floor(number / 10M) % 10;

            if (tens == 1)
            {
                suffix = "th";
            }
            else
            {
                switch (ones)
                {
                    case 1:
                        suffix = "st";
                        break;

                    case 2:
                        suffix = "nd";
                        break;

                    case 3:
                        suffix = "rd";
                        break;

                    default:
                        suffix = "th";
                        break;
                }
            }

            if (isSup)
            {
                return String.Format("{0}<sup>{1}</sup>", number, suffix);
            }
            return String.Format("{0}{1}", number, suffix);
        }

        public static int GetMonthsBetween(DateTime? fromdate, DateTime? todate)
        {
            if (!fromdate.HasValue || !todate.HasValue)
            {
                return 0;
            }

            DateTime from = fromdate.Value;
            DateTime to = todate.Value;

            if (from > to) return GetMonthsBetween(to, from);

            var monthDiff = Math.Abs((to.Year * 12 + (to.Month - 1)) - (from.Year * 12 + (from.Month - 1)));

            if (from.AddMonths(monthDiff) > to || to.Day < from.Day)
            {
                return monthDiff - 1;
            }
            else
            {
                return monthDiff;
            }
        }

        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }
            return null; // could also return string.Empty
        }

        public static void SetCookie(string keyName, string value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[keyName];
            if (cookie == null) cookie = new HttpCookie(keyName);
            cookie.Value = Convert.ToString(value);
            cookie.Expires = DateTime.Now.AddDays(3);
            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        public static string GetCookie(string keyName)
        {
            if (HttpContext.Current.Request.Cookies[keyName] == null)
            {
                return null;
            }
            return HttpContext.Current.Request.Cookies[keyName].Value;
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public static byte[] CreateImage(string text, int fontSize, int width, int height)
        {
            using (var b = new Bitmap(width, height))
            {
                using (var g = Graphics.FromImage(b))
                {
                    using (var br = new SolidBrush(System.Drawing.Color.Red))
                    {
                        using (var f = new System.Drawing.Font("Arial Unicode MS", fontSize))
                        {
                            g.DrawString(text, f, br, 10, 10);
                            using (var ms = new System.IO.MemoryStream())
                            {
                                b.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                return ms.ToArray();
                            }
                        }
                    }
                }
            }
        }

        public static int TotalPageCount(string file)
        {
            using (StreamReader sr = new StreamReader(System.IO.File.OpenRead(file)))
            {
                Regex regex = new Regex(@"/Type\s*/Page[^s]");
                MatchCollection matches = regex.Matches(sr.ReadToEnd());

                return matches.Count;
            }
        }

        public static string ReplaceForFilepath(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            if (value.Contains("Plesk"))
            {
                value = value.Replace(@"D:\Plesk\Vhosts\synzeal.com\spms.synzeal.com", "..");
            }
            return value.Replace("..", "");
        }

        public static int ClosestTo(this IEnumerable<int> collection, int target)
        {
            // NB Method will return int.MaxValue for a sequence containing no elements.
            // Apply any defensive coding here as necessary.
            var closest = int.MaxValue;
            var minDifference = int.MaxValue;
            foreach (var element in collection)
            {
                var difference = Math.Abs((long)element - target);
                if (minDifference > difference)
                {
                    minDifference = (int)difference;
                    closest = element;
                }
            }

            return closest;
        }

        public static string ones(string Number)
        {
            int _Number = Convert.ToInt32(Number);
            string name = "";
            switch (_Number)
            {

                case 1:
                    name = "One";
                    break;
                case 2:
                    name = "Two";
                    break;
                case 3:
                    name = "Three";
                    break;
                case 4:
                    name = "Four";
                    break;
                case 5:
                    name = "Five";
                    break;
                case 6:
                    name = "Six";
                    break;
                case 7:
                    name = "Seven";
                    break;
                case 8:
                    name = "Eight";
                    break;
                case 9:
                    name = "Nine";
                    break;
            }
            return name;
        }

        public static string tens(string Number)
        {
            int _Number = Convert.ToInt32(Number);
            string name = null;
            switch (_Number)
            {
                case 10:
                    name = "Ten";
                    break;
                case 11:
                    name = "Eleven";
                    break;
                case 12:
                    name = "Twelve";
                    break;
                case 13:
                    name = "Thirteen";
                    break;
                case 14:
                    name = "Fourteen";
                    break;
                case 15:
                    name = "Fifteen";
                    break;
                case 16:
                    name = "Sixteen";
                    break;
                case 17:
                    name = "Seventeen";
                    break;
                case 18:
                    name = "Eighteen";
                    break;
                case 19:
                    name = "Nineteen";
                    break;
                case 20:
                    name = "Twenty";
                    break;
                case 30:
                    name = "Thirty";
                    break;
                case 40:
                    name = "Fourty";
                    break;
                case 50:
                    name = "Fifty";
                    break;
                case 60:
                    name = "Sixty";
                    break;
                case 70:
                    name = "Seventy";
                    break;
                case 80:
                    name = "Eighty";
                    break;
                case 90:
                    name = "Ninety";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                    }
                    break;
            }
            return name;
        }
        public static string ConvertWholeNumber(decimal? Numberdata)
        {
            string Number = Convert.ToString(Numberdata);
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX    
                bool isDone = false;//test if already translated    
                double dblAmt = (Convert.ToDouble(Number));
                if (dblAmt > 0)
                {
                    beginsZero = Number.StartsWith("0");

                    int numDigits = Number.Length;
                    int pos = 0;//store digit grouping    
                    String place = "";//digit grouping name:hundres,thousand,etc...    
                    switch (numDigits)
                    {
                        case 1://ones' range    

                            word = ones(Number);
                            isDone = true;
                            break;
                        case 2://tens' range    
                            word = tens(Number);
                            isDone = true;
                            break;
                        case 3://hundreds' range    
                            pos = (numDigits % 3) + 1;
                            place = " Hundred ";
                            break;
                        case 4://thousands' range    
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " Thousand ";
                            break;
                        case 7://millions' range    
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " Million ";
                            break;
                        case 10://Billions's range    
                        case 11:
                        case 12:

                            pos = (numDigits % 10) + 1;
                            place = " Billion ";
                            break;
                        //add extra case options for anything above Billion...    
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)    
                        if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumber(Convert.ToDecimal(Number.Substring(0, pos))) + place + ConvertWholeNumber(Convert.ToDecimal(Number.Substring(pos)));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumber(Convert.ToDecimal(Number.Substring(0, pos))) + ConvertWholeNumber(Convert.ToDecimal(Number.Substring(pos)));
                        }

                        //check for trailing zeros    
                        //if (beginsZero) word = " and " + word.Trim();    
                    }
                    //ignore digit grouping names    
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }

        public static List<PriceQtyModel> PreparePriceModel(this string pricevalue)
        {
            //800 mg@400 USD X 2 = 800 USD,
            //25 mg@100 USD,
            //50 mg@520 USD X 2 = 1040 USD,
            //900 mg@4 USD X 3 = 12 USD
            var model = new List<PriceQtyModel>();
            if (string.IsNullOrEmpty(pricevalue))
            {
                return model;
            }
            var allpricedata = pricevalue.Split(',');
            if (allpricedata.Count() == 1)
            {
                var submodel = new PriceQtyModel();
                var qty = "";
                var price = "";
                if (allpricedata[0].IndexOf("X") != -1)
                {
                    string packs = allpricedata[0].Split('X')[1];
                    int packsize = 0;
                    try
                    {
                        packsize = Convert.ToInt32(System.Text.RegularExpressions.Regex.Match(packs, @"\d+").Value);
                        qty = Convert.ToString(Convert.ToInt32(allpricedata[0].Split(' ')[0]) * Convert.ToInt32(packsize));
                    }
                    catch
                    {
                        qty = "0";
                    }
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
                        try
                        {
                            price = System.Text.RegularExpressions.Regex.Match(allpricedata[0].Split('@')[1], @"\d+").Value;
                        }
                        catch (Exception)
                        {
                            price = "";
                        }
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
                        submodel.Price = Convert.ToString(price);
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
                        var packsize = 0;
                        var qty = System.Text.RegularExpressions.Regex.Match(item.Split('@')[0], @"\d+").Value;
                        var price = System.Text.RegularExpressions.Regex.Match(item.Split('@')[1], @"\d+").Value;
                        if (item.Split('X').Count() > 1)
                        {
                            if (item.Split('X')[1].Contains("="))
                            {
                                packsize = Convert.ToInt32(item.Split('X')[1].Split('=')[0]);
                            }
                            else
                            {
                                packsize = Convert.ToInt32(item.Split('X')[1]);
                            }
                        }
                        if (!string.IsNullOrEmpty(price))
                        {
                            submodel.MG = Convert.ToString(qty);
                            submodel.Price = Convert.ToString(price);
                            submodel.PackSize = packsize;
                            model.Add(submodel);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return model;
        }

        public static string GetPriceFromPriceString(this string value)
        {
            var price = "";
            if (string.IsNullOrEmpty(value))
            {
                return price;
            }
            try
            {
                if (value.IndexOf("X") != -1)
                {
                    string packs = value.Split('X')[1];
                    int packsize = Convert.ToInt32(System.Text.RegularExpressions.Regex.Match(packs, @"\d+").Value);
                    if (value.IndexOf("=") != -1)
                    {
                        //var mg = Convert.ToInt32(value.Split('=')[0].Split('X')[0].Replace("mg", "").Trim())* Convert.ToInt32(value.Split('=')[0].Split('X')[1].Trim());
                        price = System.Text.RegularExpressions.Regex.Match(value.Split('=')[1], @"\d+").Value;
                    }
                    else
                    {
                        price = System.Text.RegularExpressions.Regex.Match(value.Split('@')[1], @"\d+").Value;
                    }
                }
                else
                {
                    price = System.Text.RegularExpressions.Regex.Match(value.Split('@')[1], @"\d+").Value;
                }
            }
            catch (Exception)
            {
            }

            return price;
        }

        public static string GetQTYFromPriceString(this string value)
        {
            var mgdata = "";
            if (string.IsNullOrEmpty(value))
            {
                return mgdata;
            }
            try
            {
                if (value.IndexOf("X") != -1)
                {
                    string packs = value.Split('X')[1];
                    int packsize = Convert.ToInt32(System.Text.RegularExpressions.Regex.Match(packs, @"\d+").Value);
                    if (value.IndexOf("=") != -1)
                    {
                        var mg = Convert.ToInt32(System.Text.RegularExpressions.Regex.Match(Convert.ToString(value.Split('@')[0]), @"\d+").Value) * packsize;
                        mgdata = Convert.ToString(mg);
                    }
                    else
                    {
                        mgdata = System.Text.RegularExpressions.Regex.Match(value.Split('@')[0], @"\d+").Value;
                    }
                }
                else
                {
                    mgdata = System.Text.RegularExpressions.Regex.Match(value.Split('@')[0], @"\d+").Value;
                }
            }
            catch (Exception)
            {
            }

            return mgdata;
        }

        public static void MargeMultiplePDF(List<string> PDFfileNames, string OutputFile)
        {
            // Create document object  
            iTextSharp.text.Document PDFdoc = new iTextSharp.text.Document();
            // Create a object of FileStream which will be disposed at the end  
            using (System.IO.FileStream MyFileStream = new System.IO.FileStream(OutputFile, System.IO.FileMode.Create))
            {
                // Create a PDFwriter that is listens to the Pdf document  
                iTextSharp.text.pdf.PdfCopy PDFwriter = new iTextSharp.text.pdf.PdfCopy(PDFdoc, MyFileStream);
                if (PDFwriter == null)
                {
                    return;
                }
                // Open the PDFdocument  
                PDFdoc.Open();
                foreach (string fileName in PDFfileNames)
                {
                    try
                    {
                        // Create a PDFreader for a certain PDFdocument  
                        iTextSharp.text.pdf.PdfReader PDFreader = new iTextSharp.text.pdf.PdfReader(fileName);

                        PDFreader.ConsolidateNamedDestinations();
                        // Add content  
                        for (int i = 1; i <= PDFreader.NumberOfPages; i++)
                        {
                            iTextSharp.text.pdf.PdfImportedPage page = PDFwriter.GetImportedPage(PDFreader, i);
                            PDFwriter.AddPage(page);
                        }
                        iTextSharp.text.pdf.PRAcroForm form = PDFreader.AcroForm;
                        if (form != null)
                        {
                            PDFwriter.CopyDocumentFields(PDFreader);
                        }

                        // Close PDFreader  
                        PDFreader.Close();
                    }
                    catch (Exception)
                    {

                    }
                }
                // Close the PDFdocument and PDFwriter  
                PDFwriter.Close();
                PDFdoc.Close();
            }// Disposes the Object of FileStream  
        }

        public static string GetProStatus(string value)
        {
            foreach (EnumList.ProStatusDDL r in Enum.GetValues(typeof(EnumList.ProStatusDDL)))
            {
                var item = Enum.GetName(typeof(EnumList.ProStatusDDL), r);
                var test = r.ToString();
                string text = SZ_Helper.GetEnumDescription((EnumList.ProStatusDDL)(int)r);
                if (Convert.ToString((int)r) == value)
                {
                    value = text;
                }
            }
            return value;
        }

        public static string encrypt(string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string TimeAgo(this DateTime dateTime)
        {
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} Sec", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ?
                    String.Format("{0} Min", timeSpan.Minutes) :
                    "1 Min";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ?
                    String.Format("{0} Hour", timeSpan.Hours) :
                    "1 Hour";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ?
                    String.Format("{0}D", timeSpan.Days) :
                    "1D";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ?
                    String.Format("{0}M", timeSpan.Days / 30) :
                    "1M";
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    String.Format("{0}Y", timeSpan.Days / 365) :
                    "1Y";
            }

            return result;
        }

        public static string GetDiscountedPrice(this string pricedata, decimal? discount, bool returnNumber = false)
        {
            var allpricedata = pricedata.Split(',');
            var alldata = new List<string>();
            foreach (var priitem in allpricedata)
            {
                var prilist = priitem.Split('@');
                var isNumeric = System.Text.RegularExpressions.Regex.IsMatch(prilist[1], @"\d");
                if (isNumeric)
                {
                    if (prilist[1].Contains("X"))
                    {
                        int mgdata = Convert.ToInt32(prilist[0].Trim().Split(' ')[0]);
                        decimal price = Convert.ToDecimal(prilist[1].Trim().Split(' ')[0]);
                        var pack = 0;

                        if (priitem.Split('X')[1].Trim().Contains("="))
                        {
                            pack = Convert.ToInt32(priitem.Split('X')[1].Trim().Split(' ')[0]);
                        }
                        else
                        {
                            pack = Convert.ToInt32(priitem.Split('X')[1].Trim());
                        }

                        if (discount > 0)
                        {
                            price = price - (price * discount.Value / 100);
                            price = Math.Round(price, MidpointRounding.ToEven);
                        }

                        if (returnNumber)
                        {
                            alldata.Add(Convert.ToString(price * pack));

                        }
                        else
                        {
                            alldata.Add(mgdata * pack + " mg@" + (price * pack) + " " + prilist[1].Trim().Split(' ')[1] + "(" + mgdata + " mg*" + pack + "vials)");
                        }
                    }
                    else
                    {
                        int mgdata = Convert.ToInt32(prilist[0].Trim().Split(' ')[0]);
                        decimal price = Convert.ToInt32(prilist[1].Trim().Split(' ')[0]);

                        if (discount > 0)
                        {
                            price = price - (price * discount.Value / 100);

                            price = Math.Round(price, MidpointRounding.ToEven);

                        }
                        if (returnNumber)
                        {
                            alldata.Add(Convert.ToString(price));
                        }
                        else
                        {
                            alldata.Add(mgdata + " mg@" + price + " " + prilist[1].Trim().Split(' ')[1]);
                        }
                        //alldata.Add(priitem);
                    }
                }
            }
            var str = string.Join(", ", alldata);

            return str;
        }
    }
}