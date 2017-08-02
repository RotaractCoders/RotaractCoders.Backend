using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Data.Entity;

namespace Infra.Repositories
{
    public class ProjetoRepository : IProjetoRepository
    {
        private Context _context;

        public ProjetoRepository()
        {
            _context = new Context();
        }

        public void Atualizar(Projeto projeto)
        {
            _context.Entry(projeto).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Projeto Buscar(int codigo)
        {
            return _context.Projeto.FirstOrDefault(x => x.Codigo == codigo);
        }

        public Projeto Incluir(Projeto projeto)
        {
            projeto = _context.Projeto.Add(projeto);
            _context.SaveChanges();

            return projeto;
        }
    }
}
