using BigSchool.DTOs;
using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace BigSchool.Controllers
{

    public class FollowingsController : ApiController
    {

        private readonly ApplicationDbContext _dbContext;
        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.Following.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId))
            {
                var following = new Following
                {
                    FollowerId = userId,
                    FolloweeId = followingDto.FolloweeId,
                };

                _dbContext.Following.Attach(following);
                _dbContext.Following.Remove(following);
                _dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                var following = new Following
                {
                    FollowerId = userId,
                    FolloweeId = followingDto.FolloweeId,
                };

                _dbContext.Following.Add(following);
                _dbContext.SaveChanges();
                return Ok();
            }
        }
    }
}
