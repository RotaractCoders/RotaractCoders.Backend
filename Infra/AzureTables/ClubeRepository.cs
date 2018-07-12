using Domain.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.AzureTables
{
    public class ClubeRepository
    {
        private BaseRepository _baseRepository = new BaseRepository();

        public void Incluir(Clube clube)
        {
            var insertOperation = TableOperation.Insert(clube);
            _baseRepository.Clube.Execute(insertOperation);
        }

        public int QuantidadeClubes(string numeroDistrito)
        {
            var tableQuery = new TableQuery<Clube>();
            TableContinuationToken continuationToken = null;
            TableQuerySegment<Clube> tableQueryResult = _baseRepository.Clube.ExecuteQuerySegmented(tableQuery, continuationToken);
            continuationToken = tableQueryResult.ContinuationToken;

            return tableQueryResult.Results.Where(x => x.NumeroDistrito == numeroDistrito && x.DataFechamento == null).Count();
        }

        public List<Clube> Listar()
        {
            TableQuery<Clube> tableQuery = new TableQuery<Clube>();
            TableContinuationToken continuationToken = null;
            TableQuerySegment<Clube> tableQueryResult = _baseRepository.Clube.ExecuteQuerySegmented(tableQuery, continuationToken);
            continuationToken = tableQueryResult.ContinuationToken;

            return tableQueryResult.Results.Where(x => x.BitAtivo == true).ToList();
        }

        public List<Clube> Listar(string numeroDistrito)
        {
            var query = new TableQuery<Clube>()
            {
                SelectColumns = new List<string>
                {
                    "Codigo", "Nome", "DataFechamento"
                },
                FilterString = TableQuery.GenerateFilterCondition("NumeroDistrito", QueryComparisons.Equal, numeroDistrito)
            };
            
            var retorno = _baseRepository.Clube.ExecuteQuery(query);

            return retorno.ToList();
        }

        public List<Clube> Listar(DateTime dataUltimaAtualizacao, string codigoClube)
        {
            string date1 = TableQuery.GenerateFilterConditionForDate("DataAtualizacao", QueryComparisons.GreaterThan, dataUltimaAtualizacao);

            string date2 = TableQuery.GenerateFilterCondition("Codigo", QueryComparisons.Equal, codigoClube);

            string finalFilter = TableQuery.CombineFilters(date1, TableOperators.And, date2);

            var query = new TableQuery<Clube>()
                .Where(finalFilter);
            var retorno = _baseRepository.Clube.ExecuteQuery(query);

            return retorno.ToList();
        }

        public void Atualizar(Clube clube)
        {
            var atualizar = Obter(clube.Codigo);

            atualizar.Atualizar(clube);

            var updateOperation = TableOperation.Replace(atualizar);
            _baseRepository.Clube.Execute(updateOperation);
        }

        public Clube Obter(string codigoClube)
        {
            var query = new TableQuery<Clube>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, codigoClube));

            var retorno = _baseRepository.Clube.ExecuteQuery(query);

            if (retorno == null)
                return null;

            return retorno.FirstOrDefault();
        }

        public Clube ObterPorCodigo(string codigo)
        {
            var query = new TableQuery<Clube>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, codigo));

            var retorno = _baseRepository.Clube.ExecuteQuery(query);

            if (retorno == null)
                return null;

            return retorno.FirstOrDefault();
        }

        public Clube ObterPorNome(string nome)
        {
            var query = new TableQuery<Clube>().Where(TableQuery.GenerateFilterCondition("Nome", QueryComparisons.Equal, nome));

            var retorno = _baseRepository.Clube.ExecuteQuery(query);

            if (retorno == null)
                return null;

            return retorno.FirstOrDefault();
        }

        public void Excluir(string codigoClube)
        {
            var deletar = Obter(codigoClube);

            var deleteOperation = TableOperation.Delete(deletar);
            _baseRepository.Clube.Execute(deleteOperation);
        }
    }
}
