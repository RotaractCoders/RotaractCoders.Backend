using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Maps
{
    public class ParceriaProjetoMap : EntityTypeConfiguration<ParceriaProjeto>
    {
        public ParceriaProjetoMap()
        {
            HasRequired(x => x.Projeto)
                .WithMany(x => x.Parcerias)
                .HasForeignKey(x => x.IdProjeto);
        }
    }
}
