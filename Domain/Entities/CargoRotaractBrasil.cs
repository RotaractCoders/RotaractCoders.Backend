using Domain.Entities.Base;
using System;

namespace Domain.Entities
{
    public class CargoRotaractBrasil : Entity
    {
        public Guid IdSocio { get; private set; }
        public Guid IdCargo { get; private set; }
        public DateTime De { get; private set; }
        public DateTime Ate { get; private set; }
        public Socio Socio { get; private set; }
        public Cargo Cargo { get; private set; }
    }
}
