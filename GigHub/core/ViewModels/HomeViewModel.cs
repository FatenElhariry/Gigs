using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Gig> UpComingGigs { get; set; }

        public IEnumerable<Attendance> userGigs { get; set; }

        public bool ShowActions { get; set; }

        
    }
}