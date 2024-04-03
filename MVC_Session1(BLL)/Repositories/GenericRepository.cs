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
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext) // ask CLR for Creating Object from "AppDbContext" 
        {
            // dbContext = new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>);
            _dbContext = dbContext;
        }

        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        public T Get(int id)
        {
            /// var department = _dbContext.Departments.Local.Where(D => D.Id == id).FirstOrDefault();
            /// if(department == null)
            ///      department = _dbContext.Departments.Where(D => D.Id == id).FirstOrDefault();

            return _dbContext.Set<T>().Find(id);
        }

        public virtual async Task <IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
                return  (IEnumerable<T>)await _dbContext.Employees.Include(E => E.Department).AsNoTracking().ToListAsync();
            else
                return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
