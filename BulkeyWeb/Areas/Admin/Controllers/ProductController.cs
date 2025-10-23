using BulkeyBook.DataAccess.Repository.IRepository;
using BulkeyBook.Model;
using Microsoft.AspNetCore.Mvc;

namespace BulkeyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _context;
        public ProductController(IProductRepository context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Product> allProduct = _context.GetAll().ToList();
            return View(allProduct);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (product == null || !ModelState.IsValid)
            {
                return RedirectToAction("Index");
            } 
            _context.Add(product);
            _context.Save();

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            Product product = _context.Get(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            _context.Update(product);
            _context.Save();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id) {
            Product product = _context.Get(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product) {
            if(product == null)
            {
                return NotFound();
            }
            _context.Delete(product);
            _context.Save();
            return RedirectToAction("Index");
        }

    }
}
