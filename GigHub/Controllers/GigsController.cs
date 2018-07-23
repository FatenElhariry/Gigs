using GigHub.Models;
using GigHub.Presistence;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;



namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        #region ugly nosie code 
        /*
         * here this class will be very depandancy to all repository on the 
         * system and it will be ugly to add a lot of refrance here the 
         * best chose is the separation of concerns so we will transfare all 
         * to unit of work class 
        private readonly GigRepository _gigRepository;
        private readonly AttendaceRepository _attendaceRepository;
        private readonly FollowingRepository _followingRepository;*/
        #endregion

        private readonly UnitOfWork _unitOfWork;

        public GigsController()
        {
            _context = new ApplicationDbContext();
            #region No Need For This 
            //_gigRepository = new GigRepository(_context);
            //_attendaceRepository = new AttendaceRepository(_context);
            //_followingRepository = new FollowingRepository(_context);
            #endregion
            _unitOfWork = new UnitOfWork(_context);
        }

        public ActionResult index(string query=null)
        {
            var upComingGig = _unitOfWork.Gigs.GetFutureGig();

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
                Attendances = _unitOfWork.Attendaces.GetFutureAttandance(userId).
                                                    ToLookup(g => g.GigId);
                        /*_attendaceRepository.GetFutureAttandance(userId).
                                       ToLookup(g => g.GigId)*/
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
            _gigRepository.Add(gig);
            _unitOfWork.Complete();
            //_context.Gigs.Add(gig);
            //_context.SaveChanges();

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