namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v12220522cambios : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserRoles");
            AlterColumn("dbo.UserRoles", "UserId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserRoles", "UserId");
            DropTable("dbo.PriceHistories");
            DropTable("dbo.PriceHistoryLines");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PriceHistoryLines",
                c => new
                    {
                        PriceHistoryId = c.Int(nullable: false),
                        PriceHisLId = c.Int(nullable: false),
                        PriceHisLItemId = c.Int(nullable: false),
                        PriceHisLItemDescription = c.String(),
                        PriceHisLOldCost = c.Single(nullable: false),
                        PriceHisLNewCost = c.Single(nullable: false),
                        PriceHisLOldPrice = c.Single(nullable: false),
                        PriceHisLNewPrice = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.PriceHistoryId, t.PriceHisLId });
            
            CreateTable(
                "dbo.PriceHistories",
                c => new
                    {
                        PriceHistoryId = c.Int(nullable: false, identity: true),
                        PriceHistoryDescription = c.String(),
                        PriceHistoryEntityId = c.Int(nullable: false),
                        PriceHistoryEntityName = c.String(),
                        PriceHistoryDateFrom = c.DateTime(nullable: false),
                        PriceHistoryDateTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PriceHistoryId);
            
            DropPrimaryKey("dbo.UserRoles");
            AlterColumn("dbo.UserRoles", "UserId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.UserRoles", "UserId");
        }
    }
}
