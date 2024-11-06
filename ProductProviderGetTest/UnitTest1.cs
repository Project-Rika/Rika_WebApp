using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductProviderGet.Functions;
using ProductProviderGet.Interfaces;
using ProductProviderGet.Models;
using Microsoft.AspNetCore.Http;

namespace ProductProviderGetTest;

public class UnitTest1
{
	private readonly Mock<ILogger<GetProductsFunction>> _mockLogger;
	private readonly Mock<IProductProvider> _mockProductProvider;

	public UnitTest1()
	{
		_mockLogger = new Mock<ILogger<GetProductsFunction>>();
		_mockProductProvider = new Mock<IProductProvider>();
	}

	[Fact]
	public async Task GetProducts_ReturnsOkResult_WithProducts()
	{
		// Arrange
		var products = new List<Product> { new Product { Id = 1, Name = "Product 1", Price = 100 } };
		_mockProductProvider.Setup(x => x.GetProductsAsync()).ReturnsAsync(products);

		var function = new GetProductsFunction(_mockLogger.Object, _mockProductProvider.Object);

		// Act
		var result = await function.Run(new DefaultHttpContext().Request) as OkObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(200, result.StatusCode);
		Assert.IsType<List<Product>>(result.Value);
	}

	[Fact]
	public async Task GetProducts_ReturnsNotFoundResult_WhenNoProducts()
	{
		// Arrange
		var products = new List<Product>(); 
		_mockProductProvider.Setup(x => x.GetProductsAsync()).ReturnsAsync(products);

		var function = new GetProductsFunction(_mockLogger.Object, _mockProductProvider.Object);

		// Act
		var result = await function.Run(new DefaultHttpContext().Request) as NotFoundObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(404, result.StatusCode);
		Assert.Equal("No products available.", result.Value);
	}
}
