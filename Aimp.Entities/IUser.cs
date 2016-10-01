using System;

namespace Aimp.Entities
{
     public interface IUser : IEntity
    {
         string FirstName { get; set; }
         string LastName { get; set; }
         string MiddleName { get; set; }
         string FirstNameGenitive { get; set; }
         string LastNameGenitive { get; set; }
         string MiddleNameGenitive { get; set; }
         string Number { get; set; }
         DateTime Date { get; set; }
         string Login { get; set; }
         string Password { get; set; }
    }
}
