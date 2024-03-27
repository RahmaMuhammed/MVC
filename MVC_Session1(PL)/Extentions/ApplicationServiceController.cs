using Microsoft.Extensions.DependencyInjection;
using MVC_Session1_BLL_.Interfaces;
using MVC_Session1_BLL_.Repositories;

namespace MVC_Session1_PL_.Extentions
{
    public static class ApplicationServiceController
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            //    services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //   services.AddSingleton<IDepartmentRepository, DepartmentRepository>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        }
    }
}
