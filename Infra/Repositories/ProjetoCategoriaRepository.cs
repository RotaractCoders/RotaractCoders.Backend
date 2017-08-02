using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infra.Repositories
{
    public class ProjetoCategoriaRepository : IProjetoCategoriaRepository
    {
        private Context _context;

        public ProjetoCategoriaRepository()
        {
            _context = new Context();
        }

        public void ExcluirPorProjeto(Guid idProjeto)
        {
            _context.ProjetoCategoria
                .Where(x => x.IdProjeto == idProjeto)
                .ToList()
                .ForEach(x => _context.ProjetoCategoria.Remove(x));

            _context.SaveChanges();
        }

        public ProjetoCategoria Incluir(ProjetoCategoria projetoCategoria)
        {
            projetoCategoria = _context.ProjetoCategoria.Add(projetoCategoria);
            _context.SaveChanges();

            return projetoCategoria;
        }
    }
}
