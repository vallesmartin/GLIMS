namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class INV4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inv",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        InventoryDescription = c.String(),
                        InventoryType = c.Int(nullable: false),
                        InventoryStatus = c.Int(nullable: false),
                        InventoryCreationDate = c.DateTime(nullable: false),
                        InventoryEndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryId);
            
            CreateTable(
                "dbo.InvLines",
                c => new
                    {
                        InventoryId = c.Int(nullable: false),
                        InventoryLineId = c.Int(nullable: false),
                        InventoryLineItemId = c.Int(nullable: false),
                        InventoryLineQty = c.Int(nullable: false),
                        InventoryLinePU = c.Int(nullable: false),
                        InventoryLineUser = c.String(),
                        InventoryLineDate = c.DateTime(nullable: false),
                        ItemData_ItemId = c.Int(),
                    })
                .PrimaryKey(t => new { t.InventoryId, t.InventoryLineId })
                .ForeignKey("dbo.Items", t => t.ItemData_ItemId)
                .ForeignKey("dbo.Inv", t => t.InventoryId, cascadeDelete: true)
                .Index(t => t.InventoryId)
                .Index(t => t.ItemData_ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvLines", "InventoryId", "dbo.Inv");
            DropForeignKey("dbo.InvLines", "ItemData_ItemId", "dbo.Items");
            DropIndex("dbo.InvLines", new[] { "ItemData_ItemId" });
            DropIndex("dbo.InvLines", new[] { "InventoryId" });
            DropTable("dbo.InvLines");
            DropTable("dbo.Inv");
        }
    }
}
