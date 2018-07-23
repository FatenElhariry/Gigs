using GigHub.core.Models;
using GigHub.core.Repositories;
using GigHub.Presistence.Models;
using System.Linq;

namespace GigHub.Presistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context ;
        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string followerId ,string followeeId)
        {
            return _context.Followings.
                            FirstOrDefault(f => f.FollowerId == followeeId && 
                                                f.FolloweeId == followeeId);
        }
    }
}