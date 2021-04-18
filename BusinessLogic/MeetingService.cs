using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.EntityModels;
using DAL.Read;
using DAL.Write;
using DTO;

namespace BusinessLogic
{
    public static class MeetingService
    {

        public static async Task<MeetingDto> GetMeetingById(int id)
        {
            Meeting entity = await MeetingRead.GetMeeting(id, a => a.Location, a => a.Organizer, a => a.Participants);
            MeetingDto result = Convert(entity);
            return result;
        }

        public static async Task<List<MeetingDto>> GetAllMeetings()
        {
            List<MeetingDto> result = (await MeetingRead.GetAllMeetings(a => a.Location, a => a.Organizer, a => a.Participants))
                .Select(ent => Convert(ent))
                .ToList();
            return result;
        }

        public static async Task CreateMeeting(MeetingDto meetingDto)
        {
            Meeting meetingEntity = Convert(meetingDto);
            await MeetingWrite.CreateMeeting(meetingEntity);
        }
        
        public static async Task UpdateMeeting(MeetingDto meetingDto)
        {
            Meeting meetingEntity = Convert(meetingDto);
            await MeetingWrite.UpdateMeeting(meetingEntity);
        }

        public static async Task AddPersonToMeeting(int participantId, int meetingId)
        {
            Task<Participant> participantTask = ParticipantRead.GetParticipant(participantId);
            Task<Meeting> meetingTask = MeetingRead.GetMeeting(meetingId, m => m.Participants);
            await Task.WhenAll(meetingTask, participantTask);
            
            if (meetingTask.Result.Participants.Any(p => p.Id == participantId))
            {
                throw new Exception("Participant is already enrolled in this meeting");
            }
            
            await MeetingWrite.AddParticipantToMeeting(participantTask.Result, meetingTask.Result);
        }

        public static async Task GetComplications()
        {
            List<Meeting> futureMeetings = await MeetingRead.GetFutureMeetings();

            // List<List<Meeting>> 

            foreach (Meeting a in futureMeetings)
            {
                List<Meeting> overlappingMeetings = futureMeetings
                    .Where(b => a.MeetingStart < b.MeetingEnd && b.MeetingStart < a.MeetingEnd)
                    .ToList();
                
                //Check if any participant is enrolled in more than one of these overlapping meetings
                var participantComplications = overlappingMeetings
                    .SelectMany(p => p.Participants)
                    .Select(p => p.Id)
                    .GroupBy(p => p)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

            }
            
            
        }
        
        /// <summary>
        /// Convert from Entity to DTO
        /// </summary>
        /// <param name="meetingEntity"></param>
        /// <returns></returns>
        public static MeetingDto Convert(Meeting meetingEntity)
        {
            MeetingDto result = new MeetingDto()
            {
                Id = meetingEntity.Id,
                Title = meetingEntity.Title,
                Description = meetingEntity.Description,
                MeetingStart = meetingEntity.MeetingStart,
                MeetingEnd = meetingEntity.MeetingEnd,
                OrganizerRefId = meetingEntity.OrganizerRefId,
                LocationRefId = meetingEntity.LocationRefId,
            };
            return result;
        }
        
        /// <summary>
        /// Convert from DTO to Entity
        /// </summary>
        /// <param name="meetingDto"></param>
        /// <returns></returns>
        public static Meeting Convert(MeetingDto meetingDto)
        {
            Meeting result = new Meeting()
            {
                Id = meetingDto.Id,
                Title = meetingDto.Title,
                Description = meetingDto.Description,
                MeetingStart = meetingDto.MeetingStart,
                MeetingEnd = meetingDto.MeetingEnd,
                OrganizerRefId = meetingDto.OrganizerRefId,
                LocationRefId = meetingDto.LocationRefId,
            };
            return result;
        }
    }
}