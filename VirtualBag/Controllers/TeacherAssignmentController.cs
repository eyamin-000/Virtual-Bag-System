using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace VirtualBag.Controllers
{
    public class TeacherAssignmentController : Controller
    {
        TeacherAssignmentService teacherAssignmentService;
        UserService userService;
        ClassService classService;
        SubjectService subjectService;

        public TeacherAssignmentController(TeacherAssignmentService teacherAssignmentService, UserService userService, ClassService classService, SubjectService subjectService)
        {
            this.teacherAssignmentService = teacherAssignmentService;
            this.userService = userService;
            this.classService = classService;
            this.subjectService = subjectService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = teacherAssignmentService.Get();

            ViewBag.Teachers = userService.Get();
            ViewBag.Classes = classService.Get();
            ViewBag.Subjects = subjectService.Get();

            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.Teachers = userService.Get().Where(u => u.Role == "Teacher").ToList();
            ViewBag.Classes = classService.Get();
            ViewBag.Subjects = subjectService.Get();

            return View(new TeacherAssignmentDTO());
        }

        [HttpPost]
        public IActionResult Create(TeacherAssignmentDTO teacherAssignmentDTO)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                var res = teacherAssignmentService.Create(teacherAssignmentDTO);

                if (res == true)
                {
                    TempData["Msg"] = "Teacher Assigned Successfully";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Teachers = userService.Get().Where(u => u.Role == "Teacher").ToList();
            ViewBag.Classes = classService.Get();
            ViewBag.Subjects = subjectService.Get();

            return View(teacherAssignmentDTO);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = teacherAssignmentService.Get(id);

            ViewBag.Teachers = userService.Get().Where(u => u.Role == "Teacher").ToList();
            ViewBag.Classes = classService.Get();
            ViewBag.Subjects = subjectService.Get();

            return View(data);
        }

        [HttpPost]
        public IActionResult Update(TeacherAssignmentDTO teacherAssignmentDTO)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                var res = teacherAssignmentService.Update(teacherAssignmentDTO);

                if (res == true)
                {
                    TempData["Msg"] = "Teacher Assignment Updated Successfully";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Teachers = userService.Get().Where(u => u.Role == "Teacher").ToList();
            ViewBag.Classes = classService.Get();
            ViewBag.Subjects = subjectService.Get();

            return View(teacherAssignmentDTO);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = teacherAssignmentService.Get(id);
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
                    teacherAssignmentService.Delete(id);
                    TempData["Msg"] = "Teacher Assignment Deleted Successfully";
                }
                catch
                {
                    TempData["Msg"] = "Cannot delete this assignment because related data exists.";
                }
            }

            return RedirectToAction("Index");
        }
    }
}
