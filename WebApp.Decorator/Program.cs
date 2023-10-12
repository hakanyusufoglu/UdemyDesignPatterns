using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebApp.Decorator.Models;
using WebApp.Decorator.Repositories;
using WebApp.Decorator.Repositories.Decorator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();

//DecoratorCache'i ve DecoratorLoggingi aktifleştiriyoruz. (Compile Time) Bu yapının daha kısa gerçekleştiren kütüphane bulunmaktadır. (Scrutor Library)

#region Decorator patternin kütüphanesiz gerçekleştirilmesi (1. yol)
//builder.Services.AddScoped<IProductRepository>(sp =>
//{
//    var context = sp.GetRequiredService<AppIdentityDbContext>();
//    var memoryCache = sp.GetRequiredService<IMemoryCache>();
//    var productRepository = new ProductRepository(context);
//    var logService = sp.GetRequiredService<ILogger<ProductRepositoryLoggingDecorator>>();

//    var cacheDecorator = new ProductRepositoryCacheDecorator(productRepository, memoryCache);

//    var logDecorator = new ProductRepositoryLoggingDecorator(cacheDecorator, logService);


//    return logDecorator;
//});
#endregion

#region Scrutor Kütüphanesi ile DecoratorPattern gerçekleştirilmesi (2. yol)

builder.Services.AddScoped<IProductRepository, ProductRepository>()
    .Decorate<IProductRepository, ProductRepositoryCacheDecorator>()
    .Decorate<IProductRepository, ProductRepositoryLoggingDecorator>(); ;


#endregion
//MsSql için
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

//Identity için
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<AppIdentityDbContext>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //ServiceProvider ile birlikte yukarıda tanımladığımız servislere ulaşabilmemizi sağlıyor (GetService servisi alır ve null olabilir, Ancak GetRequiredService ise bu servisi mutlaka almam lazım diyorum)
    var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    //Migration yapılma işlemi - Uygulama ayağa kalktığında migrationi update-database gibi uygular ve db yoksa otomatik olarak dbyi oluşturur.
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
