
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
  public class MainCategoryRepository : Repository<MainCategory>, IMainCategoryRepository
    {
        private readonly ApplicationDbContext _db;
      
        public MainCategoryRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public bool MainCategoryExists(string name)
        {
            bool obj = _db.MainCategory.Any(a => a.name.ToLower().Trim() == name.ToLower().Trim());
            return obj;
        }

        public void Update(MainCategory mainCategory)
        {
            var obj = _db.MainCategory.FirstOrDefault(s => s.id  ==mainCategory.id);
            if(obj!=null)
            {
                obj.name = mainCategory.name;
                obj.img = mainCategory.img;
                obj.isactive = mainCategory.isactive;
                obj.isdeleted = mainCategory.isdeleted;

                //_db.SaveChanges();
            }

        }
    }
}
