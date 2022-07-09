using BuildPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using BuildPage.Interfaces;

namespace BuildPage.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUser _user;
        public LoginController(IUser user, IHttpContextAccessor httpContextAccessor)
        {
            _user = user;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Login()
        {
            UserModel model = new UserModel();
            model.IsResponse = false;
            ViewBag.LoginName = _httpContextAccessor.HttpContext?.Session.GetString("user");
            ViewBag.LoginId = _httpContextAccessor.HttpContext?.Session.GetInt32("userid");
            return View(model);
        }
        [HttpPost]
        public IActionResult Login(UserModel model)
        {
            model.IsResponse = true;
            if (ModelState.IsValid)
            {
                if (_user.Login(model) != null)
                {
                    var user = _user.Login(model);

                    _httpContextAccessor.HttpContext?.Session.SetString("user", user.Name);
                    _httpContextAccessor.HttpContext?.Session.SetInt32("userid", user.Id);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    model.IsSuccess = false;
                    model.Message = "Błąd z logowaniem";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext?.Session.SetString("user", "");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Profil(ChangePassNameModel model)
        {
            model.IsResponse = false;
            model.NameRe = " ";

            ViewBag.LoginName = _httpContextAccessor.HttpContext?.Session.GetString("user");
            ViewBag.LoginId = _httpContextAccessor.HttpContext?.Session.GetInt32("userid");

            return View(model);
        }
        
        public IActionResult PassUpdate()
        {
            ChangePassNameModel model = new ChangePassNameModel();
            model.IsResponse = false;
            ViewBag.LoginName = _httpContextAccessor.HttpContext?.Session.GetString("user");
            ViewBag.LoginId = _httpContextAccessor.HttpContext?.Session.GetInt32("userid");
            return View(model);
        }
        
        [HttpPost]
        public IActionResult PassUpdate(ChangePassNameModel model)
        {
            model.IsResponse = true;
            ViewBag.LoginName = _httpContextAccessor.HttpContext?.Session.GetString("user");
            ViewBag.LoginId = _httpContextAccessor.HttpContext?.Session.GetInt32("userid");

            if (model.Pass == model.PassRe)
            {
                UserModel user = new UserModel { Id = ViewBag.LoginId, Name = ViewBag.LoginName, Password = model.Pass };
                _user.ChangePassword(user);
                model.IsSuccess = true;
                model.Message = "Zmieniono hasło";
                return View(model);

            }
            else
            {
                model.IsSuccess = false;
                model.Message = "Hasła nie są takie same";
                return View(model);
            }
        }
        public IActionResult NameUpdate()
        {
            ChangePassNameModel model = new ChangePassNameModel();
            model.IsResponse = false;
            ViewBag.LoginName = _httpContextAccessor.HttpContext?.Session.GetString("user");
            ViewBag.LoginId = _httpContextAccessor.HttpContext?.Session.GetInt32("userid");
            return View(model);
        }
        [HttpPost]
        public IActionResult NameUpdate(ChangePassNameModel model)
        {
            model.IsResponse = true;
            ViewBag.LoginId = _httpContextAccessor.HttpContext?.Session.GetInt32("userid");

            if (model.Name == model.NameRe)
            {
                UserModel user = new UserModel { Id = ViewBag.LoginId, Name = model.Name };
                _user.ChangeName(user);
                model.IsSuccess = true;
                model.Message = "Zmieniono nazwę";
                return View(model);
            }
            else
            {
                model.IsSuccess = false;
                model.Message = "Nazwy nie są takie same";
                return View(model);
            }

        }
    }
}
