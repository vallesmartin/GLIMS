namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Roles1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Changes",
                c => new
                    {
                        ChangeObjectId = c.String(nullable: false, maxLength: 128),
                        ChangeLastAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ChangeObjectId);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        LogId = c.Long(nullable: false, identity: true),
                        LogAt = c.DateTime(nullable: false),
                        LogCode = c.String(),
                        LogDescription = c.String(),
                        LogValue = c.Long(nullable: false),
                        LogUser = c.String(),
                        LogOldValue = c.String(),
                        LogNewValue = c.String(),
                    })
                .PrimaryKey(t => t.LogId);
            
            AddColumn("dbo.UserRoles", "UserRoleIsBiller", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserRoles", "UserRoleIsAdmin", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserRoles", "UserRoleIsViewMaster");
            DropColumn("dbo.UserRoles", "UserRoleCanUpdateMaster");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRoles", "UserRoleCanUpdateMaster", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserRoles", "UserRoleIsViewMaster", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserRoles", "UserRoleIsAdmin");
            DropColumn("dbo.UserRoles", "UserRoleIsBiller");
            DropTable("dbo.Logs");
            DropTable("dbo.Changes");
        }
    }
}
