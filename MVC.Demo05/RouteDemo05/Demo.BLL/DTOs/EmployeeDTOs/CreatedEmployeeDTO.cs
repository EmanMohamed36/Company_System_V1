using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Models.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTOs.EmployeeDTOs
{
    public class CreatedEmployeeDTO
    {
        [Required (ErrorMessage = "Name Can't be Null")]
        [MaxLength(50, ErrorMessage = "Max length should be 50 character")]
        [MinLength(5, ErrorMessage = "Min length should be 5 characters")]
        public string Name { get; set; } = null!;
        [Range(22, 30)]
        public int? Age { get; set; }
        [RegularExpression("^[1-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$",
           ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        [Range(0,double.MaxValue , ErrorMessage ="Salary Can't be Nagative")]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        [Required(ErrorMessage ="Gender Reguired")]
        public Gender? Gender { get; set; }
        [Required(ErrorMessage = "Employee Type Reguired")]
        public EmployeeType? EmployeeType { get; set; }
        public IFormFile? Image { get; set; }
    }
}
