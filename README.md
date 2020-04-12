# MicroService
aspnet core 3.1 关于微服务架构1.0

consul 总结：
1. 使用consul可以注册与发现服务，还有健康检查，自动下线，但不管调用

2. 没有实现负载均衡， 这个需要自己处理，本代码有3种方式。

3. 没有解决客户端和服务端调用的复杂关系。

consul 下载地址：https://www.consul.io/downloads.html
命令：consul agent -dev

aspnet core 3.1 微服务构架2.0

Ocelot + polly (GateWay网关)

polly 缓存  限流 熔断 合并请求 等服务治理

 JSON Web Token（JWT）授权

增加 Ocelot+ IdentityServe验证


文档地址：https://ocelot.readthedocs.io/en/latest/introduction/gettingstarted.html

![Image text](https://raw.githubusercontent.com/hezudao25/MicroService/master/MicroService/wwwroot/image/2.0.png)



