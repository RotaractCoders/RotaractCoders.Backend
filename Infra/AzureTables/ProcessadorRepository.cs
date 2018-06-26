using Domain.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.AzureTables
{
    public class ProcessadorRepository
    {
        private BaseRepository _baseRepository = new BaseRepository();

        public void Incluir(Processador processador)
        {
            var insertOperation = TableOperation.Insert(processador);
            _baseRepository.Processador.Execute(insertOperation);
        }

        public Processador BuscarProcessoEmAndamento()
        {
            TableQuery<Processador> tableQuery = new TableQuery<Processador>();
            TableContinuationToken continuationToken = null;
            TableQuerySegment<Processador> tableQueryResult = _baseRepository.Processador.ExecuteQuerySegmented(tableQuery, continuationToken);
            continuationToken = tableQueryResult.ContinuationToken;

            return tableQueryResult.Results.FirstOrDefault(x => 
                x.StatusProcessamentoClube == "Processando" || x.StatusProcessamentoClube == "AguardandoProcessamento" ||
                x.StatusProcessamentoSocio == "Processando" || x.StatusProcessamentoSocio == "AguardandoProcessamento" ||
                x.StatusProcessamentoProjeto == "Processando" || x.StatusProcessamentoProjeto == "AguardandoProcessamento");
        }

        public void Atualizar(Processador processador)
        {
            var atualizar = Obter(processador.RowKey);

            atualizar.Atualizar(processador);

            var updateOperation = TableOperation.Replace(atualizar);
            _baseRepository.Processador.Execute(updateOperation);
        }

        public Processador Obter(string id)
        {
            var query = new TableQuery<Processador>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id));

            var retorno = _baseRepository.Processador.ExecuteQuery(query);

            if (retorno == null)
                return null;

            return retorno.FirstOrDefault();
        }

        public void Excluir(string id)
        {
            var deletar = Obter(id);

            var deleteOperation = TableOperation.Delete(deletar);
            _baseRepository.Processador.Execute(deleteOperation);
        }
    }
}
