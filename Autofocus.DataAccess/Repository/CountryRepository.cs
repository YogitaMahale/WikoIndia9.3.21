
using Autofocus.DataAccess.Data;
using Autofocus.DataAccess.Repository;
using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autofocus.DataAccess.Repository
{
  public class CountryRepository : Repository<CountryRegistration>, ICountryRepository
    {
        private readonly ApplicationDbContext _db;
      
        public CountryRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(CountryRegistration countryRegistration)
        {
            var obj = _db.CountryRegistration.FirstOrDefault(s => s.id  ==countryRegistration.id);
            if(obj!=null)
            {
                obj.countrycode = countryRegistration.countrycode;
                obj.countryname = countryRegistration.countryname;
               
                obj.isactive = countryRegistration.isactive;
                obj.isdeleted = countryRegistration.isdeleted;

                //_db.SaveChanges();
            }

        }
    }
}
