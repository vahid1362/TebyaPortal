using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Rest.Serialization;
using Portal.core.Infrastructure;
using Portal.core.Membership;
using Portal.Infrastructure;
using Portal.Service.Media;
using Portal.Service.News;

namespace Portal.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            Mapper = mapper;
        }

        public IConfiguration Configuration { get; }
        public IMapper Mapper { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<PortalDbContext>(c =>
            {
                c.UseSqlServer(Configuration.GetConnectionString("MarketingContext"));
            });
            services.AddDbContext<PortaIIdentityContext>(c =>
            {
                c.UseSqlServer(Configuration.GetConnectionString("IdentityContext"));
            });


          
          
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new ReadOnlyJsonContractResolver()).AddNToastNotifyToastr();
            services.AddAutoMapper();
            services.AddKendo();
            services.AddTransient(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IPictureService, PictureService>();
            services.AddTransient<IHostingEnvironment, HostingEnvironment>();
            services.AddIdentity<AppUser, IdentityRole>();

        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
