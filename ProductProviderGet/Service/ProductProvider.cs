using ProductProviderGet.Models;
using System.Text.Json;

namespace ProductProviderGet.Services;

public class ProductProvider
    {
        private readonly HttpClient _httpClient;

        public ProductProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("My_Api");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<Product>>(json);
                return products ?? new List<Product>();
            }
            else
            {
                return new List<Product>(); 
            }
        }
    }

