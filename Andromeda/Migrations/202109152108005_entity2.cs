namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entity2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Entities", "EntityInternalCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Entities", "EntityInternalCode");
        }
    }
}
