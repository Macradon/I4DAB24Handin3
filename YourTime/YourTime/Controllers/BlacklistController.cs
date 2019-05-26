using System.Collections.Generic;
using YourTime.Models;
using YourTime.Services;
using Microsoft.AspNetCore.Mvc;

namespace YourTime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BlacklistController : ControllerBase
    {
        private readonly BlacklistServices _blacklistServices;

        public BlacklistController(BlacklistServices wallService)
        {
            _blacklistServices = wallService;
        }

        [HttpGet]
        public ActionResult<List<Blacklist>> Get()
        {
            return _blacklistServices.Get();
        }

        [HttpGet("{id:length(24)}", Name = "Getblk")]
        public ActionResult<Blacklist> Get(string id)
        {
            var blacklist = _blacklistServices.Get(id);

            if (blacklist == null)
            {
                return NotFound();
            }

            return blacklist;
        }

        [HttpPost]
        public ActionResult<Blacklist> Create(Blacklist blacklist)
        {
            _blacklistServices.Create(blacklist);

            return CreatedAtRoute("Getblk", new { id = blacklist.UserId.ToString() }, blacklist);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Blacklist blkIn)
        {
            var wall = _blacklistServices.Get(id);

            if (wall == null)
            {
                return NotFound();
            }

            _blacklistServices.Update(id, blkIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var blacklist = _blacklistServices.Get(id);

            if (blacklist == null)
            {
                return NotFound();
            }

            _blacklistServices.Remove(blacklist.UserId);

            return NoContent();
        }
    }
}

