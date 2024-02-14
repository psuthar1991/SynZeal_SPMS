using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace Synzeal_FTP_Sync
{
    class Program
    {
        static void Main(string[] args)
        {
             StartSyncing();
            
        }

        public static void StartSyncing()
        {
            string sourceDirectory = ConfigurationManager.AppSettings["sourcedir"].ToString();
            //string targetDirectory = @"\\Test_Zenith";

            string ftpUser = ConfigurationManager.AppSettings["ftpUser"].ToString();
            string ftpPassword = ConfigurationManager.AppSettings["ftpPassword"].ToString();
            string ftpServer = ConfigurationManager.AppSettings["ftpServer"].ToString();

            MyFtpClient ftp = new MyFtpClient(ftpUser, ftpPassword, ftpServer, sourceDirectory);
            ftp.UploadDirectory();

            Environment.Exit(0);
        }

        private static void OperateOnSourceFiles(string source, string targetDir)
        {
            //Processes current source folder files
            foreach (var file in Directory.GetFiles(source))
            {
                OverWrite(targetDir, file);
            }

            //Recursively processes files in source subfolders
            List<string> subfolders = Directory.GetDirectories(source).ToList();
            foreach (var subfolder in subfolders)
            {
                OperateOnSourceFiles(subfolder, targetDir);
            }
        }
        private static void OverWrite(string target, string sourcefile)
        {
            //Grab file name
            var strSrcFile = sourcefile.Split(Path.DirectorySeparatorChar).Last();

            //Search current target directory FILES, and copy only if same file name
            List<string> targetfiles = Directory.GetFiles(target).Select(file => file.Split(Path.DirectorySeparatorChar).Last()).ToList();
            if (targetfiles.Contains(strSrcFile))
            {
               // File.Copy(sourcefile, Path.Combine(target, Path.GetFileName(strSrcFile)), true);

            }

            //Recursively search current target directory SUBFOLDERS if any
            List<string> subfolders = Directory.GetDirectories(target).ToList();
            foreach (var subfolder in subfolders)
            {
                OverWrite(subfolder, sourcefile);
            }
        }
    }
}
