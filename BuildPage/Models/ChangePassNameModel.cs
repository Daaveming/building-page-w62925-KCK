using System.ComponentModel.DataAnnotations;

namespace BuildPage.Models
{
    public class ChangePassNameModel : ResponseModel
    {
        [Required(ErrorMessage = "Nie podałeś nowej nazwy")]
        public string Pass { get; set; }
        [Required(ErrorMessage = "Nie podałeś ponownie nowej nazwy")]
        public string PassRe { get; set; }
        [Required(ErrorMessage = "Nie podałeś nowego hasła")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Nie podałeś ponownie nowego hasła")]
        public string NameRe { get; set; }

    }
}
