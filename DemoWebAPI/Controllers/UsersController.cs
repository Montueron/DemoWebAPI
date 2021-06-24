using DataAccess;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly DataContext _dc;

        public UsersController(DataContext dc)
        {
            _dc = dc;
        }

        //Only admin role may perform this request
        [HttpGet]
        [Authorize]
        public ActionResult<List<User>> Get()
        {
            var currentUser = HttpContext.User;
            string currentUserRole = string.Empty;
            if (currentUser.HasClaim(c => c.Type == "UserRole"))
            {
                currentUserRole = currentUser.Claims.FirstOrDefault(c => c.Type == "UserRole").Value;
            }
            //Todo Error Handling
            if (currentUserRole == "admin")
            {
                return Ok(UserHelper.GetUsers(_dc));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("{userName}")]
        [Authorize]
        public ActionResult<User> GetUser(string userName)
        {
            //Todo Error Handling
            //Todo this operation should only retrieve the userInfo that corresponds to the logguedInUser, except for admin
            return Ok(UserHelper.GetUserByUserName(_dc, userName));
        }

    }
}
