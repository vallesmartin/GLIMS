namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeToNative : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Changes", "ChangeLastAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Documents", "DocumentDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Documents", "DocumentPreparedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Documents", "DocumentBilledAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Documents", "DocumentDeliveredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Inventories", "InventoryCreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Inventories", "InventoryEndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inventories", "InventoryEndDate");
            DropColumn("dbo.Inventories", "InventoryCreationDate");
            DropColumn("dbo.Documents", "DocumentDeliveredAt");
            DropColumn("dbo.Documents", "DocumentBilledAt");
            DropColumn("dbo.Documents", "DocumentPreparedAt");
            DropColumn("dbo.Documents", "DocumentDate");
            DropColumn("dbo.Changes", "ChangeLastAt");
        }
    }
}
