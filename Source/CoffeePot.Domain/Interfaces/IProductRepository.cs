using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Interfaces.Common;

namespace CoffeePot.Domain.Interfaces;

public interface IProductRepository : IRepository
{
  Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken);
  Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken);
  Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken);
}
