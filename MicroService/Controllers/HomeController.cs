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
using Consul;

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
            //分布式（从一进程到多进程）
            // base.ViewBag.Users = this._iUserService.UserAll();
            string url = "http://HezudaoService/api/user/All"; //调用webapi
            // consul做了DNS域名解析
            #region 调用consul 集群
            {
                ConsulClient client = new ConsulClient(c =>
                {
                    c.Address = new Uri("http://localhost:8500/");
                    c.Datacenter = "dc1";
                });
                var response = client.Agent.Services().Result.Response;
                //foreach(var item in response)
                //{
                //    Console.WriteLine("***************************");
                //    Console.WriteLine(item.Key);
                //    var server = item.Value;
                //    Console.WriteLine($"{server.Address}--{server.Port}--{server.Service}");
                //    Console.WriteLine("***************************");
                //}
                Uri uri = new Uri(url);
                string groupName = uri.Host;
                /// 服务注册 和恢复
                /// consul 下载地址 https://www.consul.io/downloads.html
                /// 运行命令 consul agent -dev
                var serviceDictionary =
                response.Where(s => s.Value.Service.Equals(groupName, StringComparison.OrdinalIgnoreCase)).ToArray();               
                //{
                //  //  url = $"http://{serviceDictionary.First().Value.Address}:{serviceDictionary.First().Value.Port}/api/user/all";  //取第一个
                //    url = $"{uri.Scheme}://{serviceDictionary.First().Value.Address}:{serviceDictionary.First().Value.Port}{uri.PathAndQuery}";
                    
                //}

                // 负载均衡
                AgentService agengService = null;
                //{
                //    // 1.平均策略， 多个实例，平均分配--随机就是平均
                //    agengService = serviceDictionary[new Random(DateTime.Now.Millisecond+iSeed++).Next(0, serviceDictionary.Length)].Value;
                //}
                //{
                //    // 2.轮询策略 --很僵化
                //    agengService = serviceDictionary[iSeed % serviceDictionary.Length].Value;
                //}
                {
                    //3.权重策略
                    List<KeyValuePair<string, AgentService>> pairsList = new List<KeyValuePair<string, AgentService>>();
                    foreach(var pair in serviceDictionary)
                    {
                        int count = int.Parse(pair.Value.Tags?[0]); //把几个元素 循环放进一个集合
                        for(int i=0;i<count;i++)
                        {
                            pairsList.Add(pair);
                        }
                    }
                    agengService = pairsList.ToArray()[new Random(iSeed++).Next(0, pairsList.Count())].Value;
                }

                url = $"{uri.Scheme}://{agengService.Address}:{agengService.Port}{uri.PathAndQuery}";
            }
            #endregion

            string content = InvokeApi(url);
            base.ViewBag.Users = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<User>>(content);
           // Console.WriteLine($" url: {url}");
            return View();
        }
        private static int iSeed = 0;
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
