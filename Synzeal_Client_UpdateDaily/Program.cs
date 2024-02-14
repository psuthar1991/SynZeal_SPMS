using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Synzeal_Client_UpdateDaily
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new WebClient()) //WebClient  
            {
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString("http://spms.synzeal.com/Form/SendDailyClientUpdateEmail"); //URI  
                Console.WriteLine(Environment.NewLine + result);
            }
        }
    }
}
