using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Maps
{
    public class ParticipanteProjetoMap : EntityTypeConfiguration<ParticipanteProjeto>
    {
        public ParticipanteProjetoMap()
        {
            HasRequired(x => x.Projeto)
                .WithMany(x => x.Participantes)
                .HasForeignKey(x => x.IdProjeto);
        }
    }
}
