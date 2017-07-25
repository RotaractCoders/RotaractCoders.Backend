using Domain.Entities.Base;
using Domain.Enum;
using System;

namespace Domain.Entities
{
    public class ProjetoCategoria : Entity
    {
        public TipoCategoria TipoCategoria { get; private set; }
        public Guid IdProjeto { get; private set; }
        public Guid IdCategoria { get; private set; }
        public Projeto Projeto { get; private set; }
        public Categoria Categoria { get; private set; }
    }
}
