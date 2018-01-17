using Domain.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Linq;

namespace Infra.AzureTables
{
    public class FaqRepository
    {
        protected BaseRepository _baseRepository = new BaseRepository();

        public void Incluir(Faq faq)
        {
            var insertOperation = TableOperation.Insert(faq);
            _baseRepository.Faq.Execute(insertOperation);
        }

        public void Atualizar(Faq faq)
        {
            var atualizar = Obter(faq.RowKey);

            atualizar.Atualizar(faq.Pergunta, faq.Resposta, faq.Posicao);

            var updateOperation = TableOperation.Replace(atualizar);
            _baseRepository.Faq.Execute(updateOperation);
        }

        public Faq Obter(string guid)
        {
            var query = new TableQuery<Faq>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, guid));

            var retorno = _baseRepository.Faq.ExecuteQuery(query);

            if (retorno == null)
                return null;

            return retorno.FirstOrDefault();
        }

        public List<Faq> Listar()
        {
            TableQuery<Faq> tableQuery = new TableQuery<Faq>();
            TableContinuationToken continuationToken = null;
            TableQuerySegment<Faq> tableQueryResult = _baseRepository.Faq.ExecuteQuerySegmented(tableQuery, continuationToken);
            continuationToken = tableQueryResult.ContinuationToken;

            return tableQueryResult.Results;
        }

        public void Excluir(string id)
        {
            var deletar = Obter(id);

            var deleteOperation = TableOperation.Delete(deletar);
            _baseRepository.Faq.Execute(deleteOperation);
        }
    }
}
