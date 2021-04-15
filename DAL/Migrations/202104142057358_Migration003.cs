namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration003 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeetingParticipants",
                c => new
                    {
                        Meeting_Id = c.Int(nullable: false),
                        Participant_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meeting_Id, t.Participant_Id })
                .ForeignKey("dbo.Meetings", t => t.Meeting_Id, cascadeDelete: true)
                .ForeignKey("dbo.Participants", t => t.Participant_Id, cascadeDelete: true)
                .Index(t => t.Meeting_Id)
                .Index(t => t.Participant_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeetingParticipants", "Participant_Id", "dbo.Participants");
            DropForeignKey("dbo.MeetingParticipants", "Meeting_Id", "dbo.Meetings");
            DropIndex("dbo.MeetingParticipants", new[] { "Participant_Id" });
            DropIndex("dbo.MeetingParticipants", new[] { "Meeting_Id" });
            DropTable("dbo.MeetingParticipants");
        }
    }
}
