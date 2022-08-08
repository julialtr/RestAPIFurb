using System.ComponentModel.DataAnnotations;

namespace RestAPIFurb.DTOs
{
    public class AdministradorDto
    {
        [Required(ErrorMessage = "Login deve ser informado.")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Senha deve ser informada.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Zà-úÀ-Ú$*&@#]{6,}$", ErrorMessage = "Senha deve conter ao menos 6 caracteres, um dígito, caracter maiúsculo e minúsculo.")]
        public string? Senha { get; set; }
    }
}
