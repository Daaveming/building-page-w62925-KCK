using System.ComponentModel.DataAnnotations.Schema;

namespace BuildPage.Models
{
    public class ResponseModel
    {
        [NotMapped]
        public string? Message { get; set; }
        [NotMapped]
        public bool IsSuccess { get; set; }
        [NotMapped]
        public bool IsResponse { get; set; }
    }
}
