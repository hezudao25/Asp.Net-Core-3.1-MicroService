using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.MicroService.Serviceinstance.Utility
{
    public static class Consulhelper
    {
        public static void ConsulRegist(this IConfiguration configuration)
        {
            ConsulClient client = new ConsulClient(c =>{
                c.Address = new Uri("http://localhost:8500/");
                c.Datacenter = "wx1";
            });

            string ip = configuration["ip"];
            int port = int.Parse(configuration["port"]); //命令行参数输入 --port=xxx --ip=xx
            int weight = string.IsNullOrWhiteSpace(configuration["weight"]) ? 1 : int.Parse(configuration["weight"]);
            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID="service"+ Guid.NewGuid(), //唯一
                Name = "HezudaoService",
                Address=ip,
                Port=port,
                Tags=new string[] {weight.ToString()}, //标签
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(12), //间隔12秒
                    HTTP=$"http://{ip}:{port}/Api/Health/Index",
                    Timeout = TimeSpan.FromSeconds(5), //检查等待时间
                    DeregisterCriticalServiceAfter=TimeSpan.FromSeconds(60)  //失败后多久移除
                }
            });

            // 命令行参数获取
            Console.WriteLine($"{ip}:{port}--{weight}");
        }
    }
}
