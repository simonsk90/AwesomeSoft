using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.EntityModels
{
    public class Participant
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public Boolean IsOrganizer { get; set; }
        
        public virtual ICollection<Meeting> EnrolledMeetings { get; set; }
    }
}