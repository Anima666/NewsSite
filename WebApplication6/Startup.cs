﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsSite.Domain.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NewsSite.Domain.Abstract;
using NewsSite.WebUi.Models;
using Microsoft.AspNetCore.Identity;
using Westwind.AspNetCore.Markdown;
using Markdig;
using Microsoft.AspNetCore.Authentication.OAuth;
using Markdig.Extensions.AutoIdentifiers;
using NewsSite.Domain.Entities;

namespace WebApplication6
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EFDbContext>(options => options.UseSqlServer(connection));
            services.AddTransient<IPostRepository, EFPostRepository>();

            services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("TestAuth")));

            services.AddIdentity<User, IdentityRole>()
              .AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders(); ;

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthentication()
            .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = "185759802078663";
                    facebookOptions.AppSecret = "d0c6f350bd4bd0b6cbab8855f073bcd5";
                })
             .AddTwitter(options =>
              {
                  options.ConsumerKey = "lRUbVkZ4gFBXUJ8n0q7Qj0eFV";
                  options.ConsumerSecret = "TgUvk9OyhhaWyJIGazRW1N6EDdDXl273MyuSkaCi5JhUQ5UPCP";
              })
              .AddVkontakte(options =>
              {
                  options.ClientId = "6475339";
                  options.ClientSecret = "vdu1OTQMRavA3Z8Fbdfk";
                 
              }); 

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();
            app.UseStaticFiles();

            //app.UseMarkdown();

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Home}/{action=Index}/{id?}");

                //   routes.MapRoute(null,
                //    "",
                //    new
                //    {
                //        controller = "Post",
                //        action = "List",
                //        tag = (string)null,
                //        page = 1
                //    }
                //);

                //   routes.MapRoute(
                //       name: null,
                //       template: "Page{page}",
                //       defaults: new { controller = "Post", action = "List", tag = (string)null },
                //       constraints: new { page = @"\d+" }
                //   );

                //   routes.MapRoute(null,
                //       "{tag}",
                //       new { controller = "Post", action = "List", page = 1 }
                //   );
                routes.MapRoute(
             name: null,
             template: "{controller=Post}/{action=ShowPost}/{id?}"
             );

                routes.MapRoute(
             name: null,
             template: "{tag}/Page{page}",
             defaults: new { controller = "Post", action = "List", tag = (string)null });

                routes.MapRoute(
              name: null,
              template: "Page{page}",
              defaults: new { controller = "Post", action = "List" });

                routes.MapRoute(
                name: "Default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "Post", action = "List" });
            });
        }
    }
}
