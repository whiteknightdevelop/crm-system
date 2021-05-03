using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Petadmin.Brokers.Interfaces;
using Petadmin.Controllers;
using Petadmin.Core.Exceptions;
using Petadmin.Core.Models;
using Petadmin.Hubs;
using Petadmin.Models;
using Petadmin.Services.Interfaces;
using Petadmin.Storage;
using Petadmin.Utilities;

namespace Petadmin.Services
{
    public class SystemService : ISystemService
    {
        #region Class Initialization
        private readonly ILogger<SystemService> _logger;
        private readonly ISystemBroker _systemBroker;
        private readonly IStorageManager<ProgressReport> _storageManager;
        private readonly IHubContext<SystemHub> _hub;
        private string _connectionId;
        private readonly long _fileSizeLimit;
        private readonly IConfiguration _configuration;
        private readonly string _uploadTempPath;

        public SystemService(ISystemBroker systemBroker, IHubContext<SystemHub> hub, IStorageManager<ProgressReport> storageManager, ILogger<SystemService> logger, IConfiguration configuration)
        {
            _systemBroker = systemBroker;
            _storageManager = storageManager;
            _hub = hub;
            _logger = logger;
            _configuration = configuration;
            _fileSizeLimit = configuration.GetValue<long>("FileSizeLimit");
            _uploadTempPath = configuration.GetValue<string>("UploadTempPath");
        }
        #endregion

        public async Task<Backup> GetBackupFile(string connectionId)
        {
            try
            {
                _connectionId = connectionId;

                Progress<ProgressReport> progress = new Progress<ProgressReport>();
                progress.ProgressChanged += ReportBackupProgress;

                var fileName = "backup.bin";
                string path = await _systemBroker.GetBackupFilePath(progress);
                byte[] fileBytes = await File.ReadAllBytesAsync(path);
                

                progress.ProgressChanged -= ReportBackupProgress;
                return new Backup
                {
                    FilePath = path,
                    FileName = fileName,
                    FileBytes = fileBytes
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        public async Task<bool> Restore(HttpRequest request, string connectionId, IProgress<ProgressReport> progress)
        {
            try
            {
                _connectionId = connectionId;

                PetadminStorageFile file = await CreateRestoreTempFile(request, progress);
                return await _systemBroker.Restore(file.Path, progress);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        private async Task<PetadminStorageFile> CreateRestoreTempFile(HttpRequest request, IProgress<ProgressReport> progress)
        {
            try
            {
                PetadminStorageFile file = await _storageManager.RestoreUpload(request, _uploadTempPath, progress);
                return file;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        private async void ReportBackupProgress(object? sender, ProgressReport e)
        {
            await _hub.Clients.Client(_connectionId).SendAsync("ReportBackupProgress", e.PercentageComplete);
        }

        public async void ReportRestoreProgress(object? sender, ProgressReport e)
        {
            await _hub.Clients.Client(_connectionId).SendAsync("ReportRestoreProgress", e.PercentageComplete, e.Message);
        }
    }
}
