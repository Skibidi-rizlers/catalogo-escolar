namespace Catalogo_Escolar_API.Services.BackupService
{
    /// <summary>
    /// Represents the backup service class
    /// </summary>
    public class BackupService : IBackupService
    {
        private readonly ILogger<BackupService> _logger;
        private readonly Timer _timer;

        /// <summary>
        /// Constructor of backup service
        /// </summary>
        /// <param name="logger">Logger</param>
        public BackupService(ILogger<BackupService> logger)
        {
            _logger = logger;
            _timer = new Timer(BackupDatabase, null, TimeSpan.Zero, TimeSpan.FromDays(1));
        }

        /// <inheritdoc/>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("BackupService is starting.");
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public void BackupDatabase(object? state)
        {
            try
            {
                var backupDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "backups"));

                if (!Directory.Exists(backupDir))
                {
                    Directory.CreateDirectory(backupDir);
                }

                var backupPath = Path.Combine(backupDir, $"Backup_{DateTime.Now:yyyyMMdd_HHmmss}.db");

                var databasePath = "..\\School.db";

                File.Copy(databasePath, backupPath);

                _logger.LogInformation($"Backup successful: {backupPath}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while backing up the database.");
            }
        }

        /// <inheritdoc/>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("BackupService is stopping.");
            _timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
