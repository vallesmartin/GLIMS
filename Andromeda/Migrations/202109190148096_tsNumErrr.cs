namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tsNumErrr : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Numerator");
            AlterColumn("dbo.Numerator", "NumeratorId", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.Numerator", "NumeratorId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Numerator");
            AlterColumn("dbo.Numerator", "NumeratorId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Numerator", "NumeratorId");
        }
    }
}
