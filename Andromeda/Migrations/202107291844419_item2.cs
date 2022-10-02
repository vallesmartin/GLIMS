namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class item2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Numerator",
                c => new
                    {
                        NumeratorId = c.String(nullable: false, maxLength: 128),
                        NumeratorLast = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.NumeratorId);
            
            AddColumn("dbo.Entities", "EntityContact", c => c.String());
            AddColumn("dbo.Entities", "EntityPhone", c => c.String());
            AddColumn("dbo.Entities", "EntityMail", c => c.String());
            AddColumn("dbo.Items", "ItemInternalDescription", c => c.String());
            AddColumn("dbo.Items", "ItemPackUnit", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "ItemPackUnit");
            DropColumn("dbo.Items", "ItemInternalDescription");
            DropColumn("dbo.Entities", "EntityMail");
            DropColumn("dbo.Entities", "EntityPhone");
            DropColumn("dbo.Entities", "EntityContact");
            DropTable("dbo.Numerator");
        }
    }
}
