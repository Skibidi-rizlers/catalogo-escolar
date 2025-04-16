namespace Catalogo_Escolar_API.Services.UniqueService
{
    /// <summary>
    /// Represents the unique service class
    /// </summary>
    public class UniqueService : IUniqueService
    {
        private readonly SchoolContext _context;
        /// <summary>
        /// Represents the constructor of the unique service class
        /// </summary>
        /// <param name="context">Context of database</param>
        public UniqueService(SchoolContext context)
        {
            _context = context;
        }
        /// <inheritdoc/>
        public bool EmailExists(string email)
        {
            var exists = _context.Users.Where(u => u.Email == email).FirstOrDefault();
            return exists != null;
        }
    }
}
