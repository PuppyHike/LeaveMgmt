using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using LeaveMgmt.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LeaveMgmt.Repository;
using LeaveMgmt.Contracts;
using LeaveMgmt.Mappings;

namespace LeaveMgmt
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }
        public IWebHostEnvironment Env { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            //Add References to Repository and Contracts to Startup file
            services.AddScoped<iLeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<iLeaveAllocationRepository, LeaveAllocationRepository>();
            services.AddScoped<iLeaveRequestRepository, LeaveRequestRepository>();

            //Add Reference to AutoMapper
            services.AddAutoMapper(typeof(Maps));

            //////Seed the database with Roles
            services.AddDefaultIdentity<Person>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();

          //  IMvcBuilder builder = services.AddRazorPages();

#if DEBUG
            //if (Env.IsDevelopment())
            //{
            //    builder.AddRazorRuntimeCompilation();
            //}
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Add UserManager and RoleManager parameters
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                                UserManager<Person> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
         
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //Call the Static Class SeedData to seed the Administrator and Member Roles
            SeedData.Seed(userManager, roleManager);


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
