using System.Collections.Generic;
using YourTime.Models;
using YourTime.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace YourTime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userService;
        private readonly CircleServices _circleService;
        private IMongoCollection<User> _users;
        private IMongoCollection<Post> _posts;

        public UserController(UserServices userService, CircleServices circleService)
        {
            _userService = userService;
            _circleService = circleService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return _userService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<User> Get( string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            return user;
        }

        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            _userService.Create(user);

            return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, User userIn)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            _userService.Update(id, userIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            _userService.Remove(user.Id);

            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult ShowFeed(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            var posts = _posts.Find(post =>
                    (post.UserID == id ||
                    (user.Circles.Contains(post.Privacy) && post.Privacy != "") ||
                    (user.Follows.Contains(post.UserID) && post.Privacy == "")) &&
                    !user.Blacklisted.Contains(post.UserID)
                    )
                .SortByDescending(post => post.Id).Limit(5).ToList();

            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult<List<Post>> ShowWall(string id, string id2)
        {
            var owner = _users.Find(userFiltering => userFiltering.Id == id).FirstOrDefault();
            var viewer = _users.Find(userFiltering => userFiltering.Id == id2).FirstOrDefault();

            if ((owner == null) || (viewer == null))
            {
                return BadRequest("User(s) not found");
            }

            if (viewer.Blacklisted.Contains(owner.Id))
            {
                return BadRequest("Viewer is blocked by owner");
            }

            var posts = _posts.Find(post =>
                    post.UserID == owner.Id &&
                    viewer.Circles.Contains(post.Privacy))
                .Limit(5).ToList();

            return Ok(posts);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Blacklist(string id, string id2)
        {
            //The User who wants to blacklist
            var user = _userService.Get(id);
            var user2 = _userService.Get(id2);

            if ((user == null) || (user2 == null))
            {
                return BadRequest("User(s) not found");
            }

            user.Blacklist.Add(id2);
            user2.Blacklisted.Add(id);

            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Follow(string id, string id2)
        {
            //The User who wants to blacklist
            var user = _userService.Get(id);
            var user2 = _userService.Get(id2);

            if ((user == null) || (user2 == null))
            {
                return BadRequest("User(s) not found");
            }

            user.Follows.Add(id2);

            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult JoinCircle(string userId, string circleId)
        {
            //The User who wants to blacklist
            var user = _userService.Get(userId);
            var circle = _circleService.Get(circleId);

            if (user == null)
            {
                return BadRequest("User not found");
            }
            if (circle == null)
            {
                return BadRequest("Circle not found");
            }

            user.Circles.Add(circleId);
            circle.UsersId.Add(userId);

            return NoContent();
        }
    }
}