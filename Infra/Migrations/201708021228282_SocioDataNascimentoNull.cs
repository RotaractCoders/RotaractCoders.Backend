namespace Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SocioDataNascimentoNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Socio", "DataNascimento", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Socio", "DataNascimento", c => c.DateTime(nullable: false));
        }
    }
}
