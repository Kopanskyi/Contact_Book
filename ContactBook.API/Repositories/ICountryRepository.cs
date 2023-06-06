using ContactBook.API.Models;
using System.Collections.Generic;

namespace ContactBook.API.Repositories
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetCountries();
    }
}
