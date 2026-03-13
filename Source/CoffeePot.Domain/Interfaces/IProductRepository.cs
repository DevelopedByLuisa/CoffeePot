using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities;

namespace CoffeePot.Domain.Interfaces;

public interface IProductRepository
{
  Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken);
  Task<Product> GetProductAsync(int id, CancellationToken cancellationToken);
  Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken);
  Task UpdateProductAsync(CancellationToken cancellationToken);
}
