using Domain.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.AzureTables
{
    public class ProjetoRepository
    {
        protected BaseRepository _baseRepository = new BaseRepository();

        public void Incluir(Projeto projeto)
        {
            var insertOperation = TableOperation.Insert(projeto);
            _baseRepository.Projeto.Execute(insertOperation);
        }

        public void Atualizar(Projeto projeto)
        {
            var atualizar = Obter(projeto.RowKey);

            atualizar.Atualizar(projeto);

            var updateOperation = TableOperation.Replace(atualizar);
            _baseRepository.Projeto.Execute(updateOperation);
        }

        public Projeto Obter(string guid)
        {
            var query = new TableQuery<Projeto>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, guid));

            var retorno = _baseRepository.Projeto.ExecuteQuery(query);

            if (retorno == null)
                return null;

            return retorno.FirstOrDefault();
        }

        public Projeto ObterPorCodigo(string codigo)
        {
            var query = new TableQuery<Projeto>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, codigo));

            var retorno = _baseRepository.Projeto.ExecuteQuery(query);

            if (retorno == null)
                return null;

            return retorno.FirstOrDefault();
        }

        public List<Projeto> Listar()
        {
            TableQuery<Projeto> tableQuery = new TableQuery<Projeto>();
            TableContinuationToken continuationToken = null;
            TableQuerySegment<Projeto> tableQueryResult = _baseRepository.Projeto.ExecuteQuerySegmented(tableQuery, continuationToken);
            continuationToken = tableQueryResult.ContinuationToken;

            return tableQueryResult.Results.ToList();
        }

        public List<Projeto> Listar(DateTime dataUltimaAtualizacao, string codigoClube)
        {
            
            var query = new TableQuery<Projeto>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterConditionForDate("DataAtualizacao", QueryComparisons.GreaterThan, dataUltimaAtualizacao),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("CodigoClube", QueryComparisons.Equal, codigoClube)));

            var retorno = _baseRepository.Projeto.ExecuteQuery(query);

            return retorno.ToList();
        }

        public void Excluir(string id)
        {
            var deletar = Obter(id);

            var deleteOperation = TableOperation.Delete(deletar);
            _baseRepository.Projeto.Execute(deleteOperation);
        }
    }
}
