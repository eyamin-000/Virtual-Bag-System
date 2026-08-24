using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using VirtualBag.Helper;

namespace VirtualBag.Controllers
{
    public class UserController : Controller
    {
        UserService userService;
        ClassService classService;

        public UserController(UserService userService, ClassService classService)
        {
            this.userService = userService;
            this.classService = classService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = userService.Get();
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.Classes = classService.Get();
            return View(new UserDTO());
        }

        [HttpPost]
        public IActionResult Create(UserDTO userDTO)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            var existingUser = userService.Get().FirstOrDefault(u => u.Email == userDTO.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email already exists");
            }

            if (ModelState.IsValid)
            {
                userDTO.Password = PasswordHelper.ToMD5(userDTO.Password);

                var res = userService.Create(userDTO);

                if (res == true)
                {
                    TempData["Msg"] = "User Created Successfully";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Classes = classService.Get();
            return View(userDTO);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = userService.Get(id);
            ViewBag.Classes = classService.Get();
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(UserDTO userDTO)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                var res = userService.Update(userDTO);

                if (res == true)
                {
                    TempData["Msg"] = "User Updated Successfully";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Classes = classService.Get();
            return View(userDTO);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = userService.Get(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(int id, string Decison)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            if (Decison.Equals("Yes"))
            {
                try
                {
                    userService.Delete(id);
                    TempData["Msg"] = "User Deleted Successfully";
                }
                catch
                {
                    TempData["Msg"] = "Cannot delete this user because related data exists.";
                }
            }

            return RedirectToAction("Index");
        }
    }
}