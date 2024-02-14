using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using MailChimp.Net.Models;
using Microsoft.AspNet.SignalR.Hosting;

namespace Synzeal_Inventory.Models
{
    public class Aws
    {
        public static string bucketname = "synzeal-quotation";
        public static string accessKey = "AKIAUF5L37FJMVXP266U";
        public static string secretKey = "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";
        public static AmazonS3Config config = new AmazonS3Config();
        public static async Task<bool> CreateBucket(string bucketName)
        {
            try
            {

                config.ServiceURL = "http://localhost:1360/";
                var credentials = new BasicAWSCredentials(accessKey, secretKey);

                AmazonS3Client client = new AmazonS3Client(credentials, RegionEndpoint.APSouth1);
                var request = new PutBucketRequest
                {
                    BucketName = bucketName,
                    UseClientRegion = true,
                };

                var response = await client.PutBucketAsync(request);
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public static async Task<bool> UploadFileasync(string filePath, string fileName)
        {
            try
            {

                config.ServiceURL = "http://localhost:1360/";
                var credentials = new BasicAWSCredentials(accessKey, secretKey);

                AmazonS3Client client = new AmazonS3Client(credentials, RegionEndpoint.APSouth1);

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    FilePath = filePath,
                    Key = fileName,
                    BucketName = "synzeal-quotation",
                    CannedACL = S3CannedACL.PublicRead
                };

                var fileTransferUtility = new TransferUtility(client);
                await fileTransferUtility.UploadAsync(uploadRequest);

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return false;
        }

        public static bool UploadFile(string filePath, string fileName)
        {
            try
            {

                config.ServiceURL = "http://localhost:1360/";
                var credentials = new BasicAWSCredentials(accessKey, secretKey);

                AmazonS3Client client = new AmazonS3Client(credentials, RegionEndpoint.APSouth1);

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    FilePath = filePath,
                    Key = fileName,
                    BucketName = "synzeal-quotation",
                    CannedACL = S3CannedACL.PublicRead
                };

                var fileTransferUtility = new TransferUtility(client);
                fileTransferUtility.Upload(uploadRequest);

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return false;
        }

        public static bool Upload(string fileName, Stream inputStream)
        {

            try
            {
                config.ServiceURL = "http://localhost:1360/";
                var credentials = new BasicAWSCredentials(accessKey, secretKey);

                AmazonS3Client client = new AmazonS3Client(credentials, RegionEndpoint.APSouth1);
                var request = new PutObjectRequest
                {
                    Key = fileName,
                    InputStream = inputStream,
                    BucketName = bucketname,
                    CannedACL = S3CannedACL.PublicRead
                };
                client.PutObject(request);
            }
            catch (Exception exception)
            {
                // log or throw;
                return false;
            }

            return true;
        }


        //public static async Task<bool> GetFile(string fileName)
        //{
        //    try
        //    {

        //        config.ServiceURL = "http://localhost:1360/";
        //        var credentials = new BasicAWSCredentials(accessKey, secretKey);

        //        AmazonS3Client client = new AmazonS3Client(credentials, RegionEndpoint.APSouth1);
        //        string objectKey = fileName;
        //        //EMR is folder name of the image inside the bucket 
        //        GetObjectRequest request = new GetObjectRequest();
        //        request.BucketName = bucketname;
        //        request.Key = objectKey;
        //        GetObjectResponse response = AmazonS3Client.GetObject(request, accessKey);
        //        response.WriteResponseStreamToFile(@"D:\\" + fileName);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return false;
        //}

        public static void S3Download(string bucketName, string _ObjectKey, string downloadPath)
        {
            IAmazonS3 _client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.APSouth1);
            TransferUtility fileTransferUtility = new TransferUtility(_client);
            fileTransferUtility.Download(downloadPath + "\\" + _ObjectKey, bucketName, _ObjectKey);
            _client.Dispose();
        }

        public static async Task AsyncDownload(string bucketName, string downloadPath, string requiredSunFolder)
        {
            var bucketRegion = RegionEndpoint.APSouth1; //Change it
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var client = new AmazonS3Client(credentials, bucketRegion);

            var request = new ListObjectsV2Request
            {
                BucketName = bucketName,
                MaxKeys = 1000
            };

            var response = await client.ListObjectsV2Async(request);
            var utility = new TransferUtility(client);

            foreach (var obj in response.S3Objects)
            {
                string currentKey = obj.Key;
                double sizeCheck = Convert.ToDouble(obj.Size);
                int fileNameLength = currentKey.Length;
                Console.WriteLine(currentKey + "---" + fileNameLength.ToString());

                if (currentKey.Contains(requiredSunFolder))
                {
                    if (currentKey.Contains(".zip")) //This helps to avoid errors related to (416) Requested Range Not Satisfiable
                    {
                        try
                        {
                            S3Download(bucketName, currentKey, downloadPath);
                        }
                        catch (Exception exTest)
                        {
                            string messageTest = currentKey + "-" + exTest;
                        }
                    }
                }
            }

        }




    }
}