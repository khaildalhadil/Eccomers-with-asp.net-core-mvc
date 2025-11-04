using BulkeyBook.DataAccess.Repository.IRepository;
using BulkeyBook.Model;
using BulkeyBook.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkeyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        //private readonly IProductRepository _context;
        //private readonly ICategoryRepository _dbCategory;
        private readonly IUnitOfWork unitOfWork;

        // to access public file or wwwroot
        private readonly IWebHostEnvironment _webHostEnvironment;

        // before unitOfWork
        //public ProductController(IProductRepository context, ICategoryRepository dbCategory, IWebHostEnvironment webHostEnvironment)
        //{
        //    _context = context;
        //    _dbCategory = dbCategory;
        //    _webHostEnvironment = webHostEnvironment;
        //}

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> allProduct = unitOfWork.product.GetAll(includeProperties: "Category").ToList();
            return View(allProduct);
        }

        public IActionResult Upsert(int id)
        {
                // we need to convert from ICategoryList to SelectListItem 👇🏻
            IEnumerable<SelectListItem> CategoryList = unitOfWork.category.GetAll(null).Select(u =>
                new SelectListItem { Value = u.Id.ToString(), Text = u.Name, }
            );

            ProductVM ProductVM = new ProductVM { CategoryList = CategoryList, Product = new Product()};

            // insert | create
            if(id == null || id == 0) {
                
                return View(ProductVM);
            }
            else {
                ProductVM.Product = unitOfWork.product.Get(id);
                return View(ProductVM);
            }
         
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM ProductVM, IFormFile? file)
        {
            // get the path for wwwroot
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            //if (ProductVM.Product.Id != 0)
            //{
            //    Product product = _context.Get(ProductVM.Product.Id);
            //    ProductVM.Product.ImageUrl = product.ImageUrl;
            //}

            if (file != null)
            {
                // get new name for the image and add it the Extension
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                // path of product to save the image in it
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                if (!string.IsNullOrEmpty(ProductVM.Product.ImageUrl))
                {
                    // delete the old image we will get the name of the image 
                    var oldImagePath = Path.Combine(wwwRootPath, ProductVM.Product.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // save the image
                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                ProductVM.Product.ImageUrl = @"\images\product\" + fileName;
            }
            else
            {
                
                Console.WriteLine(ProductVM.Product.ImageUrl);
            }

            if (ProductVM.Product == null || !ModelState.IsValid)
            {


                //Console.WriteLine(ModelState.er);
                TempData["error"] = "something went wrong";
                return RedirectToAction("Index");
            }

            if (ProductVM.Product.Id == 0)
            {
                unitOfWork.product.Add(ProductVM.Product);
                TempData["success"] = "The Product Added Successfuly";
            } else
            {
                unitOfWork.product.Update(ProductVM.Product);
                TempData["success"] = "The Product Edited Successfuly";
            }
            unitOfWork.Save();
            return RedirectToAction("Index");
        }


        //[HttpPost]
        //public IActionResult Edit(Product product)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        TempData["error"] = "something went wrong";
        //        return NotFound();
        //    }


        //    return RedirectToAction("Index");
        //}

        public IActionResult Delete(int id)
        {
            Product product = unitOfWork.product.Get(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product) {
            if(product == null)
            {
                TempData["error"] = "something went wrong";
                return NotFound();
            }
            unitOfWork.product.Delete(product);
            unitOfWork.Save();
            TempData["success"] = "The Product Deleted Successfuly";
            return RedirectToAction("Index");
        }

        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> allProduct = unitOfWork.product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data= allProduct });

        }
        #endregion

    }
}
