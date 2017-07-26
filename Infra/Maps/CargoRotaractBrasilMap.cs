using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infra.Maps
{
    public class CargoRotaractBrasilMap : EntityTypeConfiguration<CargoRotaractBrasil>
    {
        public CargoRotaractBrasilMap()
        {
            HasRequired(x => x.Cargo)
                .WithMany(x => x.CargoRotaractBrasil)
                .HasForeignKey(x => x.IdCargo);

            HasRequired(x => x.Socio)
                .WithMany(x => x.CargosRotaractBrasil)
                .HasForeignKey(x => x.IdSocio);
        }
    }
}
