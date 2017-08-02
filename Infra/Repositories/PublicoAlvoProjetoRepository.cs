using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infra.Repositories
{
    public class PublicoAlvoProjetoRepository : IPublicoAlvoProjetoRepository
    {
        private Context _context;

        public PublicoAlvoProjetoRepository()
        {
            _context = new Context();
        }

        public void ExcluirPorProjeto(Guid idProjeto)
        {
            _context.PublicoAlvoProjeto
                .Where(x => x.IdProjeto == idProjeto)
                .ToList()
                .ForEach(x => _context.PublicoAlvoProjeto.Remove(x));

            _context.SaveChanges();
        }

        public PublicoAlvoProjeto Incluir(PublicoAlvoProjeto publicoAlvoProjeto)
        {
            publicoAlvoProjeto = _context.PublicoAlvoProjeto.Add(publicoAlvoProjeto);
            _context.SaveChanges();

            return publicoAlvoProjeto;
        }
    }
}
