using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.EntityModels;

namespace DAL.Read
{
    public static class ParticipantRead
    {

        public static async Task<Participant> GetParticipant(int participantId, params Expression<Func<Participant, object>>[] includes)
        {
            using (AwesomeContext context = new AwesomeContext())
            {
                Participant result = await context.Participants
                    .IncludeRange(includes)
                    .FirstAsync(a => a.Id == participantId);
                return result;
            }
        }
        
        public static async Task<List<Meeting>> GetMeetingsForParticipant(int participantId)
        {
            using (AwesomeContext context = new AwesomeContext())
            {
                List<Meeting> result = await context.Participants
                    .Where(a => a.Id == participantId)
                    .SelectMany(a => a.EnrolledMeetings)
                    .ToListAsync();

                return result;
            }
        }
        
    }
}