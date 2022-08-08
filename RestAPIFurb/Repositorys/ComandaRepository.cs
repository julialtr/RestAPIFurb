using Microsoft.AspNetCore.Mvc;
using RestAPIFurb.Models;
using RestAPIFurb.Repositorys.Interfaces;

namespace RestAPIFurb.Repositorys
{
    public class ComandaRepository : IComandaRepository
    {
        private readonly DataContext _context;

        public ComandaRepository(DataContext context)
        {
            _context = context;
        }

        public void Delete(Comanda comanda)
        {
            _context.comandas.Remove(comanda);
        }

        public void DeleteRange(int id)
        {
            _context.comandasProdutos.RemoveRange(_context.comandasProdutos.Where(cp => cp.IdComanda == id));
        }

        public object Get()
        {
            return from comandas in _context.comandas
                   select new
                   {
                       idUsuario = comandas.Usuario.Id,
                       nomeUsuario = comandas.Usuario.Nome,
                       telefoneUsuario = comandas.Usuario.Telefone,
                   };
        }

        public Comanda GetByID(int id)
        {
            return _context.comandas.Find(id);
        }

        public object GetJsonByID(int id)
        {
            return (from comandas in _context.comandas
                    where comandas.Id == id
                    select new
                    {
                        idUsuario = comandas.Usuario.Id,
                        nomeUsuario = comandas.Usuario.Nome,
                        telefoneUsuario = comandas.Usuario.Telefone,

                        produtos = (from produtos in _context.produtos
                                    join comandasProdutos in _context.comandasProdutos on produtos.Id equals comandasProdutos.IdProduto
                                    where comandasProdutos.IdComanda == comandas.Id
                                    select new
                                    {
                                        id = produtos.Id,
                                        nome = produtos.Nome,
                                        preco = produtos.Preco,
                                    }).ToList()
                    }).FirstOrDefault();
        }

        public void Insert(Comanda comanda)
        {
            _context.comandas.Add(comanda);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
