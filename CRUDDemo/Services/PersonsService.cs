using Entites;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

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
            _persons = new List<Person>();
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
            if (personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }

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
            throw new NotImplementedException();
        }
    }
}
