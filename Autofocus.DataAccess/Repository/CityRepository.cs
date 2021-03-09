
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
  public class CityRepository : Repository<CityRegistration>, ICityRepository
    {
        private readonly ApplicationDbContext _db;
      
        public CityRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(CityRegistration cityRegistration)
        {
            var obj = _db.CityRegistration.FirstOrDefault(s => s.id  == cityRegistration.id);
            if(obj!=null)
            {
                obj.stateid = cityRegistration.stateid;
                obj.cityName = cityRegistration.cityName;
               
                obj.isactive = cityRegistration.isactive;
                obj.isdeleted = cityRegistration.isdeleted;

                //_db.SaveChanges();
            }

        }
    }
}
