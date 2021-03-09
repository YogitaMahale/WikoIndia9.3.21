
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
  public class packingTypeRepository : Repository<packingType>, IpackingTypeRepository
    {
        private readonly ApplicationDbContext _db;
      
        public packingTypeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(packingType packingType)
        {
            var obj = _db.packingType.FirstOrDefault(s => s.id  ==packingType.id);
            if(obj!=null)
            {
                obj.name = packingType.name;
                
                obj.isactive = packingType.isactive;
                obj.isdeleted = packingType.isdeleted;

                //_db.SaveChanges();
            }

        }
    }
}
