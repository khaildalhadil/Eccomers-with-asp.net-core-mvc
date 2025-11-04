using System.Text.RegularExpressions;
using BulkeyBook.DataAccess.Repository.IRepository;
using BulkeyBook.Model;
using Microsoft.AspNetCore.Mvc;

namespace BulkeyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        private readonly IUnitOfWork _untiOfWord;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _untiOfWord = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> products = _untiOfWord.product.GetAll(includeProperties: "Category").ToList();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            Product product = _untiOfWord.product.Get(id, includeProperties: "Category");
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
