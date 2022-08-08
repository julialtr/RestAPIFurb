using System.ComponentModel.DataAnnotations;

namespace RestAPIFurb.DTOs
{
    public class ProdutoDto
    {
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú\-\s]+$", ErrorMessage = "Nome deve conter apenas letras.")]
        public string? Nome { get; set; }

        [RegularExpression(@"^[0-9]+\,?[0-9]*$", ErrorMessage = "Preço deve conter apenas números.")]
        public decimal Preco { get; set; }
    }
}
