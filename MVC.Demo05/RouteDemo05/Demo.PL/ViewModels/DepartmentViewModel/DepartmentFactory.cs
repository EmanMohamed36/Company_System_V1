using Demo.BLL.DTOs.DepartmentDTOs;

namespace Demo.PL.ViewModels.DepartmentViewModel
{
    static class DepartmentFactory
    {
        public static DepartmentViewModel FromDetailsDTOsTOEditViewModel
        (this DepartmentDetailsDTO departmentDTO)
        {
            return new DepartmentViewModel()
            {
                Code = departmentDTO.Code,
                Name = departmentDTO.Name,
                Description = departmentDTO.Description,
                CreatedOn = departmentDTO.DateOfCreation
            };

        }
        public static UpdatedDepartmentDTO FromEditViewModelToUpdateDTO
       (this DepartmentViewModel departmentDTO)
        {
            return new UpdatedDepartmentDTO()
            {
                Code = departmentDTO.Code,
                Name = departmentDTO.Name,
                Description = departmentDTO.Description,
                CreatedOn = departmentDTO.CreatedOn
            };

        }

        public static CreatedDepartmentDTO FromViewModelToCreatedDTO
      (this DepartmentViewModel departmentView)
        {
            return new CreatedDepartmentDTO()
            {
                Code = departmentView.Code,
                Name = departmentView.Name,
                Description = departmentView.Description,
                CreatedOn = departmentView.CreatedOn
            };

        }
}   }   
