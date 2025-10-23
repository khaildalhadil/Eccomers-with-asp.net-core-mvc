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
        }

        //public T Get(Expression<Func<T, bool>> filter)
        //{
        //    IQueryable<T> query = dbSet;
        //    return query.Where(filter).FirstOrDefault();
        //}
        public T Get(int id)
        {
            var data = dbSet.Find(id);
            
            if(data == null)
            {
                return null;
            }

            return data;

        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
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
