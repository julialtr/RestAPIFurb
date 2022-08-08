using RestAPIFurb.Models;
using RestAPIFurb.Repositorys.Interfaces;

namespace RestAPIFurb.Repositorys
{
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly DataContext _context;

        public AdministradorRepository(DataContext context)
        {
            _context = context;
        }

        public Administrador GetByLogin(string login)
        {
            return _context.administradores.Where(u => u.Login.ToLower().Equals(login.ToLower())).FirstOrDefault();
        }

        public void Insert(Administrador administrador)
        {
            _context.administradores.Add(administrador);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
