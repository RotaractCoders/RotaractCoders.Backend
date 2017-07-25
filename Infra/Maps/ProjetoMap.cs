using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infra.Maps
{
    public class ProjetoMap : EntityTypeConfiguration<Projeto>
    {
        public ProjetoMap()
        {
            HasRequired(x => x.Clube)
                .WithMany(x => x.Projetos)
                .HasForeignKey(x => x.IdClube);
        }
    }
}
