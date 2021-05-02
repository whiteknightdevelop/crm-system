using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Petadmin.Core.Models;
using Petadmin.Models;

namespace Petadmin.Services.Interfaces
{
    public interface ISystemService
    {
        Task<Backup> GetBackupFile(string connectionId);
        Task<bool> Restore(HttpRequest request, string connectionId, IProgress<ProgressReport> progress);
        void ReportRestoreProgress(object? sender, ProgressReport e);
    }
}
