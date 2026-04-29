using HW4._27.ImageUpload.Data;
using HW4._27.ImageUpload.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HW4._27.ImageUpload.Web.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;
        private object _webHostEnvironment;

        public HomeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Upload()
        {
            return View();
        }

        public IActionResult Index()
        {
            var repo = new ImageRepository(_configuration.GetConnectionString("ConStr"));
            HomePageViewModel vm = new HomePageViewModel { Images = repo.GetAll() };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Submit(string title, IFormFile image)
        {

            //_webHostEnvironment.WebRootPath; - this is the full path to the wwwroot folder

            string fileName = $"{Guid.NewGuid()}-{image.FileName}";
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

            using FileStream fs = new FileStream(fullPath, FileMode.Create);
            image.CopyTo(fs);
            var repo = new ImageRepository(_configuration.GetConnectionString("ConStr"));
            repo.UploadImage(new Image
            {
                Title = title,
                ImagePath = fileName,
                DateSubmitted = DateTime.Now
            });
            return RedirectToAction("Index");

        }
    }
}
