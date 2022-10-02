namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inventory : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Inventories", newName: "Inv");
            RenameTable(name: "dbo.InventoryLines", newName: "InvLines");
            //DropForeignKey("dbo.InventoryLines", "ItemId", "dbo.Items");
            //DropIndex("dbo.InvLines", new[] { "ItemId" });
            //RenameColumn(table: "dbo.InvLines", name: "ItemId", newName: "ItemData_ItemId");
            DropPrimaryKey("dbo.InvLines");
            AddColumn("dbo.InvLines", "InventoryItemId", c => c.Int(nullable: false));
            AddColumn("dbo.InvLines", "InventoryLineUser", c => c.String());
           // AlterColumn("dbo.InvLines", "ItemData_ItemId", c => c.Int());
            AddPrimaryKey("dbo.InvLines", new[] { "InventoryId", "InventoryLineId" });
            //CreateIndex("dbo.InvLines", "ItemData_ItemId");
            //AddForeignKey("dbo.InvLines", "ItemData_ItemId", "dbo.Items", "ItemId");
            DropColumn("dbo.InvLines", "InventoryLineUserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvLines", "InventoryLineUserName", c => c.String());
            DropForeignKey("dbo.InvLines", "ItemData_ItemId", "dbo.Items");
            DropIndex("dbo.InvLines", new[] { "ItemData_ItemId" });
            DropPrimaryKey("dbo.InvLines");
            AlterColumn("dbo.InvLines", "ItemData_ItemId", c => c.Int(nullable: false));
            DropColumn("dbo.InvLines", "InventoryLineUser");
            DropColumn("dbo.InvLines", "InventoryItemId");
            AddPrimaryKey("dbo.InvLines", new[] { "InventoryLineId", "InventoryId" });
            RenameColumn(table: "dbo.InvLines", name: "ItemData_ItemId", newName: "ItemId");
            CreateIndex("dbo.InvLines", "ItemId");
            AddForeignKey("dbo.InventoryLines", "ItemId", "dbo.Items", "ItemId", cascadeDelete: true);
            RenameTable(name: "dbo.InvLines", newName: "InventoryLines");
            RenameTable(name: "dbo.Inv", newName: "Inventories");
        }
    }
}
