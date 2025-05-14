using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using ServiceContracts.Enums;

namespace CRUDTests
{
    public class PersonsServiceTest
    {
        // private fields
        private readonly IPersonsService _personService;

        // contructor
        public PersonsServiceTest()
        {
            _personService = new PersonsService();
        }

        #region AddPerson
        // When null value is supplied as PersonAddRequest,
        // it should throw ArguementNullException
        [Fact]
        public void AddPerson_Null()
        {
            // Arrange
            PersonAddRequest? personAddRequest = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => { _personService.AddPerson(personAddRequest); });
        }

        // When null value is supplied as PersonName,
        // it should throw ArguementNullException
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            // Arrange
            PersonAddRequest? personAddRequest = new() { PersonName = null };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => { _personService.AddPerson(personAddRequest); });
        }

        // When proper value is supplied as PersonName,
        // it should insert the person into the persons list;
        // and it should return an object of PersonResponse,
        // which includes with the newly generated person id
        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            // Arrange
            PersonAddRequest? personAddRequest = new() { PersonName = "Person name...",
            Email = "person@example.com", Address = "sample address", CountryID = Guid.NewGuid(), Gender = GenderOptions.Male,
            DateOfBirth = DateTime.Parse("2001-01-01"), RecieveNewsLetters = true};

            // Act
            PersonResponse person_response_from_add = _personService.AddPerson(personAddRequest);
            List<PersonResponse> persons_list = _personService.GetAllPersons();

            // Assert
            Assert.True(person_response_from_add.PersonID != Guid.Empty);
            Assert.Contains(person_response_from_add, persons_list);
        }
        #endregion
    }
}
