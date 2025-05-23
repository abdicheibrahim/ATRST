using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModel.Identity;

namespace ProjetAtrst.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user=await _userManager.FindByEmailAsync(model.Email);
                if(user==null)
                {
                    ModelState.AddModelError(string.Empty, "Username or Password is wrong");
                    return View(model);
                }
                var vaildPassword= await _userManager.CheckPasswordAsync(user,model.Password);
                if(vaildPassword==false)
                {
                    ModelState.AddModelError(string.Empty, "Username or Password is wrong");
                    return View(model);
                }
                 var result= await _signInManager.PasswordSignInAsync(user,model.Password,model.RememberMe,false);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var user=await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        Email=model.Email,
                        UserName=model.Email
                    };
                    var result=await _userManager.CreateAsync(newUser,model.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(newUser, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty,error.Description);
                    }
                    return View(model);
                }
                ModelState.AddModelError(string.Empty, "User Email Is Exist");
                return View(model);
               
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
