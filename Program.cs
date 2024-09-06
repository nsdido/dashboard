using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Climate_Watch.Data;
using Climate_Watch.Models;
using Climate_Watch.Repository;
using CW_Website.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Climate_Watch;

public class Program {
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
        //                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        //
        //
        //builder.Services.AddDbContext<ApplicationDbContext>(options =>
        //    options.UseSqlite(connectionString));
        builder.Services.AddRazorPages();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            // optionsBuilder.UseSqlServer("Server=82.115.26.134;Initial Catalog=Feed;User ID=sa;" +
            //                             "Password=L(63luggHIkedq5>;TrustServerCertificate=True",
            //     _ => _.UseNetTopologySuite());

            // Server=185.164.72.119;Initial Catalog=Storage2;User ID=sa;" +
            // "Password=drTT4$POLmnZNcS;TrustServerCertificate=True"
            //options.UseSqlServer("Data Source =.; Initial Catalog = EShopDatabse; Integrated Security = true");
            //     options.UseSqlServer(
            //         "Server=185.164.72.119;Initial Catalog=Dashboard;User ID=sa;Password=drTT4$POLmnZNcS;TrustServerCertificate=True");
            // });

             options.UseSqlServer(
                 "Server=82.115.26.134;Initial Catalog=IdentityGoogleAccount;User ID=sa;Password=L(63luggHIkedq5>;TrustServerCertificate=True");            
        });
        
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

       // builder.Services.AddDefaultIdentity<IdentityUser>(options 
       //         => options.SignIn.RequireConfirmedAccount = true)
       //     .AddEntityFrameworkStores<ApplicationDbContext>();
       // 
        
        
        builder.Services.AddHttpContextAccessor();

      
        
        builder.Services.AddScoped<IHistoricalDataRepository, HistoricalDataRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IPlaceRepository, PlaceRepository>();
        builder.Services.AddScoped<IEntityRepository, EntityRepository>();
        builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
        builder.Services.AddScoped<ITemperatureRepository, TemperatureRepository>();
        builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
        builder.Services.AddScoped<IHumidityRepository, HumidityRepository>();
        
        
        
        builder.Services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
        builder.Services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
            option =>
            {
                option.LoginPath = "/Account/Login";
                option.LogoutPath = "/Account/Logout";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            });
        
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            // app.UseMigrationsEndPoint();
            
        }
        else
        {
            app.UseDeveloperExceptionPage();
          //  app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}