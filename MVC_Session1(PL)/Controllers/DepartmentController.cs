using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_Session1_BLL_.Interfaces;
using MVC_Session1_BLL_.Repositories;
using MVC_Session1_DAL_.Models;
using MVC_Session1_PL_.ViewModels;
using System;
using System.Collections.Generic;

namespace MVC_Session1_PL_.Controllers
{
    // Inhertince : DepartmentController is a Controller
    // Association : DepartmentController has a DepartmentRepository
    public class DepartmentController : Controller
    {
        //  private readonly IDepartmentRepository _departmentRepositery;

        private readonly IWebHostEnvironment _env;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper,
            /*IDepartmentRepository departmentRepo,*/ IWebHostEnvironment env)
        //Ask CLR for creating an object from "IDepartmentRepository"
        {
            // _departmentRepositery = departmentRepository;
            _unitOfWork = unitOfWork;
            _env = env;
        }
        public IActionResult Index()
        {
            // var department = _departmentRepositery.GetAll();
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            var mappedDep = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDep);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel department)
        {
            if (ModelState.IsValid) // server side validation
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(department);
                // var count = _departmentRepositery.Add(department);
                _unitOfWork.DepartmentRepository.Add(mappedDep);
                var count = _unitOfWork.Complete();
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
            var department = _unitOfWork.DepartmentRepository.Get(id.Value);
            // var department = _departmentRepositery.Get(id.Value);
            if (department == null)
                return NotFound(); // 404

            return View(department);

        }


        // Department/Edit/10
        // Department/Edit
        // [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(departmentVM);
            try
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                //  _unitOfWork.Repository<Department>().Update(mappedDep);
                _unitOfWork.DepartmentRepository.Update(mappedDep);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                // 2. Friendly Message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Ocuured during Updating Department");
                return View(departmentVM);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DepartmentViewModel departmentVM)
        {
            try
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                //  _unitOfWork.Repository<Department>().Delete(mappedDep);
                _unitOfWork.DepartmentRepository.Delete(mappedDep);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                // 2. Friendly Message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Ocuured during Deleting Department");
                return View(departmentVM);
            }
        }
    }
}
