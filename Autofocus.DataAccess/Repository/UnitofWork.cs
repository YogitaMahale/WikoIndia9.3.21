using Autofocus.DataAccess.Data;
using Autofocus.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autofocus.DataAccess.Repository
{
   public  class UnitofWork:IUnitofWork
    {
        private readonly ApplicationDbContext _db;
        public UnitofWork(ApplicationDbContext db)
        {
            _db = db;
             
            country = new CountryRepository(_db);
            state = new StateRepository(_db);
            city = new CityRepository(_db);
            sp_call = new SP_CALL(_db);
            slider = new sliderRepository(_db);
            packingType = new packingTypeRepository(_db);
            packingSize = new packingSizeRepository(_db);
            grade = new GradeRepository(_db);
            mainCategory = new MainCategoryRepository(_db);
            subcategory= new SubcategoryRepository(_db);
            applicationUser = new ApplicationUserRepository(_db);
            product = new ProductRepository(_db);
            productDetails = new ProductDetailsRepository(_db);
            productMaster = new ProductMasterRepository(_db);
            productSize = new productSizeRepository(_db);
        }
       
        public ICountryRepository country { get; private set; }
        public ICityRepository city { get; private set; }
        public IStateRepository state { get; private set; }
        public IsliderRepository slider { get; private set; }
         
        public ISP_CALL sp_call { get; private set; }
        public IpackingTypeRepository packingType { get; private set; }
        public IpackingSizeRepository packingSize { get; private set; }
        public IGradeRepository grade{ get; private set; }
        public IMainCategoryRepository mainCategory{ get; private set; }
        public ISubcategoryRepository subcategory{ get; private set; }
        public IApplicationUserRepository applicationUser{ get; private set; }
        public IProductRepository product{ get; private set; }
        public IProductDetailsRepository productDetails{ get; private set; }
        public IProductMasterRepository productMaster { get; private set; }
        public IproductSizeRepository productSize { get; private set; }
        public void Dispose()
        {
            _db.Dispose();
        }
        //public void Save()
        //{
        //    _db.SaveChanges();
        //}
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false ;

        }
    }
}
