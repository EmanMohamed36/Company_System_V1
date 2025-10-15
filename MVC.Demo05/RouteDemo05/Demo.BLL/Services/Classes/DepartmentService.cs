using Demo.BLL.DTOs.DepartmentDTOs;
using Demo.BLL.Factories;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Classes
{
    public class DepartmentServices(IDepartmentRepository _DepartmentRepository) : IDepartmentServices
    // interface injection with primary constructor
    {
        // Get All Departments
        //Data transfere object DTO

        public IEnumerable<DepartmentDTO> GetAllDepartment()
        {
            
            var departments = _DepartmentRepository.GetAll().Select(d => d.ToDepartmentDTO()); //Extension Method
            return departments;
            #region IEnumerable
            //var res = _DepartmentRepository.GetIEnumerable()
            //                               .Where(d => d.IsDeleted == false)
            //                               .Select(d => new DepartmentDTO
            //                               {
            //                                   DepId = d.Id,
            //                                   Name = d.Name,
            //  
            // });
            #endregion

            #region IQueryable
            //var res = _DepartmentRepository.GetIQueryable()
            //                            .Where(d => d.IsDeleted == false)
            //                            .Select(d => new DepartmentDTO
            //                            {
            //                                DepId = d.Id,
            //                                Name = d.Name,
            //                            });
            //return res.ToList(); 
            #endregion
        }

        //Get department by id 
        // Manual Mapping
        //Constructor Mapping
        //Extension Method

        public DepartmentDetailsDTO? GetDepartmentById(int id)
        {
            var department = _DepartmentRepository.GetById(id);

            //return department is null ? null : new DepartmentDetailsDTO()
            //    {
            //        Id = department.Id,
            //        Name = department.Name,
            //        Code = department.Code,
            //        Description = department.Description,
            //        CreatedBy = department.CreatedBy,
            //        LastModifiedBy = department.LastModifiedBy,
            //        IsDeleted = department.IsDeleted,
            //        DateOfCreation = DateOnly.FromDateTime(department.CreatedOn),
            //        LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn)

            //};
            //  return department is null ? null : new DepartmentDetailsDTO(department); // Constructor Mapping
            return department is null ? null : department.ToDepartmentDetailsDTO(); // Constructor Mapping


        }

        //Add Department

        public int AddDepartment(CreatedDepartmentDTO departmentDTO)
        {

            return _DepartmentRepository.Add(departmentDTO.ToEntity());
        }

        //Update Department

        public int UpdateDepartment(UpdatedDepartmentDTO departmentDTO)
        {
            return _DepartmentRepository.Update(departmentDTO.ToEntity());
        }


        public bool DeleteDepartment(int id)
        {
            var department = _DepartmentRepository.GetById(id);
            if (department is not null)
            {
                department.IsDeleted = true;

                return _DepartmentRepository.Update(department) > 0 ? true : false;
            }
            else return false;
        }


    }
}
