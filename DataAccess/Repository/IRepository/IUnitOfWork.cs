using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkeyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository category { get; }
        IProductRepository product { get;  }
        IShoppingCartRepository shoppingCart { get; }
        IApplicationUserRepository users { get; }
        IOrderDetailRepository orderDetail { get; }
        IOrderHeaderRepository orderHeader { get; }
        void Save();

    }
}
