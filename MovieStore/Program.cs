using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieStore.Models.Domain;
using MovieStore.Repositories.Abstract;
using MovieStore.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserAuthenticationServices, UserAuthenticationServices>();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("conn")
    )) ;
builder.Services.AddIdentity<ApplicationUsers, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IGenreService, GenreService>();
//builder.Services.ConfigureExternalCookie(options=>options.LoginPath="/UserAuthentication/Login");
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
