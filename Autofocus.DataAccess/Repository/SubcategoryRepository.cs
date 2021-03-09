
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
  public class SubcategoryRepository : Repository<Subcategory>, ISubcategoryRepository
    {
        private readonly ApplicationDbContext _db;
      
        public SubcategoryRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Subcategory subcategory)
        {
            var obj = _db.Subcategory.FirstOrDefault(s => s.id  == subcategory.id);
            if(obj!=null)
            {
                obj.mainCategroyId = subcategory.mainCategroyId;
                obj.name= subcategory.name;
                obj.img = subcategory.img;
                obj.isactive = subcategory.isactive;
                obj.isdeleted = subcategory.isdeleted;

                //_db.SaveChanges();
            }

        }
    }
}
