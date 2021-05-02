using System;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.Services.Interfaces
{
    public interface IBackupDatabaseService
    {
        Task<string> Backup(IProgress<ProgressReport> progress);
        Task<bool> Restore(string path, IProgress<ProgressReport> progress);
        void DeleteTempFile(string path);
    }
}
