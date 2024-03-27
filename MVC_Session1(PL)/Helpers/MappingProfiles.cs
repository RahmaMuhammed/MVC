using AutoMapper;
using MVC_Session1_DAL_.Models;
using MVC_Session1_PL_.ViewModels;

namespace MVC_Session1_PL_.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
          //  CreateMap<DepartmentViewModel, Department>().ReverseMap();

        }
    }
}
