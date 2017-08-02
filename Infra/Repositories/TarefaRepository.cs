using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infra.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private Context _context;

        public TarefaRepository()
        {
            _context = new Context();
        }

        public void ExcluirPorProjeto(Guid idProjeto)
        {
            _context.Tarefa
                .Where(x => x.IdProjeto == idProjeto)
                .ToList()
                .ForEach(x => _context.Tarefa.Remove(x));

            _context.SaveChanges();
        }

        public Tarefa Incluir(Tarefa tarefa)
        {
            tarefa = _context.Tarefa.Add(tarefa);
            _context.SaveChanges();

            return tarefa;
        }
    }
}
