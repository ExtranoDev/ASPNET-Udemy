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
        public PersonsService()
        {
            _persons = [];
            _countriesService = new CountriesService();
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
                nameof(Person.PersonName) => [.. allPersons.Where(temp => string.IsNullOrEmpty(temp.PersonName)
                    || temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase))],
                nameof(Person.Email) => [.. allPersons.Where(temp => string.IsNullOrEmpty(temp.Email)
                    || temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))],
                nameof(Person.DateOfBirth) => [.. allPersons
                        .Where(temp => temp.DateOfBirth == null || temp.DateOfBirth.Value.ToString("dd MMM yyyy")
                        .Contains(searchString, StringComparison.OrdinalIgnoreCase))],
                nameof(Person.Gender) => [.. allPersons.Where(temp => string.IsNullOrEmpty(temp.Gender)
                    || temp.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase))],
                nameof(Person.CountryID) => [.. allPersons.Where(temp => string.IsNullOrEmpty(temp.Country)
                    || temp.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase))],
                nameof(Person.Address) => [.. allPersons.Where(temp => string.IsNullOrEmpty(temp.Address)
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
    }
}
