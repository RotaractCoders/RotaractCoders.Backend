using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infra.Maps
{
    public class ClubeMap : EntityTypeConfiguration<Clube>
    {
        public ClubeMap()
        {
            HasRequired(x => x.Distrito)
                .WithMany(x => x.Clubes)
                .HasForeignKey(x => x.IdDistrito);
        }
    }
}
