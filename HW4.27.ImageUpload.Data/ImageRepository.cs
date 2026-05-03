using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4._27.ImageUpload.Data
{
    public class ImageRepository
    {
        private readonly string _connectionString;
        public ImageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Image GetById(int id)
        {
            using var ctx = new ImageDataContext(_connectionString);
            
            return ctx.Images.FirstOrDefault(i => i.Id == id);
        }
        public List<Image> GetAll()
        {
            using var ctx = new ImageDataContext(_connectionString);
            return ctx.Images.ToList();
        }

        public void UploadImage(Image image)
        {
            using var ctx = new ImageDataContext(_connectionString);
            
            ctx.Images.Add(image);
            ctx.SaveChanges();
        }

        public int GetLikesCount(int imageId)
        {
            using var ctx = new ImageDataContext(_connectionString);
            if (GetById(imageId) != null)
            {
                return ctx.Images.FirstOrDefault(i => i.Id == imageId).Likes;
            }
            else
            {
                return 0;
            }
        }

        public void AddLike(int imageId)
        {
            using var ctx = new ImageDataContext(_connectionString);
            Image image = ctx.Images.FirstOrDefault(i => i.Id == imageId);
            if (image != null)
            {
                image.Likes++;
            }
            ctx.SaveChanges();
        }
    }
}
