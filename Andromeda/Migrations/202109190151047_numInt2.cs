namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class numInt2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Numerator");
            AlterColumn("dbo.Numerator", "NumeratorCode", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Numerator", "NumeratorCode");
            DropColumn("dbo.Numerator", "NumeratorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Numerator", "NumeratorId", c => c.DateTime(nullable: false));
            DropPrimaryKey("dbo.Numerator");
            AlterColumn("dbo.Numerator", "NumeratorCode", c => c.String());
            AddPrimaryKey("dbo.Numerator", "NumeratorId");
        }
    }
}
