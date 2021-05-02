using System;
using System.IO;
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
using Petadmin.Core.Exceptions;
using Petadmin.Core.Models;
using Petadmin.Hubs;
using Petadmin.Models;
using Petadmin.Services.Interfaces;
using Petadmin.Utilities;

namespace Petadmin.Services
{
    public class SystemService : ISystemService
    {
        #region Class Initialization
        private readonly ILogger<SystemService> _logger;
        private readonly ISystemBroker _systemBroker;
        private readonly IHubContext<SystemHub> _hub;
        private string _connectionId;
        private readonly long _fileSizeLimit;
        private readonly string[] _permittedExtensions = { ".bin" };
        private readonly string _targetFilePath;
        private static readonly FormOptions DefaultFormOptions = new();

        public SystemService(ISystemBroker systemBroker, IHubContext<SystemHub> hub, ILogger<SystemService> logger, IConfiguration configuration)
        {
            _systemBroker = systemBroker;
            _hub = hub;
            _logger = logger;
            _fileSizeLimit = configuration.GetValue<long>("FileSizeLimit");
            _targetFilePath = configuration.GetValue<string>("StoredFilesPath");
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

                string path = await CreateRestoreTempFile(request, progress);
                return await _systemBroker.Restore(path, progress);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        private async Task<string> CreateRestoreTempFile(HttpRequest request, IProgress<ProgressReport> progress)
        {
            try
            {
                ProgressReport report = new ProgressReport();
                report.Message = "יוצר קובץ זמני...";
                report.PercentageComplete = 5;
                progress.Report(report);

                string trustedFileNamePath = "";

                if (!MultipartRequestHelper.IsMultipartContentType(request.ContentType))
                {
                    _logger.LogError("Upload File - Restore", $"The request couldn't be processed. (Wrong ContentType)");
                    throw new Exception("The request couldn't be processed. (Wrong ContentType)");
                }

                var boundary = MultipartRequestHelper.GetBoundary(
                    MediaTypeHeaderValue.Parse(request.ContentType),
                    DefaultFormOptions.MultipartBoundaryLengthLimit);
                var reader = new MultipartReader(boundary, request.Body);

                var section = await reader.ReadNextSectionAsync();

                report.PercentageComplete = 15;
                progress.Report(report);

                while (section != null)
                {
                    var hasContentDispositionHeader = 
                        ContentDispositionHeaderValue.TryParse(
                            section.ContentDisposition, out var contentDisposition);

                    if (hasContentDispositionHeader)
                    {
                        if (!MultipartRequestHelper
                            .HasFileContentDisposition(contentDisposition))
                        {
                            _logger.LogError("Upload File - Restore", $"The request couldn't be processed. (HasFileContentDisposition)");
                            throw new Exception("The request couldn't be processed. (HasFileContentDisposition)");
                        }

                        var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                            contentDisposition.FileName.Value);
                        var trustedFileNameForFileStorage = Path.GetRandomFileName();

                        var streamedFileContent = await FileHelpers.ProcessStreamedFile(
                            section, contentDisposition, _logger, 
                            _permittedExtensions, _fileSizeLimit);

                        trustedFileNamePath = Path.Combine(_targetFilePath, trustedFileNameForFileStorage);

                        await using var targetStream = File.Create(trustedFileNamePath);
                        await targetStream.WriteAsync(streamedFileContent);

                        _logger.LogInformation(
                            "Uploaded file '{TrustedFileNameForDisplay}' saved to " +
                            "'{TargetFilePath}' as {TrustedFileNameForFileStorage}", 
                            trustedFileNameForDisplay, _targetFilePath, 
                            trustedFileNameForFileStorage);
                    }
                    section = await reader.ReadNextSectionAsync();
                }

                return trustedFileNamePath;
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
