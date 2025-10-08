using AutoMapper;
using Demo.BLL.DTOs.EmployeeDTOs;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Repositories.Interfaces;


namespace Demo.BLL.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository,
                                IMapper _mapper): IEmployeeService
    {
        public IEnumerable<EmployeeDTO> GetAllEmployee(bool withTrack = false)
        {
            var employees = _employeeRepository.GetAll();
            var employeesDTO = _mapper.Map<IEnumerable<Employee> , IEnumerable<EmployeeDTO>>(employees);    

            return employeesDTO;
        }
       public  EmployeeDetailsDTO? GetEmployeeById(int id)
        { 
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return null;
            var employeeDTO = _mapper.Map<Employee , EmployeeDetailsDTO>(employee);
            return employeeDTO;
        }
        public int AddEmployee(CreatedEmployeeDTO employeeDTO)
        { 
            var employee = _mapper.Map<CreatedEmployeeDTO , Employee>(employeeDTO);
            return _employeeRepository.Add(employee);
        }
        public int UpdateEmployee(UpdatedEmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<UpdatedEmployeeDTO, Employee>(employeeDTO);
            return _employeeRepository.Update(employee);
        }
        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            { 
                employee.IsDeleted = true;
                return _employeeRepository.Update(employee) > 0 ? true:false;
            }
        }

       
    }
}
