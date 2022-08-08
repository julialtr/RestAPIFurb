using RestAPIFurb.Models;

namespace RestAPIFurb.Repositorys.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Get();
        Usuario GetByID(int id);
        void Insert(Usuario usuario);
        void Delete(Usuario usuario);
        void Save();
    }
}
