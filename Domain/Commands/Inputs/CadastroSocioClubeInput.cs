using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Inputs
{
    public class CadastroSocioClubeInput
    {
        public string NumeroDistrito { get; set; }
        public string NomeClube { get; set; }
        public DateTime Posse { get; set; }
        public DateTime Desligamento { get; set; }
    }
}
