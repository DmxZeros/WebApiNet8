using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Nome do usuário requirido")]
        public string? UserName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email requirido")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Senha requirida")]
        public string? Password { get; set; }
    }
}
