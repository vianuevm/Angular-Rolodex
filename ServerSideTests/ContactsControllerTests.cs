using Moq;
using Rolodex3.APIControllers;
using Rolodex3.Models;
using Rolodex3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Xunit;
namespace ServerSideTests
{
    public class ContactsControllerTests
    {

        private List<Person> contacts = new List<Person>
               {
                    new Person { Id = 1,
                        firstName = "John",
                        lastName = "Lennon",
                        Address = "123 Strawberry Fields",
                        City = "Forever",
                        State = "UK",
                        Zip = "12344"
                    },
                    new Person { Id = 2,
                        firstName = "Paul",
                        lastName = "McCartney",
                        Address = "456 Penny Lane",
                        City = "London",
                        State = "UK",
                        Zip = "55423"
                    },
                    new Person {Id = 3,
                        firstName =  "George",
                        lastName = "Harrison",
                        Address = "141 Something",
                        City = "London",
                        State = "UK",
                        Zip = "55627"
                    },
                    new Person { Id = 4,
                        firstName = "Ringo",
                        lastName = "Starr",
                        Address = "1669 Octopus' Garden",
                        City = "New York",
                        State = "NY",
                        Zip = "12345"
                    }
                };

        static List<PhoneNumberType> types = new List<PhoneNumberType>
        {
            new PhoneNumberType { Id=1, type="home" },
            new PhoneNumberType { Id=2, type="work" },
            new PhoneNumberType { Id=3, type="cell" }
        };

        private Mock<IContactsDataService> testEnvironmentSetup()
        {
            var mockService = new Mock<IContactsDataService>();
            mockService.Setup(srv => srv.People).Returns(contacts.AsQueryable());
            mockService.Setup(srv => srv.PhoneNumberTypes).Returns(types.AsQueryable());

            mockService.Setup(m => m.AddPerson(It.IsAny<Person>()))
                .Callback((Person p) => contacts.Add(p));
            mockService.Setup(m => m.RemovePerson(It.IsAny<Person>()))
                .Callback((Person p) => contacts.Remove(p));

            mockService.Setup(m => m.SaveChanges()).Verifiable();

            return mockService;
        }


        [Fact]
        public void GetReturnFourDefaultContacts()
        {
            //Arrange
            var mockService = testEnvironmentSetup();
            mockService.Setup(srv => srv.People).Returns(contacts.AsQueryable());
            mockService.Setup(srv => srv.PhoneNumberTypes).Returns(types.AsQueryable());

            var undertest = new ContactsController(mockService.Object);

            // Act:
            var result = undertest.Get();

            // Assert:
            Assert.Equal(4, result.Count());
        }

        [Fact]
        public void RetrieveASingleContactReturnsTheProperIndex()
        {
            // Arrange:
            var undertest = new ContactsController();

            // Act:
            var result = undertest.Get(3);

            // Assert:
            Assert.Equal(3, result.id);

        }

        [Fact]
        public void GettingAnInvalidIndexReturnsNull()
        {
            // Arrange:
            var undertest = new ContactsController();

            // Act:
            var result = undertest.Get(6);

            // Assert:
            Assert.Null(result);
        }

        [Fact]
        public void GettingANegativeReturnsNull()
        {
            // Arrange:
            var undertest = new ContactsController();

            // Act:
            var result = undertest.Get(-3);

            // Assert:
            Assert.Null(result);
        }

        [Fact]
        public void PostWithBlankContactAddsToList()
        {
            // Arrange:
            var undertest = new ContactsController();
            var currentLength = undertest.Get().Count();

            // Act:
            var addition = new Contact();
            Assert.Throws<HttpResponseException>(() => undertest.Post(addition));

            // Assert:
            var newCollection = undertest.Get();

            Assert.Equal(currentLength, newCollection.Count());
            Assert.Equal(0, newCollection.Where(c => c == addition).Count());
        }

        [Fact]
        public void PostWithValidContactAddsToList()
        {
            // Arrange:
            var undertest = new ContactsController();
            var currentLength = undertest.Get().Count();

            // Act:
            var addition = new Contact(
                "Pete",
                "Best",
                "326 Sticks Lane",
                "LIverpool",
                "UK",
                "anon.jpg",
                "83567");
            undertest.Post(addition);

            // Assert:
            var newCollection = undertest.Get();

            Assert.Equal(currentLength + 1, newCollection.Count());
            Assert.Equal(1, newCollection.Where(c => c == addition).Count());
        }

        [Fact]
        public void PutUpdatesAContact()
        {
            // Arrange:
            var mockService = testEnvironmentSetup();
            var undertest = new ContactsController(mockService.Object);
            var target = undertest.Get(1);

            // Act:
            undertest.Put(1, new Contact
            {
                id = 1,
                firstName = target.firstName,
                lastName = "Ono Lennon",
                address = target.address,
                city = target.city,
                state = target.state,
                zip = target.zip,
                homePhone = target.homePhone,
                workPhone = target.workPhone,
                cellPhone = target.cellPhone
            });

            // Assert:
            var updatedContact = undertest.Get(1);

            Assert.Equal("Ono Lennon", updatedContact.lastName);
        }

        [Fact]
        public void DeleteAnExistingContactRemovesTheContact()
        {
            // Arrange:
            var mockService = testEnvironmentSetup();
            var undertest = new ContactsController(mockService.Object);

            Assert.Equal(1, undertest.Get().Where(c => c.id == 2).Count());

            // Act:
            undertest.Delete(2);

            // Assert:
            var count = undertest.Get().Where(c => c.id == 2).Count();

            Assert.Equal(0, count);

        }

    }


}
