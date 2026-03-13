using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Exceptions;
using CoffeePot.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoffeePot.Infrastructure.Repositories;

public class UserRepository(ApplicationContext applicationContext, ILogger<UserRepository> logger)
  : IUserRepository
{
  public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken)
  {
    return await applicationContext.Users.ToListAsync(cancellationToken);
  }

  public async Task<User> GetUserAsync(int id, CancellationToken cancellationToken)
  {
    var loadedUser = await applicationContext.Users.Where(x => x.Id == id)
      .FirstOrDefaultAsync(cancellationToken);

    return loadedUser ?? throw new NotFoundException();
  }

  public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken)
  {
    try
    {
      applicationContext.Users.Add(user);
      await applicationContext.SaveChangesAsync(cancellationToken);
      return user;
    }
    catch (Exception e)
    {
      logger.LogError(e, e.Message);
      throw;
    }
  }

  public async Task UpdateUserAsync(CancellationToken cancellationToken)
  {
    await applicationContext.SaveChangesAsync(cancellationToken);
  }
}
