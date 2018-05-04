using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.OmirBrasil.Results
{
    public class OmirClubeResult
    {
        public string Nome { get; set; }
        public string Site { get; set; }
        public string Facebook { get; set; }
        public string Email { get; set; }
        public DateTime? DataFundacao { get; set; }
        public string RotaryPadrinho { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string NumeroDistrito { get; set; }
        public string Codigo { get; set; }
        public List<OmirClubeSocioResult> Socios { get; set; }
    }
}
