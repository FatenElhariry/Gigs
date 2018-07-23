using GigHub.core.Models;

namespace GigHub.core.ViewModels
{
    public class GigDetailsViewModel
    {
        public Gig Gig { get; set; }

        public bool IsAttending { get; set; }
        public bool IsFollowing { get;  set; }
    }
}