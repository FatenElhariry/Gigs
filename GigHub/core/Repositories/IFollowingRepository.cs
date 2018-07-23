using GigHub.core.Models;

namespace GigHub.core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string followerId, string followeeId);
    }
}