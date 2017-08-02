using Domain.Entities.Base;
using System;

namespace Domain.Entities
{
    public class CargoClube : Entity
    {
        public Guid IdSocio { get; private set; }
        public Guid IdCargo { get; private set; }
        public Guid IdClube { get; private set; }
        public DateTime De { get; private set; }
        public DateTime Ate { get; private set; }
        public Socio Socio { get; private set; }
        public Cargo Cargo { get; private set; }
        public Clube Clube { get; private set; }

        protected CargoClube()
        {

        }

        public CargoClube(Guid idSocio, Guid idCargo, Guid idClube, DateTime de, DateTime ate)
        {
            IdSocio = idSocio;
            IdCargo = idCargo;
            IdClube = idClube;
            De = de;
            Ate = ate;
        }
    }
}
