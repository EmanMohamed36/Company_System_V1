using Demo.BLL.DTOs.DepartmentDTOs;
using Demo.DAL.Models.DepartmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Factories
{
    public static class DepartmentFactory
    {
        public static DepartmentDTO ToDepartmentDTO(this Department department)
        {
            return new DepartmentDTO()
            {
                DepId = department.Id,
                Name = department.Name,
                Description = department.Description,
                Code = department.Code,
                DateOfCreation = DateOnly.FromDateTime(department.CreatedOn)
            };
        }

        public static DepartmentDetailsDTO ToDepartmentDetailsDTO(this Department department)
        {
            return new DepartmentDetailsDTO()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedBy = department.CreatedBy,
                LastModifiedBy = department.LastModifiedBy,
                IsDeleted = department.IsDeleted,
                DateOfCreation = DateOnly.FromDateTime(department.CreatedOn),
                LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn)
               

            };
        }
        public static Department ToEntity(this CreatedDepartmentDTO departmentDTO)
        {
            return new Department()
            {
                Name = departmentDTO.Name,
                Code = departmentDTO.Code,
                Description = departmentDTO.Description,
                CreatedOn = departmentDTO.CreatedOn.ToDateTime(new TimeOnly())
            };

        }

        public static Department ToEntity(this UpdatedDepartmentDTO departmentDTO)
        {
            return new Department()
            {
                Id = departmentDTO.Id,
                Name = departmentDTO.Name,
                Code = departmentDTO.Code,
                Description = departmentDTO.Description,
                CreatedOn = departmentDTO.CreatedOn.ToDateTime(new TimeOnly())
            };

        }
    }
}
