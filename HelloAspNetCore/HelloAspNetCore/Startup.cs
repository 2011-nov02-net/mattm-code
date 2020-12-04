using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HelloAspNetCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //app.Use(async (context, next) =>
                //{
                //    try
                //    {
                //        await next();
                //    }
                //    catch (Exception e)
                //    {
                //        await context.Response.WriteAsync(e.ToString());
                //    }
                //});
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                //               route parameter
                //                         |
                endpoints.MapGet("/asdf/{word}", async context =>
                {
                    // e.g. /asdf/microsoft?name=nick
                    var word = context.Request.RouteValues["word"];
                    var name = context.Request.Query["name"];

                    await context.Response.WriteAsync($"Hello {name}! ({word})");
                });
            });

            app.Use(async (context, next) =>
            {
                // request processing logic before the next middleware runs
                if (context.Request.Path == "/")
                {
                    int zero = 0;
                    int x = 1 / zero;
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync(@"<!DOCTYPE html>
<html>
  <head>
  </head>
  <body>
    This is the home page
  </body>
</html>
");
                }
                else
                {
                    // later middlewares run
                    await next();
                    // request processing logic that runs AFTER any later middlewares
                    Console.WriteLine("this prints after the other delegate runs");
                }
            });

            app.Run(async context =>
            {
                // this object has all the details of the request
                HttpRequest request = context.Request;

                // we can modify this object to set up the response.
                HttpResponse response = context.Response;

                response.ContentType = "text/html";
                await response.WriteAsync(@"<!DOCTYPE html>
<html>
  <head>
  </head>
  <body>
    Hello world!
  </body>
</html>
");
            });
        }
    }
}
