namespace Catalogo_Escolar_API.Model.Payloads.Assignment
{
    /// <summary>
    /// Payload for updating an assignment
    /// </summary>
    public class UpdateAssignmentPayload
    {
        /// <summary>
        /// The id of the assignment to be updated
        /// </summary>
        public int Id;
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
            return $"UpdateAssignmentPayload (id: {Id}, title: {Title}, description: {Description}, dueDate:{DueDate})";
        }
    }
}
