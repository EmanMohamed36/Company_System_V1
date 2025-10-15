using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.DepartmentViewModel
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        [Display(Name = "Creation Date")]
        public DateOnly CreatedOn { get; set; }

        public int CreatedBy { get; set; }
    }
}
