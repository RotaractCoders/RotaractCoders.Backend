using Domain.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.AzureTables
{
    public class ArquivoRepository
    {
        private BaseRepository _baseRepository = new BaseRepository();

        public void Incluir(Arquivo arquivo)
        {
            var insertOperation = TableOperation.Insert(arquivo);
            _baseRepository.Arquivo.Execute(insertOperation);
        }

        public List<Arquivo> Listar()
        {
            TableQuery<Arquivo> tableQuery = new TableQuery<Arquivo>();
            TableContinuationToken continuationToken = null;
            TableQuerySegment<Arquivo> tableQueryResult = _baseRepository.Arquivo.ExecuteQuerySegmented(tableQuery, continuationToken);
            continuationToken = tableQueryResult.ContinuationToken;

            return tableQueryResult.Results;
        }

        public List<Arquivo> Listar(DateTime dataUltimaAtualizacao)
        {
            var query = new TableQuery<Arquivo>()
                .Where(TableQuery.GenerateFilterConditionForDate("DataAtualizacao", QueryComparisons.GreaterThan, dataUltimaAtualizacao));
            var retorno = _baseRepository.Arquivo.ExecuteQuery(query);

            return retorno.ToList();
        }

        public void Atualizar(Arquivo arquivo)
        {
            var atualizar = Obter(arquivo.RowKey);

            atualizar.Atualizar(arquivo.Nome, arquivo.Categoria, arquivo.Link);

            var updateOperation = TableOperation.Replace(atualizar);
            _baseRepository.Arquivo.Execute(updateOperation);
        }

        public Arquivo Obter(string id)
        {
            var query = new TableQuery<Arquivo>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id));

            var retorno = _baseRepository.Arquivo.ExecuteQuery(query);

            if (retorno == null)
                return null;

            return retorno.FirstOrDefault();
        }

        public void Excluir(string id)
        {
            var deletar = Obter(id);

            var deleteOperation = TableOperation.Delete(deletar);
            _baseRepository.Arquivo.Execute(deleteOperation);
        }
    }
}
