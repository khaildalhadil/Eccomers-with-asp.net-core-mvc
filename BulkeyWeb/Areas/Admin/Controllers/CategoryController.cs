using BulkeyBook.DataAccess;
using BulkeyBook.DataAccess.Repository.IRepository;
using BulkeyBook.Model;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Category = BulkeyBook.Model.Category;

namespace BulkeyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private const string TEMP_SUCCESS = "success";
        private const string TEMP_DELETE = "delete";
        private const string TEMP_UPDATE = "update";

        private readonly IUnitOfWork unitOfWord;

        public CategoryController(IUnitOfWork db)
        {
            unitOfWord = db;
        }

        public IActionResult Index()
        {
            List<Category> Categorys = unitOfWord.category.GetAll(null).ToList();
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
                unitOfWord.category.Add(category);
                unitOfWord.Save();
                TempData["success"] = "The Category Added Successfuly";
            }

            return RedirectPermanent("/Category");
        }

        public ActionResult Edit(string id)
        {
            Category category = unitOfWord.category.Get(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            //_db.Categorys.Update()

            return Redirect("/Category");
        }

        public ActionResult Delete(string id)
        {
            //Category category = unitOfWord.Get(c => c.Id == id);
            Category category = unitOfWord.category.Get(id);
            unitOfWord.category.Delete(category);
            unitOfWord.Save();
            return Redirect("/Category");
        }

    }
}
