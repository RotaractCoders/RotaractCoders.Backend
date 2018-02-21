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

        public List<Clube> Listar()
        {
            TableQuery<Clube> tableQuery = new TableQuery<Clube>();
            TableContinuationToken continuationToken = null;
            TableQuerySegment<Clube> tableQueryResult = _baseRepository.Clube.ExecuteQuerySegmented(tableQuery, continuationToken);
            continuationToken = tableQueryResult.ContinuationToken;

            return tableQueryResult.Results.Where(x => x.BitAtivo == true).ToList();
        }

        public List<Clube> Listar(DateTime dataUltimaAtualizacao)
        {
            var query = new TableQuery<Clube>()
                .Where(TableQuery.GenerateFilterConditionForDate("DataAtualizacao", QueryComparisons.GreaterThan, dataUltimaAtualizacao));
            var retorno = _baseRepository.Clube.ExecuteQuery(query);

            return retorno.Where(x => x.BitAtivo == true).ToList();
        }

        public void Atualizar(Clube clube)
        {
            var atualizar = Obter(clube.RowKey);

            atualizar.Atualizar(clube);

            var updateOperation = TableOperation.Replace(atualizar);
            _baseRepository.Clube.Execute(updateOperation);
        }

        public Clube Obter(string id)
        {
            var query = new TableQuery<Clube>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id));

            var retorno = _baseRepository.Clube.ExecuteQuery(query);

            if (retorno == null)
                return null;

            return retorno.FirstOrDefault();
        }

        public void Excluir(string id)
        {
            var deletar = Obter(id);

            var deleteOperation = TableOperation.Delete(deletar);
            _baseRepository.Clube.Execute(deleteOperation);
        }
    }
}
