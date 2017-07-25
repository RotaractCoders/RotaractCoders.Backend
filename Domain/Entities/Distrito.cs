using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Distrito
    {
        public long Id { get; private set; }
        public string Numero { get; private set; }
        public List<Clube> Clubes { get; private set; }
    }
}
