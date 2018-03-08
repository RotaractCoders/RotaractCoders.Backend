using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.OmirBrasil.Results
{
    public class OmirSocioResult
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string FotoUrl { get; set; }
        public string Apelido { get; set; }
        public DateTime? DataNascimento { get; set; }

        public List<OmirSocioClubeResult> Clubes { get; set; }
        public List<OmirSocioCargoClubeResult> CargosClube { get; set; } = new List<OmirSocioCargoClubeResult>();
        public List<OmirSocioCargoDistritalResult> CargosDistritais { get; set; } = new List<OmirSocioCargoDistritalResult>();
        public List<OmirSocioCargoRotaractBrasilResult> CargosRotaractBrasil { get; set; } = new List<OmirSocioCargoRotaractBrasilResult>();
    }
}
