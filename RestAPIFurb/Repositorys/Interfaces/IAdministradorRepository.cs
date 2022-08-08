using RestAPIFurb.Models;

namespace RestAPIFurb.Repositorys.Interfaces
{
    public interface IAdministradorRepository
    {
        void Insert(Administrador administrador);
        void Save();
        Administrador GetByLogin(string login);
    }
}
