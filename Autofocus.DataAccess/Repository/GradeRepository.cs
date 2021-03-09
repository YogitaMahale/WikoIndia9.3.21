
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
  public class GradeRepository : Repository<Grade>, IGradeRepository
    {
        private readonly ApplicationDbContext _db;
      
        public GradeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Grade grade)
        {
            var obj = _db.Grade.FirstOrDefault(s => s.id  ==grade.id);
            if(obj!=null)
            {
                obj.type = grade.type;              
                obj.isactive = grade.isactive;
                obj.isdeleted = grade.isdeleted;

                //_db.SaveChanges();
            }

        }
    }
}
