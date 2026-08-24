using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace VirtualBag.Controllers
{
    public class HomeworkController : Controller
    {
        HomeworkService homeworkService;
        ClassService classService;
        SubjectService subjectService;
        UserService userService;
        TeacherAssignmentService teacherAssignmentService;
        NotificationService notificationService;

        public HomeworkController(
            HomeworkService homeworkService,
            ClassService classService,
            SubjectService subjectService,
            UserService userService,
            TeacherAssignmentService teacherAssignmentService,
            NotificationService notificationService)
        {
            this.homeworkService = homeworkService;
            this.classService = classService;
            this.subjectService = subjectService;
            this.userService = userService;
            this.teacherAssignmentService = teacherAssignmentService;
            this.notificationService = notificationService;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "Teacher" && role != "Student")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = homeworkService.Get();

            if (role == "Teacher")
            {
                var teacherId = HttpContext.Session.GetInt32("UserId");
                data = data.Where(h => h.TeacherId == teacherId).ToList();
            }

            if (role == "Student")
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var student = userService.Get(userId ?? 0);

                if (student != null)
                {
                    data = data.Where(h => h.ClassId == student.ClassId).ToList();
                }
                else
                {
                    data = new List<HomeworkDTO>();
                }
            }

            foreach (var item in data)
            {
                if (item.Deadline < DateTime.Now)
                {
                    item.DeadlineStatus = "Overdue";
                }
                else
                {
                    item.DeadlineStatus = "Pending";
                }
            }

            ViewBag.Classes = classService.Get();
            ViewBag.Subjects = subjectService.Get();
            ViewBag.Teachers = userService.Get();

            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            var teacherId = HttpContext.Session.GetInt32("UserId") ?? 0;

            var assignments = teacherAssignmentService.Get().Where(a => a.TeacherId == teacherId).ToList();

            var classIds = assignments.Select(a => a.ClassId).ToList();
            var subjectIds = assignments.Select(a => a.SubjectId).ToList();

            ViewBag.Classes = classService.Get().Where(c => classIds.Contains(c.ClassId)).ToList();

            ViewBag.Subjects = subjectService.Get().Where(s => subjectIds.Contains(s.SubjectId)).ToList();

            var dto = new HomeworkDTO();
            dto.TeacherId = teacherId;

            return View(dto);
        }

        [HttpPost]
        public IActionResult Create(HomeworkDTO homeworkDTO)
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            var teacherId = HttpContext.Session.GetInt32("UserId") ?? 0;
            homeworkDTO.TeacherId = teacherId;

            var isAssigned = teacherAssignmentService.Get() .Any(a => a.TeacherId == teacherId && a.ClassId == homeworkDTO.ClassId && a.SubjectId == homeworkDTO.SubjectId);

            if (!isAssigned)
            {
                ModelState.AddModelError("", "You are not assigned to this class and subject.");
            }

            if (ModelState.IsValid)
            {
                var res = homeworkService.Create(homeworkDTO);

                if (res == true)
                {
                    var students = userService.Get().Where(u => u.Role == "Student" && u.ClassId == homeworkDTO.ClassId).ToList();

                    foreach (var student in students)
                    {
                        var notification = new NotificationDTO()
                        {
                            UserId = student.UserId,
                            Message = "New homework added: " + homeworkDTO.Title,
                            IsRead = false,
                            CreatedAt = DateTime.Now
                        };

                        notificationService.Create(notification);
                    }

                    TempData["Msg"] = "Homework Created Successfully";
                    return RedirectToAction("Index");
                }
            }

            var assignments = teacherAssignmentService.Get().Where(a => a.TeacherId == teacherId).ToList();

            var classIds = assignments.Select(a => a.ClassId).ToList();
            var subjectIds = assignments.Select(a => a.SubjectId).ToList();

            ViewBag.Classes = classService.Get().Where(c => classIds.Contains(c.ClassId)).ToList();

            ViewBag.Subjects = subjectService.Get().Where(s => subjectIds.Contains(s.SubjectId)).ToList();

            return View(homeworkDTO);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = homeworkService.Get(id);

            var teacherId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (data == null || data.TeacherId != teacherId)
            {
                return RedirectToAction("Index");
            }

            var assignments = teacherAssignmentService.Get().Where(a => a.TeacherId == teacherId).ToList();

            var classIds = assignments.Select(a => a.ClassId).ToList();
            var subjectIds = assignments.Select(a => a.SubjectId).ToList();

            ViewBag.Classes = classService.Get().Where(c => classIds.Contains(c.ClassId)).ToList();

            ViewBag.Subjects = subjectService.Get().Where(s => subjectIds.Contains(s.SubjectId)).ToList();

            return View(data);
        }

        [HttpPost]
        public IActionResult Update(HomeworkDTO homeworkDTO)
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            var teacherId = HttpContext.Session.GetInt32("UserId") ?? 0;
            homeworkDTO.TeacherId = teacherId;

            var isAssigned = teacherAssignmentService.Get().Any(a => a.TeacherId == teacherId && a.ClassId == homeworkDTO.ClassId && a.SubjectId == homeworkDTO.SubjectId);

            if (!isAssigned)
            {
                ModelState.AddModelError("", "You are not assigned to this class and subject.");
            }

            if (ModelState.IsValid)
            {
                var res = homeworkService.Update(homeworkDTO);

                if (res == true)
                {
                    TempData["Msg"] = "Homework Updated Successfully";
                    return RedirectToAction("Index");
                }
            }

            var assignments = teacherAssignmentService.Get().Where(a => a.TeacherId == teacherId).ToList();

            var classIds = assignments.Select(a => a.ClassId).ToList();
            var subjectIds = assignments.Select(a => a.SubjectId).ToList();

            ViewBag.Classes = classService.Get().Where(c => classIds.Contains(c.ClassId)).ToList();

            ViewBag.Subjects = subjectService.Get().Where(s => subjectIds.Contains(s.SubjectId)).ToList();

            return View(homeworkDTO);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = homeworkService.Get(id);

            var teacherId = HttpContext.Session.GetInt32("UserId");
            if (data == null || data.TeacherId != teacherId)
            {
                return RedirectToAction("Index");
            }

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
                try
                {
                    homeworkService.Delete(id);
                    TempData["Msg"] = "Homework Deleted Successfully";
                }
                catch
                {
                    TempData["Msg"] = "Cannot delete this homework because related submissions exist.";
                }
            }

            return RedirectToAction("Index");
        }
    }
}