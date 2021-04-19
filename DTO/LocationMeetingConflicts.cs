using System.Collections.Generic;

namespace DTO
{
    public class LocationMeetingConflicts
    {
        public LocationMeetingConflicts(int locationId, int targetMeetingId)
        {
            this.LocationId = locationId;
            this.TargetMeetingId = targetMeetingId;
            this.OverlappingMeetings = new List<int>();
        }
        
        public int LocationId { get; set; }

        public int TargetMeetingId { get; set; }
        
        public List<int> OverlappingMeetings { get; set; }
    }
}