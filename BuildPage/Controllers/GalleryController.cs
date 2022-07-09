using Microsoft.AspNetCore.Mvc;
using BuildPage.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Http.Controllers;
using System.Net;
using BuildPage.Interfaces;

namespace BuildPage.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IHostEnvironment _hostingEnvironment;
        private readonly IImages _image;
        public GalleryController(IHostEnvironment environment, IImages image, IHttpContextAccessor httpContextAccessor)
        {
            _hostingEnvironment = environment;
            _image = image;
            _httpContextAccessor = httpContextAccessor;
            ViewBag.LoginName = _httpContextAccessor.HttpContext?.Session.GetString("user");
        }
        public IActionResult Gallery()
        {
            ViewBag.LoginName = _httpContextAccessor.HttpContext?.Session.GetString("user");
            return View(_image.GetAll());
        }
        public IActionResult UploadImage()
        {
            UploadImageModel model = new UploadImageModel();
            model.IsResponse = false;
            return View(model);
        }
        [HttpPost]
        public IActionResult UploadImage(UploadImageModel model)
        {
            
            if (ModelState.IsValid)
            {
                model.IsResponse = true;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                FileInfo fileInfo = new FileInfo(model.File.FileName);
                string fileName = Path.GetRandomFileName() + fileInfo.Extension;
                ImageModel img = new() { File = fileName };
                _image.AddImage(img);
                string fileNameWithPath = Path.Combine(path, fileName);
                Console.WriteLine(fileNameWithPath);
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    model.File.CopyTo(stream);

                model.IsSuccess = true;
                model.Message = "Pomyślnie dodano";
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteImage(int id)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            string fileNameWithPath = Path.Combine(path, _image.GetImage(id));
            FileInfo file = new FileInfo(fileNameWithPath);
            file.Delete();
            _image.RemoveImage(id);


            return RedirectToAction("Gallery","Gallery");
        }
    }
}
