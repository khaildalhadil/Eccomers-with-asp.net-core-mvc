using BulkeyBook.DataAccess.Repository.IRepository;
using BulkeyBook.Model;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkeyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> companys = _unitOfWork.company.GetAll(null).ToList();
            return View(companys);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.company.Add(company);
                _unitOfWork.Save();
                TempData["success"] = "The Company Added Successfuly";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int id) {

            Company company = _unitOfWork.company.Get(id.ToString());
            return View(company);
        }

        [HttpPost]
        public IActionResult Edit(Company company) {

            if (ModelState.IsValid)
            {
                _unitOfWork.company.update(company);
                _unitOfWork.Save();
                TempData["success"] = "The Company Update Successfuly";
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult Delete(int id) {

            Company company = _unitOfWork.company.Get(id.ToString());
            return View(company);
        }

        [HttpPost]
        public IActionResult Delete(Company company) {

            _unitOfWork.company.Delete(company);
            _unitOfWork.Save();
            TempData["success"] = "The Company Delete Successfuly";
            return RedirectToAction("Index");
        }
    }
}
