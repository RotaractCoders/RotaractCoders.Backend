using Domain.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.AzureTables
{
    public class SocioRepository
    {
        private BaseRepository _baseRepository = new BaseRepository();

        public Socio Incluir(Socio socio)
        {
            var insertOperation = TableOperation.Insert(socio);
            _baseRepository.Socio.Execute(insertOperation);

            return socio;
        }

        public int QuantidadeSocios(string numeroDistrito)
        {
            var tableQuery = new TableQuery<Socio>();
            TableContinuationToken continuationToken = null;
            TableQuerySegment<Socio> tableQueryResult = _baseRepository.Socio.ExecuteQuerySegmented(tableQuery, continuationToken);
            continuationToken = tableQueryResult.ContinuationToken;

            return tableQueryResult.Results.Where(x => 
                x.ClubesSerializado.Contains("\"NumeroDistrito\":\"4430\"") &&
                x.ClubesSerializado.Contains("\"Desligamento\":\"0001-01-01T00:00:00\"")).Count();
        }

        public List<Socio> Listar()
        {
            var tableQuery = new TableQuery<Socio>();
            TableContinuationToken continuationToken = null;
            TableQuerySegment<Socio> tableQueryResult = _baseRepository.Socio.ExecuteQuerySegmented(tableQuery, continuationToken);
            continuationToken = tableQueryResult.ContinuationToken;

            return tableQueryResult.Results.Where(x => x.BitAtivo == true).ToList();
        }

        public List<Socio> Listar(string codigoClube)
        {
            var query = new TableQuery<Socio>()
            {
                SelectColumns = new List<string>
                {
                    "Nome", "CodigoSocio", "Foto", "CodigoClube", "ClubesSerializado"
                },
                FilterString = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, codigoClube)
            };

            var retorno = _baseRepository.Socio.ExecuteQuery(query);

            return retorno.Where(x => x.BitAtivo == true).ToList();
        }

        public List<Socio> Listar(string codigoClube, DateTime dataUltimaAtualizacao)
        {
            string date1 = TableQuery.GenerateFilterConditionForDate("DataAtualizacao", QueryComparisons.GreaterThan, dataUltimaAtualizacao);

            string date2 = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, codigoClube);

            string finalFilter = TableQuery.CombineFilters(date1, TableOperators.And, date2);

            var query = new TableQuery<Socio>()
                .Where(finalFilter);

            var retorno = _baseRepository.Socio.ExecuteQuery(query);

            return retorno.Where(x => x.BitAtivo == true).ToList();
        }

        public void Atualizar(Socio socio)
        {
            var atualizar = ObterPorCodigo(socio.CodigoSocio, socio.CodigoClube);

            atualizar.Atualizar(socio);

            var updateOperation = TableOperation.Replace(atualizar);
            _baseRepository.Socio.Execute(updateOperation);
        }

        public Socio ObterPorCodigo(string codigoSocio)
        {
            var query = new TableQuery<Socio>()
                .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, codigoSocio));

            var retorno = _baseRepository.Socio.ExecuteQuery(query);

            if (retorno == null)
                return null;

            return retorno.FirstOrDefault();
        }

        public Socio ObterPorCodigo(string codigoSocio, string codigoClube)
        {
            var query = new TableQuery<Socio>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, codigoClube),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, codigoSocio)));
            
            var retorno = _baseRepository.Socio.ExecuteQuery(query);

            if (retorno == null)
                return null;

            return retorno.FirstOrDefault();
        }

        public void Excluir(string codigoSocio, string codigoClube)
        {
            var deletar = ObterPorCodigo(codigoSocio, codigoClube);

            var deleteOperation = TableOperation.Delete(deletar);
            _baseRepository.Socio.Execute(deleteOperation);
        }
    }
}
