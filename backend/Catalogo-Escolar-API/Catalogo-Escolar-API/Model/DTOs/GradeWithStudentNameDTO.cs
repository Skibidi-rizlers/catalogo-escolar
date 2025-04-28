namespace Catalogo_Escolar_API.Model.DTOs
{
    public class GradeWithStudentNameDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int CourseId { get; set; }
        public int AssignmentId { get; set; }
        public double Value { get; set; }
        public DateTime GivenAt { get; set; }
    }
}
