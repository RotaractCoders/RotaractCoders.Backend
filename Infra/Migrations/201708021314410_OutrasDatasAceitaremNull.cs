namespace Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OutrasDatasAceitaremNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CargoClube", "De", c => c.DateTime());
            AlterColumn("dbo.CargoDistrito", "De", c => c.DateTime());
            AlterColumn("dbo.CargoRotaractBrasil", "De", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CargoRotaractBrasil", "De", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CargoDistrito", "De", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CargoClube", "De", c => c.DateTime(nullable: false));
        }
    }
}
