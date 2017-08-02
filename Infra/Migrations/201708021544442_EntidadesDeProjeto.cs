namespace Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntidadesDeProjeto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeioDeDivulgacaoProjeto",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                        IdProjeto = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projeto", t => t.IdProjeto)
                .Index(t => t.IdProjeto);
            
            CreateTable(
                "dbo.ParceriaProjeto",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                        IdProjeto = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projeto", t => t.IdProjeto)
                .Index(t => t.IdProjeto);
            
            CreateTable(
                "dbo.ParticipanteProjeto",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                        IdProjeto = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projeto", t => t.IdProjeto)
                .Index(t => t.IdProjeto);
            
            CreateTable(
                "dbo.PublicoAlvoProjeto",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                        IdProjeto = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projeto", t => t.IdProjeto)
                .Index(t => t.IdProjeto);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PublicoAlvoProjeto", "IdProjeto", "dbo.Projeto");
            DropForeignKey("dbo.ParticipanteProjeto", "IdProjeto", "dbo.Projeto");
            DropForeignKey("dbo.ParceriaProjeto", "IdProjeto", "dbo.Projeto");
            DropForeignKey("dbo.MeioDeDivulgacaoProjeto", "IdProjeto", "dbo.Projeto");
            DropIndex("dbo.PublicoAlvoProjeto", new[] { "IdProjeto" });
            DropIndex("dbo.ParticipanteProjeto", new[] { "IdProjeto" });
            DropIndex("dbo.ParceriaProjeto", new[] { "IdProjeto" });
            DropIndex("dbo.MeioDeDivulgacaoProjeto", new[] { "IdProjeto" });
            DropTable("dbo.PublicoAlvoProjeto");
            DropTable("dbo.ParticipanteProjeto");
            DropTable("dbo.ParceriaProjeto");
            DropTable("dbo.MeioDeDivulgacaoProjeto");
        }
    }
}
