namespace Catalogo_Escolar_API.Model.DTOs
{
    public class ClassDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StudentDTO> Students { get; set; }
    }
}
