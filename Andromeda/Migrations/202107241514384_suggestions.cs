namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class suggestions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CategoryOrder", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "ItemSuggLow", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "ItemSuggHigh", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "ItemSuggStep", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "ItemOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "ItemOrder");
            DropColumn("dbo.Items", "ItemSuggStep");
            DropColumn("dbo.Items", "ItemSuggHigh");
            DropColumn("dbo.Items", "ItemSuggLow");
            DropColumn("dbo.Categories", "CategoryOrder");
        }
    }
}
