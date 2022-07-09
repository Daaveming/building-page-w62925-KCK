using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildPage.Models
{
    [Table("calendar")]
    public class CalendarModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
