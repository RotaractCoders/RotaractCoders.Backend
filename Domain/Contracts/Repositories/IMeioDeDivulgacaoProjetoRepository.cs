using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IMeioDeDivulgacaoProjetoRepository
    {
        void ExcluirPorProjeto(Guid idProjeto);
        MeioDeDivulgacaoProjeto Incluir(MeioDeDivulgacaoProjeto meioDeDivulgacaoProjeto);
    }
}
