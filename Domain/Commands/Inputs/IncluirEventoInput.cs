using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Inputs
{
    public class IncluirEventoInput
    {
        public string RowKey { get; set; }
        public string Nome { get; set; }
        public string Realizador { get; set; }
        public string TipoEvento { get; set; }
        public string Programa { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEvento { get; set; }
    }
}
