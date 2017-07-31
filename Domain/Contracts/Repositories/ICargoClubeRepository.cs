using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Contracts.Repositories
{
    public interface ICargoClubeRepository
    {
        List<CargoClube> ListarPorSocio(Guid idSocio);
        void Incluir(CargoClube cargoClube);
        void Excluir(Guid idCargoClube);
    }
}
