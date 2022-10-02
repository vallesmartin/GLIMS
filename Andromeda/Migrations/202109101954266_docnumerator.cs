namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class docnumerator : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "DocumentType", c => c.Int(nullable: false));
            AddColumn("dbo.Documents", "DocumentEndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Documents", "DocumentNumerator", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "DocumentNumerator");
            DropColumn("dbo.Documents", "DocumentEndDate");
            DropColumn("dbo.Documents", "DocumentType");
        }
    }
}
