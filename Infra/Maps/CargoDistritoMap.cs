using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infra.Maps
{
    public class CargoDistritoMap : EntityTypeConfiguration<CargoDistrito>
    {
        public CargoDistritoMap()
        {
            HasRequired(x => x.Cargo)
                .WithMany(x => x.CargoDistritos)
                .HasForeignKey(x => x.IdCargo);

            HasRequired(x => x.Socio)
                .WithMany(x => x.CargosDistritais)
                .HasForeignKey(x => x.IdSocio);

            HasRequired(x => x.Distrito)
                .WithMany(x => x.CargosDistritais)
                .HasForeignKey(x => x.IdDistrito);
        }
    }
}
