using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Configuration;
using HtmlAgilityPack;
using System.Data.Entity.Validation;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Data.Entity;
using Synzeal_Inventory.Entity;
using System;
using Synzeal_Inventory.Models;
using System.Text;
using System.Security.Cryptography;

namespace Synzeal_Inventory.Controllers
{
    public class HomeController : Controller
    {
        public SynzealLiveEntities db = new SynzealLiveEntities();
        public ActionResult Index()
        {
            //var Slogan = db.SZ_Slogan.ToList();
            //Random rnd = new Random();
            //int r = rnd.Next(Slogan.Count);
            //ViewBag.Slogan = Slogan[r];
            return View();
        }

        public ActionResult pinterest(string q)
        {
            string outputLink = string.Empty;
            string URL = "https://www.pinterest.ca/search/pins/?q=" + q;
            URL = "https://in.pinterest.com/search/pins/?q=" + q + "&rs=typed";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
            req.Referer = "http://stackoverflow.com";
            req.UserAgent = "Mozilla/5.0";
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            string source;
            using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                source = reader.ReadToEnd();
                var htmldocument = new HtmlDocument();

                htmldocument.LoadHtml(source);
                var div = htmldocument.DocumentNode.Descendants("div").Where(x => x.GetAttributeValue("class", "").Equals("hCL kVc L4E MIw")).ToList();

                foreach (var item in div)
                {
                    var innerhtmldocument = new HtmlDocument();
                    innerhtmldocument.LoadHtml(item.InnerHtml);
                    var productNameRecord = innerhtmldocument.DocumentNode.Descendants("p").Where(x => x.GetAttributeValue("class", "").Equals("cat-product-name")).FirstOrDefault();
                    if (productNameRecord != null)
                    {
                        var pUrl = innerhtmldocument.DocumentNode.Descendants("a").Where(x => x.GetAttributeValue("class", "").Equals("targetBlank")).FirstOrDefault();
                        var priceold = innerhtmldocument.DocumentNode.Descendants("div").Where(x => x.GetAttributeValue("class", "").Equals("price-old")).FirstOrDefault().InnerText.Replace("Retail Price", "");
                        var pricecurrent = innerhtmldocument.DocumentNode.Descendants("div").Where(x => x.GetAttributeValue("class", "").Equals("price_with_emi")).FirstOrDefault().InnerText.Replace("Offer Price", "");
                        string productName = productNameRecord.InnerText.Trim();
                        outputLink += pUrl.Attributes.Where(x => x.Name == "href").Select(x => x.Value).FirstOrDefault() + " / Price Old : " + priceold + " / Price New : " + pricecurrent + "<br>";
                    }
                }
            }
            return Content(outputLink);
        }

        public ActionResult woodenstreet(string caturl, int pageno)
        {
            //http://localhost:1360/home/woodenstreet?caturl=https://www.woodenstreet.com/beds-without-storage&pageno=10
            string outputLink = string.Empty;

            for (int i = 1; i <= pageno; i++)
            {
                string URL = caturl + "?pages=" + i;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
                req.Referer = "http://stackoverflow.com";
                req.UserAgent = "Mozilla/5.0";
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();


                string source;
                using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    source = reader.ReadToEnd();
                    var htmldocument = new HtmlDocument();

                    htmldocument.LoadHtml(source);
                    var div = htmldocument.DocumentNode.Descendants("article").Where(x => x.GetAttributeValue("class", "").Equals("box")).ToList();


                    foreach (var item in div)
                    {
                        var innerhtmldocument = new HtmlDocument();
                        innerhtmldocument.LoadHtml(item.InnerHtml);
                        var productNameRecord = innerhtmldocument.DocumentNode.Descendants("div").Where(x => x.GetAttributeValue("class", "").Equals("in")).FirstOrDefault();
                        if (productNameRecord != null)
                        {
                            var pUrl = innerhtmldocument.DocumentNode.Descendants("a").Where(x => x.GetAttributeValue("class", "").Equals("targetBlank")).FirstOrDefault();
                            var priceold = innerhtmldocument.DocumentNode.Descendants("div").Where(x => x.GetAttributeValue("class", "").Equals("price-old")).FirstOrDefault().InnerText.Replace("Retail Price", "");
                            var pricecurrent = innerhtmldocument.DocumentNode.Descendants("div").Where(x => x.GetAttributeValue("class", "").Equals("price_with_emi")).FirstOrDefault().InnerText.Replace("Offer Price", "");
                            string productName = productNameRecord.InnerText.Trim();

                            outputLink += pUrl.Attributes.Where(x => x.Name == "href").Select(x => x.Value).FirstOrDefault() + " / Price Old : " + priceold + " / Price New : " + pricecurrent + "<br>";
                        }

                        //decimal price = Convert.ToDecimal(innerhtmldocument.DocumentNode.Descendants("cat-product-name").FirstOrDefault().InnerHtml.Replace("$", ""));
                        //if (price >= 19 && price <= 200)
                        //{
                        //    string record = innerhtmldocument.DocumentNode.Descendants("a").FirstOrDefault().Attributes.FirstOrDefault().Value;
                        //    outputLink += "https://www.zebradiscounts.com" + record + "<br>";
                        //}
                    }
                }
            }



            return Content(outputLink);
        }
        public ActionResult CrawlWebsite(string caturl, int pageno)
        {
            string outputLink = string.Empty;

            for (int i = 1; i <= pageno; i++)
            {
                string URL = caturl + "?pagesize=90&pagenumber=" + i;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
                req.Referer = "http://stackoverflow.com";
                req.UserAgent = "Mozilla/5.0";
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();


                string source;
                using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    source = reader.ReadToEnd();
                    var htmldocument = new HtmlDocument();

                    htmldocument.LoadHtml(source);
                    var div = htmldocument.DocumentNode.Descendants("div").Where(x => x.GetAttributeValue("class", "").Equals("item-box")).ToList();


                    foreach (var item in div)
                    {
                        var innerhtmldocument = new HtmlDocument();
                        innerhtmldocument.LoadHtml(item.InnerHtml);

                        decimal price = Convert.ToDecimal(innerhtmldocument.DocumentNode.Descendants("span").FirstOrDefault().InnerHtml.Replace("$", ""));
                        if (price >= 19 && price <= 200)
                        {
                            string record = innerhtmldocument.DocumentNode.Descendants("a").FirstOrDefault().Attributes.FirstOrDefault().Value;
                            outputLink += "https://www.zebradiscounts.com" + record + "<br>";
                        }
                    }
                }
            }



            return Content(outputLink);
        }
        public ISet<string> GetNewLinks(string content)
        {
            Regex regexLink = new Regex("(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))");

            ISet<string> newLinks = new HashSet<string>();
            foreach (var match in regexLink.Matches(content))
            {
                if (!newLinks.Contains(match.ToString()))
                    newLinks.Add(match.ToString());
            }

            return newLinks;
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();

            HttpCookie aCookie;
            string cookieName;
            int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;
                if (cookieName != "IsOTPValidate")
                {
                    aCookie = new HttpCookie(cookieName);
                    aCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(aCookie);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult sendSMS()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 |
                                   SecurityProtocolType.Tls11 |
                                   SecurityProtocolType.Tls |
                                   SecurityProtocolType.Ssl3;
            //string result = "";
            //String message = HttpUtility.UrlEncode("This is your message");
            //using (var wb = new WebClient())
            //{
            //    byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
            //    {
            //    {"apikey" , "NDU2MTMzNmY3NjMxNzE3NDU1NmU2MjMwMzY0ODcyNmM="},
            //    {"numbers" , "919033303353"},
            //    {"message" , message},
            //    {"sender" , "SZOTP"}
            //    });
            //    result = System.Text.Encoding.UTF8.GetString(response);

            //}

            String result;
            string apiKey = "NDU2MTMzNmY3NjMxNzE3NDU1NmU2MjMwMzY0ODcyNmM=";
            string numbers = "919033303353"; // in a comma seperated list
            string message = "This is your message";
            string sender = "600010";

            String url = "https://api.textlocal.in/send/?apikey=" + apiKey + "&numbers=" + numbers + "&message=" + message + "&sender=" + sender;
            //refer to parameters to complete correct url string

            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

            objRequest.Method = "POST";
            objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(url);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                // Close and clean up the StreamReader
                sr.Close();
            }
            return Content(result);
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {

            Session.Abandon();
            Session.Clear();

            HttpCookie aCookie;
            string cookieName;
            int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;
                if (cookieName != "IsOTPValidate")
                {
                    aCookie = new HttpCookie(cookieName);
                    aCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(aCookie);
                }
            }

            string ipAddress = GetIPAddress();
            string email = form.Get("Email");
            string password = form.Get("Password");
            var customer = ValidateCustomer(email, password);
            if (customer != null)
            {
                string customerName = string.Empty;
                var genericAttr = db.GenericAttributes.Where(x => x.KeyGroup == "Customer" && x.EntityId == customer.Id).ToList();
                if (genericAttr.Count > 0)
                {
                    customerName = genericAttr.Where(x => x.Key == "FirstName").Select(x => x.Value).FirstOrDefault() + " " + genericAttr.Where(x => x.Key == "LastName").Select(x => x.Value).FirstOrDefault();
                }
                MemoryCacheManager objCache = new MemoryCacheManager();
                var tagsData = objCache.Get("cache.tagsData", () =>
                {
                    return db.SZ_RuleDetail.ToList();
                });
                string redirectAction = string.Empty;
                SessionCookieManagement.IsLogin = true;
                SessionCookieManagement.UserEmail = customer.Email;
                SessionCookieManagement.UserId = customer.Id;
                SessionCookieManagement.UserName = customerName;
                SessionCookieManagement.Name = customer.GetFullName(); 
                SessionCookieManagement.CountryId = 0;
                if (!Request.Url.Host.Contains("localhost") && ipAddress != "14.194.54.2" && ipAddress != "103.254.244.213" && ipAddress != "182.70.115.168" && customer.Email != "parthsuthar2010@gmail.com")
                {
                    if (!customer.OTPValidationDate.HasValue)
                    {
                        customer.OTPValidationDate = DateTime.Now.AddDays(-5);
                    }
                    if (DateTime.Now.Date != customer.OTPValidationDate.Value.Date)
                    {
                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                        string sRandomOTP = GenerateRandomOTP(6, saAllowedCharacters);
                        customer.OTP = sRandomOTP;
                        customer.LastIpAddress = ipAddress;
                        db.Entry(customer).State = EntityState.Modified;
                        db.SaveChanges();

                        if (!string.IsNullOrEmpty(customer.SecEmail))
                        {
                            OTPGenerationEmail(customer.SecEmail, sRandomOTP);
                            return RedirectToAction("otpvalidation", "Home", new { token = Common.encrypt(SessionCookieManagement.UserEmail) });
                        }
                        else
                        {
                            TempData["Message"] = "You have not assign any email address for OTP.";
                            return View();
                        }
                    }
                    else
                    {
                        customer.LastIpAddress = ipAddress;
                        db.Entry(customer).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    customer.LastIpAddress = ipAddress;
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                }
                List<CustomerRole> customerRoles = customer.CustomerRoles.ToList();

                if (customerRoles.Where(x => x.Name.ToLower().Contains("master admin")).Count() > 0)
                {
                    SessionCookieManagement.IsMasterAdmin = true;
                    SessionCookieManagement.IsPurchase = true;
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("quote")).Count() > 0)
                {
                    SessionCookieManagement.IsQuote = true;
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("project")).Count() > 0)
                {
                    SessionCookieManagement.IsProjectLogin = true;
                    redirectAction = "Project";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("documentation")).Count() > 0)
                {
                    SessionCookieManagement.IsDocumentation = true;
                    redirectAction = "Dispatch";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("invoice")).Count() > 0)
                {
                    SessionCookieManagement.IsInvoice = true;
                    redirectAction = "Invoice";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("dispatch")).Count() > 0)
                {
                    SessionCookieManagement.IsDispatch = true;
                    redirectAction = "Dispatch";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("domestic dispatch")).Count() > 0)
                {
                    SessionCookieManagement.IsDomesticDispatch = true;
                    redirectAction = "Dispatch";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("export dispatch")).Count() > 0)
                {
                    SessionCookieManagement.IsExportDispatch = true;
                    redirectAction = "Dispatch";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("purchase")).Count() > 0)
                {
                    SessionCookieManagement.IsPurchase = true;
                    redirectAction = "Purchase";
                }
                if (customerRoles.Where(x => x.SystemName.ToLower().Contains("miniadmin")).Count() > 0)
                {
                    SessionCookieManagement.IsMiniAdmin = true;
                    redirectAction = "QuotationList";
                }
                if (customerRoles.Where(x => x.SystemName.ToLower().Contains("analytical")).Count() > 0)
                {
                    SessionCookieManagement.IsAnalytical = true;
                    redirectAction = "ScientistQueryModule";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("client")).Count() > 0)
                {
                    SessionCookieManagement.IsClient = true;
                    SessionCookieManagement.LoginCompanyName = customer.AdminComment.ToLower();
                    HttpCookie LoginCompanyName = new HttpCookie("LoginCompanyName", customer.AdminComment.ToLower());
                    LoginCompanyName.Expires = DateTime.Now.AddDays(1);
                    Response.SetCookie(LoginCompanyName);
                    redirectAction = "Client";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("pendingupload")).Count() > 0)
                {
                    SessionCookieManagement.IsPendingUpload = true;
                    //SessionCookieManagement.LoginCompanyName = customer.AdminComment.ToLower();
                    redirectAction = "PendingUpload";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("scientist")).Count() > 0)
                {
                    SessionCookieManagement.IsScientist = true;
                    redirectAction = "SciDashboard";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("subscientist")).Count() > 0)
                {
                    SessionCookieManagement.IsScientist = false;
                    SessionCookieManagement.IsSubScientist = true;
                    redirectAction = "SciDashboard";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("followup")).Count() > 0)
                {
                    SessionCookieManagement.IsFollowUp = true;
                    redirectAction = "TodayDashboard";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("price")).Count() > 0)
                {
                    SessionCookieManagement.IsPrice = true;
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("administrators")).Count() > 0)
                {
                    SessionCookieManagement.IsAdmin = true;
                    redirectAction = "TodayDashboard";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("qc")).Count() > 0)
                {
                    SessionCookieManagement.IsQC = true;
                    redirectAction = "QcModuleList";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("international")).Count() > 0)
                {
                    SessionCookieManagement.IsInternational = true;
                    redirectAction = "MainDashboard";
                }

                if (customerRoles.Where(x => x.Name.ToLower().Contains("control substance")).Count() > 0)
                {
                    SessionCookieManagement.IsControlledSubstance = true;
                    redirectAction = "Project";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("glp")).Count() > 0)
                {
                    SessionCookieManagement.IsGLP = true;
                    redirectAction = "Project";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("support")).Count() > 0)
                {
                    SessionCookieManagement.IsSupport = true;
                    redirectAction = "Project";
                }

                var useraccessData = db.SZ_UserManagement.Where(x => x.UserId == customer.Id).FirstOrDefault();
                if (useraccessData != null && useraccessData.DefaultCurrencyId.HasValue)
                {
                    SessionCookieManagement.DefaultCurrencyId = useraccessData.DefaultCurrencyId.Value;
                }
                else
                {
                    SessionCookieManagement.DefaultCurrencyId = 1;
                }

                if (!string.IsNullOrEmpty(redirectAction))
                {
                    return RedirectToAction(redirectAction, "Form");
                }

                TempData["Message"] = "You have not assign any role for accessing this system.";
                return View();
            }

            TempData["Message"] = "Please enter proper email and password";
            return View();
        }

        public ActionResult otpvalidation(string token)
        {
            string email = Common.Decrypt(token);
            ViewBag.EMail = email;
            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        public ActionResult OTPValidation(FormCollection form)
        {
            string email = form.Get("Email");
            string password = form.Get("OTP");
            string token = form.Get("Token");
            var customer = db.Customers.Where(x => x.Email.ToLower() == email.ToLower() && x.Active == true && x.Deleted == false).FirstOrDefault();
            if (customer != null)
            {
                if (customer.OTP != password)
                {
                    TempData["Message"] = "Please enter valid OTP number";
                    return RedirectToAction("otpvalidation", "Home", new { token = token });
                }

                SessionCookieManagement.IsOTPValidate = true;
                string customerName = string.Empty;
                var genericAttr = db.GenericAttributes.Where(x => x.KeyGroup == "Customer" && x.EntityId == customer.Id).ToList();
                if (genericAttr.Count > 0)
                {
                    customerName = genericAttr.Where(x => x.Key == "FirstName").Select(x => x.Value).FirstOrDefault() + " " + genericAttr.Where(x => x.Key == "LastName").Select(x => x.Value).FirstOrDefault();
                }

                string redirectAction = string.Empty;
                SessionCookieManagement.IsLogin = true;
                SessionCookieManagement.UserEmail = customer.Email;
                SessionCookieManagement.UserId = customer.Id;
                SessionCookieManagement.UserName = customerName;
                SessionCookieManagement.Name = customer.GetFullName();
                List<CustomerRole> customerRoles = customer.CustomerRoles.ToList();

                if (customerRoles.Where(x => x.Name.ToLower().Contains("master admin")).Count() > 0)
                {
                    SessionCookieManagement.IsMasterAdmin = true;
                    SessionCookieManagement.IsPurchase = true;
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("quote")).Count() > 0)
                {
                    SessionCookieManagement.IsQuote = true;
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("project")).Count() > 0)
                {
                    SessionCookieManagement.IsProjectLogin = true;
                    redirectAction = "Project";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("invoice")).Count() > 0)
                {
                    SessionCookieManagement.IsInvoice = true;
                    redirectAction = "Invoice";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("dispatch")).Count() > 0)
                {
                    SessionCookieManagement.IsDispatch = true;
                    redirectAction = "Dispatch";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("purchase")).Count() > 0)
                {
                    SessionCookieManagement.IsPurchase = true;
                    redirectAction = "Purchase";
                }
                if (customerRoles.Where(x => x.SystemName.ToLower().Contains("miniadmin")).Count() > 0)
                {
                    SessionCookieManagement.IsMiniAdmin = true;
                    redirectAction = "QuotationList";
                }
                if (customerRoles.Where(x => x.SystemName.ToLower().Contains("analytical")).Count() > 0)
                {
                    SessionCookieManagement.IsAnalytical = true;
                    redirectAction = "ScientistQueryModule";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("client")).Count() > 0)
                {
                    SessionCookieManagement.IsClient = true;
                    SessionCookieManagement.LoginCompanyName = customer.AdminComment.ToLower();
                    HttpCookie LoginCompanyName = new HttpCookie("LoginCompanyName", customer.AdminComment.ToLower());
                    LoginCompanyName.Expires = DateTime.Now.AddDays(1);
                    Response.SetCookie(LoginCompanyName);
                    redirectAction = "Client";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("pendingupload")).Count() > 0)
                {
                    SessionCookieManagement.IsPendingUpload = true;
                    //SessionCookieManagement.LoginCompanyName = customer.AdminComment.ToLower();
                    redirectAction = "PendingUpload";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("scientist")).Count() > 0)
                {
                    SessionCookieManagement.IsScientist = true;
                    redirectAction = "SciDashboard";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("subscientist")).Count() > 0)
                {
                    SessionCookieManagement.IsScientist = false;
                    SessionCookieManagement.IsSubScientist = true;
                    redirectAction = "SciDashboard";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("followup")).Count() > 0)
                {
                    SessionCookieManagement.IsFollowUp = true;
                    redirectAction = "TodayDashboard";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("price")).Count() > 0)
                {
                    SessionCookieManagement.IsPrice = true;
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("administrators")).Count() > 0)
                {
                    SessionCookieManagement.IsAdmin = true;
                    redirectAction = "TodayDashboard";
                }
                if (customerRoles.Where(x => x.Name.ToLower().Contains("qc")).Count() > 0)
                {
                    SessionCookieManagement.IsQC = true;
                    redirectAction = "QcModuleList";
                }

                if (customerRoles.Where(x => x.Name.ToLower().Contains("international")).Count() > 0)
                {
                    SessionCookieManagement.IsInternational = true;
                    redirectAction = "MainDashboard";
                }

                customer.OTPValidationDate = DateTime.Now;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();

                //if (!customer.OTPValidationDate.HasValue)
                //{
                //    customer.OTPValidationDate = DateTime.Now.AddDays(-5);
                //}
                //if (DateTime.Now.Date != customer.OTPValidationDate.Value)
                //{
                //    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

                //    string sRandomOTP = GenerateRandomOTP(6, saAllowedCharacters);
                //    customer.OTP = sRandomOTP;
                //    db.Entry(customer).State = EntityState.Modified;
                //    db.SaveChanges();

                //    OTPGenerationEmail(customer.SecEmail, sRandomOTP);

                //    return RedirectToAction("otpvalidation", "Home", new { token = encrypt(SessionCookieManagement.UserEmail) });
                //}

                if (!string.IsNullOrEmpty(redirectAction))
                {
                    return RedirectToAction(redirectAction, "Form");
                }

                TempData["Message"] = "You have not assign any role for accessing this system.";
                return View();
            }

            TempData["Message"] = "Please enter proper email and password";
            return View();
        }

        public void OTPGenerationEmail(string email, string otp)
        {
            try
            {
                MailMessage mail = new MailMessage();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 |
                                   SecurityProtocolType.Tls11 |
                                   SecurityProtocolType.Tls |
                                   SecurityProtocolType.Ssl3;
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["Email.Host"]);
                mail.From = new MailAddress(ConfigurationManager.AppSettings["Email.Username"], "SynZeal Standards");
                mail.To.Add(email);
                mail.Bcc.Add("parth.suthar@synzeal.com");
                mail.Bcc.Add("it@synzeal.com");
                //mail.To. = adminemail;
                mail.Subject = "OTP for SPMS Login";

                string html = System.IO.File.ReadAllText(Server.MapPath("~/Mail/OTPValidation.html"));
                html = html.Replace("#username", SessionCookieManagement.UserName);
                html = html.Replace("#otp", otp);
                mail.Body = html;
                mail.IsBodyHtml = true;
                SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Email.Username"], ConfigurationManager.AppSettings["Email.Password"]);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

            }
            catch (DbEntityValidationException e)
            {
                string errortext = "Line: " + e.LineNumber();


                foreach (var eve in e.EntityValidationErrors)
                {
                    errortext += "following validation errors:" + eve.Entry.Entity.GetType().Name + " ::: " + eve.Entry.State;
                    foreach (var ve in eve.ValidationErrors)
                    {
                        errortext += "- Property:" + ve.PropertyName + " / Error mesasge : " + ve.ErrorMessage;
                    }
                }

                System.IO.File.WriteAllText(Server.MapPath("~/img/Errorlog.txt"), System.DateTime.Now + " /Quote Data Error : " + errortext);

            }
        }

        private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
        {

            string sOTP = String.Empty;

            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)

            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;

        }
        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        private string GetComputer_LanIP()
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
            foreach (IPAddress ipAddress in ipEntry.AddressList)
            {
                if (ipAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    return ipAddress.ToString();
                }
            }
            return "-";
        }

        public enum PasswordFormat
        {
            Clear = 0,
            Hashed = 1,
            Encrypted = 2
        }
        public Customer ValidateCustomer(string usernameOrEmail, string password)
        {
            var customer = db.Customers.Where(x => x.Email == usernameOrEmail).FirstOrDefault();

            if (customer == null)
                return null;
            if (customer.Deleted)
                return null;
            if (!customer.Active)
                return null;

            string pwd = "";
            switch ((PasswordFormat)customer.PasswordFormatId)
            {
                case PasswordFormat.Encrypted:
                    //pwd = _encryptionService.EncryptText(password);
                    break;
                case PasswordFormat.Hashed:
                    pwd = CreatePasswordHash(password, customer.PasswordSalt);
                    break;
                default:
                    pwd = password;
                    break;
            }

            bool isValid = pwd == customer.Password;
            if (!isValid)
                return null;

            //save last login date
            customer.LastLoginDateUtc = DateTime.UtcNow;

            return customer;
        }
        public string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1")
        {
            return CreateHash(Encoding.UTF8.GetBytes(String.Concat(password, saltkey)), passwordFormat);
        }

        /// <summary>
        /// Create a data hash
        /// </summary>
        /// <param name="data">The data for calculating the hash</param>
        /// <param name="hashAlgorithm">Hash algorithm</param>
        /// <returns>Data hash</returns>
        public string CreateHash(byte[] data, string hashAlgorithm = "SHA1")
        {
            if (String.IsNullOrEmpty(hashAlgorithm))
                hashAlgorithm = "SHA1";

            //return FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPassword, passwordFormat);
            var algorithm = HashAlgorithm.Create(hashAlgorithm);
            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name");

            var hashByteArray = algorithm.ComputeHash(data);
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        public ActionResult TestEmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 |
                                   SecurityProtocolType.Tls11 |
                                   SecurityProtocolType.Tls |
                                   SecurityProtocolType.Ssl3;
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["Email.Host"]);
                mail.From = new MailAddress(ConfigurationManager.AppSettings["Email.Username"], "SynZeal Standards");
                mail.To.Add("parthsuthar2010@gmail.com");
                //mail.To. = adminemail;
                mail.Subject = "SynZeal Research Test Email";

                string html = System.IO.File.ReadAllText(Server.MapPath("~/Mail/TestEmail.html"));
                mail.Body = html;
                mail.IsBodyHtml = true;
                SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Email.Username"], ConfigurationManager.AppSettings["Email.Password"]);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

                return Content("Success");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult SettleFormData()
        {
            var formdata = db.SZ_QuoteDetailForm.ToList();
            formdata.ForEach(item =>
            {
                SZ_QuoteDetails_Form objdetform = new SZ_QuoteDetails_Form();
                objdetform.CreatedDate = DateTime.Now;
                objdetform.FormId = item.Id;
                objdetform.QuoteDetailsId = item.QuotationDetailsId;
                db.SZ_QuoteDetails_Form.Add(objdetform);
            });
            db.SaveChanges();
            return Content("success");
        }

        public ActionResult SettleQCData()
        {
            string approvedstatus = Convert.ToString((int)EnumList.ApprovedStatus.QCApproved);

            var model = (from i in db.SZ_QuoteDetailForm
                         join t3 in db.SZ_QuoteDetails_Form on i.Id equals t3.FormId
                         join q in db.SZ_QuotationDetail on t3.QuoteDetailsId equals q.Id
                         where i.IsDraftEntry == true
                         && (i.ApprovalStatus == null || i.ApprovalStatus != approvedstatus)
                         && (i.IsDispatchedEntry == null || i.IsDispatchedEntry == false)
                         select i).ToList();

            var allforms = db.SZ_QuoteDetailForm.ToList();
            foreach (var item in allforms)
            {
                var formid = item.Id;
                var ismatchdata = model.Where(x => x.Id == formid).Any();
                if (!ismatchdata)
                {
                    item.ApprovalStatus = Convert.ToString((int)EnumList.ApprovedStatus.QCApproved);
                    db.Entry(item).State = EntityState.Modified;
                }
                else
                {

                }
            }
            db.SaveChanges();

            return Content("success");
        }

    }
}
