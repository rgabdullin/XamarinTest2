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
        private string accountName;
        private string accountKey;
        private string containerName;
        public CloudStorage(string container, string name, string key)
        {
            containerName = container;
            accountKey = key;
            accountName = name;
        }

        public async Task<string> DownloadBlob(string blobName)
        {
            string connectionString = $"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={accountKey};EndpointSuffix=core.windows.net;";
            //string connectionString = $"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={accountKey};";

            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(blobName);

            string res = await blob.DownloadTextAsync();

            return res;
        }
        public async Task UploadBlob(string blobName, string content)
        {
            string connectionString = $"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={accountKey};EndpointSuffix=core.windows.net;";
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(blobName);

            await blob.UploadTextAsync(content);
        }
        public async Task<List<CloudBlockBlob>> GetBlobList()
        {
            string connectionString = $"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={accountKey};EndpointSuffix=core.windows.net;";
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);

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
