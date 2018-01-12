using Domain.Contracts.Commands;
using FluentValidator;
using System;
using System.Collections.Generic;

namespace Domain.Commands.Inputs
{
    public class CadastrarCargoSocioInput : Notifiable, ICommand
    {
        public string Nome { get; set; }
        public string TipoCargo { get; set; }
        public int GestaoDe { get; set; }
        public int GestaoAte { get; set; }
    }
}
