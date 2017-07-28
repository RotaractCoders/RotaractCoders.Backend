using Domain.Contracts.Commands;
using FluentValidator;
using System;

namespace Domain.Commands.Inputs
{
    public class CriarClubeInput : Notifiable, ICommand
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Site { get; set; }
        public string Facebook { get; set; }
        public string Email { get; set; }
        public DateTime? DataFundacao { get; set; }
        public string RotaryPadrinho { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string numeroDistrito { get; set; }
    }
}
