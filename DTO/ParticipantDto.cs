using System;

namespace DTO
{
    public class ParticipantDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public Boolean IsOrganizer { get; set; }
    }
}