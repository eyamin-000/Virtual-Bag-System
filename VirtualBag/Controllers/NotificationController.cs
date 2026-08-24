using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace VirtualBag.Controllers
{
    public class NotificationController : Controller
    {
        NotificationService notificationService;

        public NotificationController(NotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var userId = HttpContext.Session.GetInt32("UserId") ?? 0;

            var data = notificationService.Get().Where(n => n.UserId == userId).OrderByDescending(n => n.CreatedAt).ToList();

            return View(data);
        }

        public IActionResult MarkAsRead(int id)
        {
            var notification = notificationService.Get(id);

            if (notification != null)
            {
                notification.IsRead = true;
                notificationService.Update(notification);
            }

            return RedirectToAction("Index");
        }
    }
}
