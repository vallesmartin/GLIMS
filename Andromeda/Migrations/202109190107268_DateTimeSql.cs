namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeSql : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Changes", "ChangeLastAt");
            DropColumn("dbo.Documents", "DocumentDate");
            DropColumn("dbo.Documents", "DocumentPreparedAt");
            DropColumn("dbo.Documents", "DocumentBilledAt");
            DropColumn("dbo.Documents", "DocumentDeliveredAt");
            DropColumn("dbo.Inventories", "InventoryCreationDate");
            DropColumn("dbo.Inventories", "InventoryEndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inventories", "InventoryEndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Inventories", "InventoryCreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Documents", "DocumentDeliveredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Documents", "DocumentBilledAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Documents", "DocumentPreparedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Documents", "DocumentDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Changes", "ChangeLastAt", c => c.DateTime(nullable: false));
        }
    }
}
