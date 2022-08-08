using System.ComponentModel.DataAnnotations;

namespace RestAPIFurb.DTOs
{
    public class UsuarioDto
    {
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú\s]+$", ErrorMessage = "Nome deve conter apenas letras.")]
        public string? Nome { get; set; }

        [RegularExpression(@"^[0-9]{10,11}$", ErrorMessage = "Telefone deve conter entre 10 e 11 caracteres numéricos.")]
        public string? Telefone { get; set; }
    }
}
