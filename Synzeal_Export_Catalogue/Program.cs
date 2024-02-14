using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.events;
using Synzeal_Export_Catalogue.Entity;
using Synzeal_Export_Catalogue.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Synzeal_Export_Catalogue
{
    class Program
    {
        public static SynzealEntities db = new SynzealEntities();
        public static int TotalPageCount(string file)
        {
            using (StreamReader sr = new StreamReader(System.IO.File.OpenRead(file)))
            {
                Regex regex = new Regex(@"/Type\s*/Page[^s]");
                MatchCollection matches = regex.Matches(sr.ReadToEnd());

                return matches.Count;
            }
        }
        public static void Main(string[] args)
        {
            string specdatafilename = Guid.NewGuid().ToString() + ".pdf";
            string outputPdfPath = System.IO.Path.GetDirectoryName(@"D:\Plesk\Vhosts\synzeal.com\httpdocs\Console Apllication\ExportCatalogue\PDF\");
            //string outputPdfPath = System.IO.Path.GetDirectoryName(@"F:\Working\Synzeal_Quotation\Synzeal_Export_Catalogue\Datas\");
            var output = new FileStream(outputPdfPath + "\\" + specdatafilename, FileMode.Create);
            PdfReader reader = null;
            Document sourceDocument = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage;
            sourceDocument = new Document();
            pdfCopyProvider = new PdfCopy(sourceDocument, output);

            //output file Open  
            sourceDocument.Open();

            //List<string> fileArray = new List<string>();
            //fileArray.Add(outputPdfPath + "\\allproducts-header.pdf");
            //fileArray.Add(outputPdfPath + "\\allproducts.pdf");

            //foreach (var item in fileArray)
            //{
            //    var fPath = item.Replace("..", "~");
            //    int pages = TotalPageCount(fPath);

            //    reader = new PdfReader(fPath);
            //    //Add pages in new file  
            //    for (int i = 1; i <= pages; i++)
            //    {
            //        try
            //        {
            //            importedPage = pdfCopyProvider.GetImportedPage(reader, i);
            //            pdfCopyProvider.AddPage(importedPage);
            //        }
            //        catch (Exception ex)
            //        {
            //            continue;
            //        }
            //    }
            //    reader.Close();
            //}
            //sourceDocument.Close();



            var productData = db.Products.Where(x => x.Deleted == false && x.Published == true).AsNoTracking().AsQueryable();

            Console.WriteLine("Enter Category Name:");
            string categoryName = Console.ReadLine();

            Console.WriteLine("Is D Labelled:");
            string dlabelled = Console.ReadLine();

            Console.WriteLine("Is Nitroso:");
            string nitroso = Console.ReadLine();

            Console.WriteLine("Is Peptide:");
            string ispeptide = Console.ReadLine();

            Console.WriteLine("Is Load Image:");
            string isloadimage = Console.ReadLine();
            bool isLoadImage = isloadimage == "true" ? true : false;
            if (categoryName != "all")
            {
                var catId = db.Categories.Where(x => x.Name.Trim().ToLower() == categoryName.Trim().ToLower() && x.Published == true && x.Deleted == false).Select(x => x.Id).FirstOrDefault();
                productData = productData.Where(x => x.MainCatId.HasValue && catId == x.MainCatId.Value).AsQueryable();
            }
            if (dlabelled == "true")
            {
                productData = productData.Where(x => x.Sku != null && x.Sku.Substring(5, (x.Sku.Length - 5)).ToLower().Contains("d")).AsQueryable();
            }
            if (nitroso == "true")
            {
                productData = productData.Where(x => x.Name.Trim().ToLower().Contains("nitroso")).AsQueryable();
            }
            if (ispeptide == "true")
            {
                productData = productData.Where(x => x.MainCatName.Trim().ToLower().Contains("bivalirudin")
                                                || x.MainCatName.Trim().ToLower().Contains("bleomycin")
                                                || x.MainCatName.Trim().ToLower().Contains("buserelin")
                                                || x.MainCatName.Trim().ToLower().Contains("calcitonin salmon")
                                                || x.MainCatName.Trim().ToLower().Contains("carbetocin")
                                                || x.MainCatName.Trim().ToLower().Contains("caspofungin")
                                                || x.MainCatName.Trim().ToLower().Contains("cetrorelix")
                                                || x.MainCatName.Trim().ToLower().Contains("dalargin")
                                                || x.MainCatName.Trim().ToLower().Contains("desmopressin")
                                                || x.MainCatName.Trim().ToLower().Contains("etelcacetide")
                                                || x.MainCatName.Trim().ToLower().Contains("ganirelix")
                                                || x.MainCatName.Trim().ToLower().Contains("goserelin")
                                                || x.MainCatName.Trim().ToLower().Contains("gramicidin")
                                                || x.MainCatName.Trim().ToLower().Contains("lanreotide")
                                                || x.MainCatName.Trim().ToLower().Contains("leuprolide")
                                                || x.MainCatName.Trim().ToLower().Contains("linaclotide")
                                                || x.MainCatName.Trim().ToLower().Contains("lypressin")
                                                || x.MainCatName.Trim().ToLower().Contains("octreotide")
                                                || x.MainCatName.Trim().ToLower().Contains("oxytocin")
                                                || x.MainCatName.Trim().ToLower().Contains("pentetreotide")
                                                || x.MainCatName.Trim().ToLower().Contains("plecanatide")
                                                || x.MainCatName.Trim().ToLower().Contains("polymyxin b")
                                                || x.MainCatName.Trim().ToLower().Contains("semaglutide")
                                                || x.MainCatName.Trim().ToLower().Contains("teduglutide")
                                                || x.MainCatName.Trim().ToLower().Contains("terlipressin")
                                                || x.MainCatName.Trim().ToLower().Contains("triptorelin")).AsQueryable();

            }
            string returnFilePath = string.Empty;
            var products = new List<Model.ExportCatalogueModel>();
            var pdatas = productData.AsEnumerable();
            var pItemData = productData.Where(x => x.Published == true && x.Deleted == false && !string.IsNullOrEmpty(x.Sku)).AsEnumerable();
            if (pItemData != null && pItemData.Count() > 0)
            {
                foreach (var pitem in pItemData)
                {
                    Model.ExportCatalogueModel objModel = new Model.ExportCatalogueModel();
                    objModel.CategoryName = pitem.MainCatName;
                    objModel.ProductName = pitem.Name;
                    objModel.InventoryStatus = pitem.ProductInstockStatus;
                    objModel.MolF = pitem.Gtin;
                    objModel.MolW = pitem.MolecularWeight;
                    objModel.Synonym = pitem.Synonym;
                    objModel.ImagePath = pitem.ImagePath;
                    objModel.CATNo = pitem.Sku;
                    objModel.CASNo = pitem.ManufacturerPartNumber;
                    products.Add(objModel);
                }
            }

            returnFilePath = CreateExportCataloguePDF(products, isLoadImage);
            Console.WriteLine("PDF Path: " + returnFilePath);
            Console.ReadLine();
            Console.ReadKey();

        }

        private static iTextSharp.text.Font fontTinyItalic = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        private static iTextSharp.text.Font fontbold = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        private static iTextSharp.text.Font fontTinyItalicColor = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.html.WebColors.GetRGBColor("#125c88"));
        private static iTextSharp.text.Font fontboldColor = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.html.WebColors.GetRGBColor("#125c88"));

        private static iTextSharp.text.Font fontTinyBold = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        private static string CreateExportCataloguePDF(List<Model.ExportCatalogueModel> products, bool isLoadImage)
        {
            string fileName = string.Empty;
            DateTime fileCreationDatetime = DateTime.Now;
            fileName = string.Format("{0}.pdf", "catalogue_" + fileCreationDatetime.ToString(@"yyyyMMdd") + "_" + fileCreationDatetime.ToString(@"HHmmss"));
            string pdfPath = System.IO.Path.GetDirectoryName(@"D:\Plesk\Vhosts\synzeal.com\httpdocs\Console Apllication\ExportCatalogue\PDF");
            //string pdfPath = System.IO.Path.GetDirectoryName(@"F:\Working\Synzeal_Quotation\Synzeal_Export_Catalogue\Datas\");
            pdfPath = pdfPath + "\\" + fileName;
            string returnpath = pdfPath;

            using (FileStream msReport = new FileStream(pdfPath, FileMode.Create))
            {
                //step 1
                using (Document pdfDoc = new Document(PageSize.A4, 0f, 0f, 78f, 25f))
                {
                    try
                    {
                        // step 2

                        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, msReport);
                        pdfWriter.PageEvent = new ITextEvents(products.Select(x => x.CategoryName).FirstOrDefault(), isLoadImage);

                        //open the stream 
                        pdfDoc.Open();
                        pdfDoc.SetMargins(0, 0, 78f, 25f);
                        int loop = 1;

                        if (products != null && products.Count > 0)
                        {
                            var categorynameList = products.Select(x => x.CategoryName).Distinct().OrderBy(x => x).ToList();
                            var categoryCount = categorynameList.Count;
                            var pageloopcount = 1;
                            var prevcategoryname = categorynameList.FirstOrDefault();

                            pageloopcount += 1;

                            PdfPTable table = new PdfPTable(5);

                            float[] widths = new float[] { 1f, 10f, 5f, 4f, 4f };
                            if (!isLoadImage)
                            {
                                table = new PdfPTable(4);
                                widths = new float[] { 1f, 15f, 4f, 4f };
                            }
                            table.WidthPercentage = 96;
                            table.SetWidths(widths);
                            table.HorizontalAlignment = 1;
                            Console.WriteLine("Total Count: " + products.Count);
                            int loopcnt = 1;
                            foreach (var item in products.OrderBy(x => x.CATNo.Substring(3)).ToList())
                            {
                                Console.WriteLine("Number: " + loopcnt + " / SKU: " + item.CATNo);
                                loopcnt += 1;
                                PdfPCell cell6 = new PdfPCell();
                                cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                                cell6.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell6.Phrase = new Phrase(loop.ToString(), fontTinyItalic);
                                table.AddCell(cell6);

                                Phrase protextphase = new Phrase();
                                protextphase.Add(new Chunk(item.ProductName, fontbold));
                                protextphase.Add(new Chunk("\n", fontTinyItalic));
                                if (!string.IsNullOrEmpty(item.Synonym))
                                {
                                    protextphase.Add(new Chunk("Synonym: ", fontbold));
                                    protextphase.Add(new Chunk(item.Synonym, fontTinyItalic));
                                    protextphase.Add(new Chunk("\n", fontTinyItalic));
                                }

                                protextphase.Add(new Chunk("Mol.F.: ", fontbold));
                                protextphase.Add(new Chunk(item.MolF, fontTinyItalic));
                                protextphase.Add(new Chunk(", Mol.Wt.: ", fontbold));
                                protextphase.Add(new Chunk(item.MolW, fontTinyItalic));
                                protextphase.Add(new Chunk("\n", fontTinyItalic));
                                protextphase.Add(new Chunk("Inventory Status: ", fontboldColor));
                                protextphase.Add(new Chunk(item.InventoryStatus, fontTinyItalicColor));

                                PdfPCell cellpro = new PdfPCell();
                                cellpro.HorizontalAlignment = Element.ALIGN_LEFT;
                                cellpro.SetLeading(1f, 2f);
                                cellpro.Phrase = protextphase;
                                table.AddCell(cellpro);

                                if (isLoadImage)
                                {
                                    if (!string.IsNullOrEmpty(item.ImagePath))
                                    {
                                        try
                                        {
                                            string destpdfPath = System.IO.Path.GetDirectoryName(@"D:\Plesk\Vhosts\synzeal.com\httpdocs\Console Apllication\ExportCatalogue\PDF\");
                                            //string destpdfPath = System.IO.Path.GetDirectoryName(@"F:\Working\Synzeal_Quotation\Synzeal_Export_Catalogue\Datas\");

                                            string destpath = System.IO.Path.GetDirectoryName(destpdfPath);

                                            //var ipath = CompressImage(item.ImagePath.Replace(".png","_500.png"), destpath, 100);
                                            var ipath = item.ImagePath.Replace(".png", "_500.png");
                                            iTextSharp.text.Image jpg = iTextSharp.text.pdf.codec.PngImage.GetImage(ipath);
                                            jpg.ScaleToFit(120f, 155.25f);
                                            PdfPCell imageCell = new PdfPCell(jpg, true);
                                            imageCell.Colspan = 1; // either 1 if you need to insert one cell
                                            imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                            imageCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                            imageCell.PaddingRight = 20f;
                                            table.AddCell(imageCell);

                                            //PdfPCell cellimage = new PdfPCell();
                                            ////var ipath = item.ImagePath.Replace("https://www.synzeal.com/", "D:/Plesk/Vhosts/synzeal.com/httpdocs/");
                                            ////   iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(ipath);
                                            //iTextSharp.text.Image jpg = iTextSharp.text.pdf.codec.PngImage.GetImage(item.ImagePath);
                                            //jpg.ScaleToFit(120f, 155.25f);

                                            ////var byteimage = Common.ImageToByteArray(System.Drawing.Image.FromFile(ipath));
                                            ////iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(byteimage);
                                            ////jpg.ScaleToFit(120f, 155.25f);

                                            //PdfPCell imageCell = new PdfPCell(jpg, true);
                                            //imageCell.Colspan = 1; // either 1 if you need to insert one cell
                                            //imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                            //imageCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                            //imageCell.PaddingRight = 20f;
                                            //table.AddCell(imageCell);

                                            ////PdfPCell cellimage = new PdfPCell();
                                            ////iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(new Uri(item.ImagePath));
                                            ////jpg.ScaleToFit(120f, 155.25f);
                                            ////PdfPCell imageCell = new PdfPCell(jpg);
                                            ////imageCell.Colspan = 1; // either 1 if you need to insert one cell
                                            ////imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                            ////imageCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                            ////imageCell.PaddingRight = 20f;
                                            ////table.AddCell(imageCell);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("Image Error: " + ex.Message + " / Stack: " + ex.StackTrace);
                                            PdfPCell cell22 = new PdfPCell();
                                            cell22.HorizontalAlignment = Element.ALIGN_CENTER;
                                            cell22.VerticalAlignment = Element.ALIGN_MIDDLE;
                                            cell22.Phrase = new Phrase("", fontTinyItalic);
                                            table.AddCell(cell22);
                                        }
                                        finally
                                        {

                                        }
                                    }
                                    else
                                    {
                                        table.AddCell(new PdfPCell());
                                    }
                                }
                                else
                                {
                                    //Phrase proimagetextphase = new Phrase();
                                    //proimagetextphase.Add(new Chunk(item.ImagePath, fontbold));
                                    //PdfPCell cellproimg = new PdfPCell();
                                    //cellproimg.HorizontalAlignment = Element.ALIGN_LEFT;
                                    //cellproimg.SetLeading(1f, 2f);
                                    //cellproimg.Phrase = proimagetextphase;
                                    //table.AddCell(cellproimg);
                                }
                                PdfPCell cell7 = new PdfPCell();
                                cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                                cell7.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell7.Phrase = new Phrase(item.CASNo, fontTinyItalic);
                                table.AddCell(cell7);

                                PdfPCell cell8 = new PdfPCell();
                                cell8.HorizontalAlignment = Element.ALIGN_CENTER;
                                cell8.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell8.Phrase = new Phrase(item.CATNo, fontTinyItalic);
                                table.AddCell(cell8);

                                loop += 1;
                            }
                            pdfDoc.Add(table);
                        }

                        pdfDoc.Close();
                    }
                    catch (Exception ex)
                    {
                        //handle exception
                        Console.WriteLine("Error: " + ex.Message + " / Stack: " + ex.StackTrace);
                        Console.ReadKey();

                    }
                    finally
                    {
                    }
                }
            }

            return returnpath;
        }


        public static string CompressImage(string SoucePath, string DestPath, int quality)
        {
            var FileName = Path.GetFileName(SoucePath);
            DestPath = DestPath + "\\PDF" + "\\" + FileName;

            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData(SoucePath);
            MemoryStream ms = new MemoryStream(bytes);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

            using (Bitmap bmp1 = new Bitmap(img))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Png);

                System.Drawing.Imaging.Encoder QualityEncoder = System.Drawing.Imaging.Encoder.Quality;

                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(QualityEncoder, quality);

                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(DestPath, jpgEncoder, myEncoderParameters);
            }

            return DestPath;
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
