namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inve : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.InventoryLines", "InventoryId");
            AddForeignKey("dbo.InventoryLines", "InventoryId", "dbo.Inventories", "InventoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventoryLines", "InventoryId", "dbo.Inventories");
            DropIndex("dbo.InventoryLines", new[] { "InventoryId" });
        }
    }
}
