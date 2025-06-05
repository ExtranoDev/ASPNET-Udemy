using Entites;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;
using System.Data;

namespace Services
{
    public class PersonsService : IPersonsService
    {
        // private field
        private readonly List<Person> _persons;
        private readonly ICountriesService _countriesService;


        // constructor
        public PersonsService(bool initialize = true)
        {
            _persons = [];
            _countriesService = new CountriesService();
            if (initialize)
            {
                _persons.Add(new Person
                {
                    PersonID = Guid.Parse("1a9eb2be-f04d-498c-b40e-936b8fcacdd4"),
                    PersonName = "Shelbi",
                    Email = "siverson0@arstechnica.com",
                    DateOfBirth = DateTime.Parse("1949-04-21"),
                    Gender = "Agender",
                    Address = "4 Pond Center",
                    RecieveNewsLetters = false
                });

                _persons.Add(new Person
                {
                    PersonID = Guid.Parse("133e64aa-7b0d-445e-ac59-a075f545c950"),
                    PersonName = "Dietrich",
                    Email = "dzebedee1@ed.gov",
                    DateOfBirth = DateTime.Parse("2008-04-21"),
                    Gender = "Bigender",
                    Address = "055 Talisman Point",
                    RecieveNewsLetters = true
                });

                _persons.Add(new Person
                {
                    PersonID = Guid.Parse("a38e6237-ef95-40f9-a853-f1299efa2cbb"),
                    PersonName = "Christabel",
                    Email = "cdunrige2@archive.org",
                    DateOfBirth = DateTime.Parse("1931-09-11"),
                    Gender = "Female",
                    Address = "2620 Elgar Parkway",
                    RecieveNewsLetters = false
                });

                _persons.Add(new Person
                {
                    PersonID = Guid.Parse("76f16611-bb89-4c50-bc6f-0e87c7c9c9c6"),
                    PersonName = "Cort",
                    Email = "ckernes3@cam.ac.uk",
                    DateOfBirth = DateTime.Parse("1992-09-10"),
                    Gender = "Male",
                    Address = "0 Cottonwood Place",
                    RecieveNewsLetters = true
                });

                _persons.Add(new Person
                {
                    PersonID = Guid.Parse("8fdd310a-7a25-4476-8460-968e5bd98153"),
                    PersonName = "Alica",
                    Email = "awicklin5@economist.com",
                    DateOfBirth = DateTime.Parse("1948-11-16"),
                    Gender = "Female",
                    Address = "3242 Bunting Lane",
                    RecieveNewsLetters = true
                });

                _persons.Add(new Person
                {
                    PersonID = Guid.Parse("1ec0424d-a101-43e5-b24c-f09604fea8c0"),
                    PersonName = "Mort",
                    Email = "mrase4@twitter.com",
                    DateOfBirth = DateTime.Parse("2019-02-27"),
                    Gender = "Male",
                    Address = "963 Montana Trail",
                    RecieveNewsLetters = false
                });

                _persons.Add(new Person
                {
                    PersonID = Guid.Parse("a5e57972-352e-4224-94f2-67a79c479b16"),
                    PersonName = "Faythe",
                    Email = "fteale6@parallels.com",
                    DateOfBirth = DateTime.Parse("1975-09-13"),
                    Gender = "Female",
                    Address = "9 Granby Way",
                    RecieveNewsLetters = true
                });

                _persons.Add(new Person
                {
                    PersonID = Guid.Parse("fece8b57-c64c-4c6b-a2ab-7957abeb0ad7"),
                    PersonName = "Arlen",
                    Email = "adarrach7@tripadvisor.com",
                    DateOfBirth = DateTime.Parse("1984-01-11"),
                    Gender = "Female",
                    Address = "870 Helena Crossing",
                    RecieveNewsLetters = false
                });

                _persons.Add(new Person
                {
                    PersonID = Guid.Parse("67741346-e5d7-4a29-8088-1da30cc32409"),
                    PersonName = "Blondelle",
                    Email = "bpindred8@xrea.com",
                    DateOfBirth = DateTime.Parse("1992-10-31"),
                    Gender = "Female",
                    Address = "36 Hanover Drive",
                    RecieveNewsLetters = true
                });

                _persons.Add(new Person
                {
                    PersonID = Guid.Parse("d633be55-78ae-42e7-9f96-5a798014902e"),
                    PersonName = "Natty",
                    Email = "nbrotherhed9@pen.io",
                    DateOfBirth = DateTime.Parse("1991-01-12"),
                    Gender = "Female",
                    Address = "63 Buell Circle",
                    RecieveNewsLetters = true
                });
            }
        
            
        }
        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            // convert person object into PersonResponse type
            PersonResponse personResponse = person.ToPersonResponse();
            // get country name from countries list
            personResponse.Country =
                _countriesService.GetCountryByCountryID(person.CountryID)?.CountryName;
            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            // check if PersonAddRequest is not null

            ArgumentNullException.ThrowIfNull(personAddRequest);

            // Model Validation
            ValidationHelper.ModelValidation(personAddRequest);

            // convert personAddRequest into Person type
            Person person = personAddRequest.ToPerson();

            // generate PersonId
            person.PersonID = Guid.NewGuid();

            // add person object to persons list
            _persons.Add(person);

            // convert person object into PersonResponse type
            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            return [.. _persons.Select(temp => temp.ToPersonResponse())];
        }

        public PersonResponse? GetPersonByPersonID(Guid? personID)
        {
            if (personID == null) return null;

            Person? person = _persons.FirstOrDefault(temp => temp.PersonID == personID);
            if (person == null) return null;

            return person.ToPersonResponse();
        }

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<PersonResponse> allPersons = GetAllPersons();
            List<PersonResponse> matchingPersons = allPersons;

            if(string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
                return matchingPersons;

            matchingPersons = searchBy switch
            {
                nameof(PersonResponse.PersonName) => [.. allPersons.Where(temp => string.IsNullOrEmpty(temp.PersonName)
                    || temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase))],
                nameof(PersonResponse.Email) => [.. allPersons.Where(temp => string.IsNullOrEmpty(temp.Email)
                    || temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))],
                nameof(PersonResponse.DateOfBirth) => [.. allPersons
                        .Where(temp => temp.DateOfBirth == null || temp.DateOfBirth.Value.ToString("dd MMM yyyy")
                        .Contains(searchString, StringComparison.OrdinalIgnoreCase))],
                nameof(PersonResponse.Gender) => [.. allPersons.Where(temp => string.IsNullOrEmpty(temp.Gender)
                    || temp.Gender.Equals(searchString, StringComparison.OrdinalIgnoreCase))],
                nameof(PersonResponse.CountryID) => [.. allPersons.Where(temp => string.IsNullOrEmpty(temp.Country)
                    || temp.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase))],
                nameof(PersonResponse.Address) => [.. allPersons.Where(temp => string.IsNullOrEmpty(temp.Address)
                    || temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase))],
                _ => allPersons,
            };
            return matchingPersons;
        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if(string.IsNullOrEmpty(sortBy))
                return allPersons;

            List<PersonResponse> sortedPersons = (sortBy, sortOrder) switch
            {
                (nameof(PersonResponse.PersonName), SortOrderOptions.ASC) =>
                [.. allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase)],

                (nameof(PersonResponse.PersonName), SortOrderOptions.DESC) =>
                [.. allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase)],

                (nameof(PersonResponse.Email), SortOrderOptions.ASC) =>
                [.. allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase)],

                (nameof(PersonResponse.Email), SortOrderOptions.DESC) =>
                [.. allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase)],

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC) =>
                [.. allPersons.OrderBy(temp => temp.DateOfBirth)],

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC) =>
                [.. allPersons.OrderByDescending(temp => temp.DateOfBirth)],

                (nameof(PersonResponse.Age), SortOrderOptions.ASC) =>
                [.. allPersons.OrderBy(temp => temp.Age)],

                (nameof(PersonResponse.Age), SortOrderOptions.DESC) =>
                [.. allPersons.OrderByDescending(temp => temp.DateOfBirth)],

                (nameof(PersonResponse.Gender), SortOrderOptions.ASC) =>
                [.. allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase)],

                (nameof(PersonResponse.Gender), SortOrderOptions.DESC) =>
                [.. allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase)],

                (nameof(PersonResponse.Country), SortOrderOptions.ASC) =>
                [.. allPersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase)],

                (nameof(PersonResponse.Country), SortOrderOptions.DESC) =>
                [.. allPersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase)],

                (nameof(PersonResponse.Address), SortOrderOptions.ASC) =>
                [.. allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase)],

                (nameof(PersonResponse.Address), SortOrderOptions.DESC) =>
                [.. allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase)],

                (nameof(PersonResponse.RecieveNewsLetters), SortOrderOptions.ASC) =>
                [.. allPersons.OrderBy(temp => temp.RecieveNewsLetters)],

                (nameof(PersonResponse.RecieveNewsLetters), SortOrderOptions.DESC) =>
                [.. allPersons.OrderByDescending(temp => temp.RecieveNewsLetters)],

                _ => allPersons,
            };

            return sortedPersons;
        }

        public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(personUpdateRequest));

            // validation
            ValidationHelper.ModelValidation(personUpdateRequest);

            // Get matching person object to update
            Person? matchingPerson = _persons.FirstOrDefault(temp => temp.PersonID == personUpdateRequest.PersonID);
            if(matchingPerson == null)
            {
                throw new ArgumentNullException($"Person with ID {personUpdateRequest.PersonID} not found");
            }

            // update all details
            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.CountryID = personUpdateRequest.CountryID;
            matchingPerson.Address = personUpdateRequest.Address;
            matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            matchingPerson.RecieveNewsLetters = personUpdateRequest.RecieveNewsLetters;

            return matchingPerson.ToPersonResponse();
        }

        public bool DeletePerson(Guid? personID)
        {
            if(personID == null)
                throw new ArgumentNullException(nameof(personID));

            Person? person = _persons.FirstOrDefault(temp => temp.PersonID == personID);
            if (person == null) return false;

            _persons.RemoveAll(temp => temp.PersonID == personID);
            return true;
        }
    }
}
