using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildPage.Models
{
    [Table("users")]
    public class UserModel : ResponseModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Nie podałeś loginu")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Nie podałeś hasła")]
        public string Password { get; set; }
        
        public int Role { get; set; }
        
    }
}
