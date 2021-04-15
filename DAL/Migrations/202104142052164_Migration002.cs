namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration002 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Meetings", "OrganizerRefId");
            AddForeignKey("dbo.Meetings", "OrganizerRefId", "dbo.Participants", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meetings", "OrganizerRefId", "dbo.Participants");
            DropIndex("dbo.Meetings", new[] { "OrganizerRefId" });
        }
    }
}
