using Domain.Contracts.Repositories;
using Domain.Entities;
using System.Linq;
using Domain.Enum;

namespace Infra.Repositories
{
    public class CargoRepository : ICargoRepository
    {
        private Context _context;

        public CargoRepository()
        {
            _context = new Context();
        }

        public Cargo Buscar(string nome, TipoCargo tipoCargo)
        {
            return _context.Cargo.FirstOrDefault(x => x.Nome == nome && x.TipoCargo == tipoCargo);
        }

        public Cargo Incluir(Cargo cargo)
        {
            cargo = _context.Cargo.Add(cargo);
            _context.SaveChanges();

            return cargo;
        }
    }
}
