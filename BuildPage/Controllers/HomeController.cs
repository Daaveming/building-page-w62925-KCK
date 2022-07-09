using BuildPage.Interfaces;
using BuildPage.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace BuildPage.Controllers
{

    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImages _image;
        private readonly IUser _user;
        private readonly ICalendar _calendar;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DateTime date;

        public HomeController(
            IWebHostEnvironment webHostEnvironment, 
            IImages image, IUser user,
            IHttpContextAccessor httpContextAccessor, 
            ICalendar calendar)
        {
            _webHostEnvironment = webHostEnvironment;
            _image = image;
            _user = user;
            _httpContextAccessor = httpContextAccessor;


            ViewBag.LoginName = _httpContextAccessor.HttpContext?.Session.GetString("user");
            _calendar = calendar;
        }

        public IActionResult Index()
        {
            ViewBag.LoginName = _httpContextAccessor.HttpContext?.Session.GetString("user");
            return View();
        }
        public IActionResult Calculators()
        {
            ViewBag.LoginName = _httpContextAccessor.HttpContext?.Session.GetString("user");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            ViewBag.LoginName = _httpContextAccessor.HttpContext?.Session.GetString("user");
            return View();
        }

        /// <summary>
        /// CALENDAR
        /// </summary>
        /// <returns></returns>
        public IActionResult Calendar()
        {
            var calendar = _calendar.GetAll();
            ViewBag.LoginName = _httpContextAccessor.HttpContext?.Session.GetString("user");

            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            var month = DateTime.Now.Month;
            var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month).ToUpper();

            ViewBag.Calendar = daysInMonth;
            ViewBag.Month = monthName;

            if(daysInMonth % 7 == 0)
            {
                ViewBag.Rows = daysInMonth / 7;
            }
            else
            {
                ViewBag.Rows = (daysInMonth / 7) + 1;
            }

            return View(calendar);
        }
        public IActionResult AddToCalendarPage(int id)
        {
            CalendarModel model = new CalendarModel();

            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, id);

            model.Date = date;

            return View(model);
        }
        [HttpPost]
        public IActionResult AddToCalendarPage(CalendarModel model)
        {
            DateTime date = new DateTime(model.Date.Year, model.Date.Month, model.Id, model.Date.Hour, model.Date.Minute, 0);
            model.Date = date;
            model.Id = 0;
            var e = _calendar.AddItem(model);
            if (!e) return View(model.Id);
            return RedirectToAction("Calendar", "Home");
        }

        public IActionResult DetailsCalendarPage(int id)
        {
            return View(_calendar.ItemDetails(id));
        }
    }
}