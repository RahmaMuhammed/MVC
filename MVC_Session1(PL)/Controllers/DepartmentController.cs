using Microsoft.AspNetCore.Mvc;
using MVC_Session1_BLL_.Interfaces;
using MVC_Session1_BLL_.Repositories;
using MVC_Session1_DAL_.Models;

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
            var department = _departmentRepositery.GetAll();
            return View(department);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid) // server side validation
            {
                var count = _departmentRepositery.Add(department);

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }


        // Department/Details/10
        // Department/Details
        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if (Id is null)
              return BadRequest(); // 400

            var department = _departmentRepositery.Get(Id.Value);
            if(department == null)
                return NotFound(); // 404

            return View(department);

        }
    }
}
