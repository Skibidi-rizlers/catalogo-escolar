using Catalogo_Escolar_API.Model;
using System.Diagnostics;

public class Class
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }

    public ICollection<StudentClass> StudentClasses { get; set; }
    public ICollection<Grade> Grades { get; set; }
    public ICollection<Assignment> Assignments { get; set; }
}
