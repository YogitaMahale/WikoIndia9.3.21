using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Autofocus.Models;
namespace Autofocus.DataAccess.Repository.IRepository
{
   public  interface ISubcategoryRepository : IRepository<Subcategory>
    {
        void Update(Subcategory  subcategory);
    }
}
