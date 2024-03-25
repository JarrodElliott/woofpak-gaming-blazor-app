using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoofpakGamingSiteServerApp.Areas.Identity;
using WoofpakGamingSiteServerApp.Data;
using Microsoft.AspNetCore.ResponseCompression;
using WoofpakGamingSiteServerApp.Hubs;
using WoofpakGamingSiteServerApp.Data.Services;
using WoofpakGamingSiteServerApp.Hosted_Services;

namespace WoofpakGamingSiteServerApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer("Data Source=192.24.186.221;Initial Catalog=WoofpakGaming;User ID=;Password="

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddUserManager<ApplicationUserManager>();

            //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //.AddEntityFrameworkStores<ApplicationDbContext>()

            services.AddRazorPages();
            services.AddAntDesign();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddHttpContextAccessor();


            //Add Services
            services.AddSingleton<TournamentService>();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            bool runServicesOnStartup = Configuration.GetValue<bool>(
               "ServiceSettings:RunServicesOnStartup");

            if (runServicesOnStartup)
            {
                services.AddHostedService<RaffleTicketMonitorService>();
                services.AddHostedService<ExtraLifeDonationMonitorService>();
            }
            //services.AddLogging()


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
                app.UseHttpsRedirection();


            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapFallbackToPage("/_Host");
            });

           
                CreateRoles(app.ApplicationServices).Wait();
            
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            using (var scope = serviceProvider.CreateScope())
            {
                var UserManager = (ApplicationUserManager)scope.ServiceProvider.GetService(typeof(ApplicationUserManager));
                var RoleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));

                //var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                //var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string[] roleNames = { "Administrator", "Moderator", "Member" };
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await RoleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                var adminEmail = Configuration.GetValue<string>(
               "AppIdentitySettings:AdminUserEmail");

                var _user = await UserManager.FindByEmailAsync(adminEmail);

                if (_user == null)
                {
                }
                else
                {
                    await UserManager.AddToRoleAsync(_user, "Administrator");
                }
            }
        }
    }
}
