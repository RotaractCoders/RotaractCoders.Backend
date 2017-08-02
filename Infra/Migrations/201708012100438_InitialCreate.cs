namespace Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cargo",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Nome = c.String(maxLength: 8000, unicode: false),
                        TipoCargo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CargoClube",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        IdSocio = c.Guid(nullable: false),
                        IdCargo = c.Guid(nullable: false),
                        IdClube = c.Guid(nullable: false),
                        De = c.DateTime(nullable: false),
                        Ate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cargo", t => t.IdCargo)
                .ForeignKey("dbo.Clube", t => t.IdClube)
                .ForeignKey("dbo.Socio", t => t.IdSocio)
                .Index(t => t.IdSocio)
                .Index(t => t.IdCargo)
                .Index(t => t.IdClube);
            
            CreateTable(
                "dbo.Clube",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        Nome = c.String(maxLength: 8000, unicode: false),
                        Site = c.String(maxLength: 8000, unicode: false),
                        Facebook = c.String(maxLength: 8000, unicode: false),
                        Email = c.String(maxLength: 8000, unicode: false),
                        DataFundacao = c.DateTime(),
                        RotaryPadrinho = c.String(maxLength: 8000, unicode: false),
                        DataFechamento = c.DateTime(),
                        IdDistrito = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Distrito", t => t.IdDistrito)
                .Index(t => t.IdDistrito);
            
            CreateTable(
                "dbo.Distrito",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Numero = c.String(maxLength: 8000, unicode: false),
                        Regiao = c.Int(nullable: false),
                        Mascote = c.String(maxLength: 8000, unicode: false),
                        Site = c.String(maxLength: 8000, unicode: false),
                        Email = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CargoDistrito",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        IdSocio = c.Guid(nullable: false),
                        IdDistrito = c.Guid(nullable: false),
                        IdCargo = c.Guid(nullable: false),
                        De = c.DateTime(nullable: false),
                        Ate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cargo", t => t.IdCargo)
                .ForeignKey("dbo.Distrito", t => t.IdDistrito)
                .ForeignKey("dbo.Socio", t => t.IdSocio)
                .Index(t => t.IdSocio)
                .Index(t => t.IdDistrito)
                .Index(t => t.IdCargo);
            
            CreateTable(
                "dbo.Socio",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        Nome = c.String(maxLength: 8000, unicode: false),
                        Apelido = c.String(maxLength: 8000, unicode: false),
                        DataNascimento = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CargoRotaractBrasil",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        IdSocio = c.Guid(nullable: false),
                        IdCargo = c.Guid(nullable: false),
                        De = c.DateTime(nullable: false),
                        Ate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cargo", t => t.IdCargo)
                .ForeignKey("dbo.Socio", t => t.IdSocio)
                .Index(t => t.IdSocio)
                .Index(t => t.IdCargo);
            
            CreateTable(
                "dbo.SocioClube",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        IdClube = c.Guid(nullable: false),
                        IdSocio = c.Guid(nullable: false),
                        Posse = c.DateTime(nullable: false),
                        Desligamento = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clube", t => t.IdClube)
                .ForeignKey("dbo.Socio", t => t.IdSocio)
                .Index(t => t.IdClube)
                .Index(t => t.IdSocio);
            
            CreateTable(
                "dbo.Projeto",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        DataUltimaAtualizacao = c.DateTime(),
                        Nome = c.String(maxLength: 8000, unicode: false),
                        Justificativa = c.String(maxLength: 8000, unicode: false),
                        DataInicio = c.DateTime(),
                        DataFim = c.DateTime(),
                        DataFinalizacao = c.DateTime(),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                        Fotos = c.String(maxLength: 8000, unicode: false),
                        Resultados = c.String(maxLength: 8000, unicode: false),
                        Dificuldade = c.String(maxLength: 8000, unicode: false),
                        PalavraChave = c.String(maxLength: 8000, unicode: false),
                        LicoesAprendidas = c.String(maxLength: 8000, unicode: false),
                        Resumo = c.String(maxLength: 8000, unicode: false),
                        IdClube = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clube", t => t.IdClube)
                .Index(t => t.IdClube);
            
            CreateTable(
                "dbo.LancamentoFinanceiro",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdProjeto = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projeto", t => t.IdProjeto)
                .Index(t => t.IdProjeto);
            
            CreateTable(
                "dbo.Objetivo",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        TipoObjetivo = c.Int(nullable: false),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                        IdProjeto = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projeto", t => t.IdProjeto)
                .Index(t => t.IdProjeto);
            
            CreateTable(
                "dbo.ProjetoCategoria",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        TipoCategoria = c.Int(nullable: false),
                        IdProjeto = c.Guid(nullable: false),
                        IdCategoria = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.IdCategoria)
                .ForeignKey("dbo.Projeto", t => t.IdProjeto)
                .Index(t => t.IdProjeto)
                .Index(t => t.IdCategoria);
            
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Nome = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tarefa",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Data = c.DateTime(),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                        IdProjeto = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projeto", t => t.IdProjeto)
                .Index(t => t.IdProjeto);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CargoClube", "IdSocio", "dbo.Socio");
            DropForeignKey("dbo.CargoClube", "IdClube", "dbo.Clube");
            DropForeignKey("dbo.Tarefa", "IdProjeto", "dbo.Projeto");
            DropForeignKey("dbo.ProjetoCategoria", "IdProjeto", "dbo.Projeto");
            DropForeignKey("dbo.ProjetoCategoria", "IdCategoria", "dbo.Categoria");
            DropForeignKey("dbo.Objetivo", "IdProjeto", "dbo.Projeto");
            DropForeignKey("dbo.LancamentoFinanceiro", "IdProjeto", "dbo.Projeto");
            DropForeignKey("dbo.Projeto", "IdClube", "dbo.Clube");
            DropForeignKey("dbo.Clube", "IdDistrito", "dbo.Distrito");
            DropForeignKey("dbo.CargoDistrito", "IdSocio", "dbo.Socio");
            DropForeignKey("dbo.SocioClube", "IdSocio", "dbo.Socio");
            DropForeignKey("dbo.SocioClube", "IdClube", "dbo.Clube");
            DropForeignKey("dbo.CargoRotaractBrasil", "IdSocio", "dbo.Socio");
            DropForeignKey("dbo.CargoRotaractBrasil", "IdCargo", "dbo.Cargo");
            DropForeignKey("dbo.CargoDistrito", "IdDistrito", "dbo.Distrito");
            DropForeignKey("dbo.CargoDistrito", "IdCargo", "dbo.Cargo");
            DropForeignKey("dbo.CargoClube", "IdCargo", "dbo.Cargo");
            DropIndex("dbo.Tarefa", new[] { "IdProjeto" });
            DropIndex("dbo.ProjetoCategoria", new[] { "IdCategoria" });
            DropIndex("dbo.ProjetoCategoria", new[] { "IdProjeto" });
            DropIndex("dbo.Objetivo", new[] { "IdProjeto" });
            DropIndex("dbo.LancamentoFinanceiro", new[] { "IdProjeto" });
            DropIndex("dbo.Projeto", new[] { "IdClube" });
            DropIndex("dbo.SocioClube", new[] { "IdSocio" });
            DropIndex("dbo.SocioClube", new[] { "IdClube" });
            DropIndex("dbo.CargoRotaractBrasil", new[] { "IdCargo" });
            DropIndex("dbo.CargoRotaractBrasil", new[] { "IdSocio" });
            DropIndex("dbo.CargoDistrito", new[] { "IdCargo" });
            DropIndex("dbo.CargoDistrito", new[] { "IdDistrito" });
            DropIndex("dbo.CargoDistrito", new[] { "IdSocio" });
            DropIndex("dbo.Clube", new[] { "IdDistrito" });
            DropIndex("dbo.CargoClube", new[] { "IdClube" });
            DropIndex("dbo.CargoClube", new[] { "IdCargo" });
            DropIndex("dbo.CargoClube", new[] { "IdSocio" });
            DropTable("dbo.Tarefa");
            DropTable("dbo.Categoria");
            DropTable("dbo.ProjetoCategoria");
            DropTable("dbo.Objetivo");
            DropTable("dbo.LancamentoFinanceiro");
            DropTable("dbo.Projeto");
            DropTable("dbo.SocioClube");
            DropTable("dbo.CargoRotaractBrasil");
            DropTable("dbo.Socio");
            DropTable("dbo.CargoDistrito");
            DropTable("dbo.Distrito");
            DropTable("dbo.Clube");
            DropTable("dbo.CargoClube");
            DropTable("dbo.Cargo");
        }
    }
}
