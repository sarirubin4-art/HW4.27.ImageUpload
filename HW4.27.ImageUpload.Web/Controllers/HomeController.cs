using HW4._27.ImageUpload.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW4._27.ImageUpload.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string _connectionString;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
           var ctx = new ImageDataContext(_connectionString);
            HomePageViewModel vm = new HomePageViewModel { Images=};
            return View();
        }

        //[HttpPost]
        //public IActionResult Submit(string title, IFormFile image)
        //{

        //    //_webHostEnvironment.WebRootPath; - this is the full path to the wwwroot folder

        //    string fileName = $"{Guid.NewGuid()}-{image.FileName}";
        //    var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

        //    using FileStream fs = new FileStream(fullPath, FileMode.Create);
        //    image.CopyTo(fs);
        //    var repo = new ImageRepository(_connectionString);
        //    repo.Add(new Image
        //    {
        //        Title = title,
        //        ImagePath = fileName
        //    });
        //    return RedirectToAction("Index");

        //}
    }
}
