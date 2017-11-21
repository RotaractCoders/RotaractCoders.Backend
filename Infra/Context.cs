using Domain.Entities;
using Infra.Maps;
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
        public DbSet<TipoEvento> TipoEvento { get; set; }

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

            modelBuilder.Configurations.Add(new CargoClubeMap());
            modelBuilder.Configurations.Add(new CargoDistritoMap());
            modelBuilder.Configurations.Add(new CargoRotaractBrasilMap());
            modelBuilder.Configurations.Add(new CargoMap());
            modelBuilder.Configurations.Add(new CategoriaMap());
            modelBuilder.Configurations.Add(new ClubeMap());
            modelBuilder.Configurations.Add(new DistritoMap());
            modelBuilder.Configurations.Add(new LancamentoFinanceiroMap());
            modelBuilder.Configurations.Add(new MeioDeDivulgacaoProjetoMap());
            modelBuilder.Configurations.Add(new ObjetivoMap());
            modelBuilder.Configurations.Add(new ParceriaProjetoMap());
            modelBuilder.Configurations.Add(new ParticipanteProjetoMap());
            modelBuilder.Configurations.Add(new ProjetoMap());
            modelBuilder.Configurations.Add(new ProjetoCategoriaMap());
            modelBuilder.Configurations.Add(new PublicoAlvoProjetoMap());
            modelBuilder.Configurations.Add(new SocioClubeMap());
            modelBuilder.Configurations.Add(new SocioMap());
            modelBuilder.Configurations.Add(new TarefaMap());
            modelBuilder.Configurations.Add(new EventoMap());
            modelBuilder.Configurations.Add(new TipoEventoMap());
        }
    }
}
