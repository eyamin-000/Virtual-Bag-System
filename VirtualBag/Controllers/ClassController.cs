using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace VirtualBag.Controllers
{
    public class ClassController : Controller
    {
        ClassService classService;

        public ClassController(ClassService classService)
        {
            this.classService = classService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            return View(new ClassDTO());
        }

        [HttpPost]
        public IActionResult Create(ClassDTO classDTO)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                var res = classService.Create(classDTO);

                if (res == true)
                {
                    TempData["Msg"] = "Class Created Successfully";
                    return RedirectToAction("Index");
                }
            }

            return View(classDTO);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = classService.Get(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(ClassDTO classDTO)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                var res = classService.Update(classDTO);

                if (res == true)
                {
                    TempData["Msg"] = "Class Updated Successfully";
                    return RedirectToAction("Index");
                }
            }

            return View(classDTO);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = classService.Get(id);
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
                    classService.Delete(id);
                    TempData["Msg"] = "Class Deleted Successfully";
                }
                catch
                {
                    TempData["Msg"] = "Cannot delete this class because related data exists.";
                }
            }

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = classService.Get();
            return View(data);
        }
    }
}
