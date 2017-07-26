using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infra.Maps
{
    public class SocioClubeMap : EntityTypeConfiguration<SocioClube>
    {
        public SocioClubeMap()
        {
            HasRequired(x => x.Clube)
                .WithMany(x => x.SociosClube)
                .HasForeignKey(x => x.IdClube);

            HasRequired(x => x.Socio)
                .WithMany(x => x.SocioClubes)
                .HasForeignKey(x => x.IdSocio);
        }
    }
}
