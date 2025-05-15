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
        private readonly ICountriesService _countryService;

        // contructor
        public PersonsServiceTest()
        {
            _personService = new PersonsService();
            _countryService = new CountriesService();
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
            Assert.Throws<ArgumentException>(() => { _personService.AddPerson(personAddRequest); });
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

        #region GetPersonByPersonID
        // If we supply null as PersonID, it should return null as
        // PersonResponse
        [Fact]
        public void GetPersonByPersonID_NullPersonID()
        {
            // Arrange
            Guid? personID = null;

            // Act
            PersonResponse person_response_from_get = _personService.GetPersonByPersonID(personID);

            // Assert
            Assert.Null(person_response_from_get);
        }

        // If we supply a valid PersonID, it should return
        // the valid person detaila as PersonResponse object
        [Fact]
        public void GetPersonByPersonID_WithPersonID()
        {
            // Arrange
            CountryAddRequest countryAddRequest = new() { CountryName = "Canada" };
            CountryResponse countryResponse = _countryService.AddCountry(countryAddRequest);

            // Act
            PersonAddRequest person_response_from_get = new PersonAddRequest() {
                PersonName = "Joshua",
                Email = "email@sample.com",
                Address = "address",
            CountryID = countryResponse.CountryID, DateOfBirth = DateTime.Parse("2001-01-01"),
            Gender = GenderOptions.Male, RecieveNewsLetters = false
            };
            PersonResponse personResponse_fromAdd = _personService.AddPerson(person_response_from_get);
            PersonResponse personResponse_fromGet = _personService.GetPersonByPersonID(personResponse_fromAdd.PersonID);

            // Assert
            Assert.Equal(personResponse_fromAdd, personResponse_fromGet);
        }
        #endregion
    }
}
