
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
  public class ProductDetailsRepository : Repository<ProductDetails>, IProductDetailsRepository
    {
        private readonly ApplicationDbContext _db;
      
        public ProductDetailsRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        //public void Update(ProductDetails productDetails)
        //{
        //    var obj = _db.ProductDetails.FirstOrDefault(s => s.id  == productDetails.id);
        //    if(obj!=null)
        //    {
        //        obj.pid = productDetails.pid;
        //        obj.bagId = productDetails.bagId;
               
        //        obj.bagPrice = productDetails.bagPrice;
        //        obj.labourCost = productDetails.labourCost;
        //        obj.isdeleted = productDetails.isdeleted;
               
        //        //_db.SaveChanges();
        //    }

        //}
    }
}
