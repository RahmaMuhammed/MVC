using Microsoft.AspNetCore.Mvc;
using MVC_Session1_BLL_.Interfaces;
using MVC_Session1_BLL_.Repositories;

namespace MVC_Session1_PL_.Controllers
{
    // Inhertince : DepartmentController is a Controller
    // Association : DepartmentController has a DepartmentRepository
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepositery;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepositery = departmentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
