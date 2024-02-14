using Microsoft.EntityFrameworkCore;
using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Helpers;
using SampleWebApiAspNetCore.Models;
using Synzeal_API.Dtos;
using Synzeal_API.Entities;
using Synzeal_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebGrease.Css.Extensions;

namespace SampleWebApiAspNetCore.Repositories
{
    public class FoodSqlRepository : IFoodRepository
    {
        private readonly SZDbContext _foodDbContext;

        public FoodSqlRepository(SZDbContext foodDbContext)
        {
            _foodDbContext = foodDbContext;
        }

        public FoodEntity GetSingle(int id)
        {
            return _foodDbContext.FoodItems.FirstOrDefault(x => x.Id == id);
        }

        public void Add(FoodEntity item)
        {
            _foodDbContext.FoodItems.Add(item);
        }

        public void Delete(int id)
        {
            FoodEntity foodItem = GetSingle(id);
            _foodDbContext.FoodItems.Remove(foodItem);
        }

        public FoodEntity Update(int id, FoodEntity item)
        {
            _foodDbContext.FoodItems.Update(item);
            return item;
        }

        public IQueryable<FoodEntity> GetAll(QueryParameters queryParameters)
        {
            IQueryable<FoodEntity> _allItems = _foodDbContext.FoodItems.AsNoTracking().OrderBy(queryParameters.OrderBy,
              queryParameters.IsDescending());

            if (queryParameters.HasQuery())
            {
                _allItems = _allItems
                    .Where(x => x.Calories.ToString().Contains(queryParameters.Query.ToLowerInvariant())
                    || x.Name.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return _allItems
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }

        public IQueryable<SZ_CompanyList> GetAllCompany(QueryParameters queryParameters)
        {
            IQueryable<SZ_CompanyList> _allItems = _foodDbContext.SZ_CompanyList.OrderBy(queryParameters.OrderBy,
              queryParameters.IsDescending());

            if (queryParameters.HasQuery())
            {
                _allItems = _allItems
                    .Where(x => x.Name.ToString().Contains(queryParameters.Query.ToLowerInvariant())
                    || x.Address.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return _allItems
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }

        public IList<SZ_QuotationDetail> GetPreviousInfoFromDB(string ProductName, string casno, string catNo, string company, int QuoteId = 0, bool isApi = false)
        {
            var outputModel = new List<SZ_QuotationDetail>();
            try
            {
                int cid = 0;
                if (company != "undefined")
                {
                    cid = Convert.ToInt32(company);
                }

                var mainModel = (from x in _foodDbContext.SZ_QuotationDetail.AsNoTracking()
                                 select x).AsQueryable();
                var model = mainModel;
                if (!string.IsNullOrEmpty(ProductName))
                {
                    ProductName = ProductName.Trim();

                    if (ProductName.ToLower().StartsWith("sz-"))
                    {
                        model = model.Where(x => x.CATNo.ToLower().Contains(ProductName.ToLower()) || x.SZ_Quotation.Ref.ToLower().Contains(ProductName.ToLower()));
                    }
                    else if (Regex.IsMatch(ProductName.Replace("-", ""), @"^-?\d+$"))
                    {
                        model = model.Where(x => x.CASNo.ToLower().Contains(ProductName.ToLower()));
                    }
                    else
                    {
                        model = model.Where(x => x.ProductName.ToLower().Contains(ProductName.ToLower()));
                    }
                    //model = model.Where(x => x.ProductName.ToLower().Contains(ProductName.ToLower()) 
                    //    || x.CASNo.ToLower().Contains(ProductName.ToLower()) || x.CATNo.ToLower().Contains(ProductName.ToLower())
                    //    || x.SZ_Quotation.Ref.ToLower().Contains(ProductName.ToLower()));
                    // model = model.Where(x => x.CATNo.ToLower().Contains(ProductName.ToLower()));
                }

                //if (isApi)
                //{
                //    var categoryData = _foodDbContext.Categories.ToList();
                //    var productCategoryData = _foodDbContext.Product_Category_Mapping.ToList();
                //    var product = db.Products.Where(x => x.Sku.ToLower().Contains(ProductName.ToLower()) && x.Published == true && x.Deleted == false).FirstOrDefault();
                //    if (product != null)
                //    {
                //        var allcats = GetApiAllProduct(product.Id, categoryData, productCategoryData);
                //        if (allcats != null && allcats.Count > 0)
                //        {
                //            model = (from x in db.SZ_QuotationDetail
                //                     where allcats.Contains(x.CATNo)
                //                     && x.SZ_Quotation.CompanyId == cid
                //                     select x).AsQueryable();
                //        }
                //    }
                //}

                model = model.Include(e => e.SZ_Quotation).AsQueryable();
                if (!string.IsNullOrEmpty(company) && company != "undefined")
                {

                    var companyPORecords = model.Where(x => x.SZ_Quotation.CompanyId == cid && !string.IsNullOrEmpty(x.SZ_Quotation.PONo)).OrderByDescending(x => x.Id).AsQueryable();
                    if (companyPORecords.Count() > 0)
                    {
                        companyPORecords.ForEach(x => outputModel.Add(x));
                    }
                    var othercompanywithpoRecords = model.Where(x => x.SZ_Quotation.CompanyId != cid && !string.IsNullOrEmpty(x.SZ_Quotation.PONo)).OrderByDescending(x => x.Id).AsQueryable();
                    if (othercompanywithpoRecords.Count() > 0)
                    {
                        othercompanywithpoRecords.ForEach(x => outputModel.Add(x));
                    }
                    var companyRecords = model.Where(x => x.SZ_Quotation.CompanyId == cid && string.IsNullOrEmpty(x.SZ_Quotation.PONo)).OrderByDescending(x => x.Id).AsQueryable();
                    if (companyRecords.Count() > 0)
                    {
                        companyRecords.ForEach(x => outputModel.Add(x));
                    }
                    var othercompanyRecords = model.Where(x => x.SZ_Quotation.CompanyId != cid && string.IsNullOrEmpty(x.SZ_Quotation.PONo)).OrderByDescending(x => x.Id).AsQueryable();
                    if (othercompanyRecords.Count() > 0)
                    {
                        othercompanyRecords.ForEach(x => outputModel.Add(x));
                    }
                    outputModel = model.OrderByDescending(x => x.MoveToProject).ThenByDescending(x => x.CreatedDate).ToList();
                }
                else
                {
                    outputModel = model.OrderByDescending(x => x.MoveToProject).ThenByDescending(x => x.CreatedDate).ToList();
                }
                if (QuoteId != 0)
                {
                    var szalreadydetails = mainModel.Where(x => x.QuoteId == QuoteId).ToList();
                    if (szalreadydetails != null && szalreadydetails.Count > 0)
                    {
                        var ids = szalreadydetails.Select(x => x.Id).ToList();
                        outputModel = outputModel.Where(x => !ids.Contains(x.Id)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            outputModel.ForEach(item =>
            {
                item.ProductName = item.ProductName.Trim();
            });

            return outputModel;
        }


        public IList<SZ_QuotationDetail> getPreviousInfoFromDBTesting(int productId)
        {
            var outputModel = new List<SZ_QuotationDetail>();
            try
            {
                int cid = 0;

                var mainModel = (from x in _foodDbContext.SZ_QuotationDetail.AsNoTracking()
                                 where x.ProductId == productId
                                 select x).AsQueryable();

                var model = mainModel;

                model = model.Include(e => e.SZ_Quotation).AsQueryable();
                outputModel = model.OrderByDescending(x => x.MoveToProject).ThenByDescending(x => x.CreatedDate).ToList();

            }
            catch (Exception ex)
            {

            }
            outputModel.ForEach(item =>
            {
                item.ProductName = item.ProductName.Trim();
            });

            return outputModel;
        }

        public async Task<IList<Product>> GetProductSkusByCasNo(string casno)
        {
            return (from x in _foodDbContext.Product.AsNoTracking()
                    where x.ManufacturerPartNumber == casno
                            && x.Published == true
                            && x.Deleted == false
                    select x).ToList();
        }

        public async Task<IList<Category>> GetAllCategory()
        {
            return (from x in _foodDbContext.Category.AsNoTracking()
                    where x.Published == true
                            && x.Deleted == false
                    select x).ToList();
        }

        public async Task<IList<MovetoProjectSAPModel>> GetMovetoProjectSAPData()
        {
            string inhouseProjectType = Convert.ToString("5");
            DateTime checkedData = new DateTime(2022, 05, 01);

            var listdata = (from i in _foodDbContext.SZ_Quotation.AsNoTracking()
                        join t2 in _foodDbContext.SZ_QuotationDetail.AsNoTracking() on i.Id equals t2.QuoteId
                        where t2.MoveToProject == true
                        && (t2.MoveToInvoice == false || t2.MoveToInvoice == null)
                        && (t2.ProjectType != inhouseProjectType || t2.ProjectType == null)
                        && i.CreatedDate >= checkedData
                        && (t2.IsSuccessSAP == null || t2.IsSuccessSAP == false)
                        && !i.CompanyName.ToLower().Contains("synzeal")
                            select i.PONo).Distinct().ToList();

            var list = (from i in _foodDbContext.SZ_Quotation.AsNoTracking()
                        join t2 in _foodDbContext.SZ_QuotationDetail.AsNoTracking() on i.Id equals t2.QuoteId
                        where t2.MoveToProject == true && (t2.MoveToInvoice == false || t2.MoveToInvoice == null)
                            && (t2.ProjectType != inhouseProjectType || t2.ProjectType == null)
                            && listdata.Contains(i.PONo)
                            && i.CreatedDate >= checkedData
                            && !i.CompanyName.ToLower().Contains("synzeal")
                        orderby t2.Id descending
                        select new
                        {
                            Quote = i,
                            QuoteDetail = t2
                        }).ToList();

            var customerData = _foodDbContext.Customer.Where(x => x.Deleted == false).ToList();
            var pIds = list.Where(x=>x.QuoteDetail.ProductId.HasValue).Select(x => x.QuoteDetail.ProductId).ToList();
            //var pIds = list.Any(x => x.QuoteDetail.ProductId.ToLower() == needle.ToLower());
            var inventoryData = _foodDbContext.SZ_Inventory.Where(x => pIds.Contains(x.ProductId)).ToList();
            var outputModel = new List<MovetoProjectSAPModel>();
            list.ForEach(item =>
            {
                var model = new MovetoProjectSAPModel();
                model.SAPSOLId = item.QuoteDetail.SAPSOLId.HasValue ? item.QuoteDetail.SAPSOLId.Value : 0;
                model.SAPSODocEntry = !string.IsNullOrEmpty(item.QuoteDetail.SAPSODocEntry) ? Convert.ToInt32(item.QuoteDetail.SAPSODocEntry) : 0;
                model.SAPSONo = !string.IsNullOrEmpty(item.QuoteDetail.SAPSONo) ? Convert.ToInt32(item.QuoteDetail.SAPSONo) : 0;
                model.Reason = item.QuoteDetail.Reason;
                model.OrderRemark = !string.IsNullOrEmpty(item.QuoteDetail.OrderRemark) ? item.QuoteDetail.OrderRemark.Trim() : ""; 
                model.ActivityStatus = !string.IsNullOrEmpty(item.QuoteDetail.ActivityStatus) ? item.QuoteDetail.ActivityStatus.Trim() : ""; 
                if(!string.IsNullOrEmpty(item.QuoteDetail.ProStatus))
                {
                    model.Projectstatus = ExceptionExtension.GetEnumDescription((EnumList.ProStatusDDL)Convert.ToInt32(item.QuoteDetail.ProStatus));
                }
                else
                {
                    model.Projectstatus = "";
                }
                model.LineRemark = !string.IsNullOrEmpty(item.QuoteDetail.Remark) ? item.QuoteDetail.Remark : "";
                model.DocType = "";
                model.QuoteId = item.Quote.Id;
                model.QuoteDetailId = item.QuoteDetail.Id;
                model.PONo = item.Quote.PONo;
                model.sapCusName = item.Quote.CompanyName;
                model.SAPCusCode = _foodDbContext.SZ_CompanyList.Where(x => x.Id == item.Quote.CompanyId).Select(x => x.SAPCode).FirstOrDefault();
                model.DocDate = item.Quote.PODate.HasValue ? item.Quote.PODate.Value.ToString("yyyyMMdd") : "";
                model.DocCurrency = GetCurrancy(item.Quote);
                model.DocRemark = !string.IsNullOrEmpty(item.Quote.Remark) ? item.Quote.Remark.Trim() : "";
                model.ItemCode = !string.IsNullOrEmpty(item.QuoteDetail.CATNo) ? item.QuoteDetail.CATNo.Trim() : "";
                model.CASNo = !string.IsNullOrEmpty(item.QuoteDetail.CASNo) ? item.QuoteDetail.CASNo : "";
                model.ItemName = item.QuoteDetail.ProductName;
                model.QuantityPO = CalculateQty(item.QuoteDetail.RequiredQty);
                model.LineType = GetProjectType(item.QuoteDetail.ProjectType);
                model.PriceBfrDiscount = CalculateBeforePriceData(item.QuoteDetail);
                model.NoofPack = CalculateNoofPackFromOrderConfirmation(item.QuoteDetail.OrderRemark);
                model.PackSize = CalculatePackSizeFromOrderConfirmation(item.QuoteDetail.OrderRemark);
                //model.PackSize = CalculatePackSize(item.QuoteDetail);
                model.Discount = item.QuoteDetail.ItemDiscount.HasValue ? item.QuoteDetail.ItemDiscount.Value : 0;
                model.COARefNumber = !string.IsNullOrEmpty(item.QuoteDetail.COARefNumber) ? item.QuoteDetail.COARefNumber : "";
                model.BatchNo = CheckBatch(inventoryData, item.QuoteDetail);
                model.IsInvoice = item.QuoteDetail.MoveToInvoice.HasValue ? item.QuoteDetail.MoveToInvoice.Value : false;
                model.PriceAfrDiscount = CalculateAftPriceData(item.QuoteDetail, model);
                model.UnitPerRate = GetUnitPerRate(model);
                outputModel.Add(model);
            });
            return outputModel.Where(x => !string.IsNullOrEmpty(x.SAPCusCode)).ToList();
        }

        public decimal GetUnitPerRate(MovetoProjectSAPModel model)
        {
           if(model.QuantityPO > 0 && model.PriceBfrDiscount > 0)
            {
                return (model.PriceBfrDiscount / model.QuantityPO);
            }
            return 0;
        }

        public int CalculatePackSizeFromOrderConfirmation(string orderRemark)
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
        public int CalculateNoofPackFromOrderConfirmation(string orderRemark)
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
        public string GetCurrancy(SZ_Quotation quote)
        {
            if(!string.IsNullOrEmpty(quote.Currency))
            {
                return quote.Currency;
            }

            if (quote.CountryType == "Export")
            {
                return "USD";
            }

            return "INR";
        }

        public string CheckBatch(List<SZ_Inventory> inventoryData, SZ_QuotationDetail quoteDetail)
        {
            var invData = inventoryData.Where(x => x.Id == quoteDetail.AdditionalBatchNo).Select(x => x.BatchNo).FirstOrDefault();
            if(string.IsNullOrEmpty(invData))
            {
                return "";
            }

            return invData;
        }

            public int CalculateQty(string requiredQty)
        {
            if(!string.IsNullOrEmpty(requiredQty))
            {
                try
                {
                    var rqty = Regex.Match(requiredQty, @"^\D*?((-?(\d+(\.\d+)?))|(-?\.\d+)).*").Value;
                    if(!string.IsNullOrEmpty(rqty))
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


        public int CalculatePackSize(SZ_QuotationDetail quoteDetail)
        {
            if (!string.IsNullOrEmpty(quoteDetail.Price))
            {
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
                                objModel.Qty = 1;
                                objModel.Price = "0";
                                mainPriceModel.Add(objModel);
                            }
                        }
                    }
                }
                return 1;
            }
            return 1;
        }

        public decimal CalculatePriceData(SZ_QuotationDetail quoteDetail)
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

        public decimal CalculateBeforePriceData(SZ_QuotationDetail quoteDetail)
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

        public decimal CalculateAftPriceData(SZ_QuotationDetail quoteDetail, MovetoProjectSAPModel model)
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

        public async Task<IList<MovetoProjectModel>> GetMovetoProjectData()
        {
            string inhouseProjectType = Convert.ToString("5");

            var list = (from i in _foodDbContext.SZ_Quotation.AsNoTracking()
                        join t2 in _foodDbContext.SZ_QuotationDetail.AsNoTracking() on i.Id equals t2.QuoteId
                        where t2.MoveToProject == true
                        && string.IsNullOrEmpty(t2.TrackingNo)
                        && (t2.ProjectType != inhouseProjectType || t2.ProjectType == null)
                        orderby t2.Id
                        select new
                        {
                            Quote = i,
                            QuoteDetail = t2
                        }).AsQueryable();

            var customerData = _foodDbContext.Customer.Where(x => x.Deleted == false).ToList();
            var pIds = list.Select(x => x.QuoteDetail.ProductId).ToList();
            var inventoryData = _foodDbContext.SZ_Inventory.Where(x => pIds.Contains(x.ProductId)).ToList();
            var outputModel = new List<MovetoProjectModel>();
            foreach (var item in list)
            {
                var model = new MovetoProjectModel();
                model.PONo = item.Quote.PONo;
                model.PODate = item.Quote.PODate;
                model.SAPCode = _foodDbContext.SZ_CompanyList.Where(x => x.Id == item.Quote.CompanyId).Select(x => x.SAPCode).FirstOrDefault();
                model.QuoteId = item.Quote.Id;
                model.QuoteRef = item.Quote.Ref;
                model.QuoteDetailId = item.QuoteDetail.Id;
                model.ProductId = item.QuoteDetail.ProductId;
                model.ProductName = item.QuoteDetail.ProductName;
                model.CASNo = item.QuoteDetail.CASNo;
                model.ImagePath = item.QuoteDetail.ImagePath;
                model.Price = item.QuoteDetail.Price;
                model.LeadTime = item.QuoteDetail.LeadTime;
                model.IsUploadServer = item.QuoteDetail.IsUploadServer;
                model.CreatedDate = item.QuoteDetail.CreatedDate;
                model.CATNo = !string.IsNullOrEmpty(item.QuoteDetail.CATNo) ? item.QuoteDetail.CATNo.Trim() : "";
                model.DisplayOrder = item.QuoteDetail.DisplayOrder;
                model.ProjectType = GetProjectType(item.QuoteDetail.ProjectType);
                model.ScientistCustomerId = item.QuoteDetail.ScientistCustomerId;
                model.ScientistCustomerName = GetCustomerName(item.QuoteDetail.ScientistCustomerId, customerData);
                model.RequiredQty = item.QuoteDetail.RequiredQty;
                model.ProjectStatus = item.QuoteDetail.ProjectStatus;
                model.ScientistStatus = item.QuoteDetail.ScientistStatus;
                model.BatchCode1 = item.QuoteDetail.BatchCode1;
                model.Qty1 = item.QuoteDetail.Qty1;
                model.BatchCode2 = item.QuoteDetail.BatchCode2;
                model.Qty2 = item.QuoteDetail.Qty2;
                model.Courier = item.QuoteDetail.Courier;
                model.TrackingNo = item.QuoteDetail.TrackingNo;
                model.Location = item.QuoteDetail.Location;
                model.RefName = item.QuoteDetail.RefName;
                model.PurposeDispatch = item.QuoteDetail.PurposeDispatch;
                model.DeliveryDate = item.QuoteDetail.DeliveryDate;
                model.DispatchedStatus = item.QuoteDetail.DispatchedStatus;
                model.ProcessState = item.QuoteDetail.ProcessState;
                model.PackDate = item.QuoteDetail.PackDate;
                model.DeliveryStatus = item.QuoteDetail.DeliveryStatus;
                model.DataPending = item.QuoteDetail.DataPending;
                model.TrackingNoDate = item.QuoteDetail.TrackingNoDate;
                model.EstimateCompleteDate = item.QuoteDetail.EstimateCompleteDate;
                model.MoveToProject = item.QuoteDetail.MoveToProject;
                model.MoveToDispatch = item.QuoteDetail.MoveToDispatch;
                model.BatchNo = inventoryData.Where(x => x.Id == item.QuoteDetail.AdditionalBatchNo).Select(x => x.BatchNo).FirstOrDefault();
                model.AdditionalBatchNo = item.QuoteDetail.AdditionalBatchNo;
                model.Remark = item.QuoteDetail.Remark;
                model.MoveToInvoice = item.QuoteDetail.MoveToInvoice;
                model.AdminScientistStatus = item.QuoteDetail.AdminScientistStatus;
                model.SrPo = item.QuoteDetail.SrPo;
                model.InvoiceRemark = item.QuoteDetail.InvoiceRemark;
                model.InvoiceNo = item.QuoteDetail.InvoiceNo;
                model.PaymentStatus = item.QuoteDetail.PaymentStatus;
                model.IsOnHold = item.QuoteDetail.IsOnHold;
                model.MoveProjectDate = item.QuoteDetail.MoveProjectDate;
                model.MoveDispatchDate = item.QuoteDetail.MoveDispatchDate;
                model.MoveToInvoiceDate = item.QuoteDetail.MoveToInvoiceDate;
                model.MoveToScientistDate = item.QuoteDetail.MoveToScientistDate;
                model.ProductRemark = item.QuoteDetail.ProductRemark;
                model.IsDataApproved = item.QuoteDetail.IsDataApproved;
                model.ScientistRemark = item.QuoteDetail.ScientistRemark;
                model.DataApprovedStatus = item.QuoteDetail.DataApprovedStatus;
                model.DataApprovalDate = item.QuoteDetail.DataApprovalDate;
                model.PurchaseDate = item.QuoteDetail.PurchaseDate;
                model.PurchaseStatus = item.QuoteDetail.PurchaseStatus;
                model.Instockdate = item.QuoteDetail.Instockdate;
                model.LastStatus = item.QuoteDetail.LastStatus;
                model.COAPath = item.QuoteDetail.COAPath;
                model.AnalyticalData = item.QuoteDetail.AnalyticalData;
                model.ClientRemark = item.QuoteDetail.ClientRemark;
                model.ClientAddress = item.QuoteDetail.ClientAddress;
                model.AttachedDataList = item.QuoteDetail.AttachedDataList;
                model.ClientStatus = item.QuoteDetail.ClientStatus;
                model.SubScientistName = item.QuoteDetail.SubScientistName;
                model.FollowUpRemark = item.QuoteDetail.FollowUpRemark;
                model.FollowUpRemarkSecond = item.QuoteDetail.FollowUpRemarkSecond;
                //model.IsPayment = item.QuoteDetail.IsPayment;
                //model.IsDispatchApprove = item.QuoteDetail.IsDispatchApprove;
                //model.EstimateDispatchDate = item.QuoteDetail.EstimateDispatchDate;
                model.OrderRemark = item.QuoteDetail.OrderRemark;
                model.LastUploadDate = item.QuoteDetail.LastUploadDate;
                model.Reason = item.QuoteDetail.Reason;
                model.ResponseClientRemarkDate = item.QuoteDetail.ResponseClientRemarkDate;
                model.IsFirstTimeDataUpload = item.QuoteDetail.IsFirstTimeDataUpload;
                model.QueryText = item.QuoteDetail.QueryText;
                model.QueryDate = item.QuoteDetail.QueryDate;
                model.IsSynthesisLog = item.QuoteDetail.IsSynthesisLog;
                model.IsAssignScientistQuery = item.QuoteDetail.IsAssignScientistQuery;
                model.IsAssignProjectQuery = item.QuoteDetail.IsAssignProjectQuery;
                model.Explanation = item.QuoteDetail.Explanation;
                model.ReportInvoiceDate = item.QuoteDetail.ReportInvoiceDate;
                model.COAApprovedDate = item.QuoteDetail.COAApprovedDate;
                model.IsQueryResolved = item.QuoteDetail.IsQueryResolved;
                model.QueryResolvedDate = item.QuoteDetail.QueryResolvedDate;
                model.IsDispatchedLock = item.QuoteDetail.IsDispatchedLock;
                model.IsFollowUpAdminChange = item.QuoteDetail.IsFollowUpAdminChange;
                model.IsFollowupChange = item.QuoteDetail.IsFollowupChange;
                model.FollowupDescription = item.QuoteDetail.FollowupDescription;
                model.ContactDetail = item.QuoteDetail.ContactDetail;
                model.DifficultyLevel = GetDiffLevelStatus(item.QuoteDetail.DifficultyLevel);
                model.IsPriority = item.QuoteDetail.IsPriority;
                model.FinalPrice = item.QuoteDetail.FinalPrice;
                model.InvoiceBatchNo = item.QuoteDetail.InvoiceBatchNo;
                model.QueryType = item.QuoteDetail.QueryType;
                model.QuoteBatchNo = item.QuoteDetail.QuoteBatchNo;
                model.ActivityStatus = item.QuoteDetail.ActivityStatus;
                model.InvoicedDate = item.QuoteDetail.InvoicedDate;
                model.BranchLocation = item.QuoteDetail.BranchLocation;
                model.ParkReason = item.QuoteDetail.ParkReason;
                model.PurchaseDDLStatus = item.QuoteDetail.PurchaseDDLStatus;
                model.PurchaseRemark = item.QuoteDetail.PurchaseRemark;
                model.ProStatus = GetProStatus(item.QuoteDetail.ProStatus);
                model.LastExplainationSecond = item.QuoteDetail.LastExplainationSecond;
                model.ExplainationSecond = item.QuoteDetail.ExplainationSecond;
                model.LastWeekUpdate = item.QuoteDetail.LastWeekUpdate;
                model.Discount = item.QuoteDetail.Discount;
                model.COAId = item.QuoteDetail.COAId;
                model.COARefNumber = item.QuoteDetail.COARefNumber;
                model.PreviousStatus = GetProStatus(item.QuoteDetail.PreviousStatus);
                model.PreviousEstCompleteDate = item.QuoteDetail.PreviousEstCompleteDate;
                model.IsHoldManually = item.QuoteDetail.IsHoldManually;
                model.Chemist = item.QuoteDetail.Chemist;
                model.ReviewSciStatus = GetProStatus(item.QuoteDetail.ReviewSciStatus);
                model.IsQC = item.QuoteDetail.IsQC;
                model.QCdate = item.QuoteDetail.QCdate;
                model.ApprovalStatus = item.QuoteDetail.ApprovalStatus;
                model.ApprovedAs = item.QuoteDetail.ApprovedAs;
                model.ApprovalComment = item.QuoteDetail.ApprovalComment;
                model.RecommendedPeriod = item.QuoteDetail.RecommendedPeriod;
                model.PrimaryStdOrdered = item.QuoteDetail.PrimaryStdOrdered;
                model.ColumnOrder = item.QuoteDetail.ColumnOrder;
                model.SystemSuitability = item.QuoteDetail.SystemSuitability;
                model.QCApprovedDate = item.QuoteDetail.QCApprovedDate;
                model.DispatchStatus = GetProStatus(item.QuoteDetail.DispatchStatus);
                model.OtherProStatus = GetProStatus(item.QuoteDetail.OtherProStatus);
                model.IsPurchase = item.QuoteDetail.IsPurchase;
                model.QuotePurchaseDate = item.QuoteDetail.QuotePurchaseDate;
                model.IsCancel = item.QuoteDetail.IsCancel;
                model.CancelDate = item.QuoteDetail.CancelDate;
                model.CancelledBy = item.QuoteDetail.CancelledBy;
                model.ApprovedForClient = item.QuoteDetail.ApprovedForClient;
                model.IsBackFromPurchase = item.QuoteDetail.IsBackFromPurchase;
                model.IsForceUpload = item.QuoteDetail.IsForceUpload;
                model.InhouseRemark = item.QuoteDetail.InhouseRemark;
                model.LastProStatus = GetProStatus(item.QuoteDetail.LastProStatus);
                model.QcApprovalDate = item.QuoteDetail.QcApprovalDate;
                outputModel.Add(model);
            }

            return outputModel.OrderBy(x=> x.QuoteDetailId).ToList();
        }

        public async Task<IList<Product>> GetProductByStartSku(string sku)
        {
            sku = sku.Trim();
            var query = from p in _foodDbContext.Product.AsNoTracking()
                        orderby p.Id
                        where p.Published && !p.Deleted &&
                        p.Sku.StartsWith(sku)
                        select p;
            var product = query.ToList();
            return product;
        }

        public async Task<ProductDetailsDto> ProductDetails(int productId)
        {
            var product = _foodDbContext.Product.AsNoTracking().Where(x => x.Id == productId).FirstOrDefault();

            var model = new ProductDetailsDto
            {
                ProductData = product,
                Id = product.Id,
                Name = product.Name,
                Sku = product.Sku,
                ManufacturerPartNumber = product.ManufacturerPartNumber,
                ShowGtin = true,
                Gtin = product.Gtin,
                DrugApiCode = product.DrugApiCode,
                Smile = product.Smile,
                Synonym = product.Synonym,
                ChemicalName = product.ChemicalName,
                MolecularWeight = product.MolecularWeight,
                FirstCatName = product.FirstCatName,
                QuotePrice = product.QuotePrice,
                IsControlledSubstance = product.IsControlledSubstance,
                ProductInstockStatus = product.ProductInstockStatus,
                MetaDescription = product.ShortDescription,
                MainCatName = product.MainCatName,
                ParentcatName = product.FirstCatName,
            };

            var inventoryData = _foodDbContext.SZ_Inventory.Where(x => x.ProductId == model.Id).ToList();
            decimal totalQty = 0;

            if (inventoryData.Count > 0)
            {
                inventoryData.ForEach(inv =>
                {
                    ProductSZInventoryModelDto invModel = new ProductSZInventoryModelDto();
                    invModel.SZInventoryId = inv.Id;
                    invModel.BatchNo = inv.BatchNo;
                    if (inv.Qty.HasValue)
                    {
                        totalQty += inv.Qty.Value;
                        invModel.Qty = inv.Qty.Value;
                    }
                    else
                    {
                        invModel.Qty = 0;
                    }
                    model.InventoryModel.Add(invModel);
                });
            }

            return model;
        }

        public virtual IList<Picture> GetPicturesByProductId(int productId, int recordsToReturn = 0)
        {
            if (productId == 0)
                return new List<Picture>();


            var query = from p in _foodDbContext.Picture
                        join pp in _foodDbContext.ProductPicture on p.Id equals pp.PictureId
                        orderby pp.DisplayOrder
                        where pp.ProductId == productId
                        select p;

            if (recordsToReturn > 0)
                query = query.Take(recordsToReturn);

            var pics = query.ToList();
            return pics;
        }

        protected virtual byte[] LoadPictureBinary(Picture picture)
        {
            if (picture == null)
                throw new ArgumentNullException("picture");

            var result = LoadPictureFromFile(picture.Id, picture.MimeType);
            return result;
        }

        protected virtual byte[] LoadPictureFromFile(int pictureId, string mimeType)
        {
            string lastPart = GetFileExtensionFromMimeType(mimeType);
            string fileName = string.Format("{0}_0.{1}", pictureId.ToString("0000000"), lastPart);
            var filePath = GetPictureLocalPath(fileName);
            if (!File.Exists(filePath))
                return new byte[0];
            return File.ReadAllBytes(filePath);
        }

        protected virtual string GetPictureLocalPath(string fileName, string imagesDirectoryPath = null)
        {
            if (String.IsNullOrEmpty(imagesDirectoryPath))
            {
                imagesDirectoryPath = "https://synzeal.com/content/images/";
            }
            var filePath = Path.Combine(imagesDirectoryPath, fileName);
            return filePath;
        }

        public virtual string GetDefaultPictureUrl(int targetSize = 0, string storeLocation = null)
        {
            string filePath = "https://synzeal.com/content/images/thumbs/default-image_350.png";
            return filePath;
        }

        protected virtual string GetFileExtensionFromMimeType(string mimeType)
        {
            if (mimeType == null)
                return null;

            //also see System.Web.MimeMapping for more mime types

            string[] parts = mimeType.Split('/');
            string lastPart = parts[parts.Length - 1];
            switch (lastPart)
            {
                case "pjpeg":
                    lastPart = "jpg";
                    break;
                case "x-png":
                    lastPart = "png";
                    break;
                case "x-icon":
                    lastPart = "ico";
                    break;
            }
            return lastPart;
        }

        public async Task<List<ProductDetailsDto>> ProductDetailByProducts(IList<Product> products)
        {
            var outputModel = new List<ProductDetailsDto>();

            products.ForEach(product =>
            {
                var model = new ProductDetailsDto
                {
                    CasNo = product.ManufacturerPartNumber,
                    ProductData = product,
                    Id = product.Id,
                    Name = product.Name,
                    Sku = product.Sku,
                    ManufacturerPartNumber = product.ManufacturerPartNumber,
                    ShowGtin = true,
                    Gtin = product.Gtin,
                    DrugApiCode = product.DrugApiCode,
                    Smile = product.Smile,
                    Synonym = product.Synonym,
                    ChemicalName = product.ChemicalName,
                    MolecularWeight = product.MolecularWeight,
                    FirstCatName = product.FirstCatName,
                    QuotePrice = product.QuotePrice,
                    IsControlledSubstance = product.IsControlledSubstance,
                    ProductInstockStatus = product.ProductInstockStatus,
                    MetaDescription = product.ShortDescription,
                    MainCatName = product.MainCatName,
                    ParentcatName = product.FirstCatName,
                };

                var inventoryData = _foodDbContext.SZ_Inventory.Where(x => x.ProductId == model.Id).ToList();
                decimal totalQty = 0;

                if (inventoryData.Count > 0)
                {
                    inventoryData.ForEach(inv =>
                    {
                        ProductSZInventoryModelDto invModel = new ProductSZInventoryModelDto();
                        invModel.SZInventoryId = inv.Id;
                        invModel.BatchNo = inv.BatchNo;
                        if (inv.Qty.HasValue)
                        {
                            totalQty += inv.Qty.Value;
                            invModel.Qty = inv.Qty.Value;
                        }
                        else
                        {
                            invModel.Qty = 0;
                        }
                        model.InventoryModel.Add(invModel);
                    });
                }

                // Image
                int pictureSize = 350;
                var picture = GetPicturesByProductId(product.Id, 1).FirstOrDefault();

                // Get Picture 
                var pictureModel = new PictureModelDto
                {
                    ImageUrl = GetPictureUrl(picture, pictureSize),
                    FullSizeImageUrl = GetPictureUrl(picture),
                    FullSizeImageUrlWithWaterMark = GetPictureUrl(picture, 1000),
                };


                outputModel.Add(model);
            });
            return outputModel;
        }


        public string GetPictureUrl(Picture picture, int targetSize = 0, bool showDefaultPicture = true, string storeLocation = null)
        {
            string url = string.Empty;
            byte[] pictureBinary = null;
            if (picture != null)
                pictureBinary = LoadPictureBinary(picture);
            if (picture == null || pictureBinary == null || pictureBinary.Length == 0)
            {
                if (showDefaultPicture)
                {
                    url = GetDefaultPictureUrl(targetSize, storeLocation);
                }
                return url;
            }
            return url;
            //string lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            //string thumbFileName;

            //lock (s_lock)
            //{
            //    string seoFileName = picture.SeoFilename; // = GetPictureSeName(picture.SeoFilename); //just for sure
            //    if (targetSize == 0)
            //    {
            //        thumbFileName = !String.IsNullOrEmpty(seoFileName) ?
            //            string.Format("{0}_{1}.{2}", picture.Id.ToString("0000000"), seoFileName, lastPart) :
            //            string.Format("{0}.{1}", picture.Id.ToString("0000000"), lastPart);
            //        var thumbFilePath = GetThumbLocalPath(thumbFileName);
            //        if (!File.Exists(thumbFilePath))
            //        {
            //            File.WriteAllBytes(thumbFilePath, pictureBinary);
            //        }
            //    }
            //    else
            //    {
            //        thumbFileName = !String.IsNullOrEmpty(seoFileName) ?
            //            string.Format("{0}_{1}_{2}.{3}", picture.Id.ToString("0000000"), seoFileName, targetSize, lastPart) :
            //            string.Format("{0}_{1}.{2}", picture.Id.ToString("0000000"), targetSize, lastPart);
            //        var thumbFilePath = GetThumbLocalPath(thumbFileName);
            //        if (!File.Exists(thumbFilePath))
            //        {
            //            using (var stream = new MemoryStream(pictureBinary))
            //            {
            //                Bitmap b = null;
            //                try
            //                {
            //                    //try-catch to ensure that picture binary is really OK. Otherwise, we can get "Parameter is not valid" exception if binary is corrupted for some reasons
            //                    b = new Bitmap(stream);
            //                }
            //                catch (ArgumentException exc)
            //                {
            //                    _logger.Error(string.Format("Error generating picture thumb. ID={0}", picture.Id), exc);
            //                }
            //                if (b == null)
            //                {
            //                    //bitmap could not be loaded for some reasons
            //                    return url;
            //                }

            //                // start watermark logic
            //                string watermarkText = "SynZeal";
            //                using (Graphics grp = Graphics.FromImage(b))
            //                {
            //                    //Set the Color of the Watermark text.

            //                    //Brush brush = new SolidBrush(Color.FromArgb(128,Color.Gray));
            //                    Brush brush = new SolidBrush(Color.FromArgb(128, Color.FromArgb(202, 200, 200)));

            //                    //Set the Font and its size.
            //                    Font font = new System.Drawing.Font("Arial", 150, FontStyle.Bold, GraphicsUnit.Pixel);

            //                    //Determine the size of the Watermark text.
            //                    SizeF textSize = new SizeF();
            //                    textSize = grp.MeasureString(Convert.ToString(watermarkText), font);

            //                    //Position the text and draw it on the image.
            //                    //Point position = new Point((b.Width - ((int)textSize.Width + 10)), (b.Height - ((int)textSize.Height + 10)));
            //                    Point position = new Point((b.Width / 2) - 250, (b.Height / 2) - 150);
            //                    grp.DrawString(Convert.ToString(watermarkText), font, brush, position);

            //                    //using (MemoryStream memoryStream = new MemoryStream())
            //                    //{
            //                    //    //Save the Watermarked image to the MemoryStream.
            //                    //    b.Save(memoryStream, ImageFormat.Png);
            //                    //    memoryStream.Position = 0;
            //                    //    return File(memoryStream.ToArray(), "image/png", fileName);
            //                    //}

            //                }
            //                //end watermark logic


            //                using (var destStream = new MemoryStream())
            //                {
            //                    var newSize = CalculateDimensions(b.Size, targetSize);
            //                    ImageBuilder.Current.Build(b, destStream, new ResizeSettings
            //                    {
            //                        Width = newSize.Width,
            //                        Height = newSize.Height,
            //                        Scale = ScaleMode.Both,
            //                        Quality = _mediaSettings.DefaultImageQuality
            //                    });
            //                    var destBinary = destStream.ToArray();
            //                    File.WriteAllBytes(thumbFilePath, destBinary);
            //                    b.Dispose();
            //                }
            //            }
            //        }
            //    }
            //}
            //url = GetThumbUrl(thumbFileName, storeLocation);
            //return url;
        }


        public string GetDiffLevelStatus(string status)
        {
            string str = "";
            foreach (DifficultyLevel r in Enum.GetValues(typeof(DifficultyLevel)))
            {
                var item = Enum.GetName(typeof(DifficultyLevel), r);
                var test = r.ToString();
                string text = GetEnumDescription((DifficultyLevel)(int)r);
                int val = (int)r;
                string selected = "";
                if (Convert.ToString(val) == status)
                {
                    str = text;
                }
            }
            return str;
        }

        public string GetProStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return "";
            }
            string str = "";
            foreach (ProStatusDDL r in Enum.GetValues(typeof(ProStatusDDL)))
            {
                var item = Enum.GetName(typeof(ProStatusDDL), r);
                var test = r.ToString();
                string text = GetEnumDescription((ProStatusDDL)(int)r);
                int val = (int)r;
                string selected = "";
                if (Convert.ToString(val) == status)
                {
                    str = text;
                }
            }

            return str;
        }
        public string GetEnumDescription(Enum value)
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
        public string GetProjectType(string type)
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

        public string GetCustomerName(int? userId, IEnumerable<Customer> customerData)
        {
            return customerData.Where(x => x.Id == userId).Select(x => GetFullName(x)).FirstOrDefault();
        }
        public string GetFullName(Customer customer)
        {

            if (customer == null)
                throw new ArgumentNullException("customer");
            var firstName = _foodDbContext.GenericAttribute.Where(x => x.KeyGroup == "Customer" && x.Key == "FirstName").Select(x => x.Value).FirstOrDefault();
            var lastName = _foodDbContext.GenericAttribute.Where(x => x.KeyGroup == "Customer" && x.Key == "LastName").Select(x => x.Value).FirstOrDefault();

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
        public async Task<IList<SZ_InventoryDto>> GetInventoryForWebsite(int productId)
        {
            var inventoryData = _foodDbContext.SZ_Inventory.Where(x => x.ProductId == productId).ToList();
            var inventoryIds = inventoryData.Select(x => x.Id).ToList();
            var coaAllRecords = (from i in _foodDbContext.SZ_MasterCOA
                                 where inventoryIds.Contains(i.BatchId)
                                 select i).ToList();
            var inventoryModel = new List<SZ_InventoryDto>();
            if (inventoryData.Count > 0)
            {
                inventoryData.ForEach(inv =>
                {
                    SZ_InventoryDto invModel = new SZ_InventoryDto();
                    invModel.SZInventoryId = inv.Id;
                    invModel.BatchNo = inv.BatchNo;
                    invModel.AvailableQty = inv.AvailableQty;
                    if (inv.Qty.HasValue)
                    {
                        invModel.Qty = inv.Qty.Value;
                    }
                    else
                    {
                        invModel.Qty = 0;
                    }
                    var coarecord = coaAllRecords.Where(x => x.BatchId == inv.Id).FirstOrDefault();
                    if (coarecord != null)
                    {
                        invModel.Purity = coarecord.HPLCGCELSD;
                        invModel.TGA = coarecord.TGALoss;
                        invModel.Appreance = coarecord.AppearanceProduct;
                        invModel.ReTestDate = coarecord.ReTestDate;
                        invModel.COA = "<a href='javascript:void(0)' onclick='OpenCOA(" + coarecord.Id + ")' >Download</a>";
                    }
                    inventoryModel.Add(invModel);
                });
            }
            return inventoryModel;
        }

        public int Count()
        {
            return _foodDbContext.FoodItems.Count();
        }

        public bool Save()
        {
            return (_foodDbContext.SaveChanges() >= 0);
        }

        public ICollection<FoodEntity> GetRandomMeal()
        {
            List<FoodEntity> toReturn = new List<FoodEntity>();

            toReturn.Add(GetRandomItem("Starter"));
            toReturn.Add(GetRandomItem("Main"));
            toReturn.Add(GetRandomItem("Dessert"));

            return toReturn;
        }

        private FoodEntity GetRandomItem(string type)
        {
            return _foodDbContext.FoodItems
                .Where(x => x.Type == type)
                .OrderBy(o => Guid.NewGuid())
                .FirstOrDefault();
        }

        public async Task<IQueryable<SZ_QuotationDetail>> GetQuoteDetailsByQuoteId(int Id)
        {
            var list = (from t2 in _foodDbContext.SZ_QuotationDetail.AsNoTracking()
                        where t2.QuoteId == Id
                        orderby t2.CreatedDate descending
                        select t2).AsQueryable();
            return list;
        }

        public List<SZ_QuotationDetail> GetQuoteDetailsByPONumber(string poNumber)
        {
            return (from x in _foodDbContext.SZ_Quotation.AsNoTracking()
                        join t2 in _foodDbContext.SZ_QuotationDetail.AsNoTracking() on x.Id equals t2.QuoteId
                        where x.PONo == poNumber
                        orderby x.CreatedDate descending
                        select t2).ToList();
        }

        public SZ_QuotationDetail GetQuoteDetailsById(int Id)
        {
            var list = (from t2 in _foodDbContext.SZ_QuotationDetail.AsNoTracking()
                        where t2.Id == Id
                        orderby t2.CreatedDate descending
                        select t2).FirstOrDefault();
            return list;
        }

        public SZ_QuotationDetail UpdateQuoteDetails(SZ_QuotationDetail quoteDetails)
        {
            _foodDbContext.SZ_QuotationDetail.Update(quoteDetails);
            
            return quoteDetails;
        }

    }
}
