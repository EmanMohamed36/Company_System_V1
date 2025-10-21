using Demo.DAL.Models.IdentityModel;
using Demo.PL.Utilities;
using Demo.PL.ViewModels.IdentityViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Demo.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager,
                                   SignInManager<ApplicationUser> _signInManager) : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)  return View(model);

            var userToAdd = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,

            };

            var res = _userManager.CreateAsync(userToAdd,model.Password).Result;
            if (res.Succeeded) return RedirectToAction(nameof(LogIn));
            else
            {
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View (model);
            }

        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }


        [HttpPost]
        public IActionResult LogIn(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user is not null)
            {
                var isValidPassword = _userManager.CheckPasswordAsync(user, model.Password).Result;
                if (isValidPassword)
                {
                    var res = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;
                    if (res.IsNotAllowed) ModelState.AddModelError(string.Empty, "Your Account is not allowed");
                    if(res.IsLockedOut) ModelState.AddModelError(string.Empty, "Your Account is locked");
                    if (res.Succeeded) return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                

            }
            else
            { 
                 ModelState.AddModelError(string.Empty, "Invalid LogIn");
               
            }
            return View(model);

        }
        [HttpGet]
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {

            return View();
        }
        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(model.Email).Result;
               
                
                if(user is not null )
                {
                    //create link
                    var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var resetPasswordLink = Url.Action("ResetPasswordLink","Account",new {email  = user.Email,token},Request.Scheme); 
                    //create email
                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = resetPasswordLink
                    };
                    //send email
                    var res = EmailSettings.SendEmail(email);
                    if (res) return RedirectToAction("CheckYourInbox");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return RedirectToAction(nameof(ForgetPassword), model);
            
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }
        public IActionResult ResetPasswordLink(string email,string token)
        {
            return View();
        }

    }
}
