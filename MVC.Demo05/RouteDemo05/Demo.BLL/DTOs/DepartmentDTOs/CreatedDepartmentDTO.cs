using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTOs.DepartmentDTOs
{
    public class CreatedDepartmentDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        [Display(Name = "Creation Date")]
        public DateOnly CreatedOn { get; set; }
    }
}
//update delete edit select of departments 

