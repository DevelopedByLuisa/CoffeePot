using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Interfaces.Common;

namespace CoffeePot.Domain.Interfaces;

public interface IUserRepository : IRepository
{
  Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken);
  Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken);
  Task<User> CreateUserAsync(User user, CancellationToken cancellationToken);
}
