using Rolodex3.Models;
using Rolodex3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rolodex3.APIControllers
{
    public class ContactsController : ApiController
    {
        
        private ContactsDBEntities entities = new ContactsDBEntities();
        // GET api/<controller>

        private IContactsDataService service;

        public ContactsController() : this(new ContactsDataService()) { }

        public ContactsController(IContactsDataService service)
        {
            this.service = service;
        }

        public IEnumerable<Contact> Get()
        {
            var query = CreateContactsQuery();
            return query.AsEnumerable();

        }

        private IQueryable<Contact> CreateContactsQuery()
        {
            var query = from person in service.People
                        let homePhoneType = service.PhoneNumberTypes.FirstOrDefault(phoneType => phoneType.type == "home").Id
                        let workPhoneType = service.PhoneNumberTypes.FirstOrDefault(phoneType => phoneType.type == "work").Id
                        let cellPhoneType = service.PhoneNumberTypes.FirstOrDefault(phoneType => phoneType.type == "cell").Id
                        let homePhone = person
                            .PhoneNumbers
                            .FirstOrDefault(phone => phone.PhoneNumberType.Id == homePhoneType)
                        let workPhone = person
                            .PhoneNumbers
                            .FirstOrDefault(phone => phone.PhoneNumberType.Id == workPhoneType)
                        let cellPhone = person
                            .PhoneNumbers
                            .FirstOrDefault(phone => phone.PhoneNumberType.Id == cellPhoneType)
                        select new Contact
                        {
                            id = person.Id,
                            firstName = person.firstName,
                            lastName = person.lastName,
                            address = person.Address,
                            city = person.City,
                            state = person.State,
                            zip = person.Zip,
                            img = person.img,
                            homePhone = (homePhone != null) ? homePhone.number : null,
                            workPhone = (workPhone != null) ? workPhone.number : null,
                            cellPhone = (cellPhone != null) ? cellPhone.number : null
                        };
            return query;
        }



        // GET api/<controller>/5
        //public Contact Get(int id)
        //{
        //    if ((id > contacts.Count) || (id < 0))
        //        return default(Contact);
        //    return contacts.Single(c => c.id == id);
        //}
        // GET api/<controller>/5
        public Contact Get(int id)
        {
            return CreateContactsQuery().SingleOrDefault(c => c.id == id);
        }

        private PhoneNumber createPhoneNumber(string type, string newNumber)
        {
            var thisPhoneType = service.PhoneNumberTypes.Single(t => t.type == type);
            var newPhone = new PhoneNumber();
            newPhone.number = newNumber;
            newPhone.PhoneNumberType = thisPhoneType;
            return newPhone;
        }


        // POST api/<controller>
        public void Post([FromBody]Contact value)
        {
            if (validateContact(value))
            {
                var newContact = new Person();
                newContact.firstName = value.firstName;
                newContact.lastName = value.lastName;
                newContact.Address = value.address;
                newContact.City = value.city;
                newContact.State = value.state;
                newContact.Zip = value.zip;
                newContact.img = value.img;
                if (!string.IsNullOrWhiteSpace(value.homePhone))
                {
                    var homePhone = createPhoneNumber("home", value.homePhone);
                    newContact.PhoneNumbers.Add(homePhone);
                }
                if (!string.IsNullOrWhiteSpace(value.workPhone))
                {
                    var workPhone = createPhoneNumber("work", value.workPhone);
                    newContact.PhoneNumbers.Add(workPhone);
                }
                if (!string.IsNullOrWhiteSpace(value.cellPhone))
                {
                    var cellPhone = createPhoneNumber("cell", value.cellPhone);
                    newContact.PhoneNumbers.Add(cellPhone);
                }

                service.AddPerson(newContact);
                service.SaveChanges();

            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.ReasonPhrase = "Tried to add invalid contact";
                throw new HttpResponseException(response);
            }
        }

        private bool validateContact(Contact value)
        {
            return !string.IsNullOrWhiteSpace(value.firstName);
        }

        private void updatePhone(Person personToUpdate, string phoneType, string valuePhone)
        {
            var phoneToUpdate = personToUpdate
        .PhoneNumbers
        .FirstOrDefault(ph => ph.PhoneNumberType == service.PhoneNumberTypes.FirstOrDefault(t => t.type == phoneType));
            if (string.IsNullOrWhiteSpace(valuePhone))
            {
                personToUpdate.PhoneNumbers.Remove(phoneToUpdate);
            }
            else
            {
                if (phoneToUpdate != null)
                    phoneToUpdate.number = valuePhone;
                else
                {
                    var newhomePhone = createPhoneNumber(phoneType, valuePhone);
                    personToUpdate.PhoneNumbers.Add(newhomePhone);
                }
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Contact value)
        {
            if (!validateContact(value))
                return;

            var personToUpdate = service.People.SingleOrDefault(p => p.Id == id);
            personToUpdate.firstName = value.firstName;
            personToUpdate.lastName = value.lastName;
            personToUpdate.Address = value.address;
            personToUpdate.City = value.city;
            personToUpdate.State = value.state;
            personToUpdate.Zip = value.zip;
            personToUpdate.img = value.img;

            updatePhone(personToUpdate, "home", value.homePhone);
            updatePhone(personToUpdate, "work", value.workPhone);
            updatePhone(personToUpdate, "cell", value.cellPhone);

            service.SaveChanges();
        }


        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            var person = (from p in service.People
                          where p.Id == id
                          select p).FirstOrDefault();

            var phonesToRemove = from phone in person.PhoneNumbers
                                 select phone;
            service.RemovePhoneNumbers(phonesToRemove);

            service.RemovePerson(person);
            service.SaveChanges();
        }
    }
}