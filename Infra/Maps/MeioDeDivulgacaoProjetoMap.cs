using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infra.Maps
{
    public class MeioDeDivulgacaoProjetoMap : EntityTypeConfiguration<MeioDeDivulgacaoProjeto>
    {
        public MeioDeDivulgacaoProjetoMap()
        {
            HasRequired(x => x.Projeto)
                .WithMany(x => x.MeiosDeDivulgacao)
                .HasForeignKey(x => x.IdProjeto);
        }
    }
}
