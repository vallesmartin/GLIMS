namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mod : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Documents", "DocumentEndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "DocumentEndDate", c => c.DateTime(nullable: false));
        }
    }
}
