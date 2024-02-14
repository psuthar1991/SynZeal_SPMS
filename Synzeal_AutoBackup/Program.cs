using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace Synzeal_AutoBackup
{
    class Program
    {

        static void Main(string[] args)
        {
            Backup();
        }

        public static void Backup()
        {
            DatabaseBackup();
            //SourceCodeBackup();
        }

        public static void DatabaseBackup()
        {
            string url = "http://spms.synzeal.com/form/APIDBBackUP";
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Headers.Add("Username", "xyz");
            webrequest.Headers.Add("Password", "abc");
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            string result = string.Empty;
            result = responseStream.ReadToEnd();
            responseStream.Close();
            webresponse.Close();

        }

        public static void SourceCodeBackup()
        {
            string startPath = @"C:\Inetpub\vhosts\synzeal.com\httpdocs";
            string zipPath = @"C:\MyDatabase\Sourcecode\synzeal_website_"+ DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_") + ".zip";
            ZipFile.CreateFromDirectory(startPath, zipPath);

        }
    }
}
