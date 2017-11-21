using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Inputs
{
    public class IncluirEventoInput
    {
        public string Nome { get; set; }
        public string Realizador { get; set; }
        public Guid IdTipoEvento { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEvento { get; set; }
    }
}
