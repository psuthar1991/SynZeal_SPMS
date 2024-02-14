using System;
using System.Net;
namespace Synzeal_PurchaseEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new WebClient()) //WebClient  
            {
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString("http://spms.synzeal.com/Form/SendDailyPurchaseEmail"); //URI  
                Console.WriteLine(Environment.NewLine + result);
            }
        }
    }
}
