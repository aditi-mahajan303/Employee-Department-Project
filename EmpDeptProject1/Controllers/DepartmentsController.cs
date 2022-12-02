using EmpDeptProject.DataAccess.Data;
using EmpDeptProject.DataAccess.Repository;
using EmpDeptProject.DataAccess.Repository.IRepository;
using EmpDeptProject.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmpDeptProject1.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {   
            var departments = _unitOfWork.Department.GetAll();
            return View(departments);
        }

        //Delete - Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _unitOfWork.Department.GetFirstOrDefault(c=>c.DeptId==id);
            
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Department.GetFirstOrDefault(c => c.DeptId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Department.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Department deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? deptId)
        {
            if (deptId == null || deptId == 0)
            {
                return NotFound();
            }

            var DepartmentFirstOrDefault = _unitOfWork.Department.GetFirstOrDefault(c => c.DeptId == deptId);

            if (DepartmentFirstOrDefault == null)
            {
                return NotFound();
            }
            return View(DepartmentFirstOrDefault);
        }

        //Edit- Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public IActionResult EditP(Department catObj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Department.Update(catObj);
                _unitOfWork.Save();
                TempData["Success"] = "Department updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(catObj);

        }

        //  [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Create- Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department catObj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Department.Add(catObj);
                _unitOfWork.Save();
                TempData["Success"] = "Department added successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(catObj);
            }


        }

    }
}
