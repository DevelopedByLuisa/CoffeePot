using System.Threading;
using System.Threading.Tasks;

namespace CoffeePot.Domain.Interfaces.Common;

public interface IRepository
{
  Task SaveChangesAsync(CancellationToken cancellationToken);
}
