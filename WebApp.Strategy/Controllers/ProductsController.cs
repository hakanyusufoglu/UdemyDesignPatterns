using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseProject.Models;
using WebApp.Strategy.Models;
using WebApp.Strategy.Repositories;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Strategy.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly UserManager<AppUser> _userManager;

        public ProductsController(IProductRepository productRepository, UserManager<AppUser> userManager)
        {
            _productRepository = productRepository;
            _userManager = userManager;
        }


        // GET: Products
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return View(await _productRepository.GetAllByUserIdAsync(user.Id));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Stock,UserId,CreatedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                product.UserId = user.Id;
                product.CreatedDate = DateTime.Now;
                await _productRepository.SaveAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Price,Stock,UserId,CreatedDate")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _productRepository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var product = await _productRepository.GetByIdAsync(id);
         
            await _productRepository.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(string id)
        {
          return await _productRepository.GetByIdAsync(id) != null;
        }
    }
}
