using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class MyWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest lWebRequest = base.GetWebRequest(uri);
            lWebRequest.Timeout = 5600;
            ((HttpWebRequest)lWebRequest).ReadWriteTimeout = 5600;
            return lWebRequest;
        }
    }
}