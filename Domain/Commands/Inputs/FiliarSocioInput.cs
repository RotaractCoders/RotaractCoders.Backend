using System;

namespace Domain.Commands.Inputs
{
    public class FiliarSocioInput
    {
        public string NumeroDistrito { get; set; }
        public string NomeClube { get; set; }
        public DateTime Posse { get; set; }
        public DateTime Desligamento { get; set; }
    }
}
