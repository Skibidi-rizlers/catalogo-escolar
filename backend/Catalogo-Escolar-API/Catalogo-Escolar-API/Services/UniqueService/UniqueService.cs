
using Microsoft.EntityFrameworkCore;

namespace Catalogo_Escolar_API.Services.UniqueService
{
    public class UniqueService : IUniqueService
    {
        private readonly SchoolContext _context;
        public UniqueService(SchoolContext context)
        {
            _context = context;
        }
        public bool EmailExists(string email)
        {
            var exists = _context.Users.Where(u => u.Email == email).FirstOrDefault();
            return exists != null;
        }
    }
}
