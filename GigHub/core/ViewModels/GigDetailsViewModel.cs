﻿using GigHub.core.Models;

namespace GigHub.ViewModels
{
    public class GigDetailsViewModel
    {
        public Gig Gig { get; set; }

        public bool IsAttending { get; set; }
        public bool IsFollowing { get;  set; }
    }
}