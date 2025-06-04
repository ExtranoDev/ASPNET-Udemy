using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using ServiceContracts.Enums;
using Xunit.Abstractions;
using System.Security.AccessControl;

namespace CRUDTests
{
    public class PersonsServiceTest
    {
        // private fields
        private readonly IPersonsService _personService;
        private readonly ICountriesService _countryService;
        private readonly ITestOutputHelper _testOutputHelper;

        // contructor
        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personService = new PersonsService();
            _countryService = new CountriesService(false);
            _testOutputHelper = testOutputHelper;
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
            PersonAddRequest? personAddRequest = new()
            {
                PersonName = "Person name...",
                Email = "person@example.com",
                Address = "sample address",
                CountryID = Guid.NewGuid(),
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2001-01-01"),
                RecieveNewsLetters = true
            };

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
            PersonAddRequest person_response_from_get = new PersonAddRequest()
            {
                PersonName = "Joshua",
                Email = "email@sample.com",
                Address = "address",
                CountryID = countryResponse.CountryID,
                DateOfBirth = DateTime.Parse("2001-01-01"),
                Gender = GenderOptions.Male,
                RecieveNewsLetters = false
            };
            PersonResponse personResponse_fromAdd = _personService.AddPerson(person_response_from_get);
            PersonResponse personResponse_fromGet = _personService.GetPersonByPersonID(personResponse_fromAdd.PersonID);

            // Assert
            Assert.Equal(personResponse_fromAdd, personResponse_fromGet);
        }
        #endregion

        #region GetAllPersons
        // The GetAllPersons() should return an empty
        // list by default
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            // Arrange
            // Act
            List<PersonResponse> person_from_get = _personService.GetAllPersons();
            // Assert
            Assert.Empty(person_from_get);
        }

        // First, we will add few persons; The GetAllPersons() should return a list of available persons
        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            // Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "Canada" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 =
                _countryService.AddCountry(country_request_1);
            CountryResponse country_response_2 =
                _countryService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new()
            {
                PersonName = "David",
                Email = "davido@davido.com",
                Gender = GenderOptions.Male,
                Address = "address",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2001-01-01")
            };

            PersonAddRequest person_request_2 = new()
            {
                PersonName = "Adeoluwa",
                Email = "deolu@davido.com",
                Gender = GenderOptions.Male,
                Address = "Oluwo street",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2001-01-01")
            };

            PersonAddRequest person_request_3 = new()
            {
                PersonName = "Vincent",
                Email = "vincent@vincent.com",
                Gender = GenderOptions.Male,
                Address = "Odo Isokun",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2001-01-01")
            };

            List<PersonAddRequest> person_requests =
            [
                person_request_1,
                person_request_2,
                person_request_3
            ];

            List<PersonResponse> person_response_list_from_add = [];

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            // print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }

            // Act
            List<PersonResponse> person_list_from_get = _personService.GetAllPersons();

            // print person_list_from_get
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response_from_get in person_list_from_get)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }

            // Assert
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                Assert.Contains(person_response_from_add, person_list_from_get);
            }
        }
        #endregion

        #region GetFilteredPerson
        // If the search text is empty and search by is "PersonName",
        // it should return all persons
        [Fact]
        public void GetFilteredPersons_EmptySearchText()
        {
            // Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "Canada" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 =
                _countryService.AddCountry(country_request_1);
            CountryResponse country_response_2 =
                _countryService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new()
            {
                PersonName = "David",
                Email = "davido@davido.com",
                Gender = GenderOptions.Male,
                Address = "address",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2001-01-01")
            };

            PersonAddRequest person_request_2 = new()
            {
                PersonName = "Adeoluwa",
                Email = "deolu@davido.com",
                Gender = GenderOptions.Male,
                Address = "Oluwo street",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2001-01-01")
            };

            PersonAddRequest person_request_3 = new()
            {
                PersonName = "Vincent",
                Email = "vincent@vincent.com",
                Gender = GenderOptions.Male,
                Address = "Odo Isokun",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2001-01-01")
            };

            List<PersonAddRequest> person_requests =
            [
                person_request_1,
                person_request_2,
                person_request_3
            ];

            List<PersonResponse> person_response_list_from_add = [];

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            // print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }

            // Act
            List<PersonResponse> person_list_from_search = _personService.GetFilteredPersons(nameof(PersonAddRequest.PersonName),
                "");

            // print person_list_from_get
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response_from_get in person_list_from_search)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }

            // Assert
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                Assert.Contains(person_response_from_add, person_list_from_search);
            }
        }

        // First we will add few persons;
        // and then we will search based on person name with some searxh string
        // It should return the matching persons
        [Fact]
        public void GetFilteredPersons_SearchByPersonName()
        {
            // Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "Canada" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 =
                _countryService.AddCountry(country_request_1);
            CountryResponse country_response_2 =
                _countryService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new()
            {
                PersonName = "David",
                Email = "davido@davido.com",
                Gender = GenderOptions.Male,
                Address = "address",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2001-01-01")
            };

            PersonAddRequest person_request_2 = new()
            {
                PersonName = "Adeoluwa",
                Email = "deolu@davido.com",
                Gender = GenderOptions.Male,
                Address = "Oluwo street",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2001-01-01")
            };

            PersonAddRequest person_request_3 = new()
            {
                PersonName = "Vincent",
                Email = "vincent@vincent.com",
                Gender = GenderOptions.Male,
                Address = "Odo Isokun",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2001-01-01")
            };

            List<PersonAddRequest> person_requests =
            [
                person_request_1,
                person_request_2,
                person_request_3
            ];

            List<PersonResponse> person_response_list_from_add = [];

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            // print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }

            // Act
            List<PersonResponse> person_list_from_search = _personService.GetFilteredPersons(nameof(PersonAddRequest.PersonName),
                "Adeoluwa");

            // print person_list_from_get
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response_from_get in person_list_from_search)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }

            // Assert
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                if(person_response_from_add.PersonName != null)
                {
                    if (person_response_from_add.PersonName.Contains("Adeoluwa", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(person_response_from_add, person_list_from_search);
                    }
                }
                
            }
        }
        #endregion

        #region GetSortedPersons
        // When we sort based on PersonName in DESC,
        // it should return the persons in descending order
        [Fact]
        public void GetSortedPersons()
        {
            // Arrange
            CountryAddRequest country_request_1 = new() { CountryName = "Canada" };
            CountryAddRequest country_request_2 = new() { CountryName = "India" };

            CountryResponse country_response_1 =
                _countryService.AddCountry(country_request_1);
            CountryResponse country_response_2 =
                _countryService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new()
            {
                PersonName = "David",
                Email = "davido@davido.com",
                Gender = GenderOptions.Male,
                Address = "address",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2001-01-01")
            };

            PersonAddRequest person_request_2 = new()
            {
                PersonName = "Adeoluwa",
                Email = "deolu@davido.com",
                Gender = GenderOptions.Male,
                Address = "Oluwo street",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2001-01-01")
            };

            PersonAddRequest person_request_3 = new()
            {
                PersonName = "Vincent",
                Email = "vincent@vincent.com",
                Gender = GenderOptions.Male,
                Address = "Odo Isokun",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2001-01-01")
            };

            List<PersonAddRequest> person_requests =
            [
                person_request_1,
                person_request_2,
                person_request_3
            ];

            List<PersonResponse> person_response_list_from_add = [];

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            // print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
            List<PersonResponse> allPersons = _personService.GetAllPersons();


            // Act
            List<PersonResponse> person_list_from_sort = _personService.GetSortedPersons(allPersons, nameof(PersonAddRequest.PersonName),
                SortOrderOptions.DESC);

            // print person_list_from_get
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response_from_get in person_list_from_sort)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }
            person_response_list_from_add = [.. person_response_list_from_add.OrderByDescending(temp => temp.PersonName)];

            // Assert
            for(int i = 0; i < person_response_list_from_add.Count; i++)
            {
                Assert.Equal(person_response_list_from_add[i], person_list_from_sort[i]);
            }
        }
        #endregion

        #region UpdatePerson
        // When we supply null as PersonUpdateRequest,
        // it should throw ArguementNullException
        [Fact]
        public void UpdatePerson_NullPerson() {
            // Arrange
            PersonUpdateRequest? person_update_request = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => {
                // Act
                _personService.UpdatePerson(person_update_request);
            });
        }

        // When we supply invalid PersonID,
        // it should throw ArguementException
        [Fact]
        public void UpdatePerson_InvalidPersonID()
        {
            // Arrange
            PersonUpdateRequest? person_update_request = new()
            {
                PersonID = Guid.NewGuid()
            };

            // Assert
            Assert.Throws<ArgumentException>(() => {
                // Act
                _personService.UpdatePerson(person_update_request);
            });
        }

        // When PersonName is null, it should throw
        [Fact]
        public void UpdatePerson_PersonNameIsNull() {
            // Arrange
            CountryAddRequest country_add_request = new CountryAddRequest()
            {
                CountryName = "UK"
            };
            CountryResponse country_response_from_add = _countryService.AddCountry(country_add_request);
            PersonAddRequest person_add_request = new() { PersonName = "John",
                CountryID = country_response_from_add.CountryID, Email = "john@example.com",
                Address = "address....", Gender = GenderOptions.Male };
            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);

            PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = null;

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _personService.UpdatePerson(person_update_request);
            });
        }

        // First, add a new person and try to update the person
        // name and email
        [Fact]
        public void UpdatePerson_PersonFullDetails()
        {
            // Arrange
            CountryAddRequest country_add_request = new CountryAddRequest()
            {
                CountryName = "UK"
            };
            CountryResponse country_response_from_add = _countryService.AddCountry(country_add_request);
            PersonAddRequest person_add_request = new()
            {
                PersonName = "John",
                CountryID = country_response_from_add.CountryID,
                Address = "John Doe's address, Lagos",
                DateOfBirth = DateTime.Parse("2001-01-01"),
                Email = "adeoti@gmail.com",
                Gender = GenderOptions.Male,
                RecieveNewsLetters = true
            };
            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);

            PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = "William";
            person_update_request.Email = "william124@example.com";

            // Act
            PersonResponse person_response_from_update =
                _personService.UpdatePerson(person_update_request);
            PersonResponse person_response_from_get =
                _personService.GetPersonByPersonID(person_response_from_update.PersonID);


            // Assert
            Assert.Equal(person_response_from_get, person_response_from_update);
        }
        #endregion

        #region DeletePerson
        // If we supply valid PersonID,
        // it should return true
        [Fact]
        public void DeletePerson_ValidPersonID()
        {
            // Arrange
            CountryAddRequest country_add_request = new CountryAddRequest()
            {
                CountryName = "UK"
            };
            CountryResponse country_response_from_add = 
                _countryService.AddCountry(country_add_request);

            PersonAddRequest person_add_request =
                new()
                {
                    Address = "address",
                    CountryID = country_response_from_add.CountryID,
                    PersonName = "Jones",
                    DateOfBirth = DateTime.Parse("2001-01-01"),
                    Email = "example@example.com",
                    Gender = GenderOptions.Male,
                };

            PersonResponse person_response_from_add =
                _personService.AddPerson(person_add_request);

            // Act
            bool isDeleted = _personService.DeletePerson(person_response_from_add.PersonID);
            // Assert
            Assert.True(isDeleted);
        }

        // If we supply invalid PersonID,
        // it should return false
        [Fact]
        public void DeletePerson_InvalidPersonID()
        {            
            // Act
            bool isDeleted = _personService.DeletePerson(Guid.NewGuid());

            // Assert
            Assert.False(isDeleted);
        }
        #endregion
    }
}
