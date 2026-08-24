using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace VirtualBag.Controllers
{
    public class HomeworkSubmissionController : Controller
    {
        HomeworkSubmissionService homeworkSubmissionService;
        HomeworkService homeworkService;
        UserService userService;

        public HomeworkSubmissionController(HomeworkSubmissionService homeworkSubmissionService, HomeworkService homeworkService, UserService userService)
        {
            this.homeworkSubmissionService = homeworkSubmissionService;
            this.homeworkService = homeworkService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "Student")
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.Homeworks = homeworkService.Get();

            var userId = HttpContext.Session.GetInt32("UserId");

            var dto = new HomeworkSubmissionDTO();
            dto.StudentId = userId ?? 0;

            return View(dto);
        }

        [HttpPost]
        public IActionResult Create(HomeworkSubmissionDTO homeworkSubmissionDTO)
        {
            if (HttpContext.Session.GetString("Role") != "Student")
            {
                return RedirectToAction("Login", "Auth");
            }

            homeworkSubmissionDTO.StudentId = HttpContext.Session.GetInt32("UserId") ?? 0;
            homeworkSubmissionDTO.SubmittedAt = DateTime.Now;
            homeworkSubmissionDTO.Status = "Submitted";

            if (ModelState.IsValid)
            {
                var res = homeworkSubmissionService.Create(homeworkSubmissionDTO);

                if (res == true)
                {
                    TempData["Msg"] = "Homework Submitted Successfully";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Homeworks = homeworkService.Get();
            return View(homeworkSubmissionDTO);
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "Teacher" && role != "Student")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = homeworkSubmissionService.Get();

            if (role == "Student")
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                data = data.Where(s => s.StudentId == userId).ToList();
            }

            return View(data);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = homeworkSubmissionService.Get(id);

            ViewBag.Homeworks = homeworkService.Get();
            ViewBag.Students = userService.Get().Where(u => u.Role == "Student").ToList();

            return View(data);
        }

        [HttpPost]
        public IActionResult Update(HomeworkSubmissionDTO homeworkSubmissionDTO)
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                var res = homeworkSubmissionService.Update(homeworkSubmissionDTO);

                if (res == true)
                {
                    TempData["Msg"] = "Submission Reviewed Successfully";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Homeworks = homeworkService.Get();
            ViewBag.Students = userService.Get().Where(u => u.Role == "Student").ToList();

            return View(homeworkSubmissionDTO);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = homeworkSubmissionService.Get(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(int id, string Decison)
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            if (Decison.Equals("Yes"))
            {
                homeworkSubmissionService.Delete(id);
            }

            return RedirectToAction("Index");
        }
    }
}
