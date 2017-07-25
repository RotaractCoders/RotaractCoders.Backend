using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infra.Maps
{
    public class TarefaMap : EntityTypeConfiguration<Tarefa>
    {
        public TarefaMap()
        {
            HasRequired(x => x.Projeto)
                .WithMany(x => x.Tarefas)
                .HasForeignKey(x => x.IdProjeto);
        }
    }
}
