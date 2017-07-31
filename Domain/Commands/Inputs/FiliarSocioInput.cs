using Domain.Contracts.Commands;
using FluentValidator;
using System;
using System.Collections.Generic;

namespace Domain.Commands.Inputs
{
    public class FiliarSocioListInput : Notifiable, ICommand
    {
        public List<FiliarSocioInput> Lista { get; set; } = new List<FiliarSocioInput>();
        public int CodigoSocio { get; set; }
    }

    public class FiliarSocioInput
    {
        public string NumeroDistrito { get; set; }
        public string NomeClube { get; set; }
        public DateTime Posse { get; set; }
        public DateTime Desligamento { get; set; }
    }
}
