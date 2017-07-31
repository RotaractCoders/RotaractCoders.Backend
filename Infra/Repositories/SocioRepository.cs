using Domain.Contracts.Repositories;
using Domain.Entities;
using System.Linq;
using System.Data.Entity;

namespace Infra.Repositories
{
    public class SocioRepository : ISocioRepository
    {
        private Context _context;

        public SocioRepository()
        {
            _context = new Context();
        }

        public void Atualizar(Socio socio)
        {
            _context.Entry(socio).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Socio Buscar(int codigo)
        {
            return _context.Socio.FirstOrDefault(x => x.Codigo == codigo);
        }

        public void Incluir(Socio socio)
        {
            _context.Socio.Add(socio);
            _context.SaveChanges();
        }
    }
}
