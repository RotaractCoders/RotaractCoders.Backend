using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Infra.AzureBlobs
{
    public class BaseBlob
    {
        public CloudBlobContainer socios { get; set; }

        public BaseBlob()
        {
            var storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=rotaract;AccountKey=0wV5I1IC9qM5ZWF6PYIGQtnnZm5p1H53FtrerOhHEYP2JZYZGN2Wk9+Bq4+06AvFidzGh0Zg/M0zjklRPF0iqg==;EndpointSuffix=core.windows.net");
            var blobClient = storageAccount.CreateCloudBlobClient();

            socios = blobClient.GetContainerReference("socios");
            socios.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            socios.CreateIfNotExists();
        }
    }
}
