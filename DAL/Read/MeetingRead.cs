using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.EntityModels;

namespace DAL.Read
{
    public class MeetingRead
    {
        public async Task<Meeting> GetMeetingById(int id, params Expression<Func<Meeting, object>>[] includes)
        {
            Meeting result;
            try
            {
                using (AwesomeContext context = new AwesomeContext())
                {
                    result = await context.Meetings
                        .IncludeRange(includes)
                        .FirstOrDefaultAsync(a => a.Id == id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
            return result;
        }

        public async Task<List<Meeting>> GetAllMeetings(params Expression<Func<Meeting, object>>[] includes)
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

    }
}