using AuctionApplication.Areas.Identity.Data;
using AuctionApplication.Core;
using AuctionApplication.Core.Interfaces;
using AuctionApplication.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AuctionApplication.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<IAuctionPersistence, AuctionSqlPersistence>();
//builder.Services.AddScoped(typeof(GenericRepository<>)); //???

// db, with dependency injection
builder.Services.AddDbContext<AuctionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuctionDbConnection")));

// identity config // AuctionApplicationIdentityContext
builder.Services.AddDbContext<AuctionApplicationIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuctionApplicationIdentityContextConnection")));
builder.Services.AddDefaultIdentity<AuctionApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AuctionApplicationIdentityContext>();

// add automapper scanning (requires AutoMapper package)
builder.Services.AddAutoMapper(typeof(Program));

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
