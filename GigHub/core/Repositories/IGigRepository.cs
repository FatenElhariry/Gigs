using GigHub.core.Models;
using System.Collections.Generic;

namespace GigHub.core.Repositories
{
    public interface IGigRepository
    {
        IEnumerable<Gig> GetFutureGig();
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigUserAttendance(string userId);
        Gig GetGigWithAttendees(int gigId);

        void Add(Gig gig);
    }
} 