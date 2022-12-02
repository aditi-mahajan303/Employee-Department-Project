using EmpDeptProject.DataAccess.Data;
using EmpDeptProject.DataAccess.Repository;
using EmpDeptProject.DataAccess.Repository.IRepository;
using EmpDeptProject.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmpDeptProject1.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmpDeptDBContext _db;

        public EmployeesController(IUnitOfWork unitOfWork, EmpDeptDBContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }

        public IActionResult Index()
        {
            var allemployees = _db.Employees.Include(c => c.Departments);
            var employees = _unitOfWork.Employee.GetAll();
            
            return View(allemployees);
        }

        //Delete - Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _unitOfWork.Employee.GetFirstOrDefault(c => c.EmpId == id);

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
            var obj = _unitOfWork.Employee.GetFirstOrDefault(c => c.EmpId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Employee.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Employee details deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ViewBag.DeptID = new SelectList(_unitOfWork.Department.GetAll(), "DeptId", "DeptName");

            var EmployeeFromDb = _unitOfWork.Employee.GetFirstOrDefault(c => c.EmpId == id);
            
            if (EmployeeFromDb == null)
            {
                return NotFound();
            }
            return View(EmployeeFromDb);
        }

        //Edit- Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public IActionResult EditP(Employee catObj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Employee.Update(catObj);
                _unitOfWork.Save();
                ViewBag.DeptId = new SelectList(_unitOfWork.Department.GetAll(), "DeptId", "DeptName");
                TempData["Success"] = "Employee details updated successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.DeptId = new SelectList(_unitOfWork.Department.GetAll(), "DeptId", "DeptName");
            return View(catObj);

        }

        //  [HttpGet]
        public IActionResult Create()
        {
            ViewBag.DeptID = new SelectList(_unitOfWork.Department.GetAll(), "DeptId", "DeptName");
            
            return View();
        }

        //Create- Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee catObj)
        {
   
            if (ModelState.IsValid)
            {
                _unitOfWork.Employee.Add(catObj);
               
                _unitOfWork.Save();
                ViewBag.DeptId = new SelectList(_unitOfWork.Department.GetAll(), "DeptId", "DeptName");
                TempData["Success"] = "Employee details added successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.DeptId = new SelectList(_unitOfWork.Department.GetAll(), "DeptId", "DeptName");

                return View(catObj);
            }


        }

    }
}

