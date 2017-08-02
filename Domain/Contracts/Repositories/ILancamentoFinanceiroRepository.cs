using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface ILancamentoFinanceiroRepository
    {
        void ExcluirPorProjeto(Guid idProjeto);
        LancamentoFinanceiro Incluir(LancamentoFinanceiro lancamentoFinanceiro);
    }
}
