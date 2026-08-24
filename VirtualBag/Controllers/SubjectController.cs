using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace VirtualBag.Controllers
{
    public class SubjectController : Controller
    {
        SubjectService subjectService;
        ClassService classService;

        public SubjectController(SubjectService subjectService, ClassService classService)
        {
            this.subjectService = subjectService;
            this.classService = classService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = subjectService.Get();
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
            return View(new SubjectDTO());
        }

        [HttpPost]
        public IActionResult Create(SubjectDTO subjectDTO)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                var res = subjectService.Create(subjectDTO);
                if (res == true)
                {
                    TempData["Msg"] = "Subject Created Successfully";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Classes = classService.Get();
            return View(subjectDTO);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = subjectService.Get(id);
            ViewBag.Classes = classService.Get();
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(SubjectDTO subjectDTO)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                var res = subjectService.Update(subjectDTO);
                if (res == true)
                {
                    TempData["Msg"] = "Subject Updated Successfully";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Classes = classService.Get();
            return View(subjectDTO);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = subjectService.Get(id);
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
                    subjectService.Delete(id);
                    TempData["Msg"] = "Subject Deleted Successfully";
                }
                catch
                {
                    TempData["Msg"] = "Cannot delete this subject because related data exists.";
                }
            }

            return RedirectToAction("Index");
        }
    }
}
