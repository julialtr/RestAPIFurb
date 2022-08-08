using System.ComponentModel.DataAnnotations;

namespace RestAPIFurb.Models
{
    public class Administrador
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Senha { get; set; }
    }
}
