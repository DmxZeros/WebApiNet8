using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Nome do usuário requirido")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Senha requirida")]
        public string? Password { get; set; }
    }
}
