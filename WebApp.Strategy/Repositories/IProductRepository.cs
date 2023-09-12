using WebApp.Strategy.Models;

namespace WebApp.Strategy.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(string id);
        Task<List<Product>> GetAllByUserIdAsync(string userId);
        Task<Product> SaveAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
