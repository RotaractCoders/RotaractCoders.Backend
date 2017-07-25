using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Maps
{
    public class ProjetoCategoriaMap : EntityTypeConfiguration<ProjetoCategoria>
    {
        public ProjetoCategoriaMap()
        {
            HasRequired(x => x.Projeto)
                .WithMany(x => x.ProjetoCategorias)
                .HasForeignKey(x => x.IdProjeto);

            HasRequired(x => x.Categoria)
                .WithMany(x => x.ProjetosCategoria)
                .HasForeignKey(x => x.IdCategoria);
        }
    }
}
