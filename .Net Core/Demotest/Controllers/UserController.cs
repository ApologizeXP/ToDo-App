using Demotest.Data;
using Demotest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demotest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DemoContext _dbContext;
        public UserController(DemoContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetUsers")]
        public IActionResult Get()
        {
            try
            {
                var users = _dbContext.Users.ToList();
                if (users.Count == 0)
                {
                    return StatusCode(404, "No user found");
                }
                return Ok(users);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error has occurred");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserRequest request)
        {
            try
            {
                
                var username = _dbContext.Users.FirstOrDefault(u => u.username == request.username);
                var password = _dbContext.Users.FirstOrDefault(u => u.password == request.password);
                if(username == null || password == null)
                {
                        return StatusCode(404, "No user found");
                }
                var iduser = username.userid;
                var idpass = password.userid;
                if (iduser != idpass)
                {
                    return StatusCode(404, "No user found");
                }
                return Ok(username);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error has occurred");
            }
        }
    }
}
