using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourTime.Models;
using YourTime.Services;

namespace YourTime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;

        public UserController(UserServices userServi)
        {
            _userServices = userServi;
        }

        [HttpGet]
        public ActionResult<List<User>> GetblkList()
        {
            return _userServices.GetBlackList();
        }

        public ActionResult<List<UserController>>  GetPost()
        {
           // return _userServices.GetPost();
           return _userServices.GetPost();
        }

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<User> Get(string id)
        {
            var user = _userServices.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            _userServices.Create(user);

            return CreatedAtRoute("Get User", new { id = user.Id.ToString() }, user);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, User UserIn)
        {
            var user = _userServices.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userServices.Update(id, user);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userServices.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userServices.RemoveCircle(user.Id);

            return NoContent();
        }
    }
}