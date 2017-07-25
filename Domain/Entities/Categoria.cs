using System.Collections.Generic;

namespace Domain.Entities
{
    public class Categoria
    {
        public long Id { get; private set; }
        public string Nome { get; private set; }
        public List<ProjetoCategoria> ProjetosCategoria { get; private set; }
    }
}
