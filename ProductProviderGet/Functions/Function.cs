using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using ProductProviderGet.Interfaces;

namespace ProductProviderGet.Functions;

public class GetProductsFunction
{
    private readonly ILogger<GetProductsFunction> _logger;
    private readonly IProductProvider _productProvider;

    
    public GetProductsFunction(ILogger<GetProductsFunction> logger, IProductProvider productProvider)
    {
        _logger = logger;
        _productProvider = productProvider;
    }

    [Function("GetProducts")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
    {
        _logger.LogInformation("Fetching products from external API via ProductProvider...");

        var products = await _productProvider.GetProductsAsync();

        if (products == null || products.Count == 0)
        {
            _logger.LogWarning("No products found.");
            return new NotFoundObjectResult("No products available.");
        }

        _logger.LogInformation("Products successfully fetched from external API.");
        return new OkObjectResult(products);
    }
}
