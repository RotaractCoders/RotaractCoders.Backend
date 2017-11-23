﻿using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infra.Maps
{
    public class CargoClubeMap : EntityTypeConfiguration<CargoClube>
    {
        public CargoClubeMap()
        {
            HasRequired(x => x.Cargo)
                .WithMany(x => x.CargoClubes)
                .HasForeignKey(x => x.IdCargo);

            HasRequired(x => x.Socio)
                .WithMany(x => x.CargosClube)
                .HasForeignKey(x => x.IdSocio);

            HasRequired(x => x.Clube)
                .WithMany(x => x.CargosClube)
                .HasForeignKey(x => x.IdClube);
        }
    }
}