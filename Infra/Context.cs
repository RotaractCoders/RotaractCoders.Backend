using Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Infra
{
    public class Context : DbContext
    {
        public DbSet<CargoClube> CargoClube { get; set; }
        public DbSet<CargoDistrito> CargoDistrito { get; set; }
        public DbSet<CargoRotaractBrasil> CargoRotaractBrasil { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Clube> Clube { get; set; }
        public DbSet<Distrito> Distrito { get; set; }
        public DbSet<LancamentoFinanceiro> LancamentoFinanceiro { get; set; }
        public DbSet<MeioDeDivulgacaoProjeto> MeioDeDivulgacaoProjeto { get; set; }
        public DbSet<Objetivo> Objetivo { get; set; }
        public DbSet<ParceriaProjeto> ParceriaProjeto { get; set; }
        public DbSet<ParticipanteProjeto> ParticipanteProjeto { get; set; }
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<ProjetoCategoria> ProjetoCategoria { get; set; }
        public DbSet<PublicoAlvoProjeto> PublicoAlvoProjeto { get; set; }
        public DbSet<SocioClube> SocioClube { get; set; }
        public DbSet<Socio> Socio { get; set; }
        public DbSet<Tarefa> Tarefa { get; set; }
        public DbSet<Evento> Evento { get; set; }

        public Context()
            : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RotaractDB;Integrated Security=True")
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

            Database.SetInitializer<Context>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
