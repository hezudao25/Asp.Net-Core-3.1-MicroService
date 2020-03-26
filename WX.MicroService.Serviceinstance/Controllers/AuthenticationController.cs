using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WX.MicroService.Interface;
using WX.MicroService.Model;

namespace WX.MicroService.Serviceinstance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService _authService;
        private IUserService _iuserService = null;
        public AuthenticationController(IAuthenticateService authService, IUserService iuserService)
        {
            this._authService = authService;
            this._iuserService = iuserService;
        }
        [AllowAnonymous]
        [HttpPost, Route("requestToken")]
        public ActionResult RequestToken([FromBody] LoginRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }

            string token;
            if (_authService.IsAuthenticated(request, out token))
            {
                return Ok(token);
            }

            return BadRequest("Invalid Request");

        }

        [HttpGet]
        [Route("Get")]
        [Authorize]
        public User Get(int id)
        {
            return this._iuserService.FindUser(id);
        }
    }

}