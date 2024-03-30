using MVC_Session1_BLL_.Interfaces;
using MVC_Session1_BLL_.Repositories;
using MVC_Session1_DAL_.Data;
using MVC_Session1_DAL_.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Session1_BLL_
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private Hashtable _repositories; // to do Depentancy For Object Of IGenericRepository
        public UnitOfWork(ApplicationDbContext dbContext) //Ask CLR to create object from "DbContext" 
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }

        public IGenericRepository<T> Repository<T>() where T : ModelBase
        {
            var key = typeof(T).Name;  //Employee

            if (!_repositories.ContainsKey(key))
            {
                if (key == nameof(Employee))
                {
                    var repository = new EmployeeRepository(_dbContext);
                    _repositories.Add(key, repository);
                }
                else
                {
                    var repository = new GenericRepository<T>(_dbContext);
                    _repositories.Add(key, repository);
                }
            }

            return _repositories[key] as IGenericRepository<T>;
        }
        public int Complete()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}
