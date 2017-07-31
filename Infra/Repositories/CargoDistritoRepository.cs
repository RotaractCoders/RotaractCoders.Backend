using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using Domain.Entities;
using System.Linq;

namespace Infra.Repositories
{
    public class CargoDistritoRepository : ICargoDistritoRepository
    {
        private Context _context;

        public CargoDistritoRepository()
        {
            _context = new Context();
        }

        public void Excluir(Guid idCargoDistrito)
        {
            _context.CargoDistrito.Remove(_context.CargoDistrito.FirstOrDefault(x => x.Id == idCargoDistrito));
            _context.SaveChanges();
        }

        public void Incluir(CargoDistrito cargoDistrito)
        {
            _context.CargoDistrito.Add(cargoDistrito);
            _context.SaveChanges();
        }

        public List<CargoDistrito> ListarPorSocio(Guid idSocio)
        {
            return _context.CargoDistrito.Where(x => x.IdSocio == idSocio).ToList();
        }
    }
}
