using Domain.Entities.Base;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Distrito : Entity
    {
        public string Numero { get; private set; }
        public int Regiao { get; private set; }
        public string Mascote { get; private set; }
        public string Site { get; set; }
        public string Email { get; set; }
        public List<Clube> Clubes { get; private set; }
        public List<CargoDistrito> CargosDistritais { get; private set; }
    }
}
