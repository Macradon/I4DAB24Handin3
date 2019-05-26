using System.Collections.Generic;
using YourTime.Models;
using YourTime.Services;
using Microsoft.AspNetCore.Mvc;

namespace YourTime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CircleController:ControllerBase
    {
        private readonly CircleServices _circleServices;

        public CircleController(CircleServices circleService)
        {
            _circleServices = circleService;
        }

        [HttpGet]
        public ActionResult<List<Circle>> Get()
        {
            return _circleServices.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetCircle")]
        public ActionResult<Circle> Get(string id)
        {
            var circles = _circleServices.Get(id);

            if (circles == null)
            {
                return NotFound();
            }

            return circles;
        }

        [HttpPost]
        public ActionResult<Circle> Create(Circle circle)
        {
            _circleServices.Create(circle);

            return CreatedAtRoute("GetCircle", new { id = circle.Id.ToString() }, circle);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Circle circleIn)
        {
            var circle = _circleServices.Get(id);

            if (circle == null)
            {
                return NotFound();
            }

            _circleServices.Update(id, circleIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var circle = _circleServices.Get(id);

            if (circle == null)
            {
                return NotFound();
            }

            _circleServices.Remove(circle.Id);

            return NoContent();
        }
    }
}

