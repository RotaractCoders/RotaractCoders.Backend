using Domain.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.Queryable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.AzureTables
{
    public class DadoEstaticoRepository
    {
        protected BaseRepository _baseRepository = new BaseRepository();

        public List<DadoEstatico> Listar()
        {
            TableQuery<DadoEstatico> tableQuery = new TableQuery<DadoEstatico>();
            TableContinuationToken continuationToken = null;
            TableQuerySegment<DadoEstatico> tableQueryResult = _baseRepository.DadoEstatico.ExecuteQuerySegmented(tableQuery, continuationToken);
            continuationToken = tableQueryResult.ContinuationToken;

            return tableQueryResult.Results;
        }

        public void Incluir(DadoEstatico dadoEstatico)
        {
            var insertOperation = TableOperation.Insert(dadoEstatico);
            _baseRepository.DadoEstatico.Execute(insertOperation);
        }

        public void Atualizar(DadoEstatico dadoEstatico)
        {
            var atualizar = Obter(dadoEstatico.Nome);

            atualizar.AtualizarDescricao(dadoEstatico.Descricao);

            var updateOperation = TableOperation.Replace(atualizar);
            _baseRepository.DadoEstatico.Execute(updateOperation);
        }

        public DadoEstatico Obter(string nome)
        {
            var query = new TableQuery<DadoEstatico>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, nome));

            var retorno = _baseRepository.DadoEstatico.ExecuteQuery(query);

            if (retorno == null)
                return null;

            return retorno.FirstOrDefault();
        }
    }
}
