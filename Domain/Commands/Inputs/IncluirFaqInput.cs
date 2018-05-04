using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Inputs
{
    public class IncluirFaqInput
    {
        public string RowKey { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
        public int Posicao { get; set; }
    }
}
