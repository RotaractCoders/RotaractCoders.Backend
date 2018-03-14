using Domain.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.AzureTables
{
    public class CargoSocioRepository
    {
        private BaseRepository _baseRepository = new BaseRepository();

        public void Incluir(List<CargoSocio> listaCargoSocio)
        {
            if (listaCargoSocio.Count == 0)
                return;

            var batchOperation = new TableBatchOperation();

            listaCargoSocio.ForEach(x =>
            {
                batchOperation.Insert(x);
            });

            _baseRepository.CargoSocio.ExecuteBatchAsync(batchOperation);
        }

        public List<CargoSocio> Listar(string codigoSocio)
        {
            var query = new TableQuery<CargoSocio>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, codigoSocio));

            var retorno = _baseRepository.CargoSocio.ExecuteQuery(query);

            return retorno.ToList();
        }

        public List<CargoSocio> Listar(string cargo, string numeroDistrito, string programa)
        {
            string filtro2 = TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("NumeroDistritoCargo", QueryComparisons.Equal, numeroDistrito),
                TableOperators.And,
                TableQuery.GenerateFilterCondition("Programa", QueryComparisons.Equal, programa));

            string filtro3 = TableQuery.CombineFilters(
                filtro2,
                TableOperators.And,
                TableQuery.GenerateFilterCondition("NomeCargo", QueryComparisons.Equal, cargo));

            var query = new TableQuery<CargoSocio>()
                .Where(filtro3);

            var retorno = _baseRepository.CargoSocio.ExecuteQuery(query);

            return retorno.ToList();
        }

        public List<CargoSocio> Listar(DateTime gestaoDe, DateTime gestaoAte, string cargo, string numeroDistrito, string programa)
        {
            string filtro1 = TableQuery.CombineFilters(
                TableQuery.GenerateFilterConditionForDate("GestaoDe", QueryComparisons.GreaterThanOrEqual, gestaoDe),
                TableOperators.And,
                TableQuery.GenerateFilterConditionForDate("GestaoAte", QueryComparisons.LessThanOrEqual, gestaoAte));

            string filtro2 = TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("NumeroDistritoClube", QueryComparisons.Equal, numeroDistrito),
                TableOperators.And,
                TableQuery.GenerateFilterCondition("Programa", QueryComparisons.Equal, programa));

            string filtro3 = TableQuery.CombineFilters(
                filtro2,
                TableOperators.And,
                TableQuery.GenerateFilterCondition("NomeCargo", QueryComparisons.Equal, cargo));

            var query = new TableQuery<CargoSocio>()
                .Where(TableQuery.CombineFilters(
                    filtro1,
                    TableOperators.And,
                    filtro3));

            var retorno = _baseRepository.CargoSocio.ExecuteQuery(query);

            return retorno.ToList();
        }

        public void Excluir(List<CargoSocio> listaCargoSocio)
        {
            if (listaCargoSocio.Count == 0)
                return;

            var batchOperation = new TableBatchOperation();

            listaCargoSocio.ForEach(x => 
            {
                batchOperation.Delete(x);
            });

            _baseRepository.CargoSocio.ExecuteBatchAsync(batchOperation);
        }
    }
}
