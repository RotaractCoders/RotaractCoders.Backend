using Domain.Commands.Inputs;
using Domain.Entities.Base;
using FluentValidator;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class SocioClube : Entity
    {
        public Guid IdClube { get; private set; }
        public Guid IdSocio { get; private set; }
        public DateTime Posse { get; private set; }
        public DateTime? Desligamento { get; private set; }
        public Clube Clube { get; private set; }
        public Socio Socio { get; private set; }

        protected SocioClube()
        {

        }

        public SocioClube(Guid idClube, Guid idSocio, DateTime posse, DateTime? desligamento)
        {
            IdClube = idClube;
            IdSocio = idSocio;
            Posse = posse;
            Desligamento = desligamento;
        }
    }
}
