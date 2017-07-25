using Domain.Enum;

namespace Domain.Entities
{
    public class ProjetoCategoria
    {
        public long Id { get; private set; }
        public TipoCategoria TipoCategoria { get; private set; }
        public long IdProjeto { get; private set; }
        public long IdCategoria { get; private set; }
        public Projeto Projeto { get; private set; }
        public Categoria Categoria { get; private set; }
    }
}
