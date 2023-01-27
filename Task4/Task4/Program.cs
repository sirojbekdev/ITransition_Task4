using Microsoft.EntityFrameworkCore;
using Task4.Areas.Identity.Data;
using Task4.Services;

namespace Task4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("TaskContextConnection") ?? throw new InvalidOperationException("Connection string 'TaskContextConnection' not found.");

            builder.Services.AddDbContext<TaskContext>(options =>
                    options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<AppUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;

            }).AddEntityFrameworkStores<TaskContext>();


            // Add services to the container.
            builder.Services.AddServerSideBlazor();
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IUserService, UserService>();

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
            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(e =>
            {
                e.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}",
                e.MapBlazorHub()
                );
            });
            app.Run();
        }
    }
}