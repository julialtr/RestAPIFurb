namespace RestAPIFurb.Models
{
    public class ComandaProduto
    {
        public int IdProduto { get; set; }
        public int IdComanda { get; set; }
        public Produto? Produto { get; set; }
        public Comanda? Comanda { get; set; }
    }
}
