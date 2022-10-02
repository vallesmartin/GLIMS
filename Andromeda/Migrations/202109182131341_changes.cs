namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "DocumentPreparedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Documents", "DocumentBilledAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Documents", "DocumentDeliveredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Inventories", "InventoryType", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "ItemDisabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Numerator", "NumeratorCode", c => c.String());
            AddColumn("dbo.UserRoles", "UserRoleIsSuperAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserRoles", "UserRoleIsSuperAdmin");
            DropColumn("dbo.Numerator", "NumeratorCode");
            DropColumn("dbo.Items", "ItemDisabled");
            DropColumn("dbo.Inventories", "InventoryType");
            DropColumn("dbo.Documents", "DocumentDeliveredAt");
            DropColumn("dbo.Documents", "DocumentBilledAt");
            DropColumn("dbo.Documents", "DocumentPreparedAt");
        }
    }
}
