using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Inputs
{
    public class ListaProjetosInput
    {
        public Guid IdCategoria { get; set; }
        public Guid IdDistrito { get; set; }
        public Guid IdClube { get; set; }
    }
}
