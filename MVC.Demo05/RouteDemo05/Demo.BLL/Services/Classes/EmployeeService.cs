using AutoMapper;
using Demo.BLL.DTOs.EmployeeDTOs;
using Demo.BLL.Services.AttachmentService;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Repositories.Interfaces;


namespace Demo.BLL.Services.Classes
{
    public class EmployeeService(IUnitOfWork _UnitOfWork,
                                 IAttachmentService _AttachmentService,
                                IMapper _mapper): IEmployeeService
    {
        public IEnumerable<EmployeeDTO> GetAllEmployee(string? EmployeeSearchName, bool withTrack = false)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(EmployeeSearchName))
            {
                employees = _UnitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                 employees = _UnitOfWork.EmployeeRepository.GetAll(e => e.Name.ToLower().Contains(EmployeeSearchName.ToLower()));

            }

            var employeesDTO = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>(employees);
            return employeesDTO;
    
            //     var res = _employeeRepository.GetAll(e => new EmployeeDTO
            //{
            //    Id = e.Id,
            //    Name = e.Name,
            //    Age = e.Age
            //});
            //return res;
        }
       public  EmployeeDetailsDTO? GetEmployeeById(int id)
        { 
            var employee = _UnitOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return null;
            var employeeDTO = _mapper.Map<Employee , EmployeeDetailsDTO>(employee);
            return employeeDTO;
        }
        public int AddEmployee(CreatedEmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<CreatedEmployeeDTO , Employee>(employeeDTO);
            if(employeeDTO.Image is not null)
            employee.ImgName = _AttachmentService.Upload(employeeDTO.Image, "Images");
            
            _UnitOfWork.EmployeeRepository.Add(employee);
            return _UnitOfWork.SaveChanges();
        }
        public int UpdateEmployee(UpdatedEmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<UpdatedEmployeeDTO, Employee>(employeeDTO);
             _UnitOfWork.EmployeeRepository.Update(employee);
            return _UnitOfWork.SaveChanges();
        }
        public bool DeleteEmployee(int id)
        {
            var employee = _UnitOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return false;
            else
            { 
                employee.IsDeleted = true;
                _UnitOfWork.EmployeeRepository.Update(employee);
                return _UnitOfWork.SaveChanges() > 0 ? true:false;
            }
        }

       
    }
}
