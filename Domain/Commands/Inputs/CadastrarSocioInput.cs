using Domain.Contracts.Commands;
using FluentValidator;
using System;

namespace Domain.Commands.Inputs
{
    public class CadastrarSocioInput : Notifiable, ICommand
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
    }
}
