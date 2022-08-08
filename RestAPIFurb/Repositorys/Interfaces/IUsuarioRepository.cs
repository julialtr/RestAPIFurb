using RestAPIFurb.Models;

namespace RestAPIFurb.Repositorys.Interfaces
{
    public interface IUsuarioRepository
    {
        object Get();
        Usuario GetByID(int id);
        public object GetJsonByID(int id);
        void Insert(Usuario usuario);
        void Delete(Usuario usuario);
        void Save();
    }
}
