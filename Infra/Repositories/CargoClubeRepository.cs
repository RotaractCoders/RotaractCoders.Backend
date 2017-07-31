using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using System.Data.Entity;

namespace Infra.Repositories
{
    public class CargoClubeRepository : ICargoClubeRepository
    {
        private Context _context;

        public CargoClubeRepository()
        {
            _context = new Context();
        }

        public void Excluir(Guid idCargoClube)
        {
            _context.CargoClube.Remove(_context.CargoClube.FirstOrDefault(x => x.Id == idCargoClube));
            _context.SaveChanges();
        }

        public void Incluir(CargoClube cargoClube)
        {
            _context.CargoClube.Add(cargoClube);
            _context.SaveChanges();
        }

        public List<CargoClube> ListarPorSocio(Guid idSocio)
        {
            return _context
                .CargoClube
                .Where(x => x.IdSocio == idSocio)
                .ToList();
        }
    }
}
