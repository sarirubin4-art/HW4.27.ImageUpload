using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4._27.ImageUpload.Data
{
    public class Image
    {
       public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public DateTime DateSubmitted { get; set; }
        public List<int> LikedByIds { get; set; }
    }
}
