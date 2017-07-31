using System;
using Domain.Contracts.Repositories;
using Domain.Entities;
using System.Linq;
using System.Data.Entity;

namespace Infra.Repositories
{
    public class DistritoRepository : IDistritoRepository
    {
        private readonly Context _context;

        public DistritoRepository()
        {
            _context = new Context();
        }

        public Distrito Atualizar(Distrito distrito)
        {
            _context.Entry(distrito).State = EntityState.Modified;
            _context.SaveChanges();
            return distrito;
        }

        public Distrito Buscar(string numero)
        {
            return _context.Distrito.FirstOrDefault(x => x.Numero == numero);
        }

        public Distrito Incluir(Distrito distrito)
        {
            distrito = _context.Distrito.Add(distrito);
            _context.SaveChanges();
            return distrito;
        }
    }
}
