using Domain.Commands.Inputs;
using Domain.Entities.Base;
using System;

namespace Domain.Entities
{
    public class Evento : Entity
    {
        public string Nome { get; protected set; }
        public string Realizador { get; protected set; }
        public Guid IdTipoEvento { get; protected set; }
        public string Descricao { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime DataEvento { get; protected set; }

        public TipoEvento TipoEvento { get; protected set; }

        protected Evento()
        {

        }

        public Evento(IncluirEventoInput input)
        {
            Nome = input.Nome;
            Realizador = input.Realizador;
            IdTipoEvento = input.IdTipoEvento;
            Descricao = input.Descricao;
            DataEvento = input.DataEvento;
            DataCriacao = DateTime.Now;
        }

        public void Atualizar(AtualizarEventoInput input)
        {
            Nome = input.Nome;
            Realizador = input.Realizador;
            IdTipoEvento = input.IdTipoEvento;
            Descricao = input.Descricao;
            DataEvento = input.DataEvento;
        }
    }
}
