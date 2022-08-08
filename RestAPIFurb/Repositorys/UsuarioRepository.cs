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

        public object Get()
        {
            return from usuario in _context.usuarios
                   select new
                   {
                       id = usuario.Id,
                       nome = usuario.Nome,
                       telefone = usuario.Telefone,
                   };
        }

        public Usuario GetByID(int id)
        {
            return _context.usuarios.Find(id);
        }

        public object GetJsonByID(int id)
        {
            return (from usuario in _context.usuarios
                   where usuario.Id == id
                   select new
                   {
                       id = usuario.Id,
                       nome = usuario.Nome,
                       telefone = usuario.Telefone,
                   }).FirstOrDefault();
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
