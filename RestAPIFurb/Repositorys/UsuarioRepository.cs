using RestAPIFurb.Models;
using RestAPIFurb.Repositorys.Interfaces;

namespace RestAPIFurb.Repositorys
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext _context;

        public UsuarioRepository(DataContext context)
        {
            _context = context;
        }

        public void Delete(Usuario usuario)
        {
            _context.usuarios.Remove(usuario);
        }

        public List<Usuario> Get()
        {
            return _context.usuarios.ToList();
        }

        public Usuario GetByID(int id)
        {
            return _context.usuarios.Find(id);
        }

        public void Insert(Usuario usuario)
        {
            _context.usuarios.Add(usuario);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
