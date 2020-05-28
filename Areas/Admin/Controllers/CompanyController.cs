using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Company company = new Company();
            if (id == null)
            {
                return View(company);
            }

            company = _unitOfWork.Company.Get(id.GetValueOrDefault());
            if (company == null)
                return NotFound();

            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(company);

        }
        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            var resultObj = _unitOfWork.Company.GetAll();
            return Json(new { data = resultObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDB = _unitOfWork.Company.Get(id);
            if (objFromDB == null)
            {
                return Json(new { success = false, message = "Error occured while deleting company!" });
            }
            else
            {
                _unitOfWork.Company.Remove(objFromDB);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Company deletion successful!" });
            }
        }
        #endregion
    }
}
