using AutoMapper;
using Demo.BLL.DTOs.EmployeeDTOs;
using Demo.DAL.Models.EmployeeModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Mapping_Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() 
        {
             //CreateMap<Employee,EmployeeDTO>(); //from Employee to EmployeeDTO
             //CreateMap< EmployeeDTO , Employee>(); // from EmployeeDTO to Employee
            CreateMap<Employee,EmployeeDTO>()
                .ForMember(dest => dest.EmpGender ,Options => Options.MapFrom(src =>src.Gender))
                .ForMember(dest => dest.EmpType , Options => Options.MapFrom(src => src.EmployeeType))                    
                .ReverseMap(); // from both direction
            CreateMap<Employee, EmployeeDetailsDTO>()
                 .ForMember(dest => dest.Gender, Options => Options.MapFrom(src => src.Gender))
                 .ForMember(dest => dest.EmployeeType, Options => Options.MapFrom(src => src.EmployeeType))
                 .ForMember(dest => dest.HiringDate , Options => Options.MapFrom(src =>DateOnly.FromDateTime( src.HiringDate)))
                 .ReverseMap();

            CreateMap<CreatedEmployeeDTO, Employee>()
                  .ForMember(dest => dest.HiringDate, Options => Options.MapFrom(src => src.HiringDate.ToDateTime(new TimeOnly())))
                  .ReverseMap();
                    
            CreateMap<UpdatedEmployeeDTO, Employee>().ForMember(dest => dest.HiringDate, Options => Options.MapFrom(src => src.HiringDate.ToDateTime(new TimeOnly())))
                  .ReverseMap();
            
        }
    }
}
