using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class SocioClube
    {
        public long Id { get; private set; }
        public long IdClube { get; private set; }
        public long IdSocio { get; private set; }
        public DateTime Posse { get; private set; }
        public DateTime? Desligamento { get; private set; }
        public Clube Clube { get; private set; }
        public Socio Socio { get; private set; }
    }
}
