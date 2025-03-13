using MongoDB.Driver;
using SalesTest.Entities;


public class ProductService
{
    private readonly IMongoCollection<Product> _products;

    public ProductService(MongoDbService mongoDbService)
    {
        _products = mongoDbService.GetCollection<Product>("Products");
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _products.Find(_ => true).ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;
        await _products.InsertOneAsync(product);
        return product;
    }

    public async Task<Product> UpdateProductAsync(string id, Product updatedProduct)
    {
        updatedProduct.UpdatedAt = DateTime.UtcNow;
        var result = await _products.ReplaceOneAsync(p => p.Id == id, updatedProduct);
        return result.IsAcknowledged ? updatedProduct : null;
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        var result = await _products.DeleteOneAsync(p => p.Id == id);
        return result.DeletedCount > 0;
    }
}
