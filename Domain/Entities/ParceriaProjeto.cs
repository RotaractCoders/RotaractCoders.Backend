using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ParceriaProjeto : Entity
    {
        public string Descricao { get; private set; }
        public Guid IdProjeto { get; private set; }

        public Projeto Projeto { get; private set; }

        public ParceriaProjeto(string descricao, Guid idProjeto)
        {
            Descricao = descricao;
            IdProjeto = idProjeto;
        }
    }
}
