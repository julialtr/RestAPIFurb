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

        public object Get()
        {
            return from produto in _context.produtos
                   select new
                   {
                       id = produto.Id,
                       nome = produto.Nome,
                       preco = produto.Preco,
                   };
        }


        public Produto GetByID(int id)
        {
            return _context.produtos.Find(id);
        }

        public object GetJsonByID(int id)
        {
            return (from produto in _context.produtos
                    where produto.Id == id
                    select new
                    {
                        id = produto.Id,
                        nome = produto.Nome,
                        preco = produto.Preco,
                    }).FirstOrDefault();
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
