using GigHub.Repositories;
using GigHub.Presistence.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using GigHub.ViewModels;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly GigRepository _gigRepository;
        private readonly AttendaceRepository _attendaceRepository;
        public HomeController()
        {
            _context = new ApplicationDbContext();
            _gigRepository = new GigRepository(_context);
            _attendaceRepository = new AttendaceRepository(_context);
        }
        public ActionResult Index(string query = null)
        {
            /*
             if i send it without .tolist and use artist.username in view it will query it again
             while the first query is retuned as IQyeryable ==> represent with DataReader opened 
             while excutation
             instead of this use this 
             _context.Gigs.Include(g => g.Artist.UserName).Where(g => g.DateTime > DateTime.Now).ToList();
             it will get the username with the query 
             */
            var upComingGig = _gigRepository.GetFutureGig();

            if (!string.IsNullOrWhiteSpace(query))
            {
                upComingGig = upComingGig.Where(g => g.Genre.Name.Contains(query) ||
                                                    g.Artist.Name.Contains(query) ||
                                                    g.Venue.Contains(query)).ToList();
            }

            //var userGigs = new List<Attendance>();
            //var userFollowing = new List<Following>();

            string userId = User.Identity.GetUserId();

            
            #region MyRegion
            //if (User.Identity.IsAuthenticated)
            //{
            //    userGigs = _context.Attendances.Include(c => c.Attendee).
            //         Where(a => a.AttendeeId == userId &&
            //                    a.Gig.DateTime > DateTime.Now).
            //        ToList();

            //    userFollowing = _context.Followings.Include(c => c.Followee).ToList();
            //}


            //upComingGig.All(c =>
            //{
            //    var attendance = _context.Attendances.
            //                FirstOrDefault(a => a.GigId == c.ID && a.AttendeeId == userId);
            //    if(attendance != null)
            //        userGigs.Add(attendance);
            //    return true;
            //});


            //HomeViewModel viewModel = new HomeViewModel()
            //{
            //    ShowActions = User.Identity.IsAuthenticated,
            //    userGigs = userGigs,
            //    UpComingGigs = upComingGig
            //};
            #endregion

            GigViewModel viewModel = new GigViewModel() {
                UpComingGigs = upComingGig,
                Attendances = _attendaceRepository.GetFutureAttandance(userId).ToLookup(g => g.GigId),
                Heading = "Upcoming Gigs",
                ShowActions = User.Identity.IsAuthenticated,
                SearchTerm = query
            };
            return View("Gigs",viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}