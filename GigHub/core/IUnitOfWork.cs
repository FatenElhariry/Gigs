using GigHub.core.Repositories;
namespace GigHub.core
{   /*there is no dependences */
    public interface IUnitOfWork
    {
        IAttendaceRepository Attendaces { get; }
        IFollowingRepository Followings { get; }
        IGigRepository Gigs { get; }
        IGenresRepository Genres { get; }

        void Complete();
    }
}