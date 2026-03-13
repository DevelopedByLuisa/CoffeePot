using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities;

namespace CoffeePot.Domain.Interfaces;

public interface IUserRepository
{
  Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken);
  Task<User> GetUserAsync(int id, CancellationToken cancellationToken);
  Task<User> CreateUserAsync(User user, CancellationToken cancellationToken);
  Task UpdateUserAsync(CancellationToken cancellationToken);
}
