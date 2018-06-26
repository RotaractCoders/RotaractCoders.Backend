using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Infra.AzureTables
{
    public class BaseRepository
    {
        public CloudTable Evento { get; private set; }
        public CloudTable DadoEstatico { get; private set; }
        public CloudTable Faq { get; private set; }
        public CloudTable Arquivo { get; private set; }
        public CloudTable Clube { get; private set; }
        public CloudTable Socio { get; private set; }
        public CloudTable Projeto { get; private set; }
        public CloudTable CargoSocio { get; private set; }
        public CloudTable Processador { get; set; }

        public BaseRepository()
        {
            var storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=rotaract;AccountKey=0wV5I1IC9qM5ZWF6PYIGQtnnZm5p1H53FtrerOhHEYP2JZYZGN2Wk9+Bq4+06AvFidzGh0Zg/M0zjklRPF0iqg==;EndpointSuffix=core.windows.net");
            var tableClient = storageAccount.CreateCloudTableClient();
            var blobClient = storageAccount.CreateCloudBlobClient();

            Evento = tableClient.GetTableReference("Evento");
            Evento.CreateIfNotExists();

            DadoEstatico = tableClient.GetTableReference("DadoEstatico");
            //DadoEstatico.CreateIfNotExists();

            Faq = tableClient.GetTableReference("Faq");
            //Faq.CreateIfNotExists();

            Arquivo = tableClient.GetTableReference("Arquivo");
            //Arquivo.CreateIfNotExists();

            Clube = tableClient.GetTableReference("Clube");
            Clube.CreateIfNotExists();

            Socio = tableClient.GetTableReference("Socio");
            Socio.CreateIfNotExists();

            Projeto = tableClient.GetTableReference("Projeto");
            Projeto.CreateIfNotExists();

            CargoSocio = tableClient.GetTableReference("CargoSocio");
            CargoSocio.CreateIfNotExists();

            Processador = tableClient.GetTableReference("Processador");
            Processador.CreateIfNotExists();
        }
    }
}
