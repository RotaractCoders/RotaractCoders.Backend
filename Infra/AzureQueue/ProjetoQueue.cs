using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.AzureQueue
{
    public class ProjetoQueue
    {
        public BaseQueue Queue { get; set; }

        public ProjetoQueue()
        {
            Queue = new BaseQueue();
        }

        public void Incluir(string codigoProjeto)
        {
            var message = new CloudQueueMessage(codigoProjeto);
            Queue.FilaProjeto.AddMessage(message);
        }

        public CloudQueueMessage ObterProximoProjeto()
        {
            return Queue.FilaProjeto.GetMessage();
        }

        public void Deletar(CloudQueueMessage mensagem)
        {
            Queue.FilaProjeto.DeleteMessage(mensagem);
        }

        public int? QuantidadeProjetos()
        {
            Queue.FilaProjeto.FetchAttributes();
            return Queue.FilaProjeto.ApproximateMessageCount;
        }
    }
}
