namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryDescription = c.String(),
                        CategoryImageName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.DocumentLines",
                c => new
                    {
                        DocumentLineId = c.Int(nullable: false),
                        DocumentId = c.Long(nullable: false),
                        DocumentLineItemId = c.Long(nullable: false),
                        DocumentLineItemDescription = c.String(),
                        DocumentLineQty = c.Int(nullable: false),
                        DocumentLineItemPrice = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.DocumentLineId, t.DocumentId })
                .ForeignKey("dbo.Documents", t => t.DocumentId, cascadeDelete: true)
                .Index(t => t.DocumentId);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentId = c.Long(nullable: false, identity: true),
                        DocumentLetter = c.String(),
                        DocumentNumber = c.Long(nullable: false),
                        EntityId = c.Int(nullable: false),
                        DocumentDate = c.DateTime(nullable: false),
                        DocumentStatus = c.Int(nullable: false),
                        DocumentTotalAmount = c.Single(nullable: false),
                        DocumentLinesQty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DocumentId)
                .ForeignKey("dbo.Entities", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
            CreateTable(
                "dbo.Entities",
                c => new
                    {
                        EntityId = c.Int(nullable: false, identity: true),
                        EntityDescription = c.String(),
                        EntityLegalName = c.String(),
                        EntityAddress = c.String(),
                        EntityLocation = c.String(),
                        EntityType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EntityId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        ItemDescription = c.String(),
                        ItemExternalCode = c.String(),
                        ItemInternalCode = c.String(),
                        ItemBarcode = c.String(),
                        ItemPrice = c.Single(nullable: false),
                        ItemCost = c.Single(nullable: false),
                        ItemSugg = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        EntityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Entities", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.EntityId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserRoleIsSeller = c.Boolean(nullable: false),
                        UserRoleIsPicker = c.Boolean(nullable: false),
                        UserRoleIsDeliverer = c.Boolean(nullable: false),
                        UserRoleIsViewMaster = c.Boolean(nullable: false),
                        UserRoleCanUpdateMaster = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserPassword = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "EntityId", "dbo.Entities");
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Documents", "EntityId", "dbo.Entities");
            DropForeignKey("dbo.DocumentLines", "DocumentId", "dbo.Documents");
            DropIndex("dbo.Items", new[] { "EntityId" });
            DropIndex("dbo.Items", new[] { "CategoryId" });
            DropIndex("dbo.Documents", new[] { "EntityId" });
            DropIndex("dbo.DocumentLines", new[] { "DocumentId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Items");
            DropTable("dbo.Entities");
            DropTable("dbo.Documents");
            DropTable("dbo.DocumentLines");
            DropTable("dbo.Categories");
        }
    }
}
