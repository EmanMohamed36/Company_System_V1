using Demo.BLL.DTOs.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Interfaces
{
    public interface IEmployeeService
    {
        int AddEmployee(CreatedEmployeeDTO employeeDTO);
        bool DeleteEmployee(int id);
        
        IEnumerable<EmployeeDTO> GetAllEmployee(string? EmployeeSearchName, bool withTrack = false);
        EmployeeDetailsDTO? GetEmployeeById(int id);
        int UpdateEmployee(UpdatedEmployeeDTO employeeDTO);
    }
}
