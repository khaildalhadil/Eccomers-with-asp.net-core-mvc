using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace BulkeyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T- Category
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        //calling a method is a type of expression
        // Func represent methods that return a value
        T Get(string id);
        // I can do like this => I GetById(int id)
        void Add(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);
    }
}
