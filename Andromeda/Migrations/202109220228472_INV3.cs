namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class INV3 : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.InvLines", "ItemData_ItemId", "dbo.Items");
            DropForeignKey("dbo.InvLines", "InventoryId", "dbo.Inv");
            DropIndex("dbo.InvLines", new[] { "InventoryId" });
            //DropIndex("dbo.InvLines", new[] { "ItemData_ItemId" });
            DropTable("dbo.Inv");
            DropTable("dbo.InvLines");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.InvLines",
                c => new
                    {
                        InventoryId = c.Int(nullable: false),
                        InventoryLineId = c.Int(nullable: false),
                        InventoryItemId = c.Int(nullable: false),
                        InventoryLineQty = c.Int(nullable: false),
                        InventoryLinePU = c.Int(nullable: false),
                        InventoryLineUser = c.String(),
                        InventoryLineDate = c.DateTime(nullable: false),
                        ItemData_ItemId = c.Int(),
                    })
                .PrimaryKey(t => new { t.InventoryId, t.InventoryLineId });
            
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
            
            CreateIndex("dbo.InvLines", "ItemData_ItemId");
            CreateIndex("dbo.InvLines", "InventoryId");
            AddForeignKey("dbo.InvLines", "InventoryId", "dbo.Inv", "InventoryId", cascadeDelete: true);
            AddForeignKey("dbo.InvLines", "ItemData_ItemId", "dbo.Items", "ItemId");
        }
    }
}
