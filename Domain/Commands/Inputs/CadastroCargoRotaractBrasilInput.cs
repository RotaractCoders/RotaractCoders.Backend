using System;

namespace Domain.Commands.Inputs
{
    public class CadastroCargoRotaractBrasilInput
    {
        public string Cargo { get; set; }
        public DateTime De { get; set; }
        public DateTime Ate { get; set; }
    }
}
