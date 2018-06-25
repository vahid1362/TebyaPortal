using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Rest.Serialization;
using Portal.core.Infrastructure;
using Portal.Infrastructure;
using Portal.Service;
using Portal.Service.Media;
using Portal.Service.News;
using QTasMarketing.Web.Areas.Admin.Extentions;

namespace QTasMarketing.Web
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
            services.AddDbContext<PortalDbContext>(c =>
            {
                c.UseSqlServer(Configuration.GetConnectionString("MarketingContext"));
            });
            services.AddDbContext<PortaIIdentityContext>(c =>
            {
                c.UseSqlServer(Configuration.GetConnectionString("IdentityContext"));
            });

            var mapper = CreateMapperConfiguration();
            services.AddSingleton(mapper);
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new ReadOnlyJsonContractResolver()).AddNToastNotifyToastr();
            services.AddKendo();
            services.AddTransient(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IPictureService, PictureService>();



        }

        private static IMapper CreateMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));

            var mapper = config.CreateMapper();
            return mapper;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetService<MarketingIdentityContext>();
            //    DataSeeder.SeedCountries(context).Wait();
            //}

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseNToastNotify();
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
