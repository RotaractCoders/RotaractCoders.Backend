using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infra.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private Context _context;

        public CategoriaRepository()
        {
            _context = new Context();
        }

        public Categoria Buscar(string nome)
        {
            return _context.Categoria.FirstOrDefault(x => x.Nome == nome);
        }

        public Categoria Incluir(Categoria categoria)
        {
            categoria = _context.Categoria.Add(categoria);
            _context.SaveChanges();

            return categoria;
        }
    }
}
