using Microsoft.EntityFrameworkCore;
using Proj.BLL.Services;
using Proj.BLL.Services.Contracts;
using Proj.BLL.Services.Proj.BLL.Services;
using Proj.DAL.DataContext;
using Proj.DAL.Repos.Contracts;
using Proj.PLL.Models;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MyApp.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddControllersWithViews();
builder.Services.AddEntityFrameworkNpgsql().AddEntityFrameworkNpgsql().AddDbContext<VshopContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("VideoGameShopCon")));
builder.Services.AddTransient(typeof(IGenericRepo<>),typeof(GenericRepo<>));

builder.Services.AddSingleton<IAccountCreationObservable, AccountCreationObservable>();
builder.Services.AddScoped<IAccountCreationObserver, LogAccountCreationObserver>();

builder.Services.AddScoped<IDeveloperService, DevService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdminService,AdminService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<ICartItemService,CartItemService>();
builder.Services.AddScoped<IShippingService, ShippingService>();


var app = builder.Build();
app.UseSession();

app.MapGet("/", async context =>
{
    // Retrieve the user ID from the session
    var userId = context.Session.GetInt32("UserId");

    if (userId.HasValue)
    {
        // User ID is available in the session
        await context.Response.WriteAsync($"Hello User {userId}");
    }
    else
    {
        // User ID is not available in the session
        await context.Response.WriteAsync("Hello World!");
    }
});



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
