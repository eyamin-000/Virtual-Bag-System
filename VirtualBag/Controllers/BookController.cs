using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace VirtualBag.Controllers
{
    public class BookController : Controller
    {
        BookService bookService;
        ClassService classService;
        SubjectService subjectService;

        public BookController(BookService bookService, ClassService classService, SubjectService subjectService)
        {
            this.bookService = bookService;
            this.classService = classService;
            this.subjectService = subjectService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Classes = classService.Get();
            ViewBag.Subjects = subjectService.Get();
            return View(new BookDTO());
        }

        [HttpPost]
        public IActionResult Create(BookDTO bookDTO)
        {
            if (ModelState.IsValid)
            {
                bookDTO.UploadedBy = HttpContext.Session.GetInt32("UserId") ?? 1;

                var res = bookService.Create(bookDTO);
                if (res == true)
                {
                    TempData["Msg"] = "Book Created Successfully";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Classes = classService.Get();
            ViewBag.Subjects = subjectService.Get();
            return View(bookDTO);
        }

        public IActionResult Index(string search)
        {
            var data = bookService.Get();

            if (!string.IsNullOrEmpty(search)) //search book
            {
                data = data.Where(b => b.Title.Contains(search)).ToList();
            }

            return View(data);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var data = bookService.Get(id);
            ViewBag.Classes = classService.Get();
            ViewBag.Subjects = subjectService.Get();
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(BookDTO bookDTO)
        {
            if (ModelState.IsValid)
            {
                bookDTO.UploadedBy = HttpContext.Session.GetInt32("UserId") ?? 1;

                var res = bookService.Update(bookDTO);
                if (res == true)
                {
                    TempData["Msg"] = "Book Updated Successfully";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Classes = classService.Get();
            ViewBag.Subjects = subjectService.Get();
            return View(bookDTO);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = bookService.Get(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(int id, string Decison)
        {
            if (Decison.Equals("Yes"))
            {
                bookService.Delete(id);
            }

            return RedirectToAction("Index");
        }
    }
}
