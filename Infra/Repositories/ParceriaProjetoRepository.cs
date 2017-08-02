using Domain.Contracts.Repositories;
using System;
using System.Linq;
using Domain.Entities;

namespace Infra.Repositories
{
    public class ParceriaProjetoRepository : IParceriaProjetoRepository
    {
        private Context _context;

        public ParceriaProjetoRepository()
        {
            _context = new Context();
        }

        public void ExcluirPorProjeto(Guid idProjeto)
        {
            _context.ParceriaProjeto
                .Where(x => x.IdProjeto == idProjeto)
                .ToList()
                .ForEach(x => _context.ParceriaProjeto.Remove(x));

            _context.SaveChanges();
        }

        public ParceriaProjeto Incluir(ParceriaProjeto parceriaProjeto)
        {
            parceriaProjeto = _context.ParceriaProjeto.Add(parceriaProjeto);
            _context.SaveChanges();

            return parceriaProjeto;
        }
    }
}
