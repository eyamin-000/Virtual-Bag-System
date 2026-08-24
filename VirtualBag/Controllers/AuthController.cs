using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using VirtualBag.Helper;

namespace VirtualBag.Controllers
{
    public class AuthController : Controller
    {
        UserService userService;

        public AuthController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            var encryptedPassword = PasswordHelper.ToMD5(Password);

            var users = userService.Get();

            var user = users.FirstOrDefault(u => u.Email == Email && u.Password == encryptedPassword && u.Status == "Active");

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetString("Role", user.Role);

                if (user.Role == "Admin")
                {
                    return RedirectToAction("Admin", "Dashboard");
                }
                else if (user.Role == "Teacher")
                {
                    return RedirectToAction("Teacher", "Dashboard");
                }
                else if (user.Role == "Student")
                {
                    return RedirectToAction("Student", "Dashboard");
                }
            }

            TempData["Msg"] = "Invalid Email or Password";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}