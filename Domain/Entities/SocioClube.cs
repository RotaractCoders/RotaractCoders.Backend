using Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class SocioClube : Entity
    {
        public Guid IdClube { get; private set; }
        public Guid IdSocio { get; private set; }
        public DateTime Posse { get; private set; }
        public DateTime? Desligamento { get; private set; }
        public Clube Clube { get; private set; }
        public Socio Socio { get; private set; }
        public List<CargoClube> CargosClube { get; set; }
    }
}
