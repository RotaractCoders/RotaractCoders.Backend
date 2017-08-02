using Domain.Entities.Base;
using Domain.Enum;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Cargo : Entity
    {
        public string Nome { get; private set; }
        public TipoCargo TipoCargo { get; private set; }
        public List<CargoClube> CargoClubes { get; private set; }
        public List<CargoDistrito> CargoDistritos { get; private set; }
        public List<CargoRotaractBrasil> CargoRotaractBrasil { get; private set; }

        protected Cargo()
        {

        }

        public Cargo(string nome, TipoCargo tipoCargo)
        {
            Nome = nome;
            TipoCargo = tipoCargo;
        }
    }
}
