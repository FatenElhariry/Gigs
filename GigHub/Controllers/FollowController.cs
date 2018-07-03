using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    public class FollowDto
    {
        public string FollowerId { get; set; }
        public string FolloweeId { get; set; }
    }
    public class FollowController : ApiController
    {
        private ApplicationDbContext _context;
        public FollowController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: api/Follow
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Follow/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Follow
        public IHttpActionResult Post(FollowDto FollowDto)
        {
            try
            {
                string FolloweeId = FollowDto.FolloweeId;
                string userID = User.Identity.GetUserId();
                bool exist = _context.Followings
                    .Any(c => c.FolloweeId == FolloweeId &&
                        c.FollowerId == userID);

                if (exist)
                    return BadRequest("this Attendance Already Exists.");

                var followee = _context.Users
                    .FirstOrDefault(c => c.Id == FolloweeId);
                if (followee == null)
                    return Content(System.Net.HttpStatusCode.NotFound,
                        $"The Artist with id {FolloweeId} is not found.");

                var follow = new Following()
                {
                    FollowerId = userID,
                    FolloweeId = FolloweeId
                };

                _context.Followings.Add(follow);
                _context.SaveChanges();
            }
            catch (System.Exception e)
            {

                return StatusCode(System.Net.HttpStatusCode.InternalServerError);
            }


            return Ok("Added Successfully.");

            
        }

        // PUT: api/Follow/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Follow/5
        public void Delete(int id)
        {
        }
    }
}
