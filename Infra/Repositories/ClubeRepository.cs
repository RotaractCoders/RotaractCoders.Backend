using Domain.Contracts.Repositories;
using System.Linq;
using Domain.Entities;
using System.Data.Entity;
using System;

namespace Infra.Repositories
{
    public class ClubeRepository : IClubeRepository
    {
        private Context _context;

        public ClubeRepository()
        {
            _context = new Context();
        }

        public void Atualizar(Clube clube)
        {
            _context.Entry(clube).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Clube Buscar(int codigo)
        {
            return _context.Clube.FirstOrDefault(x => x.Codigo == codigo);
        }

        public Clube BuscarPorNomeEDistrito(string nomeClube)
        {
            return _context.Clube.FirstOrDefault(x => x.Nome.ToLower() == nomeClube.ToLower());
        }
        
        public void Incluir(Clube clube)
        {
            _context.Clube.Add(clube);
            _context.SaveChanges();
        }
    }
}
