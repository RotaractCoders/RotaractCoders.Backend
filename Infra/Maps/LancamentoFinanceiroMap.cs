using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infra.Maps
{
    public class LancamentoFinanceiroMap : EntityTypeConfiguration<LancamentoFinanceiro>
    {
        public LancamentoFinanceiroMap()
        {
            HasRequired(x => x.Projeto)
                .WithMany(x => x.LancamentosFinanceiros)
                .HasForeignKey(x => x.IdProjeto);
        }
    }
}
