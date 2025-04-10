public class Grade
{
    public int Id { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; }

    public int ClassId { get; set; }
    public Class Class { get; set; }

    public double Value { get; set; }
    public DateTime GivenAt { get; set; }
}
