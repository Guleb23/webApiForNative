using Microsoft.AspNetCore.Http;

namespace webApiFoeNative.Models
{
    public class DoctorModelDTO
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int Stash { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public IFormFile ProfilePhoto { get; set; } // Фото профиля
    }
}
