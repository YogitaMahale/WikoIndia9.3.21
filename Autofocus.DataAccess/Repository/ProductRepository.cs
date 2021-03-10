
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

        public void Update(Product product)
        {
            var obj = _db.Product.FirstOrDefault(s => s.id == product.id);
            if (obj != null)
            {
                obj.userId = product.userId;
                obj.productmasterId = product.productmasterId;
                obj.gradeId = product.gradeId;
                obj.productSizeId = product.productSizeId;
                obj.spotRate = product.spotRate;
                obj.quantityAvailable = product.quantityAvailable;
                obj.rateTill = product.rateTill;
                obj.cityId = product.cityId;
                obj.packingTypeId = product.packingTypeId;
                obj.isNegotiable = product.isNegotiable;
                obj.isdeleted = product.isdeleted;
                obj.tradeId = product.tradeId;
                
                //_db.SaveChanges();
            }

        }
    }
}
