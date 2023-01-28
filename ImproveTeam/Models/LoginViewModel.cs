using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ImproveTeam.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Логин не может быть пустым")]
        [DisplayName("Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Пароль не может быть пустым")]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
