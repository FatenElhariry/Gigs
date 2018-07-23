using GigHub.core.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.core.ViewModels
{
    public class GigViewModel
    {

        public IEnumerable<Gig> UpComingGigs { get; set; }

        public ILookup<int,Attendance> Attendances { get; set; }

        public bool ShowActions { get; set; }
        public string Heading { get;  set; }
        public string SearchTerm { get; internal set; }
    }
}