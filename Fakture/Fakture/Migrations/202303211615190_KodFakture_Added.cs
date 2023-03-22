namespace Fakture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KodFakture_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fakture", "Kod", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fakture", "Kod");
        }
    }
}
