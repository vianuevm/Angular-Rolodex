using Rolodex3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Rolodex3.Services
{
    public class ContactsDataService : IContactsDataService
    {
        private ContactsDBEntities entities = new ContactsDBEntities();

        public IQueryable<Person> People { get { return entities.People; } }
        public IQueryable<PhoneNumber> PhoneNumbers { get { return entities.PhoneNumbers; } }
        public IQueryable<PhoneNumberType> PhoneNumberTypes { get { return entities.PhoneNumberTypes; } }

        public void AddPerson(Person personToAdd)
        {
            entities.People.Add(personToAdd);

        }

        public void RemovePerson(Person person)
        {
            entities.People.Remove(person);
        }

        public void RemovePhoneNumbers(IEnumerable<PhoneNumber> phonesToRemove)
        {
            entities.PhoneNumbers.RemoveRange(phonesToRemove);

        }

        public void SaveChanges()
        {
            entities.SaveChanges();
        }
    }
}