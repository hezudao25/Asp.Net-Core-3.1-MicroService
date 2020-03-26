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
    public class HealthController : ControllerBase    {
       
        private IConfiguration _iConfiguration = null;
        public HealthController(ILogger<HealthController> logger,IConfiguration iconfiguration)
        {
                
            this._iConfiguration = iconfiguration;
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            Console.WriteLine($"{this._iConfiguration["port"]}");
            return Ok();
        }
       
    }
}