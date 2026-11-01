using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit3;
using CoffeePot.API.DTOs.Product;
using CoffeePot.API.Services;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Repositories;
using Moq;
using Xunit;

namespace Tests.UnitTests.CoffeePot.API.Services;

public class ProductServiceTest
{
  [Theory]
  [AutoData]
  public async Task CreateProductAsync_ShouldReturnProduct(WriteProductDto writeProductDto)
  {
    var productRepositoryMock = new Mock<IGenericRepository<Product>>();
    productRepositoryMock
      .Setup(repository => repository.CreateEntityAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync((Product product, CancellationToken _) => product);
    var productService = new ProductService(productRepositoryMock.Object);

    var result = await productService.CreateProductAsync(writeProductDto, CancellationToken.None);

    Assert.Equal(writeProductDto.Name, result.Name);
    Assert.Equal(writeProductDto.Description, result.Description);
    Assert.Equal(writeProductDto.UnitPrice, result.UnitPrice);

    productRepositoryMock.Verify(
      repository => repository.CreateEntityAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
  }

  [Theory]
  [InlineData(true)]
  [InlineData(false)]
  public async Task GetAllProductsAsync_ShouldReturnProducts(bool includeDeleted)
  {
    var testProduct1 = Product.Create("test1", "foo1", 4.44m);
    var testProduct2 = Product.Create("test2", "foo2", 1.11m);
    var products = new List<Product> { testProduct1, testProduct2 };

    var productRepositoryMock = new Mock<IGenericRepository<Product>>();
    productRepositoryMock
      .Setup(repository => repository.GetEntitiesAsync(includeDeleted, It.IsAny<CancellationToken>()))
      .ReturnsAsync(products);
    var productService = new ProductService(productRepositoryMock.Object);

    var result = (await productService.GetAllProductsAsync(includeDeleted, CancellationToken.None)).ToList();

    Assert.Equal(2, result.Count);
    Assert.Equal(testProduct1.Name, result[0].Name);
    Assert.Equal(testProduct2.Name, result[1].Name);

    productRepositoryMock.Verify(
      repository => repository.GetEntitiesAsync(includeDeleted, It.IsAny<CancellationToken>()),
      Times.Once());
  }

  [Fact]
  public async Task GetProductByIdAsync_ShouldReturnProduct()
  {
    var testProduct = Product.Create("test", "foo", 1.11m);

    var productRepositoryMock = new Mock<IGenericRepository<Product>>();
    productRepositoryMock
      .Setup(repository => repository.GetEntityByIdAsync(1, It.IsAny<CancellationToken>()))
      .ReturnsAsync(testProduct);
    var productService = new ProductService(productRepositoryMock.Object);

    var result = await productService.GetProductByIdAsync(1, CancellationToken.None);

    Assert.Equal(testProduct.Name, result.Name);
    Assert.Equal(testProduct.Description, result.Description);
    Assert.Equal(testProduct.UnitPrice, result.UnitPrice);

    productRepositoryMock.Verify(repository => repository.GetEntityByIdAsync(1, It.IsAny<CancellationToken>()),
      Times.Once);
  }
}
