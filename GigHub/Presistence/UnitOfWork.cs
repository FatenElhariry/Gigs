using GigHub.Models;
using GigHub.Repositories;

namespace GigHub.Presistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public GigRepository Gigs { get; private set; }
        public AttendaceRepository Attendaces { get; private set; }
        public FollowingRepository Followings { get; private set; }
        

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(_context);
            Attendaces = new AttendaceRepository(_context);
            Followings = new FollowingRepository(_context);

        }
        /// <summary>
        /// you can call it save or saveChanges the best for me is complete 
        /// baceuse it complete the work that i am going to do it's more semantic
        /// </summary>
        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}