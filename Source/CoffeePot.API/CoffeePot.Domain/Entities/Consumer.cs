using CoffeePot.Domain.Entities.Common;

namespace CoffeePot.Domain.Entities;

public class Consumer : BaseEntity
{
  public string Forename { get; set; }
  public string Surname { get; set; }

  public void Update(string forename, string surname)
  {
    Forename = forename;
    Surname = surname;
    RegisterChange();
  }
}
