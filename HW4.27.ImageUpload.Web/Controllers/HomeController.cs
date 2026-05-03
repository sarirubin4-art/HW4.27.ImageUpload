using HW4._27.ImageUpload.Data;
using HW4._27.ImageUpload.Web.Extensions;
using HW4._27.ImageUpload.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace HW4._27.ImageUpload.Web.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public void AddLike(int imageId)
        {
            var repo = new ImageRepository(_configuration.GetConnectionString("ConStr"));

            repo.AddLike(imageId);

            var likedIds = HttpContext.Session.Get<List<int>>("LikedImageIds") ?? new List<int>();
            if (!likedIds.Contains(imageId))
            {
                likedIds.Add(imageId);
                HttpContext.Session.Set("LikedImageIds", likedIds);
            }
        }

        [HttpPost]
        public IActionResult GetLikedImageIds()
        {
            var likedIds = HttpContext.Session.Get<List<int>>("LikedImageIds") ?? new List<int>();
            return Json(likedIds);
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

        public IActionResult GetLikesCount(int imageId)
        {
            var repo = new ImageRepository(_configuration.GetConnectionString("ConStr"));
            return Json(repo.GetLikesCount(imageId));            
        }

        public IActionResult GetAllImages()
        {
            var repo = new ImageRepository(_configuration.GetConnectionString("ConStr"));
            return Json(repo.GetAll());

        }

        public IActionResult ImagePage(int id)
        {
            var repo = new ImageRepository(_configuration.GetConnectionString("ConStr"));
            Data.Image image = repo.GetById(id);
            if (image != null)
            {
                return View(new ImageViewModel { Image = image });
            }
            else
            {
                return Redirect("/home/index");
            }
        }

        [HttpPost]
        public IActionResult Submit(string title, IFormFile image)
        {

            //_webHostEnvironment.WebRootPath; - this is the full path to the wwwroot folder

            string fileName = $"{image.FileName}-{Guid.NewGuid()}";
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

            using FileStream fs = new FileStream(fullPath, FileMode.Create);
            image.CopyTo(fs);
            var repo = new ImageRepository(_configuration.GetConnectionString("ConStr"));
            repo.UploadImage(new Data.Image
            {
                Title = title,
                ImagePath = fileName,
                DateSubmitted = DateTime.Now
            });
            return RedirectToAction("Index");

        }
    }
}
