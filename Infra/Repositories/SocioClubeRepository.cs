using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using Domain.Entities;
using System.Linq;

namespace Infra.Repositories
{
    public class SocioClubeRepository : ISocioClubeRepository
    {
        private Context _context;

        public SocioClubeRepository()
        {
            _context = new Context();
        }

        public SocioClube Buscar(Guid idSocio, Guid idClube)
        {
            return _context.SocioClube.FirstOrDefault(x => x.IdSocio == idSocio && x.IdClube == idClube);
        }

        public List<SocioClube> BuscarPorSocio(Guid idSocio)
        {
            return _context.SocioClube
                .Where(x => x.IdSocio == idSocio)
                .ToList();
        }

        public void Excluir(Guid idSocioClube)
        {
            _context.SocioClube.Remove(_context.SocioClube.FirstOrDefault(x => x.Id == idSocioClube));
        }

        public void Incluir(SocioClube socioClube)
        {
            _context.SocioClube.Add(socioClube);
        }
    }
}
