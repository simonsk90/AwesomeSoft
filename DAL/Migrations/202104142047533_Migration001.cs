namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Meetings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocationRefId = c.Int(nullable: false),
                        OrganizerRefId = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        MeetingStart = c.DateTime(nullable: false),
                        MeetingEnd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationRefId, cascadeDelete: true)
                .Index(t => t.LocationRefId);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        IsOrganizer = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meetings", "LocationRefId", "dbo.Locations");
            DropIndex("dbo.Meetings", new[] { "LocationRefId" });
            DropTable("dbo.Participants");
            DropTable("dbo.Meetings");
            DropTable("dbo.Locations");
        }
    }
}
