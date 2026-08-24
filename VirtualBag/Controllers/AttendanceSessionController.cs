using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace VirtualBag.Controllers
{
    public class AttendanceSessionController : Controller
    {
        AttendanceSessionService attendanceSessionService;
        ClassService classService;
        SubjectService subjectService;
        UserService userService;
        TeacherAssignmentService teacherAssignmentService;

        public AttendanceSessionController(
            AttendanceSessionService attendanceSessionService,
            ClassService classService,
            SubjectService subjectService,
            UserService userService,
            TeacherAssignmentService teacherAssignmentService)
        {
            this.attendanceSessionService = attendanceSessionService;
            this.classService = classService;
            this.subjectService = subjectService;
            this.userService = userService;
            this.teacherAssignmentService = teacherAssignmentService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            var teacherId = HttpContext.Session.GetInt32("UserId");

            var data = attendanceSessionService.Get().Where(a => a.TeacherId == teacherId).OrderByDescending(a => a.AttendanceDate).ToList();

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

            var dto = new AttendanceSessionDTO();
            dto.TeacherId = teacherId;

            return View(dto);
        }

        [HttpPost]
        public IActionResult Create(AttendanceSessionDTO attendanceSessionDTO)
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            var teacherId = HttpContext.Session.GetInt32("UserId") ?? 0;
            attendanceSessionDTO.TeacherId = teacherId;

            var isAssigned = teacherAssignmentService.Get().Any(a => a.TeacherId == teacherId && a.ClassId == attendanceSessionDTO.ClassId && a.SubjectId == attendanceSessionDTO.SubjectId);

            if (!isAssigned)
            {
                ModelState.AddModelError("", "You are not assigned to this class and subject.");
            }

            if (ModelState.IsValid)
            {
                var res = attendanceSessionService.Create(attendanceSessionDTO);

                if (res == true)
                {
                    TempData["Msg"] = "Attendance Session Created Successfully";
                    return RedirectToAction("Index");
                }
            }

            var assignments = teacherAssignmentService.Get().Where(a => a.TeacherId == teacherId).ToList();

            var classIds = assignments.Select(a => a.ClassId).ToList();
            var subjectIds = assignments.Select(a => a.SubjectId).ToList();

            ViewBag.Classes = classService.Get().Where(c => classIds.Contains(c.ClassId)).ToList();

            ViewBag.Subjects = subjectService.Get().Where(s => subjectIds.Contains(s.SubjectId)).ToList();

            return View(attendanceSessionDTO);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = attendanceSessionService.Get(id);

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
        public IActionResult Update(AttendanceSessionDTO attendanceSessionDTO)
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            var teacherId = HttpContext.Session.GetInt32("UserId") ?? 0;
            attendanceSessionDTO.TeacherId = teacherId;

            var isAssigned = teacherAssignmentService.Get().Any(a => a.TeacherId == teacherId && a.ClassId == attendanceSessionDTO.ClassId && a.SubjectId == attendanceSessionDTO.SubjectId);

            if (!isAssigned)
            {
                ModelState.AddModelError("", "You are not assigned to this class and subject.");
            }

            if (ModelState.IsValid)
            {
                var res = attendanceSessionService.Update(attendanceSessionDTO);

                if (res == true)
                {
                    TempData["Msg"] = "Attendance Session Updated Successfully";
                    return RedirectToAction("Index");
                }
            }

            var assignments = teacherAssignmentService.Get().Where(a => a.TeacherId == teacherId).ToList();

            var classIds = assignments.Select(a => a.ClassId).ToList();
            var subjectIds = assignments.Select(a => a.SubjectId).ToList();

            ViewBag.Classes = classService.Get().Where(c => classIds.Contains(c.ClassId)).ToList();

            ViewBag.Subjects = subjectService.Get().Where(s => subjectIds.Contains(s.SubjectId)).ToList();

            return View(attendanceSessionDTO);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = attendanceSessionService.Get(id);

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
                    attendanceSessionService.Delete(id);
                    TempData["Msg"] = "Attendance Session Deleted Successfully";
                }
                catch
                {
                    TempData["Msg"] = "Cannot delete this session because attendance records exist.";
                }
            }

            return RedirectToAction("Index");
        }
    }
}