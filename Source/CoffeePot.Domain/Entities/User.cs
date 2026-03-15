using CoffeePot.Domain.Entities.Common;

namespace CoffeePot.Domain.Entities;

public class User : BaseEntity
{
  public string Forename { get; set; }
  public string Surname { get; set; }
  public string Email { get; set; }

  public void UpdateUser(string forename, string surname, string email)
  {
    Forename = forename;
    Surname = surname;
    Email = email;
    RegisterChange();
  }
}
