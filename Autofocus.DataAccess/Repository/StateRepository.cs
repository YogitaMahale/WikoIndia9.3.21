
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
  public class StateRepository : Repository<StateRegistration>, IStateRepository
    {
        private readonly ApplicationDbContext _db;
      
        public StateRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(StateRegistration StateRegistration)
        {
            var obj = _db.StateRegistration.FirstOrDefault(s => s.id  == StateRegistration.id);
            if(obj!=null)
            {
                obj.countryid = StateRegistration.countryid;
                obj.StateName = StateRegistration.StateName;
               
                obj.isactive = StateRegistration.isactive;
                obj.isdeleted = StateRegistration.isdeleted;

                //_db.SaveChanges();
            }

        }
    }
}
