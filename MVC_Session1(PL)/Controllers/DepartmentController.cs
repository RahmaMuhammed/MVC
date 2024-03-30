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
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest(); // 400

            var department = _departmentRepositery.Get(id.Value);
            if (department == null)
                return NotFound(); // 404

            return View(department);

        }


        // Department/Edit/10
        // Department/Edit
        // [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest(); // 400
            }

            var department = _departmentRepositery.Get(id.Value);
            if (department == null)
            {
                return NotFound(); // 404
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _departmentRepositery.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest(); // 400

            var department = _departmentRepositery.Get(id.Value);
            if (department == null)
                return NotFound(); // 404

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = _departmentRepositery.Get(id);
            if (department == null)
                return NotFound();

            try
            {
                _departmentRepositery.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Handle the exception
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An error occurred while deleting the department.");
                return View(department);
            }
        }
    }
}
