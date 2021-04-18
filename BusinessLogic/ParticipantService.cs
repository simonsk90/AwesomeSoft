using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.EntityModels;
using DAL.Read;
using DAL.Write;
using DTO;

namespace BusinessLogic
{
    public static class ParticipantService
    {
        public static async Task<ParticipantDto> GetParticipant(int participantId)
        {
            Participant entity = await ParticipantRead.GetParticipant(participantId, a => a.EnrolledMeetings);
            ParticipantDto result = Convert(entity);
            return result;
        }
        public static async Task CreateParticipant(ParticipantDto participant)
        {
            Participant participantEntity = Convert(participant);
            await ParticipantWrite.CreateParticipant(participantEntity);
        }
        public static async Task UpdateParticipant(ParticipantDto participant)
        {
            Participant participantEntity = Convert(participant);
            await ParticipantWrite.UpdateParticipant(participantEntity);
        }
        public static async Task<List<MeetingDto>> GetMeetingsForParticipant(int participantId)
        {
            List<MeetingDto> result = (await ParticipantRead.GetMeetingsForParticipant(participantId))
                .Select(ent => MeetingService.Convert(ent))
                .ToList();
            return result;
        }
        public static ParticipantDto Convert(Participant entity)
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
        public static Participant Convert(ParticipantDto participantDto)
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