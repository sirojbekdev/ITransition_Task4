using BlazorApp.Areas.Identity;
using BlazorApp.Areas.Identity.Data;
using BlazorApp.Data;
using BlazorApp.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MainContextConnection") ?? throw new InvalidOperationException("Connection string 'MainContextConnection' not found.");

builder.Services.AddDbContext<MainContext>(options =>
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
    }).AddEntityFrameworkStores<MainContext>();

builder.Services.Configure<SecurityStampValidatorOptions>(op =>
{
    op.ValidationInterval = TimeSpan.Zero;
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAntDesign();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAuthorizationHandler, CorrectUserHandler>();
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("IsActive", policy => policy.Requirements.Add(new CorrectUserRequirement()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseAuthentication();
app.UseAuthorization();

app.Run();
