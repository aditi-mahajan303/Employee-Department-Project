
using EmpDeptProject.DataAccess.Data;
using EmpDeptProject.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmpDeptProject1.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmpDeptDBContext _db;

        public HomeController(EmpDeptDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var employees = _db.Employees;
            return View(employees);
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}