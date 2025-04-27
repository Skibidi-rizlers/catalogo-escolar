namespace Catalogo_Escolar_API.Model.Payloads.Assignment
{
    /// <summary>
    /// Payload for creating an assignment
    /// </summary>
    public class CreateAssignmentPayload
    {
        /// <summary>
        /// The id of the class to which the assignment belongs
        /// </summary>
        public int ClassId { get; set; }
        /// <summary>
        /// The title of the assignment
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// The description of the assignment
        /// </summary>
        public required string Description { get; set; }
        /// <summary>
        /// The date when the assignment is due
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <inheritdoc/>
        public override string? ToString()
        {
            return $"CreateAssignmentPayload (classId: {ClassId}, title: {Title}, description: {Description}, dueDate:{DueDate})";
        }
    }
}
