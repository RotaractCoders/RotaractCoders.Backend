using Domain.ConsolidadoEntities;
using Microsoft.WindowsAzure.Storage.Table;
using System.Linq;

namespace Infra.AzureTables.Consolidado
{
    public class DistritoRepository
    {
        private BaseRepository _baseRepository = new BaseRepository();

        public void Incluir(Distrito distrito)
        {
            var insertOperation = TableOperation.Insert(distrito);
            _baseRepository.Distrito.Execute(insertOperation);
        }

        public Distrito Obter(string numero)
        {
            var query = new TableQuery<Distrito>().Where(
                TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Brasil"),
                TableOperators.And,
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, numero)));

            var retorno = _baseRepository.Distrito.ExecuteQuery(query);

            if (retorno == null)
                return null;

            return retorno.FirstOrDefault();
        }

        public void DeletarSeExistir(string numeroDistrito)
        {
            var deletar = Obter(numeroDistrito);

            if (deletar == null)
                return;

            var deleteOperation = TableOperation.Delete(deletar);
            _baseRepository.Distrito.Execute(deleteOperation);
        }
    }
}
