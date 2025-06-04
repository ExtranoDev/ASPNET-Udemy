using Entites;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        // private field
        private readonly List<Country> _countries;

        // Constructor
        public CountriesService(bool initialize = true)
        {
            _countries = [];
            if (initialize)
            {
                // Initialize with some default countries
                _countries.AddRange([
                    new() { CountryID = Guid.Parse("4365496B-67EB-4CC9-8573-68FCE18CEB53"), CountryName = "United States" },
                    new() { CountryID = Guid.Parse("432F0CD2-D015-47AE-80B4-F9B04D357A84"), CountryName = "Canada" },
                    new() { CountryID = Guid.Parse("90DC72A9-EB3B-4509-AFF3-C755FCDA402F"), CountryName = "United Kingdom" },
                    new() { CountryID = Guid.Parse("4A5DD850-FA99-41AB-B8F7-7C61EFC8F4AA"), CountryName = "Australia" }
                ]);
            }
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {            
            // Validation: countryAddRequest parameter can't be null
            if(countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            // Validation: CountryName can't be null
            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }

            // Validation: CountryName can't be duplicate
            if (_countries.Where(temp => temp.CountryName == countryAddRequest.CountryName).Count() > 0)
            {
                throw new ArgumentException("Given country name already exists");
            }

            // Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            // generate CountryID
            country.CountryID = Guid.NewGuid();

            // Add country object into _countries
            _countries.Add(country);
            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null) return null;

            Country? country_response_from_list =
            _countries.FirstOrDefault(temp => temp.CountryID == countryID);

            if (country_response_from_list == null) return null;
            return country_response_from_list.ToCountryResponse();
        }
    }
}
