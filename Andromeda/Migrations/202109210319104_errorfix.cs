namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class errorfix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "DocumentLastUpdateAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "DocumentLastUpdateAt");
        }
    }
}
