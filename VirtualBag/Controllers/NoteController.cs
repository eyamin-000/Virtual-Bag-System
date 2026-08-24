using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace VirtualBag.Controllers
{
    public class NoteController : Controller
    {
        NoteService noteService;
        SubjectService subjectService;
        UserService userService;

        public NoteController(NoteService noteService, SubjectService subjectService, UserService userService)
        {
            this.noteService = noteService;
            this.subjectService = subjectService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Subjects = subjectService.Get();
            ViewBag.Students = userService.Get().Where(u => u.Role == "Student").ToList();

            return View(new NoteDTO());
        }

        [HttpPost]
        public IActionResult Create(NoteDTO noteDTO)
        {
            if (ModelState.IsValid)
            {
                noteDTO.LastUpdated = DateTime.Now;

                var res = noteService.Create(noteDTO);

                if (res == true)
                {
                    TempData["Msg"] = "Note Created Successfully";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Subjects = subjectService.Get();
            ViewBag.Students = userService.Get().Where(u => u.Role == "Student").ToList();

            return View(noteDTO);
        }

        public IActionResult Index()
        {
            var data = noteService.Get();
            return View(data);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var data = noteService.Get(id);

            ViewBag.Subjects = subjectService.Get();
            ViewBag.Students = userService.Get().Where(u => u.Role == "Student").ToList();

            return View(data);
        }

        [HttpPost]
        public IActionResult Update(NoteDTO noteDTO)
        {
            if (ModelState.IsValid)
            {
                noteDTO.LastUpdated = DateTime.Now;

                var res = noteService.Update(noteDTO);

                if (res == true)
                {
                    TempData["Msg"] = "Note Updated Successfully";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Subjects = subjectService.Get();
            ViewBag.Students = userService.Get().Where(u => u.Role == "Student").ToList();

            return View(noteDTO);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = noteService.Get(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(int id, string Decison)
        {
            if (Decison.Equals("Yes"))
            {
                noteService.Delete(id);
            }

            return RedirectToAction("Index");
        }
    }
}
