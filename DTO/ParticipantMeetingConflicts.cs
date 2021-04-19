using System.Collections.Generic;

namespace DTO
{
    public class ParticipantMeetingConflicts
    {

        public ParticipantMeetingConflicts(int participantId, int targetMeetingId)
        {
            this.ParticipantId = participantId;
            this.TargetMeetingId = targetMeetingId;
            this.OverlappingMeetings = new List<int>();
        }
        
        public int ParticipantId { get; set; }

        public int TargetMeetingId { get; set; }
        
        public List<int> OverlappingMeetings { get; set; }
    }
}