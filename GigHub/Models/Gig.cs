using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Gig
    {
        public int ID { get; set; }

        [Required]
        [StringLength(128)]
        public string ArtistId{ get; set; }

        public DateTime DateTime { get; set; }

        public bool IsCanceled { get; set; }

        [Required]
        [StringLength(255)]
        public string  Venue { get; set; }

        [Required]
        public byte GenreId { get; set; }

        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }

        [ForeignKey("ArtistId")]
        public virtual ApplicationUser Artist { get; set; }

        public virtual List<Attendance> Attendances { get; set; }
    }

}