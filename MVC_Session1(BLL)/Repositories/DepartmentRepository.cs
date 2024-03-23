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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) //Ask CLR To Create object from ApplicationDbContext
            : base(dbContext) 
        {

        }

    }
}
