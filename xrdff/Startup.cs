using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace xrdff
{
    /// <summary>
    /// 执行顺序：构造函数 -> ConfigureServices -> Configure
    /// StartUp只是一个概念，类的名称是可以任意的，只需要在启动UseStartup中指定你启动类即可
    /// </summary>
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// 构造函数
        /// 通过注入实例化了该类用到的对象
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env, IConfiguration config, ILoggerFactory loggerFactory)
        {
            _env = env;
            _config = config;
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// 配置应用需要的服务（可选）
        /// 一般约定风格services.Addxxx，这样就可以让做这些服务在应用和configure方法使用
        /// </summary>
        /// <param name="services">IServiceCollection是一个原生的IOC容器，所有需要用到的服务都可以注册到里面</param>
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var logger = _loggerFactory.CreateLogger<Startup>();

            if (_env.IsDevelopment())
            {
                //开发环境配置
                logger.LogInformation("Development environment");
            }
            else
            {
                //非开发环境配置
                logger.LogInformation($"Enbironment:{_env.EnvironmentName}");
            }

        }

        /// <summary>
        /// 创建应用的处理管道,用于构建管道处理Http请求
        /// 管道中中的每个中间件（Middleware）组件负责请求处理和选择是否将请求传递到管道中的下一个组件
        /// 这里可以添加自己想要的中间件来处理每一个Http请求，一般是使用上面的ConfigreServices方法中注册好的服务
        /// 一般用法是app.Usexxx，这个Usexxx方法是基于IApplicationBuilder的扩展
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //使用开发者错误页去展现应用运行错误
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //在下面的中间件，可以使用异常处理中间件去捕获异常
                //建议放在中间件的最前面，这样可以捕获稍后调用中发生的任何异常
                app.UseExceptionHandler("/Error");
            }

            //返回一个静态的文件并且结束管道
            //app.UseStaticFiles();

            //使用cookie策略中间件来遵守欧盟通用护具保护法规（GDPR）的规定
            //app.UseCookiePolicy();

            //在用户访问安全资源之前进行身份验证
            //app.UseAuthentication();

            //如果应用程序使用会话状态，则在Cookie策略中间件之后和MVC中间件之前调用会话中间件
            //app.UseSession();

            ///添加MVC得到请求的处理管道
            //app.UseMvc();

            app.Run(async (context) =>
            {
                //解决中文乱码问题
                context.Response.ContentType = "text/plain; charset=utf-8";
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
