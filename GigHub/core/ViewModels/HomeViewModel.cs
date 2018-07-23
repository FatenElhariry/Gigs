using GigHub.core.Models;
using System.Collections.Generic;

namespace GigHub.core.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Gig> UpComingGigs { get; set; }

        public IEnumerable<Attendance> userGigs { get; set; }

        public bool ShowActions { get; set; }

        
    }
}