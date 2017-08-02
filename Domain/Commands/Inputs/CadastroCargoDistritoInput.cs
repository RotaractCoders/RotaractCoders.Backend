using Domain.Contracts.Commands;
using FluentValidator;
using System;
using System.Collections.Generic;

namespace Domain.Commands.Inputs
{
    public class CadastroCargoDistritoInput : Notifiable, ICommand
    {
        public int CodigoSocio { get; set; }
        public List<CargoDistritoInput> Lista { get; set; } = new List<CargoDistritoInput>();
    }

    public class CargoDistritoInput
    {
        public string Cargo { get; set; }
        public string Distrito { get; set; }
        public DateTime? De { get; set; }
        public DateTime? Ate { get; set; }
    }
}
