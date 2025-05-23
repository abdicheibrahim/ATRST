using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Models;

namespace ProjetAtrst.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        public UserController(UserManager <ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EditProfile()
        {
            return View();
        }
        public IActionResult Education()
        {
            return View();
        }
        public IActionResult Employments()
        {
            return View();
        }
        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View("Register", model);
        //    ApplicationUser user = new ApplicationUser()
        //    {
        //        Email = model.Email
        //    };
        //    try
        //    {
                
        //        var logiinResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
        //        if (logiinResult.Succeeded)
        //        {
        //            RedirectToAction("Index", "Home");
        //        }
             
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return View(new LoginViewModel());
        //}
        //public IActionResult Logout() { 
        // return View();
        //}
        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View(new LoginViewModel());
        //}
        //[HttpPost]
        //public async Task<IActionResult> Register(LoginViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View("Register", model);
        //    ApplicationUser user = new ApplicationUser()
        //    {
        //        Email = model.Email
        //    };
        //    try
        //    {
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //     if (result.Succeeded)
        //        {
        //            var logiinResult=await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
        //            if (logiinResult.Succeeded) 
        //            {
        //                RedirectToAction("Index", "Home");
        //            }
        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //    return View(new LoginViewModel());
        //}
    }
}
