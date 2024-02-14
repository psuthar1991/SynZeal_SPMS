using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class ITextEvents : PdfPageEventHelper
    {
        string _apiname = "";

        public ITextEvents(string APIname)
        {
            _apiname = APIname;
        }


        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;

        #region Fields
        private string _header;
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(110, 130);
                footerTemplate = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {
            }
            catch (System.IO.IOException ioe)
            {
            }
        }
        private iTextSharp.text.Font fontTinyItalic = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        private iTextSharp.text.Font fontbold = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        private iTextSharp.text.Font fontTinyItalicColor = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.html.WebColors.GetRGBColor("#125c88"));
        private iTextSharp.text.Font fontboldColor = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.html.WebColors.GetRGBColor("#125c88"));

        private iTextSharp.text.Font fontTinyBold = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);


        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);
            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font basewhiteFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE);
            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            Phrase p1Header = new Phrase("Sample Header Here", baseFontNormal);

            //Create PdfTable object
            PdfPTable pdfTab = new PdfPTable(3);
            pdfTab.WidthPercentage = 100;
            //We will have to create separate cells to include image logo and 2 separate strings
            //Row 1
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(new Uri("http://spms.synzeal.com/img/szQualitylogo.png"));
            jpg.ScaleToFit(120f, 155.25f);
            PdfPCell imageCell = new PdfPCell(jpg);
            imageCell.Colspan = 2; // either 1 if you need to insert one cell
            imageCell.Border = 0;
            imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
            imageCell.PaddingRight = 20f;

            PdfPCell pdfCell1 = new PdfPCell();
            PdfPCell pdfCell2 = new PdfPCell();
            PdfPCell pdfCell3 = new PdfPCell(imageCell);
            pdfCell3.PaddingBottom = 5f;
            String text = "Page " + writer.PageNumber + " of ";
            if(cb == null)
            {
                cb = writer.DirectContent;
            }
            if(bf == null)
            {
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            }
            if (headerTemplate == null)
            {
                headerTemplate = cb.CreateTemplate(100, 100);
            }
            if (footerTemplate == null)
            {
                footerTemplate = cb.CreateTemplate(50, 50);
            }

            ////Add paging to header
            //{
            //    cb.BeginText();
            //    cb.SetFontAndSize(bf, 12);
            //    cb.SetTextMatrix(document.PageSize.GetRight(20), document.PageSize.GetTop(40));
            //    cb.ShowText(text);
            //    cb.EndText();
            //    float len = bf.GetWidthPoint(text, 12);
            //    cb.AddTemplate(headerTemplate, document.PageSize.GetRight(20) + len, document.PageSize.GetTop(30));
            //}
            //Add paging to footer
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 12);
                cb.SetTextMatrix(document.PageSize.GetRight(180), document.PageSize.GetBottom(20));
                cb.ShowText("www.synzeal.com");
                cb.EndText();
                float len = bf.GetWidthPoint(text, 12);
                cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) + len, document.PageSize.GetBottom(20));
            }

            //Row 2
            PdfPCell pdfCell10 = new PdfPCell();
            pdfCell10.Colspan = 3;
            PdfPCell pdfCell4 = new PdfPCell(new Phrase(_apiname, basewhiteFontNormal));

            //Row 3 
            PdfPCell pdfCell5 = new PdfPCell(new Phrase("Date:" + PrintTime.ToShortDateString(), baseFontBig));
            PdfPCell pdfCell6 = new PdfPCell();
            PdfPCell pdfCell7 = new PdfPCell(new Phrase("TIME:" + string.Format("{0:t}", DateTime.Now), baseFontBig));

            
            //set the alignment of all three cells and set border to 0
            pdfCell1.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell2.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell3.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell4.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell5.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell6.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell7.HorizontalAlignment = Element.ALIGN_CENTER;

            pdfCell2.VerticalAlignment = Element.ALIGN_BOTTOM;
            pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell4.VerticalAlignment = Element.ALIGN_TOP;
            pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell7.VerticalAlignment = Element.ALIGN_MIDDLE;

            pdfCell4.Colspan = 3;
            pdfCell4.BackgroundColor = WebColors.GetRGBColor("#125c88");
            pdfCell4.PaddingTop = 5f;
            pdfCell4.PaddingBottom = 5f;

            pdfCell5.Colspan = 3;
            pdfCell1.Border = 0;
            pdfCell2.Border = 0;
            pdfCell3.Border = 0;
            pdfCell4.Border = 0;
            pdfCell5.Border = 0;
            pdfCell6.Border = 0;
            pdfCell7.Border = 0;

            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 97;
            
            float[] widths = new float[] { 1f, 10f, 5f, 4f, 4f };
            table.SetWidths(widths);
            table.HorizontalAlignment = 1;

            PdfPCell cell1 = new PdfPCell();
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell1.Phrase = new Phrase("#", fontTinyBold);
            table.AddCell(cell1);

            PdfPCell cell2 = new PdfPCell();
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2.Phrase = new Phrase("Product Details", fontTinyBold);
            table.AddCell(cell2);

            PdfPCell cell3 = new PdfPCell();
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            cell3.Phrase = new Phrase("Structure", fontTinyBold);
            cell3.PaddingTop = 5;
            cell3.PaddingBottom = 5;
            table.AddCell(cell3);

            PdfPCell cell4 = new PdfPCell();
            cell4.HorizontalAlignment = Element.ALIGN_CENTER;
            cell4.Phrase = new Phrase("CAS No.", fontTinyBold);
            table.AddCell(cell4);

            PdfPCell cell5 = new PdfPCell();
            cell5.HorizontalAlignment = Element.ALIGN_CENTER;
            cell5.Phrase = new Phrase("CAT No.", fontTinyBold);
            table.AddCell(cell5);

            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell2);
            pdfTab.AddCell(pdfCell3);
            pdfTab.AddCell(pdfCell10);
            //pdfTab.AddCell(pdfCell4);


            pdfCell6.Colspan = 3;
            pdfCell6.PaddingTop = 10;
            pdfCell6.PaddingBottom = 10;
            pdfTab.AddCell(pdfCell6);

            pdfCell5.AddElement(table);

            pdfTab.AddCell(pdfCell5);
            //pdfTab.AddCell(pdfCell6);
            //pdfTab.AddCell(pdfCell7);

            pdfTab.TotalWidth = document.PageSize.Width;
            pdfTab.WidthPercentage = 50;
            //pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;    

            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            pdfTab.WriteSelectedRows(0, -1, 0, document.PageSize.Height - 14, writer.DirectContent);
            //set pdfContent value

            ////Move the pointer and draw line to separate header section from rest of page
            //cb.MoveTo(40, document.PageSize.Height - 100);
            //cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 100);
            //cb.Stroke();

            ////Move the pointer and draw line to separate footer section from rest of page
            //cb.MoveTo(40, document.PageSize.GetBottom(50));
            //cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(50));
            //cb.Stroke();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
            if (cb == null)
            {
                cb = writer.DirectContent;
            }
            if (bf == null)
            {
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            }
            if (headerTemplate == null)
            {
                headerTemplate = cb.CreateTemplate(100, 100);
            }
            if (footerTemplate == null)
            {
                footerTemplate = cb.CreateTemplate(50, 50);
            }
            headerTemplate.BeginText();
            headerTemplate.SetFontAndSize(bf, 12);
            headerTemplate.SetTextMatrix(0, 0);
            headerTemplate.ShowText("");
            headerTemplate.EndText();

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 12);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText("");
            footerTemplate.EndText();
        }
    }
}