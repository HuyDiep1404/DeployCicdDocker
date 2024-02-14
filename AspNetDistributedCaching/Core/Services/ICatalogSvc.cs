using AspNetDistributedCaching.Models;

namespace AspNetDistributedCaching.Core.Services
{
    public interface ICatalogSvc
    {
        Task<IList<Category>> GetCategories();
        Task<Category> GetCategory(string slug);
        Task<Product> GetProduct(string slug, string productName);
        Task<IList<Product>> GetProductsByCategory(string catSlug);
    }
}
