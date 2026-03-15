using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Exceptions;
using CoffeePot.Domain.Interfaces;
using CoffeePot.Domain.Interfaces.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoffeePot.Infrastructure.Repositories;

public class UserRepository(ApplicationContext applicationContext, ILogger<UserRepository> logger)
  : IGenericRepository<User>
{
  public async Task<IEnumerable<User>> GetAsync(CancellationToken cancellationToken)
  {
    return await applicationContext.Users.ToListAsync(cancellationToken);
  }

  public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
  {
    var loadedUser = await applicationContext.Users.Where(x => x.Id == id)
      .FirstOrDefaultAsync(cancellationToken);

    return loadedUser ?? throw new EntityNotFoundException($"No user with the ID {id} could be found.");
  }

  public async Task<User> CreateAsync(User entity, CancellationToken cancellationToken)
  {
    try
    {
      applicationContext.Users.Add(entity);
      await applicationContext.SaveChangesAsync(cancellationToken);
      return entity;
    }
    catch (Exception e)
    {
      logger.LogError(e, e.Message);
      throw;
    }
  }

  public async Task<User> UpdateAsync(User entity, CancellationToken cancellationToken)
  {
    await applicationContext.SaveChangesAsync(cancellationToken);
    return entity;
  }
}
