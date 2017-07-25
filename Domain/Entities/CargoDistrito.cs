using System;

namespace Domain.Entities
{
    public class CargoDistrito
    {
        public long Id { get; private set; }
        public long IdSocio { get; private set; }
        public long IdDistrito { get; private set; }
        public long IdCargo { get; private set; }
        public DateTime De { get; private set; }
        public DateTime Ate { get; private set; }
        public Socio Socio { get; private set; }
        public Distrito Distrito { get; private set; }
        public Cargo Cargo { get; private set; }
    }
}
