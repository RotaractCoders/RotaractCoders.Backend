using System;

namespace Domain.Entities
{
    public class CargoClube
    {
        public long Id { get; private set; }
        public long IdSocioClube { get; private set; }
        public long IdCargo { get; private set; }
        public DateTime De { get; private set; }
        public DateTime Ate { get; private set; }
        public SocioClube Socio { get; private set; }
        public Cargo Cargo { get; private set; }
    }
}
