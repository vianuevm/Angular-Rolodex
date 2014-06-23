using System.Data.Entity;
using Rolodex3.Models;
using System.Linq;
using System.Collections.Generic;

namespace Rolodex3.Services
{
    public interface IContactsDataService
    {
        IQueryable<Person> People { get; }
        IQueryable<PhoneNumber> PhoneNumbers { get; }
        IQueryable<PhoneNumberType> PhoneNumberTypes { get; }
        void AddPerson(Person personToAdd);

        void SaveChanges();
        void RemovePhoneNumbers(IEnumerable<PhoneNumber> phonesToRemove);
        void RemovePerson(Person person);
    }
}