using System;
using System.Collections.Generic;
using System.Text;

namespace Autofocus.DataAccess.Repository.IRepository
{
   public  interface IUnitofWork:IDisposable
    {
    
       
        ICountryRepository country { get; }
        IStateRepository state { get; }
        ICityRepository city { get; }
        IsliderRepository slider { get; }
        IpackingTypeRepository packingType { get; }
        IpackingSizeRepository packingSize { get; }
        IGradeRepository grade { get; }
        IMainCategoryRepository mainCategory { get; }
        ISubcategoryRepository subcategory { get; }
        IApplicationUserRepository applicationUser  { get; }
        IProductRepository product  { get; }
        IProductDetailsRepository productDetails  { get; }

        IProductMasterRepository productMaster { get; }
        IproductSizeRepository productSize { get; }
        ISP_CALL sp_call { get; }

        //void Save();
        bool Save();
    }
}
