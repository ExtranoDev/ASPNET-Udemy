﻿using ServiceContracts.DTO;
using System.Dynamic;

namespace ServiceContracts
{
    /// <summary>
    /// Reperesents business logic for manipulating Country
    /// entity
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds a country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country Object to add</param>
        /// <returns>Returns the country object after adding it 
        /// (including newly generated country)</returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

        /// <summary>
        /// Returns all countries from the list
        /// </summary>
        /// <returns>All Countries from the list as List of CountryResponse</returns>
        List<CountryResponse> GetAllCountries();

        /// <summary>
        /// Return a Country object based on the given country id
        /// </summary>
        /// <param name="countryID">CountryID (guid) to search</param>
        /// <returns>Matching country as CountryResponse object</returns>
        CountryResponse? GetCountryByCountryID(Guid? countryID);
    }
}
