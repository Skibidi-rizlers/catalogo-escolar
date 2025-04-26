namespace Catalogo_Escolar_API.Model
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public ICollection<Grade> Grades { get; set; }
    }

}
