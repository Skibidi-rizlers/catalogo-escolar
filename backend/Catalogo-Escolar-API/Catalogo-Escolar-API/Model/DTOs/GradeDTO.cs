namespace Catalogo_Escolar_API.Model.DTOs
{
    public class GradeDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int AssignmentId { get; set; }
        public double Value { get; set; }
        public DateTime GivenAt { get; set; }
    }
}
