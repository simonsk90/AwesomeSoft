
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using DAL.EntityModels;
using DAL.Write;

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
            
            context.Participants.AddOrUpdate(x => x.Name, seedParticipants);
            
            Location[] seedLocations =
            {
                new Location() { Name = "Seed_Location_01" },
                new Location() { Name = "Seed_Location_02" },
                new Location() { Name = "Seed_Location_03" },
            };
            
            context.Locations.AddOrUpdate(x => x.Name, seedLocations);

            //Save context in order to retrieve the generated id's from seedLocation and seedParticipants.
            //That way we can later assign those id's to Meetings foreign keys.
            context.SaveChanges();

            string participantSimonName = seedParticipants[0].Name;

            Location locationEntity = context.Locations.First();
            Participant participantEntity = context.Participants.Single(a => a.Name == participantSimonName);

            List<Participant> participantEntities = context.Participants
                .ToList();

            participantEntities = participantEntities
                .Where(a => seedParticipants.Any(b => b.Name == a.Name))
                .ToList();

            Meeting[] seedMeetings =
            {
                new Meeting()
                {
                    LocationRefId = locationEntity.Id,
                    OrganizerRefId = participantEntity.Id,
                    Title = "Seed_Meeting_01",
                    Description = "Seed_meeting_description_bla_01",
                    MeetingStart = new DateTime(2021, 05, 01, 12, 00, 00), 
                    MeetingEnd = new DateTime(2021, 05, 01, 12, 05, 00), 
                    Participants = participantEntities
                },
                new Meeting()
                {
                    LocationRefId = locationEntity.Id,
                    OrganizerRefId = participantEntity.Id,
                    Title = "Seed_Meeting_02",
                    Description = "Overlapping",
                    MeetingStart = new DateTime(2021, 05, 01, 13, 00, 00), 
                    MeetingEnd = new DateTime(2021, 05, 01, 14, 00, 00), 
                    Participants = participantEntities.Take(1).ToList()
                },
                new Meeting()
                {
                    LocationRefId = locationEntity.Id,
                    OrganizerRefId = participantEntity.Id,
                    Title = "Seed_Meeting_03",
                    Description = "Overlapping",
                    MeetingStart = new DateTime(2021, 05, 01, 13, 15, 00), 
                    MeetingEnd = new DateTime(2021, 05, 01, 14, 15, 00), 
                    Participants = participantEntities.Take(1).ToList()
                },
                new Meeting()
                {
                    LocationRefId = locationEntity.Id,
                    OrganizerRefId = participantEntity.Id,
                    Title = "Seed_Meeting_04",
                    Description = "Overlapping",
                    MeetingStart = new DateTime(2021, 05, 01, 13, 30, 00), 
                    MeetingEnd = new DateTime(2021, 05, 01, 14, 30, 00), 
                    Participants = participantEntities.Take(1).ToList()
                },
                new Meeting()
                {
                    LocationRefId = locationEntity.Id,
                    OrganizerRefId = participantEntity.Id,
                    Title = "Seed_Meeting_05",
                    Description = "Overlapping only with Seed_Meeting_04",
                    MeetingStart = new DateTime(2021, 05, 01, 14, 25, 00), 
                    MeetingEnd = new DateTime(2021, 05, 01, 15, 25, 00), 
                    Participants = participantEntities.Take(1).ToList()
                }
            };
            
            context.Meetings.AddOrUpdate(x => x.Title, seedMeetings);
            
        }
    } 
}