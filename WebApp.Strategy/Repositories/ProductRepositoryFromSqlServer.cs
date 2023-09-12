using BaseProject.Models;
using Microsoft.EntityFrameworkCore;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Repositories
{
    //
    public class ProductRepositoryFromSqlServer : IProductRepository
    {
        private readonly AppIdentityDbContext _context;
        public ProductRepositoryFromSqlServer(AppIdentityDbContext appIdentityDbContext)
        {
            _context = appIdentityDbContext;
        }
        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);// = _context.Entry(product).State=EntityState.Deleted; 
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllByUserIdAsync(string userId)
        {
            return await _context.Products.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> SaveAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
