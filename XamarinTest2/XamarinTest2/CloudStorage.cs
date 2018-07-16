using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;


namespace XamarinTest2
{
    class CloudStorage
    {
        public static CloudBlobContainer GetContainer(string containerName)
        {
            //string connectionString = $"DefaultEndpointsProtocol=https;AccountName={Config.accountName};AccountKey={Config.accountKey};EndpointSuffix=core.windows.net";
            string connectionString = $"DefaultEndpointsProtocol=https;AccountName={Config.accountName};AccountKey={Config.accountKey}";

            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);

            return container;
        }

        public static async Task<string> DownloadBlob(string blobName)
        {
            var container = GetContainer("container1");
            var blob = container.GetBlockBlobReference(blobName);

            string res = await blob.DownloadTextAsync();

            return res;
        }
        public static async Task UploadBlob(string blobName, string content)
        {
            var container = GetContainer("container1");
            var blob = container.GetBlockBlobReference(blobName);

            try
            {
                await blob.UploadTextAsync(content);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static async Task<List<CloudBlockBlob>> GetBlobList()
        {
            var container = GetContainer("container1");

            BlobContinuationToken token = null;

            List<CloudBlockBlob> blobList = new List<CloudBlockBlob>();
            do
            {
                var responce = await container.ListBlobsSegmentedAsync(token);
                token = responce.ContinuationToken;
                foreach (var blob in responce.Results.OfType<CloudBlockBlob>())
                {
                    blobList.Add(blob);
                }
            } while (token != null);

            return blobList;
        }
    }
}
