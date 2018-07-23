using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Attendance
    {
        public virtual Gig Gig { get; set; }
        public virtual ApplicationUser Attendee { get; set; }

        [Key]
        [Column(name: "GigId", Order =1)]

        [ForeignKey("Gig")]
        public int GigId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Attendee")]
        public string AttendeeId { get; set; }

    }
}