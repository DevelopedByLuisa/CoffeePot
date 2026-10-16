using System;

namespace CoffeePot.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
  public EntityNotFoundException() : base() { }

  public EntityNotFoundException(string entityName, int id)
  {
    
  }
}
