using System;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Brokers.Interfaces
{
    public interface ISystemBroker
    {
        Task<string> GetBackupFilePath(IProgress<ProgressReport> progress);
        Task<bool> Restore(string path, IProgress<ProgressReport> progress);
    }
}
