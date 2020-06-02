using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BulkyBook.Data;
using BulkyBook.Models;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Utility;
using Dapper;
using Microsoft.AspNetCore.Authorization;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null)
            {
                return View(coverType);
            }
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.ProcCoverTypeGetType, parameter);
            if (coverType == null)
                return NotFound();

            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                var parameter = new DynamicParameters();
                parameter.Add("Name", coverType.Name);
                if (coverType.Id == 0)
                {
                    _unitOfWork.SP_Call.Execute(SD.ProcCoverTypeCreate, parameter);
                }
                else
                {
                    parameter.Add("@Id", coverType.Id);
                    _unitOfWork.SP_Call.Execute(SD.ProcCoverTypeUpdate, parameter);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);

        }
        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            var resultObj = _unitOfWork.SP_Call.List<CoverType>(SD.ProcCoverTypeGetTypes);
            return Json(new { data = resultObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var objFromDB = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.ProcCoverTypeGetType, parameter);
            if (objFromDB == null)
            {
                return Json(new { success = false, message = "Error occured while deleting the cover type!" });
            }
            else
            {
                _unitOfWork.SP_Call.Execute(SD.ProcCoverTypeDelete, parameter);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Cover type deletion successful!" });
            }
        }
        #endregion
    }
}
