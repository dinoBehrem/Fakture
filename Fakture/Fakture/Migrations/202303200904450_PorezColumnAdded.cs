namespace Fakture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PorezColumnAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fakture", "Porez", c => c.Int(nullable: false));
            AddColumn("dbo.StavkeFakture", "UkupnaCijena", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.StavkeFakture", "UkipnaCijenaSaPorezom");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StavkeFakture", "UkipnaCijenaSaPorezom", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.StavkeFakture", "UkupnaCijena");
            DropColumn("dbo.Fakture", "Porez");
        }
    }
}
