using Autofocus.DataAccess.Data;
using Autofocus.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Autofocus.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;
       
        public Repository(ApplicationDbContext db)
        {
            this._db = db;
            this.dbset = _db.Set<T>();
        }
        //public Repository(ApplicationDbContext db)
        //{
        //    this.db = db;
        //}

        //public Repository(ApplicationDbContext db, DbSet<T> _dbset)
        //{
        //    _db = db;
        //    this.dbset = _db.Set<T>();
        //}
        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public T Get(int id)
        {
            return dbset.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if(filter!=null)
            {
                query = query.Where(filter);

            }
            if(includeProperties!=null)
            {
                foreach (var includeprop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
            }
            if(orderby !=null)
            {
                return orderby(query).ToList();
            }
            return query.ToList();
        }

        public T GetFirstorDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);

            }
            if (includeProperties != null)
            {
                foreach (var includeprop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
            } 
         
            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T entity = dbset.Find(id);
            Remove(entity);
        }

        public void Remove(T entity)
        {
           dbset.Remove(entity);
        }

        public void Remove(IEnumerable<T> entity)
        {
            dbset.RemoveRange(entity);
        }

        //public void RemoveRange(IEnumerable<T> entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
