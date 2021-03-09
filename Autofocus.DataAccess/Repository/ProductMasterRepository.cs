
using Autofocus.DataAccess.Data;
using Autofocus.DataAccess.Repository;
using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autofocus.DataAccess.Repository
{
  public class ProductMasterRepository : Repository<ProductMaster>, IProductMasterRepository
    {
        private readonly ApplicationDbContext _db;
      
        public ProductMasterRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(ProductMaster productMaster)
        {
            var obj = _db.ProductMaster.FirstOrDefault(s => s.id  == productMaster.id);
            if(obj!=null)
            {
                obj.subCategroyId = productMaster.subCategroyId;
                obj.name  = productMaster.name ;               
                obj.img = productMaster.img ;
                obj.description  = productMaster.description;
                 
                //_db.SaveChanges();
            }

        }
        public IEnumerable<ProductMaster> SelectAllProductMaster()
        {
            var obj = _db.ProductMaster.Include(x => x.Subcategory).Where(x => x.isdeleted == false).ToList();
            
                return obj;
             
        }

        public bool ProductMasterExists(string name)
        {
            bool obj = _db.ProductMaster.Any(a => a.name.ToLower().Trim() == name.ToLower().Trim());
            return obj;
        }
    }
}
