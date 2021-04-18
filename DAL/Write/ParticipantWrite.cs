using System.Data.Entity;
using System.Threading.Tasks;
using DAL.EntityModels;

namespace DAL.Write
{
    public class ParticipantWrite
    {
        
        public static async Task CreateParticipant(Participant participant)
        {
            using (AwesomeContext context = new AwesomeContext())
            {
                context.Participants.Add(participant);
                await context.SaveChangesAsync();
            }
        }
        
        /// <summary>
        /// Update by overwriting everything
        /// </summary>
        /// <param name="newParticipant"></param>
        /// <returns></returns>
        public static async Task UpdateParticipant(Participant newParticipant)
        {
            using (AwesomeContext context = new AwesomeContext())
            {
                context.Participants.Attach(newParticipant);
                context.Entry(newParticipant).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
        
    }
}