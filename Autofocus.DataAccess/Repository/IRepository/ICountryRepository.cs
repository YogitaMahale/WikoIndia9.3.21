using Microsoft.AspNetCore.Mvc.Rendering;
using Autofocus.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Autofocus.DataAccess.Repository.IRepository
{
  public   interface ICountryRepository : IRepository<CountryRegistration>
    {
        void Update(CountryRegistration CountryRegistration);
        
    }
}
