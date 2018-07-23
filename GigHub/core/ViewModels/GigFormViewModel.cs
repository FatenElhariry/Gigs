using GigHub.core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        [Required]
        public string Venue { get; set; }

        [Required]
        //[FutureDate]
        public string  Date { get; set; }

        [Required]
        public String Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        [Required]
        [RegularExpression(@"^(?!0\.00)\d{1,100}(\.\d{1,4})?$")]
        
        public double amount { get; set; }

        public DateTime DateTime {
            get {
                if (Date == null && Time == null)
                    return new DateTime();

               return  DateTime.Parse($"{Date} {Time}");
            }
        }
        public IEnumerable<Genre>  Genres{ get; set; }
    }
}