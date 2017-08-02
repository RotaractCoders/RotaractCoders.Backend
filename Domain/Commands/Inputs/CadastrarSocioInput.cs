using Domain.Contracts.Commands;
using FluentValidator;
using System;

namespace Domain.Commands.Inputs
{
    public class CadastroSocioInput : Notifiable, ICommand
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Email { get; set; }
    }
}
