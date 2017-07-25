using Domain.Entities;
using Infra.Maps;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Infra
{
    public class Context : DbContext
    {
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Clube> Clube { get; set; }
        public DbSet<Distrito> Distrito { get; set; }
        public DbSet<LancamentoFinanceiro> LancamentoFinanceiro { get; set; }
        public DbSet<Objetivo> Objetivo { get; set; }
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<ProjetoCategoria> ProjetoCategoria { get; set; }
        public DbSet<Tarefa> Tarefa { get; set; }

        public Context()
            : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ControleCasaDB;Integrated Security=True")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == "Id")
                .Configure(p => p
                    .IsKey()
                    .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Configurations.Add(new CategoriaMap());
            modelBuilder.Configurations.Add(new ClubeMap());
            modelBuilder.Configurations.Add(new DistritoMap());
            modelBuilder.Configurations.Add(new LancamentoFinanceiroMap());
            modelBuilder.Configurations.Add(new ObjetivoMap());
            modelBuilder.Configurations.Add(new ProjetoMap());
            modelBuilder.Configurations.Add(new ProjetoCategoriaMap());
            modelBuilder.Configurations.Add(new TarefaMap());
        }
    }
}
