using BulkeyBook.DataAccess;
using BulkeyBook.DataAccess.Repository.IRepository;
using BulkeyBook.Model;
using Microsoft.AspNetCore.Mvc;
using Category = BulkeyBook.Model.Category;

namespace BulkeyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private const string TEMP_SUCCESS = "success";
        private const string TEMP_DELETE = "delete";
        private const string TEMP_UPDATE = "update";

        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }

        public IActionResult Index()
        {
            List<Category> Categorys = _categoryRepo.GetAll().ToList();
            return View(Categorys);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category) {

            if(ModelState.IsValid)
            {
                _categoryRepo.Add(category);
                _categoryRepo.Save();
            }

            return RedirectPermanent("/Category");
        }

        public ActionResult Edit(int id)
        {
            Category category = _categoryRepo.Get(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            //_db.Categorys.Update()

            return Redirect("/Category");
        }

        public ActionResult Delete(int id)
        {
            //Category category = _categoryRepo.Get(c => c.Id == id);
            Category category = _categoryRepo.Get(id);
            _categoryRepo.Delete(category);
            _categoryRepo.Save();
            return Redirect("/Category");
        }

    }
}
