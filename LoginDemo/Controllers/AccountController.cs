using LoginDemo.Models; // for User
using Microsoft.AspNetCore.Identity; // for SignInManager
using Microsoft.AspNetCore.Mvc; // for Controller class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LoginDemo.Controllers
{
    public class AccountController: Controller
    {
        private UserManager<User>   _userManager;
        private SignInManager<User> _signManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Username, string Password)
        {
            if (ModelState.IsValid)
            {
                var result = await _signManager.PasswordSignInAsync(Username,
                    Password, false, false);
                if (result.Succeeded)
                {
                    return View("LoginOK");
                }
            }

            return View();
        }
    }
}
