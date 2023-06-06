using ContactBook.API.Models;
using System.Collections.Generic;

namespace ContactBook.API.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IEnumerable<Country> _countries;

        public CountryRepository()
        {
            _countries = new List<Country>
            {
                new Country
                {
                    Id = 1,
                    Name = "Ukraine"
                },
                new Country
                {
                    Id = 2,
                    Name = "Germany"
                },
                new Country
                {
                    Id = 3,
                    Name = "Italy"
                },
                new Country
                {
                    Id = 4,
                    Name = "Great Britain"
                }
            };
        }

        public IEnumerable<Country> GetCountries()
        {
            return _countries;
        }
    }
}
