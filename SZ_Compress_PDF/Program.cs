using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZ_Compress_PDF
{
    class Program
    {
        static void Main(string[] args)
        {
            // Type your username and press enter
            Console.WriteLine("Enter Folder Path:");

            // Create a string variable and get user input from the keyboard and store it in the variable
            string folderpath = Console.ReadLine();


            string[] pdfFiles = Directory.GetFiles(folderpath, "*.pdf")
                                     .Select(Path.GetFileName)
                                     .ToArray();

            foreach (var file in pdfFiles)
            {
                iTextSharp.text.Document myDocument = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 0, 0, 0, 0);
                PdfReader reader = new PdfReader(folderpath + "\\" + file);
                var totalPages = reader.NumberOfPages + 1;
                for (int i = 1; i < totalPages; i++)
                {
                    reader.SetPageContent(i, reader.GetPageContent(i), PdfStream.BEST_COMPRESSION, true);
                }
                string newfilename = Path.GetFileName(file) + "_compress" + Path.GetExtension(file);
                PdfStamper stamper = new PdfStamper(reader, new FileStream(folderpath + "\\" + newfilename, FileMode.Create), PdfWriter.VERSION_1_5);
                stamper.FormFlattening = true;
                stamper.SetFullCompression();
                stamper.Close();
            }
        }
    }
}
