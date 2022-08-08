using RestAPIFurb.Models;
using RestAPIFurb.Repositorys.Interfaces;

namespace RestAPIFurb.Repositorys
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DataContext _context;

        public ProdutoRepository(DataContext context)
        {
            _context = context;
        }

        public void Delete(Produto produto)
        {
            _context.produtos.Remove(produto);
        }

        public List<Produto> Get()
        {
            return _context.produtos.ToList();
        }

        public Produto GetByID(int id)
        {
            return _context.produtos.Find(id);
        }

        public void Insert(Produto produto)
        {
            _context.produtos.Add(produto);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
