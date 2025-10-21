using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.IdentityViewModel
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
