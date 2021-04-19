using System;
using System.Collections.Generic;

namespace DTO
{
    public class MeetingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime MeetingStart { get; set; }
        public DateTime MeetingEnd { get; set; }

        public int OrganizerRefId { get; set; }
        public int LocationRefId { get; set; }
        

    }
}