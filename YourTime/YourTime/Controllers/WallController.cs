using System.Collections.Generic;
using YourTime.Models;
using YourTime.Services;
using Microsoft.AspNetCore.Mvc;

namespace YourTime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WallController : ControllerBase
    {
        private readonly WallServices _wallServices;

        public WallController(WallServices wallService)
        {
            _wallServices = wallService;
        }

        [HttpGet]
        public ActionResult<List<Wall>> Get()
        {
            return _wallServices.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetWall")]
        public ActionResult<Wall> Get(string id)
        {
            var wall = _wallServices.Get(id);

            if (wall == null)
            {
                return NotFound();
            }

            return wall;
        }

        [HttpPost]
        public ActionResult<Wall> Create(Wall wall)
        {
            _wallServices.Create(wall);

            return CreatedAtRoute("GetWall", new { id = wall.UserId.ToString() }, wall);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Wall WallIn)
        {
            var wall = _wallServices.Get(id);

            if (wall == null)
            {
                return NotFound();
            }

            _wallServices.Update(id, WallIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var wall = _wallServices.Get(id);

            if (wall == null)
            {
                return NotFound();
            }

            _wallServices.Remove(wall.UserId);

            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult AddPost(string wallId, string postId)
        {
            var wall = _wallServices.Get(wallId);

            if (wall == null)
            {
                return NotFound();
            }

            wall.PostIds.Add(postId);

            return NoContent();
        }
    }
}

