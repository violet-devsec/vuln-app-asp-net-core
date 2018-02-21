using Microsoft.AspNetCore.Http;

namespace PicShopper.Web.Models
{
    public class UploadFile
    {
        public string Title { get; set; }
        public int Price { get; set; }
        public IFormFile Content { get; set; }
    }
}
