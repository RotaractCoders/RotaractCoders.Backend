namespace Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Evento", "IdTipoEvento", "dbo.TipoEvento");
            DropIndex("dbo.Evento", new[] { "IdTipoEvento" });
            AddColumn("dbo.Evento", "TipoEvento", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Evento", "Programa", c => c.String(maxLength: 8000, unicode: false));
            DropColumn("dbo.Evento", "IdTipoEvento");
            DropTable("dbo.TipoEvento");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TipoEvento",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Evento", "IdTipoEvento", c => c.Guid(nullable: false));
            DropColumn("dbo.Evento", "Programa");
            DropColumn("dbo.Evento", "TipoEvento");
            CreateIndex("dbo.Evento", "IdTipoEvento");
            AddForeignKey("dbo.Evento", "IdTipoEvento", "dbo.TipoEvento", "Id");
        }
    }
}
