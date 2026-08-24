using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace VirtualBag.Controllers
{
    public class AttendanceController : Controller
    {
        AttendanceService attendanceService;
        AttendanceSessionService attendanceSessionService;
        UserService userService;

        public AttendanceController(AttendanceService attendanceService,
            AttendanceSessionService attendanceSessionService,
            UserService userService)
        {
            this.attendanceService = attendanceService;
            this.attendanceSessionService = attendanceSessionService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = attendanceService.Get();
            return View(data);
        }

        [HttpGet]
        public IActionResult Mark(int sessionId)
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            var session = attendanceSessionService.Get(sessionId);

            var students = userService.Get().Where(u => u.Role == "Student" && u.ClassId == session.ClassId).ToList();

            var existingAttendance = attendanceService.Get().Where(a => a.SessionId == sessionId).ToList();

            ViewBag.Session = session;
            ViewBag.Students = students;
            ViewBag.ExistingAttendance = existingAttendance;

            return View();
        }

        [HttpPost]
        public IActionResult Mark(int sessionId, List<int> StudentIds, List<string> Remarks)
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            for (int i = 0; i < StudentIds.Count; i++)
            {
                string status = Request.Form["Statuses[" + i + "]"];

                if (string.IsNullOrEmpty(status))
                {
                    continue;
                }

                string remark = "";

                if (Remarks != null && Remarks.Count > i)
                {
                    remark = Remarks[i];
                }

                var oldData = attendanceService.Get().FirstOrDefault(a => a.SessionId == sessionId && a.StudentId == StudentIds[i]);

                if (oldData == null)
                {
                    var attendanceDTO = new AttendanceDTO()
                    {
                        SessionId = sessionId,
                        StudentId = StudentIds[i],
                        Status = status,
                        Remarks = remark
                    };

                    attendanceService.Create(attendanceDTO);
                }
                else
                {
                    oldData.Status = status;
                    oldData.Remarks = remark;

                    attendanceService.Update(oldData);
                }
            }

            TempData["Msg"] = "Attendance Marked Successfully";
            return RedirectToAction("Index", "AttendanceSession");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = attendanceService.Get(id);
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
                attendanceService.Delete(id);
            }

            return RedirectToAction("Index");
        }
    }
}
