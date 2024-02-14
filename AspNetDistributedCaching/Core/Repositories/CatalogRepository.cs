using AspNetDistributedCaching.Core.Options;
using AspNetDistributedCaching.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AspNetDistributedCaching.Core.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        readonly IMongoDatabase _db;
        readonly MongoClient _client;
        readonly string catCol = "categories";
        readonly string prodCol = "products";

        public CatalogRepository(MongoOptions cfg)
        {
            _client = new MongoClient(cfg.ConnectionString);
            _db = _client.GetDatabase(cfg.Db);
        }

        public async Task<IList<Category>> GetCategories()
        {
            var c = _db.GetCollection<Category>(catCol);
            return (await c.FindAsync(new BsonDocument())).ToList();
        }

        public async Task<Category> GetCategory(string slug)
        {
            var c = _db.GetCollection<Category>(catCol);
            var filter = Builders<Category>.Filter.Eq("Slug", slug);
            return (await c.FindAsync<Category>(filter)).SingleOrDefault();
        }

        public async Task<Product> GetProduct(string slug, string productName)
        {
            var c = _db.GetCollection<Product>(prodCol);
            var filter = Builders<Product>.Filter.Eq("Slug", slug) & Builders<Product>.Filter.Eq("Name", productName); ;
            return (await c.FindAsync<Product>(filter)).SingleOrDefault();
        }

        public async Task<IList<Product>> GetProductsByCategory(string catSlug)
        {
            var c = _db.GetCollection<Product>(prodCol);
            var filter = Builders<Product>.Filter.Eq("CategoryId", catSlug);
            return (await c.FindAsync<Product>(filter)).ToList();
        }

        Task<IList<Category>> ICatalogRepository.GetCategories()
        {
            return GetCategories();
        }

        Task<Category> ICatalogRepository.GetCategory(string slug)
        {
            throw new NotImplementedException();
        }

        Task<Product> ICatalogRepository.GetProduct(string slug, string productName)
        {

            return GetProduct(slug, productName);
        }

        Task<IList<Product>> ICatalogRepository.GetProductsByCategory(string slug)
        {
            return GetProductsByCategory(slug);
        }
    }
}
