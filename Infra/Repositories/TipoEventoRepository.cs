using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Commands.Results;
using Domain.Entities;

namespace Infra.Repositories
{
    public class TipoEventoRepository : ITipoEventoRepository
    {
        protected Context _context;

        public TipoEventoRepository()
        {
            _context = new Context();
        }

        public List<ListaTipoEventoResult> Listar()
        {
            return _context.TipoEvento.ToList()
                .Select(x => new ListaTipoEventoResult
                {
                    Id = x.Id,
                    Descricao = x.Descricao
                }).ToList();
        }

        public void Incluir(TipoEvento tipoEvento)
        {
            _context.TipoEvento.Add(tipoEvento);
            _context.SaveChanges();
        }

        public void Deletar(Guid id)
        {
            _context.TipoEvento.Remove(_context.TipoEvento.FirstOrDefault(x => x.Id == id));
            _context.SaveChanges();
        }
    }
}
