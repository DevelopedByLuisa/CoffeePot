using CoffeePot.Domain.Entities.Common;

namespace CoffeePot.Domain.Entities;

public class User : BaseEntity
{
  public string Forename { get; set; }
  public string Surname { get; set; }
  public string Email { get; set; }
}
