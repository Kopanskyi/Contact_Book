using ContactBook.API.Models;
using System.Collections.Generic;

namespace ContactBook.API.Services
{
    public interface ICountryService
    {
        IEnumerable<Country> GetCountries();
    }
}
