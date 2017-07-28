using System;

namespace Domain.Commands.Inputs
{
    public class CadastroCargoDistritoInput
    {
        public string Cargo { get; set; }
        public string Distrito { get; set; }
        public DateTime De { get; set; }
        public DateTime Ate { get; set; }
    }
}
