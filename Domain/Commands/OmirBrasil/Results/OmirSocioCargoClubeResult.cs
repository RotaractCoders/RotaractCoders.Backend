using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.OmirBrasil.Results
{
    public class OmirSocioCargoClubeResult
    {
        public string NomeCargo { get; set; }
        public string Clube { get; set; }
        public DateTime? De { get; set; }
        public DateTime? Ate { get; set; }
    }
}
