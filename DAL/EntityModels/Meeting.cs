using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.EntityModels
{
    public class Meeting
    {

        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Location))]
        public int LocationRefId { get; set; }
        /// <summary>
        /// Foreign key to organizer. Navigation created via fluent API because
        /// of some issue with Data annotations and multiple relations to the same table.
        /// </summary>
        public int OrganizerRefId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime MeetingStart { get; set; }
        public DateTime MeetingEnd { get; set; }

        
        public virtual Location Location { get; set; }
        public virtual Participant Organizer { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
        
    }
}