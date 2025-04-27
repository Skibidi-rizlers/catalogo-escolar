using Catalogo_Escolar_API.Model;

public class Grade
{
    public int Id { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; }

    public int ClassId { get; set; }
    public Class Class { get; set; }

    public int AssignmentId { get; set; }
    public Assignment Assignment { get; set; }

    public double Value { get; set; }
    public DateTime GivenAt { get; set; }
}
