using System.Collections.Generic;
using System.Linq;
using Domain.Commands.Results;
using Domain.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Infra.AzureTables
{
    public class EventoRepository
    {
        protected BaseRepository _baseRepository = new BaseRepository();

        public void Atualizar(Evento evento)
        {
            var eventoAtualizar = Obter(evento.RowKey);

            eventoAtualizar.Atualizar(evento);

            var updateOperation = TableOperation.Replace(eventoAtualizar);
            _baseRepository.Evento.Execute(updateOperation);
        }

        public DetalheEventoResult Buscar(string id)
        {
            var evento = Obter(id);

            return new DetalheEventoResult(evento);
        }

        public void Deletar(string id)
        {
            var evento = Obter(id);

            var deleteOperation = TableOperation.Delete(evento);
            _baseRepository.Evento.Execute(deleteOperation);
        }

        public void Incluir(Evento evento)
        {
            var insertOperation = TableOperation.Insert(evento);
            _baseRepository.Evento.Execute(insertOperation);
        }

        public List<Evento> Listar()
        {
            TableQuery<Evento> tableQuery = new TableQuery<Evento>();
            TableContinuationToken continuationToken = null;
            TableQuerySegment<Evento> tableQueryResult = _baseRepository.Evento.ExecuteQuerySegmented(tableQuery, continuationToken);
            continuationToken = tableQueryResult.ContinuationToken;

            return tableQueryResult.Results;
        }

        public List<Evento> Listar(DateTime dataUltimaAtualizacao)
        {
            var query = new TableQuery<Evento>()
                .Where(TableQuery.GenerateFilterConditionForDate("DataAtualizacao", QueryComparisons.GreaterThan, dataUltimaAtualizacao));
            var retorno = _baseRepository.Faq.ExecuteQuery(query);

            return retorno.ToList();
        }

        public Evento Obter(string id)
        {
            var query = new TableQuery<Evento>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id));

            var retorno = _baseRepository.Evento.ExecuteQuery(query);

            if (retorno == null)
                return null;

            return retorno.FirstOrDefault();
        }
    }
}
