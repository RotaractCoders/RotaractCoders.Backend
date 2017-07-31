using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Contracts.Repositories
{
    public interface ICargoDistritoRepository
    {
        void Incluir(CargoDistrito cargoDistrito);
        List<CargoDistrito> ListarPorSocio(Guid idSocio);
        void Excluir(Guid idCargoDistrito);
    }
}
