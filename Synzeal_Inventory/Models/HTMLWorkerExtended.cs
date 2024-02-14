using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class HTMLWorkerExtended : HTMLWorker
    {
        LineSeparator line = new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER,0);
        DottedLineSeparator dottedline = new DottedLineSeparator();
        public HTMLWorkerExtended(IDocListener document) : base(document)
        {

        }
        public override void StartElement(string tag, IDictionary<string, string> str)
        {
            if (tag.Equals("hrline"))
                document.Add(new Chunk(line));
            else
                base.StartElement(tag, str);
        }
    }
}