using Domain.Entities.Base;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Categoria : Entity
    {
        public string Nome { get; private set; }
        public List<ProjetoCategoria> ProjetosCategoria { get; private set; }

        protected Categoria()
        {

        }

        public Categoria(string nome)
        {
            Nome = nome;
        }
    }
}
