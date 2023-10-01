using BaseProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Composite.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
        //newUser'�n Id'si eklenir.
        var newUser = new AppUser { UserName = "user1", Email = "user1@outlook.com" };
        userManager.CreateAsync(newUser, "Password12*").Wait();
        userManager.CreateAsync(new AppUser { UserName = "user2", Email = "user2@outlook.com" }, "Password12*").Wait();
        userManager.CreateAsync(new AppUser { UserName = "user3", Email = "user3@outlook.com" }, "Password12*").Wait();
        userManager.CreateAsync(new AppUser { UserName = "user4", Email = "user4@outlook.com" }, "Password12*").Wait();
        userManager.CreateAsync(new AppUser { UserName = "user5", Email = "user5@outlook.com" }, "Password12*").Wait();

        //Uygulama aya�a kalkarken top (en �st) kategorileri ekliyoruz.
        var newCategory1 = new Category { Name = "Su� romannlar�", ReferenceId = 0, UserId = newUser.Id };
        var newCategory2 = new Category { Name = "Cinayet romannlar�", ReferenceId = 0, UserId = newUser.Id };
        var newCategory3 = new Category { Name = "Polisiye romannlar�", ReferenceId = 0, UserId = newUser.Id };

        identityDbContext.Categories.AddRange(newCategory1, newCategory2, newCategory3);

        identityDbContext.SaveChanges();

        //Alt kategori ekliyoruz.

        var subCategory1 = new Category { Name = "Su� romanlar� 1", ReferenceId = newCategory1.Id, UserId = newUser.Id };
        var subCategory2 = new Category { Name = "Cinayet romanlar� 1", ReferenceId = newCategory2.Id, UserId = newUser.Id };
        var subCategory3 = new Category { Name = "Polisiye romanlar� 1", ReferenceId = newCategory3.Id, UserId = newUser.Id };

        identityDbContext.Categories.AddRange(subCategory1, subCategory2, subCategory3);

        identityDbContext.SaveChanges();

        //Bir alt kategori daha
        var subCategory4 = new Category { Name = "Cinayer romanlar� 1.1", ReferenceId = subCategory2.Id, UserId = newUser.Id };

        identityDbContext.Categories.Add(subCategory4);
        identityDbContext.SaveChanges();
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
