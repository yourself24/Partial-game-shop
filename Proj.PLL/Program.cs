using Microsoft.EntityFrameworkCore;
using Proj.BLL.Services;
using Proj.BLL.Services.Contracts;
using Proj.DAL.DataContext;
using Proj.DAL.Repos.Contracts;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEntityFrameworkNpgsql().AddEntityFrameworkNpgsql().AddDbContext<VshopContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("VideoGameShopCon")));
builder.Services.AddTransient(typeof(IGenericRepo<>),typeof(GenericRepo<>));

builder.Services.AddSingleton<IAccountCreationObservable, AccountCreationObservable>();
builder.Services.AddScoped<IAccountCreationObserver, LogAccountCreationObserver>();

builder.Services.AddScoped<IDeveloperService, DevService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdminService,AdminService>();
var app = builder.Build();




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
