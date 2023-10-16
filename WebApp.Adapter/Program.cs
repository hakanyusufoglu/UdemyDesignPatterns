using WebApp.Adapter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Adapter.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAdvanceImageProcess,AdvanceImageProcess>();
builder.Services.AddScoped<IImageProcess, AdvanceImageProcessAdapter>();

//MsSql i�in
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

//Identity i�in
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<AppIdentityDbContext>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //ServiceProvider ile birlikte yukar�da tan�mlad���m�z servislere ula�abilmemizi sa�l�yor (GetService servisi al�r ve null olabilir, Ancak GetRequiredService ise bu servisi mutlaka almam laz�m diyorum)
    var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    //Migration yap�lma i�lemi - Uygulama aya�a kalkt���nda migrationi update-database gibi uygular ve db yoksa otomatik olarak dbyi olu�turur.
    identityDbContext.Database.Migrate();

    if (!userManager.Users.Any())
    {
        userManager.CreateAsync(new AppUser { UserName = "user1", Email = "user1@outlook.com" }, "Password12*").Wait();
        userManager.CreateAsync(new AppUser { UserName = "user2", Email = "user2@outlook.com" }, "Password12*").Wait(); 
        userManager.CreateAsync(new AppUser { UserName = "user3", Email = "user3@outlook.com" }, "Password12*").Wait(); 
        userManager.CreateAsync(new AppUser { UserName = "user4", Email = "user4@outlook.com" }, "Password12*").Wait(); 
        userManager.CreateAsync(new AppUser { UserName = "user5", Email = "user5@outlook.com" }, "Password12*").Wait(); 
    }
}
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
