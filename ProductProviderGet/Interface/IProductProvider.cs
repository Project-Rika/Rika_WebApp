using ProductProviderGet.Models;

namespace ProductProviderGet.Interfaces;

public interface IProductProvider
{
    Task<List<Product>> GetProductsAsync();
}
