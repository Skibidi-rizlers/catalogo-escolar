using System.Diagnostics;
public class Student
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<StudentClass> StudentClasses { get; set; }
    public ICollection<Grade> Grades { get; set; }
}
