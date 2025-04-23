namespace Catalogo_Escolar_API.Services.UniqueService
{
    /// <summary>
    /// Represents the user service class
    /// </summary>
    public class UserService : IUserService
    {
        private readonly SchoolContext _context;
        /// <summary>
        /// Represents the constructor of the user service class
        /// </summary>
        /// <param name="context">Context of database</param>
        public UserService(SchoolContext context)
        {
            _context = context;
        }
        /// <inheritdoc/>
        public bool EmailExists(string email)
        {
            var exists = _context.Users.Where(u => u.Email == email).FirstOrDefault();
            return exists != null;
        }

        /// <inheritdoc/>
        public User? GetUserById(int id)
        {
            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            return user;
        }

        /// <inheritdoc/>
        public bool Update(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
