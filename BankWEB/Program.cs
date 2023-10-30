using BankWEB.Data;
using BankWEB.DataContext;
using BankWEB.Helpers;
using BankWEB.Infrastructure;
using BankWEB.Interfaces;
using BankWEB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options =>
{
    options.ModelBinderProviders.Insert(0, new GlobalTransferModelBinderProvider());
    options.ModelBinderProviders.Insert(1, new DecimalModelBinderProvider());
});

builder.Logging.AddDebug();
builder.Logging.AddConsole();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient("BankAPI", c =>
{
    c.BaseAddress = new Uri("https://localhost:44314/api/values/");
});
builder.Services.AddDbContext<AuthBankContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AuthBankContext>().AddDefaultTokenProviders();
builder.Services.AddTransient<IBankData, BankDataAPI>();
//builder.Services.AddTransient<ITagHelperComponent, DropdownTagHelper>();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 5;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    options.Lockout.AllowedForNewUsers = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
    options.SlidingExpiration = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseDefaultFiles();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
