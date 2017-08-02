using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infra.Repositories
{
    public class ObjetivoRepository : IObjetivoRepository
    {
        private Context _context;

        public ObjetivoRepository()
        {
            _context = new Context();
        }

        public void ExcluirPorProjeto(Guid idProjeto)
        {
            _context.Objetivo
                .Where(x => x.IdProjeto == idProjeto)
                .ToList()
                .ForEach(x => _context.Objetivo.Remove(x));

            _context.SaveChanges();
        }

        public Objetivo Incluir(Objetivo objetivo)
        {
            objetivo = _context.Objetivo.Add(objetivo);
            _context.SaveChanges();

            return objetivo;
        }
    }
}
