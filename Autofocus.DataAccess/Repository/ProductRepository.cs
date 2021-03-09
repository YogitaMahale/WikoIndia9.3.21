
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
  public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
      
        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        //public void Update(Product product)
        //{
        //    var obj = _db.Product.FirstOrDefault(s => s.id  == product.id);
        //    if(obj!=null)
        //    {
        //        obj.userId = product.userId;
        //        obj.subCategoryId = product.subCategoryId;               
        //        obj.gradeId = product.gradeId;
        //        obj.packingSizeId = product.packingSizeId;
        //        obj.quantity = product.quantity;
        //        obj.spotRate = product.spotRate;
        //        obj.cityId = product.cityId;
        //        obj.rateTill = product.rateTill;
        //        obj.isdeleted = product.isdeleted;

        //        //_db.SaveChanges();
        //    }

        //}
    }
}
