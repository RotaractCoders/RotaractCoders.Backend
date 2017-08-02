using Domain.Contracts.Commands;
using FluentValidator;
using System;
using System.Collections.Generic;

namespace Domain.Commands.Inputs
{
    public class CadastroCargoRotaractBrasilInput : Notifiable, ICommand
    {
        public int CodigoSocio { get; set; }
        public List<CargoRotaractBrasilInput> Lista { get; set; } = new List<CargoRotaractBrasilInput>();
    }

    public class CargoRotaractBrasilInput
    {
        public string Cargo { get; set; }
        public DateTime De { get; set; }
        public DateTime Ate { get; set; }
    }
}
