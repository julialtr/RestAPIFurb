using RestAPIFurb.Models;

namespace RestAPIFurb.Repositorys.Interfaces
{
    public interface IProdutoRepository
    {
        object Get();
        Produto GetByID(int id);
        public object GetJsonByID(int id);
        void Insert(Produto produto);
        void Delete(Produto produto);
        void Save();
    }
}
