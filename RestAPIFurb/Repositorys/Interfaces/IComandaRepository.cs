using RestAPIFurb.Models;

namespace RestAPIFurb.Repositorys.Interfaces
{
    public interface IComandaRepository
    {
        object Get();
        object GetJsonByID(int id);
        Comanda GetByID(int id);
        void Insert(Comanda comanda);
        void Delete(Comanda comanda);
        void DeleteRange(int id);
        void Save();
    }
}
