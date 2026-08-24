using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace VirtualBag.Controllers
{
    public class DashboardController : Controller
    {
        TeacherAssignmentService teacherAssignmentService;
        ClassService classService;
        SubjectService subjectService;
        UserService userService;
        HomeworkService homeworkService;
        AttendanceService attendanceService;

        public DashboardController(
            TeacherAssignmentService teacherAssignmentService,
            ClassService classService,
            SubjectService subjectService,
            UserService userService,
            HomeworkService homeworkService,
            AttendanceService attendanceService)
        {
            this.teacherAssignmentService = teacherAssignmentService;
            this.classService = classService;
            this.subjectService = subjectService;
            this.userService = userService;
            this.homeworkService = homeworkService;
            this.attendanceService = attendanceService;
        }

        public IActionResult Admin()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        public IActionResult Teacher()
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            var teacherId = HttpContext.Session.GetInt32("UserId") ?? 0;

            var assignments = teacherAssignmentService.Get().Where(a => a.TeacherId == teacherId).ToList();

            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Assignments = assignments;
            ViewBag.Classes = classService.Get();
            ViewBag.Subjects = subjectService.Get();
            ViewBag.TotalAssignments = assignments.Count;

            return View();
        }

        public IActionResult Student()
        {
            if (HttpContext.Session.GetString("Role") != "Student")
            {
                return RedirectToAction("Login", "Auth");
            }

            var studentId = HttpContext.Session.GetInt32("UserId") ?? 0;
            var student = userService.Get(studentId);

            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Student = student;

            var className = "Not Assigned";

            if (student != null && student.ClassId != null)
            {
                var cls = classService.Get(student.ClassId.Value);

                if (cls != null)
                {
                    className = cls.ClassName;
                }
            }

            ViewBag.ClassName = className;

            var homeworkList = homeworkService.Get();

            if (student != null && student.ClassId != null)
            {
                homeworkList = homeworkList.Where(h => h.ClassId == student.ClassId).ToList();
            }
            else
            {
                homeworkList = new List<HomeworkDTO>();
            }

            ViewBag.TotalHomework = homeworkList.Count;
            ViewBag.PendingHomework = homeworkList.Where(h => h.Deadline >= DateTime.Now).Count();

            var attendanceList = attendanceService.Get().Where(a => a.StudentId == studentId).ToList();

            ViewBag.PresentCount = attendanceList.Where(a => a.Status == "Present").Count();

            ViewBag.AbsentCount = attendanceList.Where(a => a.Status == "Absent").Count();

            int totalAttendance = attendanceList.Count;
            double attendancePercentage = 0;

            if (totalAttendance > 0)
            {
                attendancePercentage = (ViewBag.PresentCount * 100.0) / totalAttendance;
            }

            ViewBag.AttendancePercentage = attendancePercentage.ToString("0.00");

            return View();
        }
    }
}