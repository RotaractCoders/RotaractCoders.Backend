using Domain.Contracts.Commands;
using FluentValidator;
using System;
using System.Collections.Generic;

namespace Domain.Commands.Inputs
{
    public class CadastroSocioInput : Notifiable, ICommand
    {
        public string RowKey { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Celular { get; set; }
        public string Clube { get; set; }
        public List<CadastrarCargoSocioInput> Cargos { get; set; }
    }
}
