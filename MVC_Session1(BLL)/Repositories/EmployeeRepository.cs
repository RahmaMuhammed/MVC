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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        // private  readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext) //Ask CLR To Create object from ApplicationDbContext
            : base(dbContext)
        {
            // _dbContext = dbContext;
        }
        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _dbContext.Employees.Where(E => E.Address.ToLower() == address.ToLower());
        }

        public IQueryable<Employee> SearchByName(string name)
        => _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name));
    }
}
