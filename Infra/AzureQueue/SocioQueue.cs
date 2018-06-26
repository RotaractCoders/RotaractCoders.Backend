using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.AzureQueue
{
    public class SocioQueue
    {
        public BaseQueue Queue { get; set; }

        public SocioQueue()
        {
            Queue = new BaseQueue();
        }

        public void Incluir(string codigoSocio)
        {
            var message = new CloudQueueMessage(codigoSocio);
            Queue.FilaSocio.AddMessage(message);
        }

        public CloudQueueMessage ObterProximoSocio()
        {
            return Queue.FilaSocio.GetMessage();
        }

        public int? QuantidadeSocios()
        {
            Queue.FilaSocio.FetchAttributes();
            return Queue.FilaSocio.ApproximateMessageCount;
        }

        public void Deletar(CloudQueueMessage mensagem)
        {
            Queue.FilaSocio.DeleteMessage(mensagem);
        }
    }
}
