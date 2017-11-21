using Domain.Entities.Base;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class TipoEvento : Entity
    {
        public string Descricao { get; protected set; }

        public List<Evento> Eventos { get; protected set; }

        protected TipoEvento()
        {

        }

        public TipoEvento(string descricao)
        {
            Descricao = descricao;
        }
    }
}
