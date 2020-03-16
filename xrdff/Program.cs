using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace xrdff
{
    public class Program
    {
        /// <summary>
        /// 程序主入口，在Startup之前执行
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //使用Build方法构建管道,返回WebHost
            //使用Run方法启动应用并开始监听所有到来的Http请求
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //使用UseStartup指定启动类
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();

        ////如果不想使用Startup类的话，可以使用下面的服务注册和管道构建
        //WebHost.CreateDefaultBuilder(args)
        //    .ConfigureAppConfiguration((hostingContext, config) =>
        //    {

        //    })
        //    .ConfigureServices(services =>
        //    {

        //    })
        //    .Configure(app =>
        //    {
        //        var loggerFactory = app.ApplicationServices
        //          .GetRequiredService<ILoggerFactory>();
        //        var logger = loggerFactory.CreateLogger<Program>();
        //        var env = app.ApplicationServices.GetRequiredServices<IHostingEnvironment>();
        //        var config = app.ApplicationServices.GetRequiredServices<IConfiguration>();

        //        logger.LogInformation("Logged in Configure");

        //        if (env.IsDevelopment())
        //        {
        //            ...
        //        }
        //        else
        //        {
        //            ...
        //        }

        //        var configValue = config["subsection:suboption1"];
        //    });

    }
}
