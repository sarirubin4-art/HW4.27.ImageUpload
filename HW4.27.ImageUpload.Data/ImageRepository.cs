using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4._27.ImageUpload.Data
{
    internal class ImageRepository
    {
        private readonly string _connectionString;
        public ImageRepository(string connectionString)
        {
            _connectionString = connectionString;
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

        public void AddLike(int userId, int imageId)
        {
            using var ctx = new ImageDataContext(_connectionString);
            Image image = ctx.Images.FirstOrDefault(i => i.Id == imageId);
            if (image != null)
            {
                image.LikedByIds.Add(userId);
            }
            ctx.SaveChanges();
        }
    }
}
