using Microsoft.EntityFrameworkCore;
using MVC_Session1_BLL_.Interfaces;
using MVC_Session1_DAL_.Data;
using MVC_Session1_DAL_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Session1_BLL_.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext) // ask CLR for Creating Object from "AppDbContext" 
        {
            // dbContext = new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>);
            _dbContext = dbContext;
        }

        public int Add(Employee entity)
        {
            _dbContext.Employees.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(Employee entity)
        {
            _dbContext.Employees.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public Employee Get(int id)
        {
            /// var department = _dbContext.Departments.Local.Where(D => D.Id == id).FirstOrDefault();
            /// if(department == null)
            ///      department = _dbContext.Departments.Where(D => D.Id == id).FirstOrDefault();

            return _dbContext.Employees.Find(id);
        }

        public IEnumerable<Employee> GetAll()
            => _dbContext.Employees.AsNoTracking().ToList();


        public int Update(Employee entity)
        {
            _dbContext.Employees.Update(entity);
            return _dbContext.SaveChanges();
        }


    }
}
