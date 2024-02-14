using System;
using System.Net;

namespace Synzeal_UpdateDaily
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WebClient wc = new WebClient();

            string inventoryjson = wc.DownloadString("http://spms.synzeal.com/form/updatedailysalessummary");

            //wc.DownloadString("http://spms.synzeal.com/form/updatedailyquoteapprovalsummary");

            wc.DownloadString("http://spms.synzeal.com/form/FetchCASNofromSAP");

            wc.DownloadString("http://spms.synzeal.com/form/FetchProjectNamefromSAP");

            wc.DownloadString("http://spms.synzeal.com/form/FetchCompanyMasterfromSAP");

            wc.DownloadString("http://spms.synzeal.com/form/UpdateQuoteAndPOValue");
        }
    }
}
