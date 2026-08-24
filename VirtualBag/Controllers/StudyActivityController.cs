using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace VirtualBag.Controllers
{
    public class StudyActivityController : Controller
    {
        StudyActivityService studyActivityService;
        UserService userService;

        public StudyActivityController(StudyActivityService studyActivityService, UserService userService)
        {
            this.studyActivityService = studyActivityService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Students = userService.Get().Where(u => u.Role == "Student").ToList();

            return View(new StudyActivityDTO());
        }

        [HttpPost]
        public IActionResult Create(StudyActivityDTO studyActivityDTO)
        {
            if (ModelState.IsValid)
            {
                studyActivityDTO.ActivityDate = DateTime.Now;

                var res = studyActivityService.Create(studyActivityDTO);

                if (res == true)
                {
                    TempData["Msg"] = "Study Activity Created Successfully";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Students = userService.Get().Where(u => u.Role == "Student").ToList();

            return View(studyActivityDTO);
        }

        public IActionResult Index()
        {
            var data = studyActivityService.Get().OrderByDescending(s => s.ActivityDate).ToList();

            return View(data);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var data = studyActivityService.Get(id);

            ViewBag.Students = userService.Get().Where(u => u.Role == "Student").ToList();

            return View(data);
        }

        [HttpPost]
        public IActionResult Update(StudyActivityDTO studyActivityDTO)
        {
            if (ModelState.IsValid)
            {
                var res = studyActivityService.Update(studyActivityDTO);

                if (res == true)
                {
                    TempData["Msg"] = "Study Activity Updated Successfully";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Students = userService.Get().Where(u => u.Role == "Student").ToList();

            return View(studyActivityDTO);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = studyActivityService.Get(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(int id, string Decison)
        {
            if (Decison.Equals("Yes"))
            {
                studyActivityService.Delete(id);
            }

            return RedirectToAction("Index");
        }
    }
}
