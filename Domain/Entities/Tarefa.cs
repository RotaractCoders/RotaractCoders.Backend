using Domain.Entities.Base;
using System;

namespace Domain.Entities
{
    public class Tarefa : Entity
    {
        public DateTime? Data { get; private set; }
        public string Descricao { get; private set; }
        public Guid IdProjeto { get; private set; }
        public Projeto Projeto { get; private set; }
    }
}
