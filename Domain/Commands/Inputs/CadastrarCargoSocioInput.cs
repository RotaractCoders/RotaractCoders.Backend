using Domain.Contracts.Commands;
using FluentValidator;
using System;
using System.Collections.Generic;

namespace Domain.Commands.Inputs
{
    public class CadastrarCargoSocioInput : Notifiable, ICommand
    {
        public int CodigoSocio { get; set; }
        public List<CargoSocioInput> Lista { get; set; } = new List<CargoSocioInput>();
    }

    public class CargoSocioInput
    {
        public string Cargo { get; set; }
        public string Clube { get; set; }
        public DateTime De { get; set; }
        public DateTime Ate { get; set; }
    }
}
