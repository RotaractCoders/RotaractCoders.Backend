using Microsoft.WindowsAzure.Storage.Queue;

namespace Infra.AzureQueue
{
    public class ClubeQueue
    {
        public BaseQueue Queue { get; set; }

        public ClubeQueue()
        {
            Queue = new BaseQueue();
        }

        public void Incluir(string codigoClube)
        {
            var message = new CloudQueueMessage(codigoClube);
            Queue.FilaClube.AddMessage(message);
        }

        public CloudQueueMessage ObterProximoClube()
        {
            return Queue.FilaClube.GetMessage();
        }

        public void Deletar(CloudQueueMessage mensagem)
        {
            Queue.FilaClube.DeleteMessage(mensagem);
        }

        public int? QuantidadeClubes()
        {
            Queue.FilaClube.FetchAttributes();
            return Queue.FilaClube.ApproximateMessageCount;
        }
    }
}
