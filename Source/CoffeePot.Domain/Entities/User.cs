using CoffeePot.Domain.Common;

namespace CoffeePot.Domain.Entities;

public class User : BaseEntity
{
  public string Forename { get; set; }
  public string Surname { get; set; }
  public string Email { get; set; }
  public string Username { get; set; }
  public string Password { get; set; }
  public bool IsAdmin { get; set; }
}
