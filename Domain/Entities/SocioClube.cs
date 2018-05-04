using Domain.Commands.Inputs;
using Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class SocioClube
    {
        public string NumeroDistrito { get; set; }
        public string Nome { get; set; }
        public DateTime? Posse { get; set; }
        public DateTime? Desligamento { get; set; }

        public SocioClube(string numeroDistrito, string nome, DateTime? posse, DateTime? desligamento)
        {
            NumeroDistrito = numeroDistrito;
            Nome = nome;
            Posse = posse;
            Desligamento = desligamento;
        }
    }
}
