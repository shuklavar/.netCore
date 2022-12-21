using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using MVCDHProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(configure => { var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); configure.Filters.Add(new AuthorizeFilter(policy)); });
//both are same thimngs but different manner
//builder.Services.AddScoped(typeof(ICustomerDAL), typeof(CustomerSqlDAL));
builder.Services.AddScoped<ICustomerDAL,CustomerSqlDAL>();


builder.Services.AddDbContext<MVCCoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));
//Registering Identity FrameWorkServices
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => { options.Password.RequiredLength = 8;options.Password.RequireDigit = false;}).AddEntityFrameworkStores<MVCCoreDbContext>().AddDefaultTokenProviders();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseStatusCodePages();
   // app.UseStatusCodePagesWithRedirects("/ClientError/{0}");
    app.UseStatusCodePagesWithReExecute("/ClientError/{0}");


    app.UseExceptionHandler("/Home/Error");
    //app.UseExceptionHandler("/ServerError");
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


