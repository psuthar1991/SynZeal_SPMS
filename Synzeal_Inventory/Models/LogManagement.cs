using Synzeal_Inventory.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Synzeal_Inventory.Models
{
    public class LogManagement
    {
        public static SynzealLiveEntities db = new SynzealLiveEntities();

        public static void AddQuoteLog(SZ_Quotation Newdatamodel, SZ_Quotation Olddatamodel)
        {
            foreach (var prop in Newdatamodel.GetType().GetProperties())
            {
                string name = prop.Name;
                object value = prop.GetValue(Newdatamodel, null);

                foreach (var oldprop in Olddatamodel.GetType().GetProperties())
                {
                    string oldname = oldprop.Name;
                    object oldvalue = oldprop.GetValue(Olddatamodel, null);
                    if (oldname == name)
                    {
                        if (Convert.ToString(value) != Convert.ToString(oldvalue))
                        {
                            string fieldname = Quotefieldname(name);
                            if (!string.IsNullOrEmpty(fieldname))
                            {
                                SystemLog objhis = new SystemLog();
                                objhis.EntityId = Olddatamodel.Id;
                                objhis.Entityname = "Quote";
                                objhis.Username = SessionCookieManagement.UserName;
                                objhis.CreatedDate = System.DateTime.Now;
                                objhis.PropertyName = name;
                                objhis.FieldName = fieldname; // need to change
                                objhis.Before = Convert.ToString(oldvalue);
                                objhis.After = Convert.ToString(value);
                                objhis.UserId = SessionCookieManagement.UserId;
                                db.SystemLogs.Add(objhis);
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
        }

        public static void AddDispatchLog(SZ_QuotationDetail Newdatamodel, SZ_QuotationDetail Olddatamodel)
        {
            var allarray = new List<string>
            {
                "ApprovalComment",
                "EstimateCompleteDate",
                "Reason",
                "AdditionalBatchNo",
                "COARefNumber",
                "DispatchStatus",
                "ProStatus",
                "ActivityStatus"
            };

            MemoryCacheManager objCache = new MemoryCacheManager();
            var genericData = objCache.Get("cache.genericData", () =>
            {
                return db.GenericAttributes.Where(x => x.KeyGroup == "Customer").ToList();
            });
            var listItems = new List<SelectListItem>();
            var scienList = objCache.Get("cache.GetScientistId", () =>
            {
                return db.GetScientistId().ToList();
            });

            scienList.ForEach(term =>
            {
                string customerName = string.Empty;
                var genericAttr = genericData.Where(x => x.EntityId == term).ToList();
                if (genericAttr.Count > 0)
                {
                    customerName = genericAttr.Where(x => x.Key == "FirstName").Select(x => x.Value).FirstOrDefault() + " " + genericAttr.Where(x => x.Key == "LastName").Select(x => x.Value).FirstOrDefault();
                }
                listItems.Add(new SelectListItem
                {
                    Text = customerName,
                    Value = term.ToString()
                });
            });
            foreach (var prop in Newdatamodel.GetType().GetProperties())
            {
                string name = prop.Name;
                object value = prop.GetValue(Newdatamodel, null);

                foreach (var oldprop in Olddatamodel.GetType().GetProperties())
                {
                    string oldname = oldprop.Name;
                    object oldvalue = oldprop.GetValue(Olddatamodel, null);
                    if (oldname == name)
                    {
                        if (Convert.ToString(value) != Convert.ToString(oldvalue))
                        {
                            if (allarray.Contains(name))
                            {
                                if (name == "DispatchStatus" ||
                                    name == "ProStatus")
                                {
                                    oldvalue = Common.GetProStatus(Convert.ToString(oldvalue));
                                    value = Common.GetProStatus(Convert.ToString(value));
                                }
                                if (name == "DispatchStatus")
                                {
                                    name = "Dispatch Status";
                                }
                                if (name == "ProStatus")
                                {
                                    name = "Status";
                                }
                                SZ_QuotationDetailLog objhis = new SZ_QuotationDetailLog();
                                objhis.QuoteId = Olddatamodel.QuoteId;
                                objhis.QuoteDetailsId = Newdatamodel.Id;
                                objhis.QuoteRef = Newdatamodel.SZ_Quotation.Ref;
                                objhis.Username = SessionCookieManagement.UserName;
                                objhis.Datetime = System.DateTime.Now;
                                objhis.PropertyName = name;
                                objhis.FieldName = name; // need to change
                                objhis.Before = Convert.ToString(oldvalue);
                                objhis.After = Convert.ToString(value);
                                objhis.UserId = SessionCookieManagement.UserId;
                                db.SZ_QuotationDetailLog.Add(objhis);
                               
                            }
                        }
                    }
                }
            }

            db.SaveChanges();
        }

        public static void AddMasterCOALog(SZ_MasterCOA Newdatamodel, SZ_MasterCOA Olddatamodel)
        {
            var allarray = new List<string>
            {
                "ProductName",
                "BatchNo",
                "AnalysisDate",
                "HPLCGCELSD",
                "TGALoss",
                "ResidueOnIgnition",
                "Potency",
                "SOLUBILITY",
                "IR",
                "Mass",
                "NMR",
                "CMR",
                "Dept",
                "StorageCon",
                "ReTestDate",
                "AdditionalInfor",
                "RefNo",
                "Purity",
                "CASNo",
                "MolecularWeight",
                "MolFormula",
                "Chemicalname",
                "AppearanceProduct",
                "CATNo",
                "Attachment",
                "Synonym",
                "ManufactureDate",
                "OtherValue",
                "CTheroretical",
                "HTheroretical",
                "NTheroretical",
                "STheroretical",
                "CPractical",
                "SPractical",
                "HPractical",
                "NPractical",
                "WegithLossBy",
                "MeltingPoint",
                "qNMR",
            };

            foreach (var prop in Newdatamodel.GetType().GetProperties())
            {
                string name = prop.Name;
                object value = prop.GetValue(Newdatamodel, null);

                foreach (var oldprop in Olddatamodel.GetType().GetProperties())
                {
                    string oldname = oldprop.Name;
                    object oldvalue = oldprop.GetValue(Olddatamodel, null);
                    if (oldname.ToLower() == name.ToLower())
                    {
                        if (Convert.ToString(value) != Convert.ToString(oldvalue))
                        {
                            if (allarray.Contains(name))
                            {
                                string fieldname = Quotefieldname(name);
                                if (!string.IsNullOrEmpty(fieldname))
                                {
                                    SystemLog objhis = new SystemLog();
                                    objhis.EntityId = Olddatamodel.Id;
                                    objhis.Entityname = "Mastercoa";
                                    objhis.Username = SessionCookieManagement.UserName;
                                    objhis.CreatedDate = System.DateTime.Now;
                                    objhis.PropertyName = name;
                                    objhis.FieldName = fieldname; // need to change
                                    objhis.Before = Convert.ToString(oldvalue);
                                    objhis.After = Convert.ToString(value);
                                    objhis.UserId = SessionCookieManagement.UserId;
                                    db.SystemLogs.Add(objhis);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void AddChildCOALog(SZ_ChildCOA Newdatamodel, SZ_ChildCOA Olddatamodel)
        {
            var allarray = new List<string>
            {
                "ProductName",
                "BatchNo",
                "AnalysisDate",
                "HPLCGCELSD",
                "TGALoss",
                "ResidueOnIgnition",
                "Potency",
                "SOLUBILITY",
                "IR",
                "Mass",
                "NMR",
                "CMR",
                "Dept",
                "StorageCon",
                "ReTestDate",
                "AdditionalInfor",
                "RefNo",
                "Purity",
                "CASNo",
                "MolecularWeight",
                "MolFormula",
                "Chemicalname",
                "AppearanceProduct",
                "CATNo",
                "Attachment",
                "Synonym",
                "ManufactureDate",
                "OtherValue",
                "CTheroretical",
                "HTheroretical",
                "NTheroretical",
                "STheroretical",
                "CPractical",
                "SPractical",
                "HPractical",
                "NPractical",
                "WegithLossBy",
                "MeltingPoint",
                "qNMR"
            };
            foreach (var prop in Newdatamodel.GetType().GetProperties())
            {
                string name = prop.Name;
                object value = prop.GetValue(Newdatamodel, null);

                foreach (var oldprop in Olddatamodel.GetType().GetProperties())
                {
                    string oldname = oldprop.Name;
                    object oldvalue = oldprop.GetValue(Olddatamodel, null);
                    if (oldname.ToLower() == name.ToLower())
                    {
                        if (Convert.ToString(value) != Convert.ToString(oldvalue))
                        {
                            if (allarray.Contains(name))
                            {
                                string fieldname = Quotefieldname(name);
                                if (!string.IsNullOrEmpty(fieldname))
                                {
                                    SystemLog objhis = new SystemLog();
                                    objhis.EntityId = Olddatamodel.Id;
                                    objhis.Entityname = "Childcoa";
                                    objhis.Username = SessionCookieManagement.UserName;
                                    objhis.CreatedDate = System.DateTime.Now;
                                    objhis.PropertyName = name;
                                    objhis.FieldName = fieldname; // need to change
                                    objhis.Before = Convert.ToString(oldvalue);
                                    objhis.After = Convert.ToString(value);
                                    objhis.UserId = SessionCookieManagement.UserId;
                                    db.SystemLogs.Add(objhis);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }

            
        }

        public static SZ_Quotation PrepareQuoteEntity(SZ_Quotation model)
        {
            var obj = new SZ_Quotation()
            {
                Id = model.Id,
                CompanyName = model.CompanyName,
                EmailAddress = model.EmailAddress,
                IsImageAttach = model.IsImageAttach,
                PONo = model.PONo,
                Remark = model.Remark,
                TermsId = model.TermsId,
                CreatedDate = model.CreatedDate,
                Ref = model.Ref,
                CompanyId = model.CompanyId,
                PODate = model.PODate,
                IsImportedQuote = model.IsImportedQuote,
                CountryType = model.CountryType,
                IsToBe = model.IsToBe,
                ClientRef = model.ClientRef,
                Attachment = model.Attachment,
                SuggChemName = model.SuggChemName,
                IsPayment = model.IsPayment,
                Auction = model.Auction,
                EmailCC = model.EmailCC,
                IsQuoteApproved = model.IsQuoteApproved,
                UserDistType = model.UserDistType,
                IsRemoveFollowup = model.IsRemoveFollowup,
                CreatedBy = model.CreatedBy,
                IsPark = model.IsPark,
                IsCOA = model.IsCOA,
                QuoteTotal = model.QuoteTotal,
                FollowUpComment = model.FollowUpComment,
                IsFollowupRequired = model.IsFollowupRequired,
                ApprovedBy = model.ApprovedBy,
                ParkReason = model.ParkReason,
                IsInstock = model.IsInstock,
                IsCustomSynthesis = model.IsCustomSynthesis,
                LayoutType = model.LayoutType,
                IsPreviewed = model.IsPreviewed,
                IsReviewed = model.IsReviewed,
                QuoteComment = model.QuoteComment,
                IsWorking = model.IsWorking,
                WorkingDate = model.WorkingDate,
                WorkingUserName = model.WorkingUserName,
                PaymentTerm = model.PaymentTerm,
                EmailReceivedDate = model.EmailReceivedDate,
                Currency = model.Currency,
                AssignTo = model.AssignTo
            };

            return obj;
        }
        public static SZ_QuotationDetail PrepareQuoteDetailEntity(SZ_QuotationDetail model)
        {
            var obj = new SZ_QuotationDetail()
            {
                QuoteId = model.QuoteId,
                ProductId = model.ProductId,
                ProductName = model.ProductName,
                CASNo = model.CASNo,
                ImagePath = model.ImagePath,
                Price = model.Price,
                LeadTime = model.LeadTime,
                IsUploadServer = model.IsUploadServer,
                CreatedDate = model.CreatedDate,
                CATNo = model.CATNo,
                DisplayOrder = model.DisplayOrder,
                ProjectType = model.ProjectType,
                ScientistCustomerId = model.ScientistCustomerId,
                RequiredQty = model.RequiredQty,
                ProjectStatus = model.ProjectStatus,
                ScientistStatus = model.ScientistStatus,
                BatchCode1 = model.BatchCode1,
                Qty1 = model.Qty1,
                BatchCode2 = model.BatchCode2,
                Qty2 = model.Qty2,
                Courier = model.Courier,
                TrackingNo = model.TrackingNo,
                Location = model.Location,
                RefName = model.RefName,
                PurposeDispatch = model.PurposeDispatch,
                DeliveryDate = model.DeliveryDate,
                DispatchedStatus = model.DispatchedStatus,
                ProcessState = model.ProcessState,
                PackDate = model.PackDate,
                DeliveryStatus = model.DeliveryStatus,
                DataPending = model.DataPending,
                TrackingNoDate = model.TrackingNoDate,
                EstimateCompleteDate = model.EstimateCompleteDate,
                MoveToProject = model.MoveToProject,
                MoveToDispatch = model.MoveToDispatch,
                BatchNo = model.BatchNo,
                AdditionalBatchNo = model.AdditionalBatchNo,
                Remark = model.Remark,
                MoveToInvoice = model.MoveToInvoice,
                AdminScientistStatus = model.AdminScientistStatus,
                SrPo = model.SrPo,
                InvoiceRemark = model.InvoiceRemark,
                InvoiceNo = model.InvoiceNo,
                PaymentStatus = model.PaymentStatus,
                IsOnHold = model.IsOnHold,
                MoveProjectDate = model.MoveProjectDate,
                MoveDispatchDate = model.MoveDispatchDate,
                MoveToInvoiceDate = model.MoveToInvoiceDate,
                MoveToScientistDate = model.MoveToScientistDate,
                ProductRemark = model.ProductRemark,
                IsDataApproved = model.IsDataApproved,
                ScientistRemark = model.ScientistRemark,
                DataApprovedStatus = model.DataApprovedStatus,
                DataApprovalDate = model.DataApprovalDate,
                PurchaseDate = model.PurchaseDate,
                PurchaseStatus = model.PurchaseStatus,
                Instockdate = model.Instockdate,
                LastStatus = model.LastStatus,
                COAPath = model.COAPath,
                AnalyticalData = model.AnalyticalData,
                ClientRemark = model.ClientRemark,
                ClientAddress = model.ClientAddress,
                AttachedDataList = model.AttachedDataList,
                ClientStatus = model.ClientStatus,
                SubScientistName = model.SubScientistName,
                FollowUpRemark = model.FollowUpRemark,
                FollowUpRemarkSecond = model.FollowUpRemarkSecond,
                IsPayment = model.IsPayment,
                IsDispatchApprove = model.IsDispatchApprove,
                EstimateDispatchDate = model.EstimateDispatchDate,
                OrderRemark = model.OrderRemark,
                LastUploadDate = model.LastUploadDate,
                Reason = model.Reason,
                ResponseClientRemarkDate = model.ResponseClientRemarkDate,
                IsFirstTimeDataUpload = model.IsFirstTimeDataUpload,
                QueryText = model.QueryText,
                QueryDate = model.QueryDate,
                IsSynthesisLog = model.IsSynthesisLog,
                IsAssignScientistQuery = model.IsAssignScientistQuery,
                IsAssignProjectQuery = model.IsAssignProjectQuery,
                Explanation = model.Explanation,
                ReportInvoiceDate = model.ReportInvoiceDate,
                COAApprovedDate = model.COAApprovedDate,
                IsQueryResolved = model.IsQueryResolved,
                QueryResolvedDate = model.QueryResolvedDate,
                IsDispatchedLock = model.IsDispatchedLock,
                IsFollowUpAdminChange = model.IsFollowUpAdminChange,
                IsFollowupChange = model.IsFollowupChange,
                FollowupDescription = model.FollowupDescription,
                ContactDetail = model.ContactDetail,
                DifficultyLevel = model.DifficultyLevel,
                IsPriority = model.IsPriority,
                ActivityStatus = model.ActivityStatus,
                COARefNumber = model.COARefNumber,
                ProStatus = model.ProStatus,
                ExplainationSecond = model.ExplainationSecond,
                LastWeekUpdate = model.LastWeekUpdate,
                PreviousEstCompleteDate = model.PreviousEstCompleteDate,
                ReviewSciStatus = model.ReviewSciStatus,
                DispatchStatus = model.DispatchStatus,
                OtherProStatus = model.OtherProStatus,
                LastProStatus = model.LastProStatus
            };

            return obj;
        }

        public static string Quotefieldname(string propname)
        {
            string fieldname = "";
            if (propname == "CompanyName")
            {
                fieldname = "Company Name";
            }
            if (propname == "EmailAddress")
            {
                fieldname = "Email Address";
            }
            if (propname == "IsImageAttach")
            {
                fieldname = "Image Attach";
            }
            if (propname == "PONo")
            {
                fieldname = "PO Number";
            }
            if (propname == "CMR")
            {
                fieldname = "13C NMR";
            }
            if (propname == "AssignTo")
            {
                fieldname = "Assign To";
            }
            

            if (string.IsNullOrEmpty(fieldname))
            {
                return propname;
            }
            return fieldname;
        }

        public static SZ_MasterCOA PrepareMasterCOAEntity(SZ_MasterCOA model)
        {
            var obj = new SZ_MasterCOA()
            {
                Id = model.Id,
                ProductName = model.ProductName,
                BatchNo = model.BatchNo,
                AnalysisDate = model.AnalysisDate,
                HPLCGCELSD = model.HPLCGCELSD,
                TGALoss = model.TGALoss,
                ResidueOnIgnition = model.ResidueOnIgnition,
                Potency = model.Potency,
                SOLUBILITY = model.SOLUBILITY,
                IR = model.IR,
                Mass = model.Mass,
                NMR = model.NMR,
                CMR = model.CMR,
                Dept = model.Dept,
                StorageCon = model.StorageCon,
                ReTestDate = model.ReTestDate,
                AdditionalInfor = model.AdditionalInfor,
                RefNo = model.RefNo,
                Purity = model.Purity,
                CASNo = model.CASNo,
                MolecularWeight = model.MolecularWeight,
                MolFormula = model.MolFormula,
                Chemicalname = model.Chemicalname,
                AppearanceProduct = model.AppearanceProduct,
                CATNo = model.CATNo,
                Attachment = model.Attachment,
                Synonym = model.Synonym,
                ManufactureDate = model.ManufactureDate,
                OtherValue = model.OtherValue,
                CTheroretical = model.CTheroretical,
                HTheroretical = model.HTheroretical,
                NTheroretical = model.NTheroretical,
                STheroretical = model.STheroretical,
                CPractical = model.CPractical,
                SPractical = model.SPractical,
                HPractical = model.HPractical,
                NPractical = model.NPractical,
                WegithLossBy = model.WegithLossBy,
                MeltingPoint = model.MeltingPoint
            };

            return obj;
        }

        public static SZ_ChildCOA PrepareChildCOAEntity(SZ_ChildCOA model)
        {
            var obj = new SZ_ChildCOA()
            {
                Id = model.Id,
                ProductName = model.ProductName,
                BatchNo = model.BatchNo,
                AnalysisDate = model.AnalysisDate,
                HPLCGCELSD = model.HPLCGCELSD,
                TGALoss = model.TGALoss,
                ResidueOnIgnition = model.ResidueOnIgnition,
                Potency = model.Potency,
                SOLUBILITY = model.SOLUBILITY,
                IR = model.IR,
                Mass = model.Mass,
                NMR = model.NMR,
                CMR = model.CMR,
                Dept = model.Dept,
                StorageCon = model.StorageCon,
                ReTestDate = model.ReTestDate,
                AdditionalInfor = model.AdditionalInfor,
                RefNo = model.RefNo,
                Purity = model.Purity,
                CASNo = model.CASNo,
                MolecularWeight = model.MolecularWeight,
                MolFormula = model.MolFormula,
                Chemicalname = model.Chemicalname,
                AppearanceProduct = model.AppearanceProduct,
                CATNo = model.CATNo,
                Attachment = model.Attachment,
                Synonym = model.Synonym,
                ManufactureDate = model.ManufactureDate,
                OtherValue = model.OtherValue,
                CTheroretical = model.CTheroretical,
                HTheroretical = model.HTheroretical,
                NTheroretical = model.NTheroretical,
                STheroretical = model.STheroretical,
                CPractical = model.CPractical,
                SPractical = model.SPractical,
                HPractical = model.HPractical,
                NPractical = model.NPractical,
                WegithLossBy = model.WegithLossBy,
                MeltingPoint = model.MeltingPoint
            };

            return obj;
        }
        
    }
}