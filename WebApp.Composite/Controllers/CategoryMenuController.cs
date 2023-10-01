using BaseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.Composite.Composite;
using WebApp.Composite.Models;

namespace WebApp.Composite.Controllers
{
    [Authorize]
    public class CategoryMenuController : Controller
    {
        private readonly AppIdentityDbContext _context;
        public CategoryMenuController(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //category => bookcomposite karşılık gelir
            //book => bookcomponent'e karşılık gelir

            //bu yapı sonsuz bir yapı olduğu için recursive metot kullanılır.

            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var categories = await _context.Categories.Include(x => x.Books).Where(x => x.UserId == userId).OrderBy(x => x.Id).ToListAsync();

            //en root menu geliyor.
            var menu = GetMenus(categories, new Category { Name = "TopCategory", Id = 0 }, new BookComposite(0, "TopMenu"));

            ViewBag.menu = menu;

            return View();
        }

        public BookComposite GetMenus(List<Category> categories, Category topCategory, BookComposite topBookComposite, BookComposite last = null)
        {
            categories.Where(x => x.ReferenceId == topCategory.Id).ToList().ForEach(categoryItem =>
            {
                var bookComposite = new BookComposite(categoryItem.Id, categoryItem.Name);

                //categoryitemin içinde kitaplar olabilir
                categoryItem.Books.ToList().ForEach(bookItem =>
                {
                    bookComposite.Add(new BookComponent(bookItem.Id, bookItem.Name));
                });
                if (last != null)
                {
                    //Ağaca yeni bir dal ekliyoruz.
                    last.Add(bookComposite);
                }
                else
                {
                    topBookComposite.Add(bookComposite);
                }

                GetMenus(categories, categoryItem, topBookComposite, bookComposite);
            });
            return topBookComposite;
        }
    }
}
