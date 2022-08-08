using RestAPIFurb.Models;

namespace RestAPIFurb.Repositorys.Interfaces
{
    public interface IProdutoRepository
    {
        List<Produto> Get();
        Produto GetByID(int id);
        void Insert(Produto produto);
        void Delete(Produto produto);
        void Save();
    }
}
