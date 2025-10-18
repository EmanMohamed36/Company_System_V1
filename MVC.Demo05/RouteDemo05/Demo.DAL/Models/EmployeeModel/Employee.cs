using Demo.DAL.Models.DepartmentModel;
using Demo.DAL.Models.Shared;
using Demo.DAL.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models.EmployeeModel
{
    public class Employee : BaseEntity
    {
       
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public bool isActive { get; set; }
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

        public string? ImgName { get; set; }

        //// Foreign Key
        //[ForeignKey(nameof(Department))]
        //public int DepartmentId { get; set; }

        //// Navigation Property - Each Employee has one Department
        //public Department Department { get; set; } = null!;

    }
}
