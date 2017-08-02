using Domain.Entities.Base;
using Domain.Enum;
using System;

namespace Domain.Entities
{
    public class Objetivo : Entity
    {
        public TipoObjetivo TipoObjetivo { get; private set; }
        public string Descricao { get; private set; }
        public Guid IdProjeto { get; private set; }
        public Projeto Projeto { get; private set; }

        protected Objetivo()
        {

        }

        public Objetivo(string descricao, Guid idProjeto, TipoObjetivo tipoObjetivo)
        {
            Descricao = descricao;
            IdProjeto = idProjeto;
            TipoObjetivo = tipoObjetivo;
        }
    }
}
