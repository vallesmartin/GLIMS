namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class INV5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvLines", "ItemData_ItemId", "dbo.Items");
            DropIndex("dbo.InvLines", new[] { "ItemData_ItemId" });
            DropColumn("dbo.InvLines", "ItemData_ItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvLines", "ItemData_ItemId", c => c.Int());
            CreateIndex("dbo.InvLines", "ItemData_ItemId");
            AddForeignKey("dbo.InvLines", "ItemData_ItemId", "dbo.Items", "ItemId");
        }
    }
}
