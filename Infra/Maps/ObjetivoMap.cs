using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infra.Maps
{
    public class ObjetivoMap : EntityTypeConfiguration<Objetivo>
    {
        public ObjetivoMap()
        {
            HasRequired(x => x.Projeto)
                .WithMany(x => x.Objetivos)
                .HasForeignKey(x => x.IdProjeto);
        }
    }
}
