using Synzeal_Qty_Update.Folder;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Synzeal_Qty_Update
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessUpdateQty();
        }
        private static readonly Regex rxNonDigits = new Regex(@"[^\d]+");
        private static string CleanStringOfNonDigits_V1(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            string cleaned = rxNonDigits.Replace(s, "");
            return cleaned;
        }
        public static void ProcessUpdateQty()
        {
            Synzeal_LiveDataEntities db = new Synzeal_LiveDataEntities();
            db.Configuration.AutoDetectChangesEnabled = false;

            var quoteDetails = db.SZ_QuotationDetail.Where(x => x.MoveToProject == true && (x.IsOnHold == false || x.IsOnHold == null) && (x.DispatchedStatus == 0 || x.DispatchedStatus == null)).ToList();
            var allInventoryList = db.SZ_Inventory.ToList();
            var resqtydata = db.SZ_ReservedQty.ToList();
            int loop = 1;
            allInventoryList.ForEach(item =>
            {
                if(item.BatchNo == "SRL-1134-245")
                {

                }
                int reqqty = 0;
                var datas = quoteDetails.Where(x => x.AdditionalBatchNo == item.Id).ToList();
                if (datas != null && datas.Count > 0)
                {
                    foreach (var qd in datas)
                    {
                        reqqty += Convert.ToInt32(CleanStringOfNonDigits_V1(qd.RequiredQty));
                    }
                }

                var allReservedQtyList = resqtydata.Where(x => x.BatchId == item.Id).ToList();
                if (allReservedQtyList != null && allReservedQtyList.Count > 0)
                {
                    foreach (var qd in allReservedQtyList)
                    {
                        reqqty += qd.Qty;
                    }
                }

                item.ReservedQty = reqqty;

                decimal retqty = 15;
                if (item.RetentionQty.HasValue)
                {
                    retqty = item.RetentionQty.Value;
                }

                item.AvailableQty = item.Qty - reqqty - retqty;
                if (item.AvailableQty < 0)
                {
                    item.AvailableQty = 0;
                }
                if(item.AvailableQty == 0)
                {
                    item.AvailableQty = 0;
                }
                using (var context = new Synzeal_LiveDataEntities())
                {
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();
                }
                Console.WriteLine(loop + " : Process Done for the " + item.BatchNo);

                loop += 1;
            });
            //db.SaveChanges();

            string inhouseProjectType = Convert.ToString(5);
            var list = (from i in db.SZ_Quotation.AsNoTracking()
                        join t2 in db.SZ_QuotationDetail.AsNoTracking() on i.Id equals t2.QuoteId
                        where t2.MoveToProject == true
                        && string.IsNullOrEmpty(t2.TrackingNo)
                        && (t2.IsOnHold == false || t2.IsOnHold == null)
                        && (t2.ProjectType != inhouseProjectType || t2.ProjectType == null)
                        && i.CompanyId != 208
                        orderby t2.MoveProjectDate descending
                        select t2).Distinct().OrderByDescending(x => x.MoveProjectDate).ThenBy(x => x.SrPo).ToList();
            loop = 1;
            if (list != null && list.Count > 0)
            {
                var auditlog = db.SZ_QuotationDetailLog.Where(x => x.FieldName == "EstimateCompleteDate").ToList();
                list.ForEach(k =>
                {
                    try
                    {
                        var oldEstCompDate = auditlog.Where(x => x.QuoteDetailsId == k.Id).OrderBy(x => x.Id).Select(x => x.Before).FirstOrDefault();
                        k.WatchListCount = 0;
                        if (k.SZ_Quotation.PODate.HasValue && k.EstimateCompleteDate.HasValue && !string.IsNullOrEmpty(oldEstCompDate))
                        {
                            var odddate = Convert.ToDateTime(oldEstCompDate);
                            var initialNumberOfDays = (odddate - k.SZ_Quotation.PODate.Value).TotalDays;
                            var CurrantNumberOfDays = (k.EstimateCompleteDate.Value - k.SZ_Quotation.PODate.Value).TotalDays;
                            if ((initialNumberOfDays * 2) <= CurrantNumberOfDays)
                            {
                                k.WatchListCount = 1;
                            }
                        }

                        Console.WriteLine(loop + " : Process Done for the " + k.Id);
                        loop += 1;
                        using (var context = new Synzeal_LiveDataEntities())
                        {
                            context.Entry(k).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(loop + " : Error for : " + k.Id + " / " + ex.Message + " / " + ex.StackTrace);

                    }
                });
                //db.SaveChanges();
            }

            var detailList = db.SZ_QuotationDetail.AsNoTracking().Where(x => x.IsCustomSynthesis == null || x.IsInStock == null).ToList();
            if (detailList != null && detailList.Count > 0)
            {
               
                    detailList.ForEach(item =>
                            {
                            try
                            {
                                var invData = allInventoryList.Where(x => x.ProductId == item.ProductId).Count();
                                if (invData > 0)
                                {
                                    item.IsInStock = true;
                                    item.IsCustomSynthesis = false;
                                }
                                else
                                {
                                    item.IsInStock = false;
                                    item.IsCustomSynthesis = true;
                                }
                                Console.WriteLine("Process Done for the QuotedetailId : " + item.Id);
                                using (var context = new Synzeal_LiveDataEntities())
                                {
                                    context.Entry(item).State = EntityState.Modified;
                                    context.SaveChanges();
                                }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(loop + " : Error for : " + item.Id + " / " + ex.Message + " / " + ex.StackTrace);

                                }
                            });
               
            }
            Environment.Exit(0);
        }
    }
}
