using System.Threading.Tasks;
using DAL.EntityModels;
using DAL.Write;
using DTO;

namespace BusinessLogic
{
    public class ParticipantService
    {
        public async Task CreateParticipant(ParticipantDto participant)
        {
            Participant participantEntity = Convert(participant);
            ParticipantWrite participantWrite = new ParticipantWrite();
            await participantWrite.CreateParticipant(participantEntity);
        }
        public async Task UpdateParticipant(ParticipantDto participant)
        {
            Participant participantEntity = Convert(participant);
            ParticipantWrite participantWrite = new ParticipantWrite();
            await participantWrite.UpdateParticipant(participantEntity);
        }
        
        public ParticipantDto Convert(Participant entity)
        {
            ParticipantDto result = new ParticipantDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Birthday = entity.Birthday,
                IsOrganizer = entity.IsOrganizer,
            };
            return result;
        }
        
        public Participant Convert(ParticipantDto participantDto)
        {
            Participant result = new Participant()
            {
                Id = participantDto.Id,
                Name = participantDto.Name,
                Birthday = participantDto.Birthday,
                IsOrganizer = participantDto.IsOrganizer,
            };
            return result;
        }
    }
}