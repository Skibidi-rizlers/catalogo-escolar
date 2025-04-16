namespace Catalogo_Escolar_API.Services.BackupService
{
    /// <summary>
    /// Represents the interface for the backup service.
    /// </summary>
    public interface IBackupService : IHostedService, IDisposable
    {
        /// <summary>
        /// Represents the function that saves the backup
        /// </summary>
        /// <param name="state">State given from Timer function. Unused.</param>
        void BackupDatabase(object? state);
    }
}
