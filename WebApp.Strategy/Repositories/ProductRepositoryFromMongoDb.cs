using MongoDB.Driver;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Repositories
{
    public class ProductRepositoryFromMongoDb : IProductRepository
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductRepositoryFromMongoDb(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");

            //MongoDb'ye bağlantı için
            var client = new MongoClient(connectionString);

            //Db yoksa baştan oluşşturacaktır
            var database = client.GetDatabase("ProductDb");

            //Tablo ismi Products olsun
            _productCollection = database.GetCollection<Product>("Products");

        }

        public async Task DeleteAsync(Product product)
        {
            await _productCollection.DeleteOneAsync(x=>x.Id == product.Id);
        }

        public async Task<List<Product>> GetAllByUserIdAsync(string userId)
        {
            return await _productCollection.Find(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> SaveAsync(Product product)
        {
            await _productCollection.InsertOneAsync(product);

            //Bu producta yeni eklenen product'ın Id'side var
            return product; 
        }

        public async Task UpdateAsync(Product product)
        {
            await _productCollection.FindOneAndReplaceAsync(x => x.Id == product.Id, product);
        }
    }
}
