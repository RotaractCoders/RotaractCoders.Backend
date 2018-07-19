using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Infra.AzureQueue
{
    public class BaseQueue
    {
        public CloudQueue FilaClube { get; set; }
        public CloudQueue FilaSocio { get; set; }
        public CloudQueue FilaProjeto { get; set; }

        public BaseQueue()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=rotaract;AccountKey=0wV5I1IC9qM5ZWF6PYIGQtnnZm5p1H53FtrerOhHEYP2JZYZGN2Wk9+Bq4+06AvFidzGh0Zg/M0zjklRPF0iqg==;EndpointSuffix=core.windows.net");
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            FilaClube = queueClient.GetQueueReference("clube");
            FilaClube.CreateIfNotExists();

            FilaSocio = queueClient.GetQueueReference("socio");
            FilaSocio.CreateIfNotExists();

            FilaProjeto = queueClient.GetQueueReference("projeto");
            FilaProjeto.CreateIfNotExists();
        }
    }
}
