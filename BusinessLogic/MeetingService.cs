using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.EntityModels;
using DAL.Read;
using DTO;

namespace BusinessLogic
{
    public class MeetingService
    {

        public async Task<MeetingDto> GetMeetingById(int id)
        {
            MeetingRead meetingRead = new MeetingRead();
            Meeting entity = await meetingRead.GetMeetingById(id, a => a.Location, a => a.Organizer, a => a.Participants);
            MeetingDto result = Convert(entity);
            return result;
        }

        public async Task<List<MeetingDto>> GetAllMeetings()
        {
            MeetingRead meetingRead = new MeetingRead();
            List<MeetingDto> result = (await meetingRead.GetAllMeetings(a => a.Location, a => a.Organizer, a => a.Participants))
                .Select(ent => Convert(ent))
                .ToList();
            return result;
        }
        
        /**
         * Convert from Entity to DTO
         */
        public MeetingDto Convert(Meeting meetingEntity)
        {
            ParticipantService participantService = new ParticipantService();
            LocationService locationService = new LocationService();
            MeetingDto result = new MeetingDto()
            {
                Id = meetingEntity.Id,
                Title = meetingEntity.Title,
                Description = meetingEntity.Description,
                MeetingStart = meetingEntity.MeetingStart,
                MeetingEnd = meetingEntity.MeetingEnd,
                Organizer = participantService.Convert(meetingEntity.Organizer),
                Location = locationService.Convert(meetingEntity.Location),
                Participants = meetingEntity.Participants.Select(a => participantService.Convert(a)).ToList()
            };
            return result;
        }
    }
}