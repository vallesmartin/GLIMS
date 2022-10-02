namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class numInt3 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Numerator");
            AddColumn("dbo.Numerator", "NumeratorId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Numerator", "NumeratorCode", c => c.String());
            AddPrimaryKey("dbo.Numerator", "NumeratorId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Numerator");
            AlterColumn("dbo.Numerator", "NumeratorCode", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Numerator", "NumeratorId");
            AddPrimaryKey("dbo.Numerator", "NumeratorCode");
        }
    }
}
