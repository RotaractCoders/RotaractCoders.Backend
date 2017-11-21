using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infra.Maps
{
    public class EventoMap : EntityTypeConfiguration<Evento>
    {
        public EventoMap()
        {
            HasRequired(x => x.TipoEvento)
                .WithMany(x => x.Eventos)
                .HasForeignKey(x => x.IdTipoEvento);
        }
    }
}
