using System.Collections.Generic;

namespace Domain.Entities
{
    public class Clube
    {
        public long Id { get; private set; }
        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public string Site { get; private set; }
        public string Facebook { get; private set; }
        public string Email { get; private set; }
        public long IdDistrito { get; private set; }
        public Distrito Distrito { get; private set; }
        public List<Projeto> Projetos { get; private set; }
        public List<SocioClube> SociosClube { get; private set; }
        public List<CargoClube> CargosClube { get; private set; }
    }
}
