using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WX.MicroService.Interface;
using WX.MicroService.Model;

namespace WX.MicroService.Serviceinstance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _iuserService = null;
        private IConfiguration _iConfiguration = null;

        public UserController(ILogger<UserController> logger, IUserService userService, IConfiguration iconfiguration)
        {
            this._logger = logger;
            this._iuserService = userService;
            this._iConfiguration = iconfiguration;
        }

        [HttpGet]
        [Route("Get")]
        public User Get(int id)
        {
            return this._iuserService.FindUser(id);
        }

        [HttpGet]
        [Route("All")]
        public IEnumerable<User> Get()
        {
            Console.WriteLine($"This is {this._iConfiguration["port"]} invoke");
            return _iuserService.UserAll().Select(u=> new Model.User() { 
            Id=u.Id,
            Account=u.Account,
            Name=u.Name,
            Role=$"{this._iConfiguration["ip"]}:{this._iConfiguration["port"]}",
            Email=u.Email,
            LoginTime=u.LoginTime,
            Password=u.Password
            });
        }
    }
}