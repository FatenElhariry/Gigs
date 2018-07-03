using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;



namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult index(string query=null)
        {
            var upComingGig = _context.Gigs.
                Include(g => g.Artist)
               .Include(g => g.Genre)
               .Where(g => g.DateTime > DateTime.Now)
                .ToList();

            if (!string.IsNullOrWhiteSpace(query))
            {
                upComingGig = upComingGig.Where(g => g.Genre.Name.Contains(query) ||
                                                    g.Artist.Name.Contains(query) ||
                                                    g.Venue.Contains(query)).ToList();
            }

            string userId = User.Identity.GetUserId();
            var attendances = _context.Attendances.
                                        Where(a => a.AttendeeId == userId &&
                                               a.Gig.DateTime > DateTime.Now).
                                       ToList().
                                       ToLookup(g => g.GigId);

            GigViewModel model = new GigViewModel()
            {
                UpComingGigs = upComingGig,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gig ",
                SearchTerm = query,
                Attendances = attendances
            };



            return View("Gigs",model);
        }

        public ActionResult Details(int gigId)
        {
            string userId = User.Identity.GetUserId();

            var gig = _context.Gigs.
                               Include(g => g.Artist ).
                               Include(g => g.Genre).
                               FirstOrDefault(g => g.ID == gigId);
                               
            GigDetailsViewModel model = new GigDetailsViewModel()
            {
                Gig = gig,
                IsAttending = _context.Attendances.
                                       Any(a => a.AttendeeId == userId && a.GigId == gigId),
                IsFollowing = _context.Followings.
                                        Any(f => f.FollowerId == userId && f.FolloweeId == gig.Artist.Id)
            };
            return View(model);
        }
        public ActionResult Attending(string query = null) {

            string userId = User.Identity.GetUserId();

            var gigs = _context.Attendances.Where(g => g.AttendeeId == userId).
                        Select(g => g.Gig).
                        Include(g => g.Artist).
                        Include(g => g.Genre).
                        ToList();


            GigViewModel model = new GigViewModel()
            {
                UpComingGigs = gigs,
                ShowActions = false,
                Heading = "Gigs I'm Attending"
            };
            return View("Gigs",model);
        }

        [Authorize]
        // GET: Gigs
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel() {
                Genres =_context.Genres.ToList()};
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