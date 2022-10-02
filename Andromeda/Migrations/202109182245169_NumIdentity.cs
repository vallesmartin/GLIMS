namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumIdentity : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Numerator");
            AlterColumn("dbo.Numerator", "NumeratorId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Numerator", "NumeratorId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Numerator");
            AlterColumn("dbo.Numerator", "NumeratorId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Numerator", "NumeratorId");
        }
    }
}
