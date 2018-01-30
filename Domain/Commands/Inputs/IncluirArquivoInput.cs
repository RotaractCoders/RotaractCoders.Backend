using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Inputs
{
    public class IncluirArquivoInput
    {
        public string RowKey { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Link { get; set; }
    }
}
