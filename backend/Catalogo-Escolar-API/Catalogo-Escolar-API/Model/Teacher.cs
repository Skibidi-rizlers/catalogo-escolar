public class Teacher
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<Class> Classes { get; set; }
}
