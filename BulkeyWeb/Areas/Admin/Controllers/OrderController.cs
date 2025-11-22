using BulkeyBook.DataAccess.Repository.IRepository;
using BulkeyBook.Model;
using Microsoft.AspNetCore.Mvc;

namespace BulkeyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<OrderHeader> orders = _unitOfWork.orderHeader.GetAll(includeProperties: "ApplicationUser").ToList();

            return View(orders);
        }
    }
}
