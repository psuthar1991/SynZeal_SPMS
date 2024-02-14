using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Synzeal_FTP_Sync
{
    class MyFtpClient
    {
        protected string FtpUser { get; set; }
        protected string FtpPass { get; set; }
        protected string FtpServerUrl { get; set; }
        protected string DirPathToUpload { get; set; }
        protected string BaseDirectory { get; set; }
        protected DateTime LastSyncTime { get; set; }
        public MyFtpClient(string ftpuser, string ftppass, string ftpserverurl, string dirpathtoupload)
        {
            this.FtpPass = ftppass;
            this.FtpUser = ftpuser;
            this.FtpServerUrl = ftpserverurl;
            this.DirPathToUpload = dirpathtoupload;
            var spllitedpath = dirpathtoupload.Split('\\').ToArray();
            // last index must be the "base" directory on the server
            this.BaseDirectory = spllitedpath[spllitedpath.Length - 1];
        }


        public void UploadDirectory()
        {
            // rename the old folder version (if exist)
            //     RenameDir(BaseDirectory);
            // create a parent folder on server
            CreateDir(BaseDirectory);

            //this.LastSyncTime = 
            if (!File.Exists(this.DirPathToUpload + @"\syncdate.txt"))
            {
                File.Create(this.DirPathToUpload + @"\syncdate.txt");

                int days = Convert.ToInt32(ConfigurationManager.AppSettings["days"].ToString());

                File.SetLastWriteTime(this.DirPathToUpload + @"\syncdate.txt", DateTime.Now.AddDays(-days));
                this.LastSyncTime = File.GetLastWriteTime(this.DirPathToUpload + @"\syncdate.txt");

                this.LastSyncTime = DateTime.Now.AddDays(-days);
            }
            else
            {
                this.LastSyncTime = File.GetLastWriteTime(this.DirPathToUpload + @"\syncdate.txt").AddMinutes(-5);
                File.SetLastWriteTime(this.DirPathToUpload + @"\syncdate.txt", DateTime.Now);
            }
            // upload the files in the most external directory of the path
            UploadAllFolderFiles(DirPathToUpload, BaseDirectory);
            // loop trough all files in subdirectories
            
            foreach (string dirPath in Directory.GetDirectories(DirPathToUpload, "*",
            SearchOption.AllDirectories))
            {
                // create the folder
                CreateDir(dirPath.Substring(dirPath.IndexOf(BaseDirectory), dirPath.Length - dirPath.IndexOf(BaseDirectory)));

                Console.WriteLine(dirPath.Substring(dirPath.IndexOf(BaseDirectory), dirPath.Length - dirPath.IndexOf(BaseDirectory)));
                UploadAllFolderFiles(dirPath, dirPath.Substring(dirPath.IndexOf(BaseDirectory), dirPath.Length - dirPath.IndexOf(BaseDirectory)));
            };
            
            File.SetLastWriteTime(this.DirPathToUpload + @"\syncdate.txt", DateTime.Now.AddMinutes(-5));
        }
        public IEnumerable<string> GetFilesInFtpDirectory(string url, string username, string password)
        {
            // Get the object used to communicate with the server.
            var request = (FtpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = new NetworkCredential(username, password);

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    var reader = new StreamReader(responseStream);
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line) == false)
                        {
                            yield return line.Split(new[] { ' ', '\t' }).Last();
                        }
                    }
                }
            }
        }

        public bool IsDateBeforeOrToday(string input)
        {
            DateTime inputTime;
            var parseResult = DateTime.TryParse(input,out inputTime);
            if (!parseResult)
            {
                return false;
            }
            return this.LastSyncTime.Date <= inputTime;
        }

        private void UploadAllFolderFiles(string localpath, string remotepath)
        {
            string[] files = Directory.GetFiles(localpath);
            List<string> comparedFileName = new List<string>();
            // get only the filenames and concat to remote path
            foreach (string file in files)
            {
                comparedFileName.Add(file.Replace(localpath + "\\", ""));
                DateTime comparisiontime = new DateTime();
                DateTime creation = File.GetCreationTime(file);
                DateTime modification = File.GetLastWriteTimeUtc(file);
                // full remote path
                var fullremotepath = remotepath + "\\" + Path.GetFileName(file);

                if (creation > modification)
                {
                    comparisiontime = creation;
                }
                else
                {
                    comparisiontime = modification;
                }
                comparisiontime = comparisiontime.AddHours(-6);

                var check = IsDateBeforeOrToday(comparisiontime.ToShortDateString());
                
                if (check)
                {

                    //local path
                    var fulllocalpath = Path.GetFullPath(file);
                    //upload to server
                    Upload(fulllocalpath, fullremotepath);

                    Console.WriteLine("Uploaded File : " + fulllocalpath);
                }
            }

            //IEnumerable<string> remoteFiles = GetFilesInFtpDirectory("ftp://" + FtpServerUrl + "/" + remotepath, FtpUser, FtpPass);
            //var remotFileString = remoteFiles.ToList();
            //IEnumerable<string> difference = remotFileString.Except(comparedFileName);
            //if (difference.Any()) {
            //    foreach(var diff in difference)
            //    {
            //        DeleteFile("ftp://" + FtpServerUrl + "/" + remotepath + "/"+diff);

            //        Console.WriteLine("Deleted File : " + remotepath + "/" + diff);
            //    }
            //}

        }

        public bool CreateDir(string dirname)
        {
            try
            {
                WebRequest request = WebRequest.Create("ftp://" + FtpServerUrl + "/" + dirname);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Proxy = new WebProxy();
                request.Credentials = new NetworkCredential(FtpUser, FtpPass);
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    if (resp.StatusCode == FtpStatusCode.PathnameCreated)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            catch
            {
                return false;
            }
        }

        public void Upload(string filepath, string targetpath)
        {
            if (filepath.Contains("syncdate.txt"))
            {
                return;
            }

            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(FtpUser, FtpPass);
                client.Proxy = null;
                var fixedpath = targetpath.Replace(@"\", "/");
                client.UploadFile("ftp://" + FtpServerUrl + "/" + fixedpath, WebRequestMethods.Ftp.UploadFile, filepath);
            }
        }

        public bool RenameDir(string dirname)
        {
            var path = "ftp://" + FtpServerUrl + "/" + dirname;
            string serverUri = path;

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
                request.Method = WebRequestMethods.Ftp.Rename;
                request.Proxy = null;
                request.Credentials = new NetworkCredential(FtpUser, FtpPass);
                // change the name of the old folder the old folder
                request.RenameTo = DateTime.Now.ToString("yyyyMMddHHmmss");
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    if (resp.StatusCode == FtpStatusCode.FileActionOK)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }


        private string DeleteFile(string fileName)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(fileName);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(FtpUser, FtpPass);

            try
            {
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    return response.StatusDescription;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
