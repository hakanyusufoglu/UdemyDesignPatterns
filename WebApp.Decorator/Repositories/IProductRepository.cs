﻿using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repositories
{
    //Decorator Design Pattern diagramında IComponent'e karşılık gelmektedir.
    public interface IProductRepository
    {
        Task<Product> GetById(int id);
        Task<List<Product>> GetAll();
        Task<List<Product>> GetAll(string userId);
        Task<Product> Save(Product product);
        Task Update(Product product);
        Task Remove(Product product);
    }
}