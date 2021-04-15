
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using DAL.EntityModels;

namespace DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AwesomeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        
        protected override void Seed(AwesomeContext context)
        {
            Participant[] seedParticipants = 
            {
                new Participant() { Name = "Seed_Simon", Birthday = new DateTime(1990, 06, 20), IsOrganizer = true},
                new Participant() { Name = "Seed_Flemming", Birthday = new DateTime(1995, 06, 20), IsOrganizer = true},
                new Participant() { Name = "Seed_Ida", Birthday = new DateTime(1998, 06, 20), IsOrganizer = false}
            };
            
            context.Participants.AddOrUpdate(x => x.Name,
                seedParticipants
            );
            
            Location seedLocation = new Location() { Name = "Seed_Location_01" };
            
            context.Locations.AddOrUpdate(x => x.Name, seedLocation);

            //Save context in order to retrieve the generated id's from seedLocation and seedParticipants.
            //That way we can later assign those id's to Meetings foreign keys.
            context.SaveChanges();

            string participantName = seedParticipants[0].Name;

            Location locationEntity = context.Locations.Single(a => a.Name == seedLocation.Name);
            Participant participantEntity = context.Participants.Single(a => a.Name == participantName);
            
            context.Meetings.AddOrUpdate(x => x.Title, new Meeting()
            {
                LocationRefId = locationEntity.Id,
                OrganizerRefId = participantEntity.Id,
                Title = "Seed_Meeting_01",
                Description = "Seed_meeting_description_bla_01",
                MeetingStart = new DateTime(2000, 01, 01), 
                MeetingEnd = new DateTime(2000, 01, 01), 
                Participants = seedParticipants
            });
        }
    } 
}