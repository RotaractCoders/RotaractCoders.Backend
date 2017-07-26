using Domain.Entities.Base;
using System;

namespace Domain.Entities
{
    public class CargoClube : Entity
    {
        public Guid IdSocioClube { get; private set; }
        public Guid IdCargo { get; private set; }
        public DateTime De { get; private set; }
        public DateTime Ate { get; private set; }
        public SocioClube SocioClube { get; private set; }
        public Cargo Cargo { get; private set; }
    }
}
