using System.Globalization;
using CourseWork.BusinessLogicLayer.ElasticSearch;
using CourseWork.BusinessLogicLayer.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using CourseWork.Extensions.StartupExtensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Nest;

namespace CourseWork
{
    public class Startup 
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            //services.AddHangfire(x => x.UseSqlServerStorage(connectionString));

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.Configure<MailOptions>(options => Configuration.GetSection("MailOptions").Bind(options));
            services.Configure<CloudinaryOptions>(options => 
                Configuration.GetSection("CloudinaryOptions").Bind(options));
            services.Configure<ElasticSearchOptions>(options =>
                Configuration.GetSection("ElasticSearchOptions").Bind(options));
            services.Configure<HomePageOptions>(options => Configuration.GetSection("HomePageOptions").Bind(options));
            services.Configure<ColorOptions>(options => Configuration.GetSection("ColorOptions").Bind(options));
            services.Configure<AwardOptions>(options => Configuration.GetSection("AwardOptions").Bind(options));
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("ru"),
                    new CultureInfo("en")
                };
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            services.AddSingleton<SearchClient>();

            services.AddRepositories();
            services.AddServices();
            services.AddMappers();
            services.CreateDatabaseRoles().Wait();
            services.RunScheduler();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app.UseStaticFiles();
            app.UseIdentity();
            //app.UseHangfireServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "spa-fallback",
                    template: "{*url}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
