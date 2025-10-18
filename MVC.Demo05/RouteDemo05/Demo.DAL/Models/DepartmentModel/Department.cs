using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models.DepartmentModel
{
    public class Department:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        // Navigation Property - One Department has many Employees
        //public ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}
