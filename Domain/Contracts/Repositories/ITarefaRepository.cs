using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface ITarefaRepository
    {
        void ExcluirPorProjeto(Guid idProjeto);
        Tarefa Incluir(Tarefa tarefa);
    }
}
