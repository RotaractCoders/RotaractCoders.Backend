using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infra.Repositories
{
    public class LancamentoFinanceiroRepository : ILancamentoFinanceiroRepository
    {
        private Context _context;

        public LancamentoFinanceiroRepository()
        {
            _context = new Context();
        }

        public void ExcluirPorProjeto(Guid idProjeto)
        {
            _context.LancamentoFinanceiro
                .Where(x => x.IdProjeto == idProjeto)
                .ToList()
                .ForEach(x => _context.LancamentoFinanceiro.Remove(x));

            _context.SaveChanges();
        }

        public LancamentoFinanceiro Incluir(LancamentoFinanceiro lancamentoFinanceiro)
        {
            lancamentoFinanceiro = _context.LancamentoFinanceiro.Add(lancamentoFinanceiro);
            _context.SaveChanges();

            return lancamentoFinanceiro;
        }
    }
}
