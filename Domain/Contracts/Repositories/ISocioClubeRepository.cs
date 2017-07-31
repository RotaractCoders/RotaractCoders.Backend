using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Contracts.Repositories
{
    public interface ISocioClubeRepository
    {
        SocioClube Buscar(Guid idSocio, Guid idClube);
        List<SocioClube> BuscarPorSocio(Guid idSocio);
        void Incluir(SocioClube socioClube);
        void Excluir(Guid idSocioClube);
    }
}
