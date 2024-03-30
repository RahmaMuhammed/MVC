using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_Session1_BLL_.Interfaces;
using MVC_Session1_BLL_.Repositories;
using MVC_Session1_DAL_.Models;
using MVC_Session1_PL_.ViewModels;
using System;
using System.Drawing;
using System.Linq;

namespace MVC_Session1_PL_.Controllers
{
    // Inhertince : EmployeeController is a Controller
    // Association :EmployeeController has a EmployeeRepository
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _env;
        //  private readonly IDepartmentRepository _departmentRepo;

        public EmployeeController(IMapper mapper,IEmployeeRepository employeeRepository, IWebHostEnvironment env /* , IDepartmentRepository departmentRepo*/)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _env = env;
            //  _departmentRepo = departmentRepo;
        }
        public IActionResult Index(string searchInp)
        {
            // Binding Through Views Dictionary : Transfare Data From Action to View [On Way]

            TempData.Keep();

            // 1.ViewData
            ViewData["Message"] = "Hello View Data";

            // 2.ViewBag
            ViewBag.Message = "Hello View Bag";

            var employee = Enumerable.Empty<Employee>();

            if (searchInp is null)
            {
                employee = _employeeRepository.GetAll();
            }
            else
            {
                employee = _employeeRepository.SearchByName(searchInp.ToLower());
            }
            return View(employee);
        }
        public IActionResult Create()
        {
            // ViewData["Departments"] = _departmentRepo.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid) // server side validation
            {
                /// Manual Mapping
                ///var mappedEmp = new Employee()
                ///{
                ///    Name = employee.Name,
                ///    Age = employee.Age,
                ///    Address = employee.Address,
                ///    Salary = employee.Salary,
                ///    Email = employee.Email,
                ///    PhoneNumber = employee.PhoneNumber,
                ///    IsActive = employee.IsActive,
                ///    HiringDate = employee.HiringDate
                ///};
                
              

                var mappEmp = _mapper.Map<EmployeeViewModel , Employee>(employee);

                //    var mappedEmp = (Employee)employee;
                var count = _employeeRepository.Add(mappEmp);
                // 3.TempData
                if (count > 0)
                    TempData["Message"] = "Department is Created Successful";
                else
                    TempData["Message"] = "Department is not Created";



                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        // Employee/Details/10
        // Employee/Details
        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest(); // 400

            var employee = _employeeRepository.Get(id.Value);
            if (employee == null)
                return NotFound(); // 404

            return View(employee);

        }

        // Employee/Edit/10
        // Employee/Edit
        // [HttpGet]
        public IActionResult Edit(int? id)
        {

            // ViewData["Departments"] = _departmentRepo.GetAll();
            if (id == null)
            {
                return BadRequest(); //400
            }
            var employee = _employeeRepository.Get(id.Value);
            if (employee == null)
            {
                return NotFound(); // 404
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest(); //400
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepository.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employee);
        }

        // Employee/Delete/10
        // Employee/Delete
        // [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest(); //400
            }
            var employee = _employeeRepository.Get(id.Value);
            if (employee == null)
            {
                return NotFound(); // 404
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee == null)
            {
                return NotFound();
            }
            try
            {
                _employeeRepository.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // log Exceptions
                // Handle the exception
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An error occurred while deleting the department.");
                return View(employee);
            }
        }
    }
}
