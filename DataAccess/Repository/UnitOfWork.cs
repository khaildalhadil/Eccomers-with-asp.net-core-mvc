using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkeyBook.DataAccess.Repository.IRepository;
using BulkeyBook.Model;

namespace BulkeyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository category { get;private set; }
        public IProductRepository product { get;private set; }
        public IShoppingCartRepository shoppingCart { get; private set; }
        public IApplicationUserRepository users { get; private set; }

        public IOrderDetailRepository orderDetail { get; private set; }

        public IOrderHeaderRepository orderHeader { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            category = new CategoryRepository(_db);
            product = new ProductRepository(_db);
            shoppingCart = new ShoppingCartRepository(_db);
            users = new ApplicationUserRepository(_db);
            orderDetail = new OrderDetailRepository(_db);
            orderHeader = new OrderHeaderRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
