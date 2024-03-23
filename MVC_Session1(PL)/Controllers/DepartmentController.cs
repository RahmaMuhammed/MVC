using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_Session1_BLL_.Interfaces;
using MVC_Session1_BLL_.Repositories;
using MVC_Session1_DAL_.Models;
using System;

namespace MVC_Session1_PL_.Controllers
{
    // Inhertince : DepartmentController is a Controller
    // Association : DepartmentController has a DepartmentRepository
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepositery;

        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepository departmentRepository, IWebHostEnvironment env)
        {
            _departmentRepositery = departmentRepository;
            _env = env;
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
        public IActionResult Details(int? Id, string ViewName = "Details")
        {
            if (Id is null)
                return BadRequest(); // 400

            var department = _departmentRepositery.Get(Id.Value);
            if (department == null)
                return NotFound(); // 404

            return View(department);

        }


        // Department/Edit/10
        // Department/Edit
        // [HttpGet]
        public IActionResult Edit(int? Id)
        {
            return Details(Id, "Edit");
            /// if (Id is null)
            ///     return BadRequest(); // 400
            ///
            /// var department = _departmentRepositery.Get(Id.Value);
            /// if (department == null)
            ///     return NotFound(); // 404
            ///
            /// return View(department);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int Id,Department department)
        {
            if (ModelState.IsValid)
                return View(department);

            try
            {
                _departmentRepositery.Update(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. log Exeption
                // 2. Friendly Message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Occured During Update Department");
                return View(department); 
            }
        }
    }
}
