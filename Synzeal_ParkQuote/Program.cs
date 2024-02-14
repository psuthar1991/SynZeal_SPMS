using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Synzeal_ParkQuote
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Park & User Dashboard Management App Started");
            Console.WriteLine(Environment.NewLine);
            using (var client = new WebClient()) //WebClient  
            {
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString("http://spms.synzeal.com/Form/ParkAllQuoteFromConsoleApplication"); //URI  
                Console.WriteLine(Environment.NewLine + result);
            }
            Console.WriteLine("Park Quote Done");
            using (var client = new WebClient()) //WebClient  
            {
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString("https://synzeal.com/api/RestAPI/UpdateProductImage"); //URI  
                Console.WriteLine(Environment.NewLine + result);
            }
            Console.WriteLine("Product Image Done");

            Console.WriteLine(Environment.NewLine);
            WebRequest request = WebRequest.Create("http://spms.synzeal.com/Form/SettleUserDashboard");
            request.Timeout = 240000;
            WebResponse myWebResponse = request.GetResponse();
            Console.WriteLine("User Dashboard Management Done");
            Console.WriteLine(Environment.NewLine);

            WebRequest requests = WebRequest.Create("http://spms.synzeal.com/Form/FetchRawMaterialfromSAP");
            requests.Timeout = 240000;
            WebResponse myWebResponses = requests.GetResponse();
            Console.WriteLine("User Dashboard Management Done");
            Console.WriteLine(Environment.NewLine);


            Console.WriteLine("Park & User Dashboard Management App Finish");
            
        }
    }
}
