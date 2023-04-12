using System.ComponentModel.DataAnnotations;

namespace Library.Common.ModelsDto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
