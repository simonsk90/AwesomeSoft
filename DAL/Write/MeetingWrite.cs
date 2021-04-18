using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DAL.EntityModels;
using DAL.Read;

namespace DAL.Write
{
    public static class MeetingWrite
    {
        public static async Task CreateMeeting(Meeting meeting)
        {
            using (AwesomeContext awesomeContext = new AwesomeContext())
            {
                awesomeContext.Meetings.Add(meeting);
                await awesomeContext.SaveChangesAsync();
            }
        }
        
        /// <summary>
        /// Update by overwriting everything
        /// </summary>
        /// <param name="meeting"></param>
        /// <returns></returns>
        public static async Task UpdateMeeting(Meeting meeting)
        {
            using (AwesomeContext context = new AwesomeContext())
            {
                context.Meetings.Attach(meeting);
                context.Entry(meeting).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public static async Task AddParticipantToMeeting(Participant participant, Meeting meeting)
        {
            using (AwesomeContext context = new AwesomeContext())
            {
                context.Meetings.Attach(meeting);
                context.Participants.Attach(participant);
                meeting.Participants.Add(participant);
                await context.SaveChangesAsync();
            }
        }
    }
}