
using Autofocus.DataAccess.Data;
using Autofocus.DataAccess.Repository;
using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.Entity;
using Autofocus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autofocus.DataAccess.Repository
{
  public class sliderRepository : Repository<slider>, IsliderRepository
    {
        private readonly ApplicationDbContext _db;
      
        public sliderRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(slider slider)
        {
            var obj = _db.slider.FirstOrDefault(s => s.id  == slider.id);
            if(obj!=null)
            {
                obj.name = slider.name;
             
                //_db.SaveChanges();
            }

        }
    }
}
