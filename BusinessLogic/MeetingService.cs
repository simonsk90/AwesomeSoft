using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
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

        /// <summary>
        /// Find conflicts in future meetings.
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetConflicts()
        {
            List<Meeting> futureMeetings = await MeetingRead.GetFutureMeetings();

            StringBuilder sb = new StringBuilder();

            List<ParticipantMeetingConflicts> participantMeetingConflicts = new List<ParticipantMeetingConflicts>();
            
            List<LocationMeetingConflicts> locationMeetingConflicts = new List<LocationMeetingConflicts>();
            
            foreach (Meeting meetingA in futureMeetings)
            {
                //Find the meetings that overlap
                List<Meeting> overlappingMeetings = futureMeetings
                    .Where(meetingB => meetingA.MeetingStart < meetingB.MeetingEnd && meetingB.MeetingStart < meetingA.MeetingEnd)
                    .ToList();
                
                #region participant meeting conflict section

                //Find participants who are enrolled in more than one of these overlapping meetings
                List<int> participantConflicts = overlappingMeetings
                    .SelectMany(p => p.Participants)
                    .Select(p => p.Id)
                    .GroupBy(p => p)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                foreach (int participantId in participantConflicts)
                {
                    if (!meetingA.Participants.Select(a => a.Id).Contains(participantId))
                    {
                        continue;
                    }
                    ParticipantMeetingConflicts pmc = new ParticipantMeetingConflicts(participantId, meetingA.Id);
                    foreach (Meeting overlappingMeeting in overlappingMeetings)
                    {
                        if (overlappingMeeting.Participants.Select(p => p.Id).Contains(participantId) && overlappingMeeting.Id != pmc.TargetMeetingId)
                        {
                            //Participant is enrolled for two meetings at the same time, which is not possible
                            pmc.OverlappingMeetings.Add(overlappingMeeting.Id);
                        }
                    }
                    participantMeetingConflicts.Add(pmc);
                }
                
                #endregion

                #region Location complication section

                //Find locations that are used in more than one of these overlapping meetings
                List<int> locationConflicts = overlappingMeetings
                    .Select(m => m.LocationRefId)
                    .GroupBy(l => l)
                    .Where(l => l.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                foreach (int locationId in locationConflicts)
                {
                    if (meetingA.LocationRefId != locationId)
                    {
                        continue;
                    }
                    LocationMeetingConflicts lmc = new LocationMeetingConflicts(locationId, meetingA.Id);
                    foreach (Meeting overlappingMeeting in overlappingMeetings)
                    {
                        if (overlappingMeeting.LocationRefId == locationId && overlappingMeeting.Id != lmc.TargetMeetingId)
                        {
                            //Location is used for two meetings at the same time, which is not possible
                            lmc.OverlappingMeetings.Add(overlappingMeeting.Id);
                        }
                    }
                    locationMeetingConflicts.Add(lmc);
                }

                #endregion
            }
                
            foreach (var pmc in participantMeetingConflicts.OrderBy(p => p.ParticipantId))
            {
                sb.Append($"Participant '{pmc.ParticipantId}' is enrolled at meeting '{pmc.TargetMeetingId}', but it is conflicting with the participant's other meetings: [{string.Join(", ", pmc.OverlappingMeetings)}] <br /> ");
            }

            sb.Append("<br />");
            
            foreach (var lmc in locationMeetingConflicts.OrderBy(l => l.LocationId))
            {
                sb.Append($"Location '{lmc.LocationId}' is assigned for meeting '{lmc.TargetMeetingId}', but it is conflicting with the location's other meetings: [{string.Join(", ", lmc.OverlappingMeetings)}] <br />");
            }

            return sb.ToString();
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