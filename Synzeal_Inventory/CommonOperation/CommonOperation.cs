using Synzeal_Inventory.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Synzeal_Inventory.Models
{
    public class CommonOperation
    {
        public static SynzealLiveEntities db = new SynzealLiveEntities();
        private const string LOCALSTRINGRESOURCES_ALL_KEY = "Nop.lsr.all-{0}";

        public static List<SelectListItem> GetCustomerRoleSelectedList(string roleName)
        {
            MemoryCacheManager objCache = new MemoryCacheManager();
            var genericData = objCache.Get("cache.genericData", () =>
            {
                return db.GenericAttributes.Where(x => x.KeyGroup == "Customer").ToList();
            });
            var quoteUserList = db.Customers.Where(x => x.Deleted == false && x.Active == true && x.CustomerRoles.Select(z => z.SystemName.ToLower()).Contains(roleName)).OrderBy(x => x.Email).ToList();
            var listAssignTo = new List<SelectListItem>();
            listAssignTo.Add(new SelectListItem
            {
                Text = "--Select--",
                Value = ""
            });
            foreach (var term in quoteUserList)
            {
                string customerName = string.Empty;
                var genericAttr = genericData.Where(x => x.EntityId == term.Id).ToList();
                if (genericAttr.Count > 0)
                {
                    customerName = genericAttr.Where(x => x.Key == "FirstName").Select(x => x.Value).FirstOrDefault() + " " + genericAttr.Where(x => x.Key == "LastName").Select(x => x.Value).FirstOrDefault();
                }
                listAssignTo.Add(new SelectListItem
                {
                    Text = term.Email,
                    Value = term.Id.ToString()
                });
            }
            return listAssignTo;
        }

        public static string GetLocalizestring(string resourceKey)
        {
            return GetResource(resourceKey, SessionCookieManagement.DefaultCurrencyId);
        }

        public static string GetResource(string resourceKey, int languageId,
            bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false)
        {
            string result = string.Empty;
            if (resourceKey == null)
                resourceKey = string.Empty;
            resourceKey = resourceKey.Trim().ToLowerInvariant();
            if (true)
            {
                //load all records (we know they are cached)
                var resources = GetAllResourceValues(languageId);
                if (resources.ContainsKey(resourceKey))
                {
                    result = resources[resourceKey].Value;
                }
                else
                {
                    result = resourceKey;
                }
            }
            return result;
        }
        public static Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues(int languageId)
        {
            MemoryCacheManager _cacheManager = new MemoryCacheManager();
            string key = string.Format(LOCALSTRINGRESOURCES_ALL_KEY, languageId);
            return _cacheManager.Get(key, () =>
            {
                //we use no tracking here for performance optimization
                //anyway records are loaded only for read-only operations
                var query = from l in db.LocaleStringResources
                            where l.LanguageId == languageId
                            orderby l.ResourceName
                            select l;
                var locales = query.ToList();
                //format: <name, <id, value>>
                var dictionary = new Dictionary<string, KeyValuePair<int, string>>();
                foreach (var locale in locales)
                {
                    var resourceName = locale.ResourceName.ToLowerInvariant();
                    if (!dictionary.ContainsKey(resourceName))
                        dictionary.Add(resourceName, new KeyValuePair<int, string>(locale.Id, locale.ResourceValue));
                }
                return dictionary;
            });
        }

        public static string SettleQtyForSPMS(SZ_Inventory r)
        {
            string qty = "";
            if (r.Qty.HasValue)
            {
                if (r.RetentionQty.HasValue)
                {
                    if ((r.Qty - r.RetentionQty) < 0)
                    {
                        qty = "0";
                    }
                    else
                    {
                        qty = Convert.ToString(r.Qty - r.RetentionQty);
                    }
                }
                else
                {
                    if ((r.Qty - 15) < 0)
                    {
                        qty = "0";
                    }
                    else
                    {
                        qty = Convert.ToString(r.Qty - 15);
                    }
                }
            }

            return qty;
        }

        public static string GetMatchingTagHTML(SZ_QuotationDetail quoteDetail, string sectionName)
        {
            var html = "";
            var tags = GetMatchingTag(quoteDetail, sectionName);
            if (!string.IsNullOrEmpty(tags))
            {
                var listoftags = tags.Split(',');
                foreach (var t in listoftags)
                {
                    if (!string.IsNullOrEmpty(t))
                    {
                        html += "<span class='clstags'>" + t + "</span>";
                    }
                }
            }
            return html;
        }
        public static string GetMatchingTag(SZ_QuotationDetail quoteDetail, string sectionName)
        {
            string tags = "";
            MemoryCacheManager objCache = new MemoryCacheManager();
            var tagsData = objCache.Get("cache.tagsData", () =>
            {
                return db.SZ_RuleDetail.ToList();
            });

            var filteredData = tagsData.Where(x => x.Value == sectionName).Select(x => x.RuleId).ToList();
            var filteredTagsData = tagsData.Where(x => filteredData.Contains(x.RuleId)).ToList();
            if (!string.IsNullOrEmpty(quoteDetail.TracebilityCost) && quoteDetail.TracebilityCost != "0")
            {
                tags += string.Join(";", filteredTagsData.Where(x => x.Value == "Tracebility").Select(x => x.Tag).ToList().ToArray());
            }
            if (!string.IsNullOrEmpty(quoteDetail.ColdShipCost) && quoteDetail.ColdShipCost != "0")
            {
                tags += string.Join(";", filteredTagsData.Where(x => x.Value == "ColdShipment").Select(x => x.Tag).ToList().ToArray());
            }
            tags += string.Join(";", filteredTagsData.Where(x => quoteDetail.CATNo != null && x.Value.Trim().ToLower() == quoteDetail.CATNo.Trim().ToLower()).Select(x => x.Tag).ToList().ToArray());
            tags += string.Join(";", filteredTagsData.Where(x => quoteDetail.SZ_Quotation.CompanyName != null && x.Value.Trim().ToLower() == quoteDetail.SZ_Quotation.CompanyName.Trim().ToLower()).Select(x => x.Tag).ToList().ToArray());
            tags += string.Join(";", filteredTagsData.Where(x => quoteDetail.SZ_Quotation.PONo != null && x.Value.Trim().ToLower() == quoteDetail.SZ_Quotation.PONo.Trim().ToLower()).Select(x => x.Tag).ToList().ToArray());
            if (quoteDetail.AdditionalBatchNo.HasValue)
            {
                var batchNo = db.SZ_Inventory.Where(x => x.Id == quoteDetail.AdditionalBatchNo).Select(x => x.BatchNo).FirstOrDefault();
                if (!string.IsNullOrEmpty(batchNo))
                {
                    tags += string.Join(";", filteredTagsData.Where(x => x.Value.Trim().ToLower() == batchNo.Trim().ToLower()).Select(x => x.Tag).ToList().ToArray());

                    //tags += filteredTagsData.Where(x => x.Value.Trim().ToLower() == batchNo.Trim().ToLower()).Select(x => x.Tag).ToList();
                }
            }

            List<string> tagsdatalist = new List<string>();
            if (!string.IsNullOrEmpty(tags))
            {
                var tlist = tags.Split(';');
                foreach (var t in tlist)
                {
                    if (!tagsdatalist.Contains(t))
                    {
                        tagsdatalist.Add(t);
                    }
                }
            }

            return String.Join(",", tagsdatalist.Select(x => x.ToString()).ToArray());
        }
    }
}