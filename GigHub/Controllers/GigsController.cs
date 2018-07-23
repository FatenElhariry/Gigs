using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;



namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly GigRepository _gigRepository;
        private readonly AttendaceRepository _attendaceRepository;
        private readonly FollowingRepository _followingRepository;
        public GigsController()
        {
            _context = new ApplicationDbContext();
            _gigRepository = new GigRepository(_context);
            _attendaceRepository = new AttendaceRepository(_context);
            _followingRepository = new FollowingRepository(_context);
        }

        public ActionResult index(string query=null)
        {
            var upComingGig = _gigRepository.GetFutureGig();

            if (!string.IsNullOrWhiteSpace(query))
            {
                upComingGig = upComingGig.Where(g => g.Genre.Name.Contains(query) ||
                                                    g.Artist.Name.Contains(query) ||
                                                    g.Venue.Contains(query)).ToList();
            }

            string userId = User.Identity.GetUserId();
            
    

            GigViewModel model = new GigViewModel()
            {
                UpComingGigs = upComingGig,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gig ",
                SearchTerm = query,
                Attendances = _attendaceRepository.GetFutureAttandance(userId).
                                       ToLookup(g => g.GigId)
            };



            return View("Gigs",model);
        }

        public ActionResult Details(int gigId)
        {
            string userId = User.Identity.GetUserId();

            var gig = _gigRepository.GetGig(gigId);
                               
            GigDetailsViewModel model = new GigDetailsViewModel()
            {
                Gig = gig,
                IsAttending = _attendaceRepository.GetAttendance(gigId,userId) != null,/*_context.Attendances.
                                       Any(a => a.AttendeeId == userId && a.GigId == gigId),*/
                IsFollowing = _followingRepository.GetFollowing(userId,gig.ArtistId) != null/*_context.Followings.
                                        Any(f => f.FollowerId == userId && f.FolloweeId == gig.Artist.Id)*/
            };
            return View(model);
        }
        public ActionResult Attending(string query = null) {

            string userId = User.Identity.GetUserId();


            GigViewModel model = new GigViewModel()
            {
                UpComingGigs = _gigRepository.GetGigUserAttendance(userId),
                ShowActions = false,
                Heading = "Gigs I'm Attending",
                Attendances = _attendaceRepository.GetFutureAttandance(userId).
                                       ToLookup(g => g.GigId)
            };
            return View("Gigs",model);
        }

        [Authorize]
        // GET: Gigs
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel() {
                Genres =_context.Genres.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel model)
        {
            string AutherId = User.Identity.GetUserId();

            //var genre = _context.Genres.Single(c => c.ID == model.Genre);
            //var author = _context.Users.Single(c => c.Id == AutherId);
            if (!ModelState.IsValid)
            {
                model.Genres = _context.Genres.ToList();
                return View("create", model);
            }
            var gig = _context.Gigs.Add(new Gig()
            {
                DateTime = model.DateTime,
                ArtistId = AutherId ,
                GenreId = model.Genre,
                Venue = model.Venue
            }); 

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Gigs()
        {


            return View();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);

        }
    }
}