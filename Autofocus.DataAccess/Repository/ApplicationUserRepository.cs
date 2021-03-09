using Autofocus.DataAccess.Data;
using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autofocus.DataAccess.Repository
{
  public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
      
        public ApplicationUserRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        
    }
}
