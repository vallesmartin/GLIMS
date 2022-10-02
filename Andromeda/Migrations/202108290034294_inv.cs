namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inv : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        InventoryDescription = c.String(),
                        InventoryStatus = c.Int(nullable: false),
                        InventoryCreationDate = c.DateTime(nullable: false),
                        InventoryEndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryId);
            
            CreateTable(
                "dbo.InventoryLines",
                c => new
                    {
                        InventoryLineId = c.Int(nullable: false),
                        InventoryId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        InventoryLinePU = c.Int(nullable: false),
                        InventoryLineQty = c.Int(nullable: false),
                        InventoryLineUserName = c.String(),
                        InventoryLineDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.InventoryLineId, t.InventoryId })
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventoryLines", "ItemId", "dbo.Items");
            DropIndex("dbo.InventoryLines", new[] { "ItemId" });
            DropTable("dbo.InventoryLines");
            DropTable("dbo.Inventories");
        }
    }
}
