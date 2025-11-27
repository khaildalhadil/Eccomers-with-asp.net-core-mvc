using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BulkeyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BulkeyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _db;

        // to get the name of the DataBase
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            // if it is Category it will be Category
            // if it is User it will set to User etc..
            this.dbSet = _db.Set<T>();
            // _db.Categories == dbSet
            // I Can Do Now dbSet.Add(category)
            //dbSet.Include(u=> u.)
            _db.Products.Include(u => u.Category);
        }

        //public T Get(Expression<Func<T, bool>> filter)
        //{
        //    IQueryable<T> query = dbSet;
        //    return query.Where(filter).FirstOrDefault();
        //}
        public T Get(Expression<Func<T, bool>> filter)
        {
            return (T)dbSet.FirstOrDefault(filter);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties)) { 
                foreach( var includeProp in includeProperties
                    .Split(new char[] { ','}, 
                    StringSplitOptions.RemoveEmptyEntries)
                    )
                {
                    query = query.Include(includeProp);
                }
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.ToList();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }


        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

    }
}
