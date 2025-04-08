namespace Catalogo_Escolar_API.Services.StudentService
{
    public interface IStudentService
    {
        Task<Student?> GetStudent(string email, string password);
        Task<bool> AddStudent(Student student);
        Task<bool> ChangePasswordForStudent(string email, string newPassword);
    }
}