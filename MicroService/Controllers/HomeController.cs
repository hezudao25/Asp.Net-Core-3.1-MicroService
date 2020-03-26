using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicroService.Models;
using WX.MicroService.Interface;
using System.Net.Http;
using WX.MicroService.Model;

namespace MicroService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _iUserService= null;
        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _iUserService = userService;
        }

        public IActionResult Index()
        {
            //分布式
            // base.ViewBag.Users = this._iUserService.UserAll();
            string url = "http://localhost:5166/api/user/All"; //调用webapi
            string content = InvokeApi(url);
            base.ViewBag.Users = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<User>>(content);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// 调用API
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string InvokeApi(string url)
        {
            using(HttpClient httpclient = new HttpClient())
            {
                HttpRequestMessage message = new HttpRequestMessage();
                message.Method = HttpMethod.Get;
                message.RequestUri = new Uri(url);
                var result = httpclient.SendAsync(message).Result;
                string content = result.Content.ReadAsStringAsync().Result;
                return content;
            }
        }
    }
}
