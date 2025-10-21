using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.IdentityViewModel
{
    public class ForgetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Email Can't be Empty")]
        public string Email { get; set; }
    }
}
