using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace GigHub.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context ;
        public GigRepository(ApplicationDbContext context)
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<Gig> GetFutureGig()
        {
            return _context.Gigs.
               Include(g => g.Artist)
              .Include(g => g.Genre)
              .Where(g => g.DateTime > DateTime.Now)
               .ToList();
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs.
                Include(g => g.Attendances.Select(a => a.Attendee)).
               FirstOrDefault() ;
        }

        public IEnumerable<Gig> GetGigUserAttendance(string userId)
        {
            return _context.Attendances.Where(g => g.AttendeeId == userId).
                         Select(g => g.Gig).
                         Include(g => g.Artist).
                         Include(g => g.Genre).
                         ToList();
        }

        public Gig GetGig(int gigId)
        {
            return _context.Gigs.Include(g => g.Artist).
                                Include(g => g.Genre).
                                SingleOrDefault(g => g.ID == gigId);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
            _context.SaveChanges();
        }
    }
}