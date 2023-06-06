using ContactBook.API.Models;
using ContactBook.API.Repositories;
using System.Collections.Generic;

namespace ContactBook.API.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository repository)
        {
            _countryRepository = repository;
        }

        public IEnumerable<Country> GetCountries()
        {
            return _countryRepository.GetCountries();
        }
    }
}
