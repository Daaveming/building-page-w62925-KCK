using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildPage.Models
{
    [Table("images")]
    public class ImageModel
    {
        [Key]
        public int Id { get; set; }
        public string File { get; set; }

    }
}
