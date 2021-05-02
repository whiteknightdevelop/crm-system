using System;
using System.Threading.Tasks;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Models;
using Petadmin.Repository.Services.Interfaces;

namespace Petadmin.Brokers
{
    public class SystemBroker : ISystemBroker
    {
        private readonly IBackupDatabaseService _backupDatabaseService;
        public SystemBroker(IBackupDatabaseService backupDatabaseService)
        {
            _backupDatabaseService = backupDatabaseService;
        }

        public Task<string> GetBackupFilePath(IProgress<ProgressReport> progress)
        {
            return _backupDatabaseService.Backup(progress);
        }

        public Task<bool> Restore(string path, IProgress<ProgressReport> progress)
        {
            return _backupDatabaseService.Restore(path, progress);
        }
    }
}
