using Domain.Enum;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Cargo
    {
        public long Id { get; private set; }
        public string Nome { get; private set; }
        public TipoCargo TipoCargo { get; private set; }
        public List<CargoClube> CargoClubes { get; private set; }
        public List<CargoDistrito> CargoDistritos { get; private set; }
        public List<CargoRotaractBrasil> CargoRotaractBrasil { get; private set; }
    }
}
