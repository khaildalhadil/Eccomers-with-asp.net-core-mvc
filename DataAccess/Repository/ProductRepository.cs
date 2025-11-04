using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkeyBook.DataAccess.Repository.IRepository;
using BulkeyBook.Model;

namespace BulkeyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            _db.Update(obj);
        }
    }
}
