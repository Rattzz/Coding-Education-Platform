using efcoreApp.Areas.Identity.Data;
using efcoreApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("efcoreAppContextConnection") ?? throw new InvalidOperationException("Connection string 'efcoreAppContextConnection' not found.");

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

        // Veritabanini baslatmak icin kullanilan kodlar
        builder.Services.AddDbContext<efcoreAppContext>(options =>
        {
            var config = builder.Configuration;
            var connectionString = config.GetConnectionString("efcoreAppContextConnection");
            options.UseSqlite(connectionString);
        });

        builder.Services.AddDefaultIdentity<efcoreAppUser>(options =>
        options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<efcoreAppContext>();

        builder.Services.AddScoped<StudentService>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Main}/{action=Index}/{id?}");

        app.MapRazorPages();

        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<efcoreAppUser>>();

            string username = "admin";
            string email = "admin@admin.com";
            string password = "1234dD*";
            if(await userManager.FindByEmailAsync(email) ==null)
            {
                var user = new efcoreAppUser();
                user.UserName = username;
                user.Email = email;
                user.EmailConfirmed = true;

                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }


        app.Run();
    }
}