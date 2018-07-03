using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;


namespace GigHub.Controllers
{

    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attendance([FromBody] AttendaceDto attendaceDto)
        { 
            try
            {
                string userID = User.Identity.GetUserId();

                var gig = _context.Gigs.FirstOrDefault(c => c.ID == attendaceDto.gigId);
                if (gig == null)
                    return Content(System.Net.HttpStatusCode.NotFound,
                        $"The Gig with id {attendaceDto.gigId} is not found.");

                var exist = _context.Attendances.
                    FirstOrDefault(c => c.GigId == attendaceDto.gigId && 
                        c.AttendeeId == userID);

                if (exist != null)
                {
                    _context.Attendances.Remove(exist);
                    _context.SaveChanges();
                }
                else
                {
                    var attendace = new Attendance()
                    {
                        AttendeeId = userID,
                        GigId = attendaceDto.gigId
                    };

                    _context.Attendances.Add(attendace);
                    _context.SaveChanges();
                }
               
               
            }
            catch (System.Exception e)
            {

                return StatusCode(System.Net.HttpStatusCode.InternalServerError);
            }
            

            return Ok("Added Successfully.");
        }

        [HttpDelete]
        public IHttpActionResult Delete(int gigId)
        {
            string userID = User.Identity.GetUserId();

            var gig = _context.Gigs.FirstOrDefault(c => c.ID == gigId);
            if (gig == null)
                return Content(System.Net.HttpStatusCode.NotFound,
                    $"The Gig with id {gigId} is not found.");

            var exist = _context.Attendances.
                FirstOrDefault(c => c.GigId == gigId &&
                    c.AttendeeId == userID);

            if (exist == null)
                return NotFound();

            _context.Attendances.Remove(exist);
            _context.SaveChanges();

            return Ok(gigId);
        }
    }
}
