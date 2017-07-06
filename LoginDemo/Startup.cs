using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using LoginDemo.Models; // for User
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // for IdentityRole
using Microsoft.EntityFrameworkCore; // for option.UseSqlServer()
using Microsoft.Extensions.Configuration; // for ConfigurationBuilder

namespace LoginDemo
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("AppSettings.json");
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // 添加 Entity Framework 服务，并且使用SQL Server  服务。
            services.AddEntityFrameworkSqlServer().
                AddDbContext<LoginDemoDbContext>(option => option.UseSqlServer(Configuration["database:connection"]));

            // 添加 Identity 服务。
            // 当 Identity 和 Entity Framework 一起使用时，需要调用 AddEntityFrameworkStores 这个方法。
            // AddEntityFrameworkStores 会配置 UserStore 服务，
            // UserStore 服务用于创建用户验证密码。
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<LoginDemoDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(ConfigureRoute);


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        private void ConfigureRoute(IRouteBuilder routeBuilder)
        {
            //Home/Index , 注意： = 两边不能有空格，否则路由无效。
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
