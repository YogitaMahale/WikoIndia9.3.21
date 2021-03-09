
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
  public class packingSizeRepository : Repository<packingSize>, IpackingSizeRepository
    {
        private readonly ApplicationDbContext _db;
      
        public packingSizeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(packingSize packingSize)
        {
            var obj = _db.packingSize.FirstOrDefault(s => s.id  == packingSize.id);
            if(obj!=null)
            {
                obj.name = packingSize.name;
                
                obj.isactive = packingSize.isactive;
                obj.isdeleted = packingSize.isdeleted;

                //_db.SaveChanges();
            }

        }
    }
}
