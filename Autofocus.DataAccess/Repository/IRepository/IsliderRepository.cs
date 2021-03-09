using Microsoft.AspNetCore.Mvc.Rendering;
using Autofocus.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofocus.Entity;

namespace Autofocus.DataAccess.Repository.IRepository
{
 public    interface IsliderRepository : IRepository<slider>
    {
        void Update(slider  slider);
    }
}
