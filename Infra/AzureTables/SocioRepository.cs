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

        public void Incluir(Socio socio)
        {
            var insertOperation = TableOperation.Insert(socio);
            _baseRepository.Socio.Execute(insertOperation);
        }

        public List<Socio> Listar()
        {
            var tableQuery = new TableQuery<Socio>();
            TableContinuationToken continuationToken = null;
            TableQuerySegment<Socio> tableQueryResult = _baseRepository.Socio.ExecuteQuerySegmented(tableQuery, continuationToken);
            continuationToken = tableQueryResult.ContinuationToken;

            return tableQueryResult.Results.Where(x => x.BitAtivo == true).ToList();
        }

        public List<Socio> Listar(DateTime dataUltimaAtualizacao)
        {
            var query = new TableQuery<Socio>()
                .Where(TableQuery.GenerateFilterConditionForDate("DataAtualizacao", QueryComparisons.GreaterThan, dataUltimaAtualizacao));
            var retorno = _baseRepository.Socio.ExecuteQuery(query);

            return retorno.Where(x => x.BitAtivo == true).ToList();
        }

        public void Atualizar(Socio socio)
        {
            var atualizar = Obter(socio.RowKey);

            atualizar.Atualizar(socio);

            var updateOperation = TableOperation.Replace(atualizar);
            _baseRepository.Socio.Execute(updateOperation);
        }

        public Socio Obter(string id)
        {
            var query = new TableQuery<Socio>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id));

            var retorno = _baseRepository.Socio.ExecuteQuery(query);

            if (retorno == null)
                return null;

            return retorno.FirstOrDefault();
        }

        public void Excluir(string id)
        {
            var deletar = Obter(id);

            var deleteOperation = TableOperation.Delete(deletar);
            _baseRepository.Socio.Execute(deleteOperation);
        }
    }
}
