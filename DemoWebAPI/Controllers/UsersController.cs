using DataAccess;
using DataAccess.Context;
using DataAccess.Models;
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

        //Dangerous, this method should not be available to everyone
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            //Todo Error Handling
            return Ok(UserHelper.GetUsers(_dc));
        }

        [HttpGet("{userName}")]
        public ActionResult<User> GetUser(string userName)
        {
            //Todo Error Handling
            return Ok(UserHelper.GetUserByUserName(_dc, userName));
        }

    }
}
