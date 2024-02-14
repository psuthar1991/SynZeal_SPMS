using Newtonsoft.Json;
using Synzeal_BatchFormData_Sync.Entity;
using Synzeal_BatchFormData_Sync.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Synzeal_BatchFormData_Sync
{
    // ref : https://www.c-sharpcorner.com/article/working-with-ftp-using-c-sharp/
    class Program
    {
        public static string host = ConfigurationSettings.AppSettings["host"];
        public static string UserId = ConfigurationSettings.AppSettings["UserId"];
        public static string Password = "&3ys6t1B"; /* LIVE*/
        //public static string Password = "$Avrk594"; /* STAGING*/
        public static string DomainName = "spms.synzeal.com"; /*LIVE*/
        //public static string DomainName = "quote.synzeal.in"; /*STAGING*/
        public static SynzealEntities db = new SynzealEntities();
        public static async Task Main(string[] args)
        {
            Console.WriteLine(Environment.NewLine + "Process start for sync Batch data");
            Console.WriteLine(Environment.NewLine + "Domain Name : " + DomainName);
            var str = System.Environment.CurrentDirectory;
            var allAPIFiles = Directory.GetDirectories(ConfigurationSettings.AppSettings["LocalPath"]);
            string inventoryuri = "http://" + DomainName + "/Form/GetInventoryDataByBatchNo?batchno=";
            string quotedetailsuri = "http://" + DomainName + "/Form/GetQuoteDetailsbyAdditionalBatchNoid?batchid=";
            string producturi = "http://" + DomainName + "/Form/GetProductById?id=";
            string quoteDetailFormuri = "http://" + DomainName + "/Form/GetQuoteDetailsFormByBatchCode?BatchCode=";
            string newFormuri = "http://" + DomainName + "/Form/NewQuotationForm";
            string updateFormuri = "http://" + DomainName + "/Form/UpdateQuotationForm";
            string quotedetailsFormmapuri = "http://" + DomainName + "/Form/NewQuotationFormMap";
            foreach (var APIFolder in allAPIFiles)
            {
                Console.WriteLine(Environment.NewLine + "API Folder Path : " + APIFolder);

                var allBatchDataFiles = Directory.GetDirectories(APIFolder);
                foreach (var batchFolder in allBatchDataFiles)
                {
                    var files = Directory.GetFiles(batchFolder);

                    //Get Batch No
                    var batchNo = batchFolder.Split('\\').LastOrDefault();
                    batchNo = batchNo.Trim();
                    Console.WriteLine(Environment.NewLine + "URL : " + inventoryuri + "" + batchNo);

                    //Check form is available or not
                    var inventoryData = new SZ_InventoryModel();
                    WebClient wc = new WebClient();
                    string inventoryjson = wc.DownloadString(inventoryuri + "" + batchNo);
                    inventoryData = JsonConvert.DeserializeObject<SZ_InventoryModel>(inventoryjson);


                    if (inventoryData == null)
                        continue;

                    var quotationDetailsForm = new SZ_QuotationDetailModel();

                    wc = new WebClient();
                    string quotationDetailsjson = wc.DownloadString(quotedetailsuri + "" + inventoryData.Id);
                    quotationDetailsForm = JsonConvert.DeserializeObject<SZ_QuotationDetailModel>(quotationDetailsjson);
                    if (quotationDetailsForm == null)
                        continue;

                    string spectralDataAttachment = string.Empty;
                    string IRAttachment = string.Empty;
                    string MassAttachment = string.Empty;
                    string HPLCAttachment = string.Empty;
                    string NMRAttchment = string.Empty;
                    string QNMRAttchment = string.Empty;
                    string TGAAttachment = string.Empty;
                    string CMRAttchment = string.Empty;
                    string DEPTAttachment = string.Empty;
                    string HRMSAttachment = string.Empty;
                    string ROIAttachment = string.Empty;
                    string ElementralAttachment = string.Empty;
                    string SERAttachment = string.Empty;
                    string GCAttachment = string.Empty;
                    string ELSDAttachment = string.Empty;
                    string ChiralAttachmenrt = string.Empty;
                    string additionalAnalysis = string.Empty;
                    string DEPTAttchment = string.Empty;
                    string HRMSAttchment = string.Empty;
                    string ElementalAttchment = string.Empty;
                    string ChiralAttchment = string.Empty;
                    string NMRInterpretaionAttachment = string.Empty; string APCIMassAttachment = string.Empty;
                    List<string> additionalAnalysisList = new List<string>();
                    // Read All files
                    foreach (var file in files)
                    {
                        var fileName = file.Split('\\').LastOrDefault();
                        string filenewname = "/Content/NewProducts/" + Guid.NewGuid().ToString() + "" + Path.GetExtension(fileName);
                        string newname = host + "/" + DomainName + "" + filenewname;
                        if (fileName.Trim().ToLower().Contains("hplc"))
                        {
                            UploadFile(file, newname);
                            HPLCAttachment = ".." + filenewname;
                            additionalAnalysisList.Add("HPLC/GC/ELSD");
                        }
                        if (fileName.Trim().ToLower().Contains("mass"))
                        {
                            UploadFile(file, newname);
                            MassAttachment = ".." + filenewname;
                            additionalAnalysisList.Add("Mass");
                        }
                        if (fileName.Trim().ToLower().Contains("ir"))
                        {
                            UploadFile(file, newname);
                            IRAttachment = ".." + filenewname;
                            additionalAnalysisList.Add("IR");
                        }

                        if (fileName.Trim().ToLower().Contains("tga"))
                        {
                            UploadFile(file, newname);
                            TGAAttachment = ".." + filenewname;
                            additionalAnalysisList.Add("Mass");
                        }

                        if (fileName.Trim().ToLower().Contains("1h") || fileName.Trim().ToLower().Contains("nmr"))
                        {
                            UploadFile(file, newname);
                            NMRAttchment = ".." + filenewname;
                            additionalAnalysisList.Add("NMR");
                        }
                        if (fileName.Trim().ToLower().Contains("13c") || fileName.Trim().ToLower().Contains("cmr"))
                        {
                            UploadFile(file, newname);
                            CMRAttchment = ".." + filenewname;
                            additionalAnalysisList.Add("CMR");
                        }


                        if (fileName.Trim().ToLower().Contains("nmr interpretation"))
                        {
                            UploadFile(file, newname);
                            NMRInterpretaionAttachment = ".." + filenewname;
                            additionalAnalysisList.Add("NMR Interpretaion");
                        }
                        if (fileName.Trim().ToLower().Contains("cmr interpretation"))
                        {
                            UploadFile(file, newname);
                            SERAttachment = ".." + filenewname;
                            additionalAnalysisList.Add("CMR Interpretaion");
                        }
                        if (fileName.Trim().ToLower().Contains("mass interpretation"))
                        {
                            UploadFile(file, newname);
                            APCIMassAttachment = ".." + filenewname;
                            additionalAnalysisList.Add("APCI Mass");
                        }

                        if (fileName.Trim().ToLower().Contains("dept"))
                        {
                            UploadFile(file, newname);
                            DEPTAttchment = ".." + filenewname;
                            additionalAnalysisList.Add("DEPT");
                        }
                        if (fileName.Trim().ToLower().Contains("hrms"))
                        {
                            UploadFile(file, newname);
                            HRMSAttchment = ".." + filenewname;
                            additionalAnalysisList.Add("HRMS");
                        }
                        if (fileName.Trim().ToLower().Contains("chns"))
                        {
                            UploadFile(file, newname);
                            ElementalAttchment = ".." + filenewname;
                            additionalAnalysisList.Add("Elemental");
                        }
                        if (fileName.Trim().ToLower().Contains("chiral"))
                        {
                            UploadFile(file, newname);
                            ChiralAttchment = ".." + filenewname;
                            additionalAnalysisList.Add("ChiralChr");
                        }
                    }
                    if (additionalAnalysisList != null && additionalAnalysisList.Count > 0)
                    {
                        additionalAnalysis = string.Join(",", additionalAnalysisList);
                    }
                    //var productData = db.Products.Where(x => x.Id == quotationDetailsForm.ProductId).FirstOrDefault();
                    wc = new WebClient();
                    string productDatajson = wc.DownloadString(producturi + "" + quotationDetailsForm.ProductId);
                    var productData = JsonConvert.DeserializeObject<ProductModel>(productDatajson);
                    //var formData = db.SZ_QuoteDetailForm.Where(x => x.BatchCode == batchNo).FirstOrDefault();

                    Console.WriteLine(Environment.NewLine + "Batch No : " + batchNo);
                    Console.WriteLine(Environment.NewLine);
                    wc = new WebClient();
                    string formDatajson = wc.DownloadString(quoteDetailFormuri + "" + batchNo);
                    var formData = JsonConvert.DeserializeObject<QuotationFormModel>(formDatajson);
                    if (formData == null)
                    {
                        //Add new Form
                        SZ_QuoteDetailForm objForm = new SZ_QuoteDetailForm();
                        objForm.QuotationDetailsId = quotationDetailsForm.Id;
                        objForm.CATNo = quotationDetailsForm.CATNo.Trim();
                        objForm.CASNo = quotationDetailsForm.CASNo;
                        objForm.Apearance = "";
                        objForm.BatchCode = batchNo;
                        objForm.CreatedDate = System.DateTime.Now;
                        objForm.Error = "";
                        objForm.HPCLCode = "";
                        objForm.ProductName = quotationDetailsForm.ProductName;
                        objForm.ProjectName = quotationDetailsForm.ProductName;
                        objForm.JournalDate = DateTime.Now;
                        objForm.MolFormula = productData.Gtin;
                        objForm.MolWeight = productData.MolecularWeight;
                        objForm.MSCode = "";
                        objForm.NMRCode = "";
                        objForm.OtherAnalysis = "";
                        objForm.Qty = "";
                        objForm.SaltName = "";
                        objForm.ScientistName = "";
                        objForm.StateCompound = "";
                        objForm.StructurePath = "";
                        objForm.SubmissionDate = DateTime.Now;
                        objForm.TypeCompound = "";
                        objForm.RbSaltMentionName = "";
                        objForm.UpdatedDate = DateTime.Now;
                        objForm.SubmittedBy = 0;
                        objForm.MolecularFormula = productData.Gtin;
                        objForm.TLName = "System Generate";
                        objForm.HPLCDate = "";
                        objForm.HPLCPurity = "";
                        objForm.ChkHygroscopic = false;
                        objForm.RbAdditionalAnalysis = additionalAnalysis;
                        objForm.Chemist = "System Generate";
                        objForm.SolidForm = "";
                        objForm.SolutionForm = "";
                        objForm.State = "";
                        objForm.SpectralDataAttachment = spectralDataAttachment;
                        objForm.IRAttachment = IRAttachment;
                        objForm.MassAttachment = MassAttachment;
                        objForm.PLCAttachment = HPLCAttachment;
                        objForm.NMRAttchment = NMRAttchment;
                        objForm.QNMRAttchment = QNMRAttchment;
                        objForm.TGAAttachment = TGAAttachment;
                        objForm.CMRAttchment = CMRAttchment;
                        objForm.DEPTAttachment = DEPTAttachment;
                        objForm.HRMSAttachment = HRMSAttachment;
                        objForm.ROIAttachment = ROIAttachment;
                        objForm.ElementralAttachment = ElementralAttachment;
                        objForm.SERAttachment = SERAttachment;
                        objForm.GCAttachment = GCAttachment;
                        objForm.ELSDAttachment = ELSDAttachment;
                        objForm.ChiralAttachmenrt = ChiralAttachmenrt;
                        objForm.UVSpectra = "";
                        objForm.OtherAnalysisAttachment = "";
                        objForm.N1NmrAttachment = "";
                        objForm.EarlierSynthesized = "";
                        objForm.IsDraftEntry = false;
                        objForm.NoOfFinalStep = 0;
                        objForm.PurificationBy = "";
                        objForm.APCIMassAttachment = APCIMassAttachment;
                        objForm.ChemdrawFileAttachment = "";
                        objForm.WeightingSlipAttachment = "";
                        objForm.NMRInterpretaionAttachment = NMRInterpretaionAttachment;
                        objForm.chkCrystallizationDone = false;
                        objForm.chkNMRDone = false;
                        objForm.TempSensitive = false;
                        objForm.Lacrymatory = false;
                        objForm.IsDispatchedEntry = false;
                        objForm.LightSensitivity = false;
                        objForm.Photostability = false;
                        objForm.ApprovalStatus = "0";
                        objForm.DEPTAttachment = DEPTAttachment;
                        objForm.HRMSAttachment = HRMSAttchment;
                        objForm.ElementralAttachment = ElementalAttchment;
                        objForm.ChiralAttachmenrt = ChiralAttchment;
                        using (var client = new WebApiClient())
                        {
                            var serializeModel = JsonConvert.SerializeObject(objForm);// using Newtonsoft.Json;
                            var response = await client.PostJsonWithModelAsync<QuotationFormModel>(newFormuri, serializeModel);
                            objForm.Id = response.Id;
                        }

                        //db.SZ_QuoteDetailForm.Add(objForm);
                        //db.SaveChanges();

                        SZ_QuoteDetails_FormModel objdetform = new SZ_QuoteDetails_FormModel();
                        objdetform.CreatedDate = DateTime.Now;
                        objdetform.FormId = objForm.Id;
                        objdetform.QuoteDetailsId = objForm.QuotationDetailsId;
                        using (var client = new WebApiClient())
                        {
                            var serializeobjdetformModel = JsonConvert.SerializeObject(objdetform);// using Newtonsoft.Json;
                            var response = await client.PostJsonWithModelAsync<SZ_QuoteDetails_FormModel>(quotedetailsFormmapuri, serializeobjdetformModel);
                            objdetform.Id = response.Id;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(NMRInterpretaionAttachment))
                        {
                            if (!string.IsNullOrEmpty(formData.NMRInterpretaionAttachment))
                            {
                                var isExist = DoesFtpDirectoryExist(host + "/" + DomainName + "/" + formData.NMRInterpretaionAttachment.Replace("../", ""));
                                if (isExist)
                                {
                                    DeleteFTPFile(host + "/" + DomainName + "/" + formData.NMRInterpretaionAttachment.Replace("../", ""));
                                }
                            }
                            formData.NMRInterpretaionAttachment = NMRInterpretaionAttachment;
                        }
                        else
                        {
                            formData.NMRInterpretaionAttachment = "";
                        }
                        if (!string.IsNullOrEmpty(SERAttachment))
                        {
                            if (!string.IsNullOrEmpty(formData.SERAttachment))
                            {
                                var isExist = DoesFtpDirectoryExist(host + "/" + DomainName + "/" + formData.SERAttachment.Replace("../", ""));
                                if (isExist)
                                {
                                    DeleteFTPFile(host + "/" + DomainName + "/" + formData.SERAttachment.Replace("../", ""));
                                }
                            }
                            formData.SERAttachment = SERAttachment;
                        }
                        else
                        {
                            formData.SERAttachment = "";
                        }
                        if (!string.IsNullOrEmpty(APCIMassAttachment))
                        {
                            if (!string.IsNullOrEmpty(formData.APCIMassAttachment))
                            {
                                var isExist = DoesFtpDirectoryExist(host + "/" + DomainName + "/" + formData.APCIMassAttachment.Replace("../", ""));
                                if (isExist)
                                {
                                    DeleteFTPFile(host + "/" + DomainName + "/" + formData.APCIMassAttachment.Replace("../", ""));
                                }
                            }
                            formData.APCIMassAttachment = APCIMassAttachment;
                        }
                        else
                        {
                            formData.APCIMassAttachment = "";
                        }
                        if (!string.IsNullOrEmpty(HPLCAttachment))
                        {
                            if (!string.IsNullOrEmpty(formData.PLCAttachment))
                            {
                                var isExist = DoesFtpDirectoryExist(host + "/" + DomainName + "/" + formData.PLCAttachment.Replace("../", ""));
                                if (isExist)
                                {
                                    DeleteFTPFile(host + "/" + DomainName + "/" + formData.PLCAttachment.Replace("../", ""));
                                }
                            }
                            formData.PLCAttachment = HPLCAttachment;
                        }
                        else
                        {
                            formData.PLCAttachment = "";
                        }
                        if (!string.IsNullOrEmpty(MassAttachment))
                        {
                            if (!string.IsNullOrEmpty(formData.MassAttachment))
                            {
                                var isExist = DoesFtpDirectoryExist(host + "/" + DomainName + "/" + formData.MassAttachment.Replace("../", ""));
                                if (isExist)
                                {
                                    DeleteFTPFile(host + "/" + DomainName + "/" + formData.MassAttachment.Replace("../", ""));
                                }
                            }
                            formData.MassAttachment = MassAttachment;
                        }
                        else
                        {
                            formData.MassAttachment = "";
                        }
                        if (!string.IsNullOrEmpty(IRAttachment))
                        {
                            if (!string.IsNullOrEmpty(formData.IRAttachment))
                            {
                                var isExist = DoesFtpDirectoryExist(host + "/" + DomainName + "/" + formData.IRAttachment.Replace("../", ""));
                                if (isExist)
                                {
                                    DeleteFTPFile(host + "/" + DomainName + "/" + formData.IRAttachment.Replace("../", ""));
                                }
                            }
                            formData.IRAttachment = IRAttachment;
                        }
                        else
                        {
                            formData.IRAttachment = "";
                        }
                        if (!string.IsNullOrEmpty(TGAAttachment))
                        {
                            if (!string.IsNullOrEmpty(formData.TGAAttachment))
                            {
                                var isExist = DoesFtpDirectoryExist(host + "/" + DomainName + "/" + formData.TGAAttachment.Replace("../", ""));
                                if (isExist)
                                {
                                    DeleteFTPFile(host + "/" + DomainName + "/" + formData.TGAAttachment.Replace("../", ""));
                                }
                            }
                            formData.TGAAttachment = TGAAttachment;
                        }
                        else
                        {
                            formData.TGAAttachment = "";
                        }
                        if (!string.IsNullOrEmpty(NMRAttchment))
                        {
                            if (!string.IsNullOrEmpty(formData.NMRAttchment))
                            {
                                var isExist = DoesFtpDirectoryExist(host + "/" + DomainName + "/" + formData.NMRAttchment.Replace("../", ""));
                                if (isExist)
                                {
                                    DeleteFTPFile(host + "/" + DomainName + "/" + formData.NMRAttchment.Replace("../", ""));
                                }
                            }
                            formData.NMRAttchment = NMRAttchment;
                        }
                        else
                        {
                            formData.NMRAttchment = "";
                        }
                        if (!string.IsNullOrEmpty(CMRAttchment))
                        {
                            if (!string.IsNullOrEmpty(formData.CMRAttchment))
                            {
                                var isExist = DoesFtpDirectoryExist(host + "/" + DomainName + "/" + formData.CMRAttchment.Replace("../", ""));
                                if (isExist)
                                {
                                    DeleteFTPFile(host + "/" + DomainName + "/" + formData.CMRAttchment.Replace("../", ""));
                                }
                            }
                            formData.CMRAttchment = CMRAttchment;
                        }
                        else
                        {
                            formData.CMRAttchment = "";
                        }

                        if (!string.IsNullOrEmpty(DEPTAttachment))
                        {
                            if (!string.IsNullOrEmpty(formData.DEPTAttachment))
                            {
                                var isExist = DoesFtpDirectoryExist(host + "/" + DomainName + "/" + formData.DEPTAttachment.Replace("../", ""));
                                if (isExist)
                                {
                                    DeleteFTPFile(host + "/" + DomainName + "/" + formData.DEPTAttachment.Replace("../", ""));
                                }
                            }
                            formData.DEPTAttachment = DEPTAttachment;
                        }
                        else
                        {
                            formData.DEPTAttachment = "";
                        }
                        if (!string.IsNullOrEmpty(HRMSAttchment))
                        {
                            if (!string.IsNullOrEmpty(formData.HRMSAttachment))
                            {
                                var isExist = DoesFtpDirectoryExist(host + "/" + DomainName + "/" + formData.HRMSAttachment.Replace("../", ""));
                                if (isExist)
                                {
                                    DeleteFTPFile(host + "/" + DomainName + "/" + formData.HRMSAttachment.Replace("../", ""));
                                }
                            }
                            formData.HRMSAttachment = HRMSAttchment;
                        }
                        else
                        {
                            formData.HRMSAttachment = "";
                        }
                        if (!string.IsNullOrEmpty(ElementralAttachment))
                        {
                            if (!string.IsNullOrEmpty(formData.ElementralAttachment))
                            {
                                var isExist = DoesFtpDirectoryExist(host + "/" + DomainName + "/" + formData.ElementralAttachment.Replace("../", ""));
                                if (isExist)
                                {
                                    DeleteFTPFile(host + "/" + DomainName + "/" + formData.ElementralAttachment.Replace("../", ""));
                                }
                            }
                            formData.ElementralAttachment = ElementralAttachment;
                        }
                        else
                        {
                            formData.ElementralAttachment = "";
                        }
                        if (!string.IsNullOrEmpty(ChiralAttachmenrt))
                        {
                            if (!string.IsNullOrEmpty(formData.ChiralAttachmenrt))
                            {
                                var isExist = DoesFtpDirectoryExist(host + "/" + DomainName + "/" + formData.ChiralAttachmenrt.Replace("../", ""));
                                if (isExist)
                                {
                                    DeleteFTPFile(host + "/" + DomainName + "/" + formData.ChiralAttachmenrt.Replace("../", ""));
                                }
                            }
                            formData.ChiralAttachmenrt = ChiralAttachmenrt;
                        }
                        else
                        {
                            formData.ChiralAttachmenrt = "";
                        }

                        formData.RbAdditionalAnalysis = additionalAnalysis;

                        using (var client = new WebApiClient())
                        {
                            var serializeModel = JsonConvert.SerializeObject(formData);// using Newtonsoft.Json;
                            var response = await client.PostJsonWithModelAsync<QuotationFormModel>(updateFormuri, serializeModel);
                            formData.Id = response.Id;
                        }
                    }
                }
            }

            Console.WriteLine(Environment.NewLine + "Process End");
        }

        public static bool CreateFolder()
        {
            string path = "/Index";
            bool IsCreated = true;
            try
            {
                WebRequest request = WebRequest.Create(host + path);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(UserId, Password);
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine(resp.StatusCode);
                }
            }
            catch (Exception ex)
            {
                IsCreated = false;
            }
            return IsCreated;
        }

        public static bool DoesFtpDirectoryExist(string dirPath)
        {
            bool isexist = false;

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirPath);
                request.Credentials = new NetworkCredential(UserId, Password);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    isexist = true;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        return false;
                    }
                }
            }
            return isexist;
        }

        public static void UploadFile(string From, string To)
        {
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(UserId, Password);
                client.UploadFile(To, WebRequestMethods.Ftp.UploadFile, From);
            }
        }

        public static List<string> GetAllFtpFiles(string ParentFolderpath)
        {
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ParentFolderpath);
                ftpRequest.Credentials = new NetworkCredential(UserId, Password);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());

                List<string> directories = new List<string>();

                string line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    var lineArr = line.Split('/');
                    line = lineArr[lineArr.Count() - 1];
                    directories.Add(line);
                    line = streamReader.ReadLine();
                }

                streamReader.Close();

                return directories;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteFTPFolder(string Folderpath)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Folderpath);
            request.Method = WebRequestMethods.Ftp.RemoveDirectory;
            request.Credentials = new System.Net.NetworkCredential(UserId, Password); ;
            request.GetResponse().Close();
        }

        public static void DeleteFTPFile(string Folderpath)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Folderpath);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new System.Net.NetworkCredential(UserId, Password); ;
            request.GetResponse().Close();
        }
    }
}
