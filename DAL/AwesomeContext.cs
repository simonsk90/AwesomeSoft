using System.Data.Common;
using System.Data.Entity;
using DAL.EntityModels;

namespace DAL
{
    public class AwesomeContext : DbContext
    {
        public AwesomeContext() : base("AwesomeSoft")
        {
            
        }
        
        public DbSet<Location> Locations { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Participant> Participants { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meeting>()
                .HasRequired(a => a.Organizer)
                .WithMany()
                .HasForeignKey(a => a.OrganizerRefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Meeting>()
                .HasMany<Participant>(m => m.Participants)
                .WithMany(p => p.EnrolledMeetings);
            
            base.OnModelCreating(modelBuilder);
        }
        
    }
}