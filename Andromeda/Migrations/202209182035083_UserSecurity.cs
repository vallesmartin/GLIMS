namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserSecurity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "UserLoginTry", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "UserLastActivity", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserLastActivity");
            DropColumn("dbo.Users", "UserLoginTry");
            DropColumn("dbo.Users", "UserActive");
        }
    }
}
