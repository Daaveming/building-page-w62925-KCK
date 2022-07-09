using System.ComponentModel.DataAnnotations;

namespace BuildPage.Models
{
    public class UploadImageModel : ResponseModel
    {
        [Required(ErrorMessage = "Proszę o wybranie zdjęcia")]
        public IFormFile File { get; set; }
    }
}
