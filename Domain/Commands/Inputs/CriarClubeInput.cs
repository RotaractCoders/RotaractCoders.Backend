using Domain.Contracts.Commands;
using FluentValidator;
using System;
using System.Collections.Generic;

namespace Domain.Commands.Inputs
{
    public class CriarClubeInput : Notifiable, ICommand
    {
        public string RowKey { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Site { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Email { get; set; }
        public DateTime? DataFundacao { get; set; }
        public string RotaryPadrinho { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string NumeroDistrito { get; set; }
        public string Programa { get; set; }
    }
}
