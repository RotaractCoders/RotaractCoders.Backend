using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Contracts.Repositories
{
    public interface ICargoRotaractBrasilRepository
    {
        void Incluir(CargoRotaractBrasil cargoRotaractBrasil);
        List<CargoRotaractBrasil> ListarPorSocio(Guid idSocio);
        void Excluir(Guid idCargoRotaractBrasil);
    }
}
