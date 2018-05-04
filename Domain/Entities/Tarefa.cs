using Domain.Entities.Base;
using System;

namespace Domain.Entities
{
    public class Tarefa : Entity
    {
        public DateTime? Data { get; set; }
        public string Descricao { get; set; }

        protected Tarefa()
        {

        }

        public Tarefa(DateTime? data, string descricao)
        {
            Data = data;
            Descricao = descricao;
        }
    }
}
