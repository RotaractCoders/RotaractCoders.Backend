using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Socio
    {
        public long Id { get; private set; }
        public string Nome { get; private set; }
        public string Apelido { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Email { get; private set; }
        public List<SocioClube> SocioClubes { get; private set; }
        public List<CargoDistrito> CargosDistritais { get; private set; }
    }
}
