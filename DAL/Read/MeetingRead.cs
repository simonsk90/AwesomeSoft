using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.EntityModels;
using DTO;

namespace DAL.Read
{
    public static class MeetingRead
    {
        public static async Task<Meeting> GetMeeting(int id, params Expression<Func<Meeting, object>>[] includes)
        {
            Meeting result;
            using (AwesomeContext context = new AwesomeContext())
            {
                result = await context.Meetings
                    .IncludeRange(includes)
                    .FirstOrDefaultAsync(a => a.Id == id);
            }
            return result;
        }

        public static async Task<List<Meeting>> GetAllMeetings(params Expression<Func<Meeting, object>>[] includes)
        {
            List<Meeting> result;
            using (AwesomeContext context = new AwesomeContext())
            {
                result = await context.Meetings
                    .IncludeRange(includes)
                    .ToListAsync();
            }
            return result;
        }

        public static async Task<List<Meeting>> GetFutureMeetings()
        {
            using (AwesomeContext context = new AwesomeContext())
            {
                List<Meeting> futureMeetings = await context.Meetings
                    .Include(a => a.Participants)
                    .Include(a => a.Location)
                    .Where(a => a.MeetingStart > DateTime.Now)
                    .ToListAsync();

                return futureMeetings;
            }
        }

    }
}