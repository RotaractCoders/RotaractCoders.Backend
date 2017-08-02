namespace Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatasAceitaremNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CargoClube", "Ate", c => c.DateTime());
            AlterColumn("dbo.CargoDistrito", "Ate", c => c.DateTime());
            AlterColumn("dbo.CargoRotaractBrasil", "Ate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CargoRotaractBrasil", "Ate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CargoDistrito", "Ate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CargoClube", "Ate", c => c.DateTime(nullable: false));
        }
    }
}
