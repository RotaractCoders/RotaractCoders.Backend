using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Commands.Results;
using Domain.Entities;
using System.Data.Entity;

namespace Infra.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        protected Context _context;

        public EventoRepository()
        {
            _context = new Context();
        }

        public List<ListaEventoResult> Listar()
        {
            return _context.Evento
                .ToList()
                .Select(x => new ListaEventoResult
                {
                    Id = x.Id,
                    Nome = x.Nome
                })
                .ToList();
        }

        public DetalheEventoResult Buscar(Guid id)
        {
            var retorno = _context.Evento.FirstOrDefault(x => x.Id == id);

            if (retorno == null)
                return null;

            return new DetalheEventoResult
            {
                Id = retorno.Id,
                DataCriacao = retorno.DataCriacao,
                DataEvento = retorno.DataEvento,
                Descricao = retorno.Descricao,
                IdTipoEvento = retorno.IdTipoEvento,
                Nome = retorno.Nome,
                Realizador = retorno.Realizador
            };
        }

        public Evento Obter(Guid id)
        {
            return _context.Evento.FirstOrDefault(x => x.Id == id);
        }

        public void Incluir(Evento evento)
        {
            _context.Evento.Add(evento);
            _context.SaveChanges();
        }

        public void Atualizar(Evento evento)
        {
            _context.Entry(evento).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(Guid id)
        {
            _context.Evento.Remove(_context.Evento.FirstOrDefault(x => x.Id == id));
            _context.SaveChanges();
        }
    }
}
