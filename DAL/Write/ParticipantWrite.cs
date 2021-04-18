using System.Data.Entity;
using System.Threading.Tasks;
using DAL.EntityModels;

namespace DAL.Write
{
    public class ParticipantWrite
    {
        
        public async Task CreateParticipant(Participant participant)
        {
            using (AwesomeContext context = new AwesomeContext())
            {
                context.Participants.Add(participant);
                await context.SaveChangesAsync();
            }
        }
        
        public async Task UpdateParticipant(Participant newParticipant)
        {
            using (AwesomeContext context = new AwesomeContext())
            {
                // Participant existingEntity = context.Participants.Find(newParticipant.Id);
                // Participant existingEntity = new Participant()
                // {
                //     Id = newParticipant.Id
                // };
                context.Participants.Attach(newParticipant);
                // context.Entry(existingEntity).CurrentValues.SetValues(newParticipant);
                context.Entry(newParticipant).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
        
    }
}