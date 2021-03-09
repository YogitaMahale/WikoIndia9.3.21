
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
  public class productSizeRepository : Repository<productSize>, IproductSizeRepository
    {
        private readonly ApplicationDbContext _db;
      
        public productSizeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public bool productSizeExists(string size)
        {
            bool obj = _db.productSize.Any(a => a.size.ToLower().Trim() == size.ToLower().Trim());
            return obj;
        }

        public void Update(productSize productSize)
        {
            var obj = _db.productSize.FirstOrDefault(s => s.id  == productSize.id);
            if(obj!=null)
            {
                obj.size = productSize.size;
                
                obj.isactive = productSize.isactive;
                obj.isdeleted = productSize.isdeleted;

                //_db.SaveChanges();
            }

        }
    }
}
