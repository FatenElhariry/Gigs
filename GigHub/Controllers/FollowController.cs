using GigHub.core.Models;
using GigHub.Presistence.Models;
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
        //public IHttpActionResult Delete (string artistId)
        //{

        //    return Ok();
        //}


        // PUT: api/Follow/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Follow/5
        public IHttpActionResult Delete(string artistID)
        {
            string userID = User.Identity.GetUserId();

            var follow = _context.Followings.
                                  FirstOrDefault(c => c.FolloweeId == artistID &&
                                                c.FollowerId == userID);
            if (follow == null)
                return Content(System.Net.HttpStatusCode.NotFound, "this follow are not found");
            else
            {
                _context.Followings.Remove(follow);
                _context.SaveChanges();
            }
            return Ok();
        }
    }
}
