using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using Domain.Entities;
using System.Linq;

namespace Infra.Repositories
{
    public class CargoRotaractBrasilRepository : ICargoRotaractBrasilRepository
    {
        private Context _context;

        public CargoRotaractBrasilRepository()
        {
            _context = new Context();
        }

        public void Excluir(Guid idCargoRotaractBrasil)
        {
            _context.CargoRotaractBrasil.Remove(_context.CargoRotaractBrasil.FirstOrDefault(x => x.Id == idCargoRotaractBrasil));
            _context.SaveChanges();
        }

        public void Incluir(CargoRotaractBrasil cargoRotaractBrasil)
        {
            _context.CargoRotaractBrasil.Add(cargoRotaractBrasil);
            _context.SaveChanges();
        }

        public List<CargoRotaractBrasil> ListarPorSocio(Guid idSocio)
        {
            return _context.CargoRotaractBrasil.Where(x => x.IdSocio == idSocio).ToList();
        }
    }
}
