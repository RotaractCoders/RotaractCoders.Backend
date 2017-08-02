using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infra.Repositories
{
    public class MeioDeDivulgacaoProjetoRepository : IMeioDeDivulgacaoProjetoRepository
    {
        private Context _context;

        public MeioDeDivulgacaoProjetoRepository()
        {
            _context = new Context();
        }

        public void ExcluirPorProjeto(Guid idProjeto)
        {
            _context.MeioDeDivulgacaoProjeto
                .Where(x => x.IdProjeto == idProjeto)
                .ToList()
                .ForEach(x => _context.MeioDeDivulgacaoProjeto.Remove(x));

            _context.SaveChanges();
        }

        public MeioDeDivulgacaoProjeto Incluir(MeioDeDivulgacaoProjeto meioDeDivulgacaoProjeto)
        {
            meioDeDivulgacaoProjeto = _context.MeioDeDivulgacaoProjeto.Add(meioDeDivulgacaoProjeto);
            _context.SaveChanges();

            return meioDeDivulgacaoProjeto;
        }
    }
}
