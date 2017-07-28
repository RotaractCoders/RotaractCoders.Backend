using System;

namespace Domain.Commands.Inputs
{
    public class CadastrarCargoSocioInput
    {
        public string Cargo { get; set; }
        public string Clube { get; set; }
        public DateTime De { get; set; }
        public DateTime Ate { get; set; }
    }
}
