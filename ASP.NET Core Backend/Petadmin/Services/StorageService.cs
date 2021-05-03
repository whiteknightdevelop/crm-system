using System;
using System.Collections.Generic;
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
using Petadmin.Core.Exceptions;
using Petadmin.Core.Models;
using Petadmin.Hubs;
using Petadmin.Services.Interfaces;
using Petadmin.Storage;
using Petadmin.Utilities;

namespace Petadmin.Services
{
    public class StorageService : IStorageService
    {
        #region Class Initialization
        private readonly ILogger<StorageService> _logger;
        private readonly IStorageBroker _storageBroker;
        private readonly IStorageManager<ProgressReport> _storageManager;
        private readonly IConfiguration _configuration;
        private readonly long _fileSizeLimit;
        private readonly string _targetFilePath;
        private static readonly FormOptions DefaultFormOptions = new();

        public StorageService(IStorageBroker storageBroker, IStorageManager<ProgressReport> storageManager, ILogger<StorageService> logger, IConfiguration configuration)
        {
            _storageBroker = storageBroker;
            _logger = logger;
            _storageManager = storageManager;
            _configuration = configuration;
            _fileSizeLimit = configuration.GetValue<long>("FileSizeLimit");
            _targetFilePath = configuration.GetValue<string>("StoragePath");
        }
        #endregion

        public async Task<IEnumerable<PetadminStorageFile>> UploadVisitFile(HttpRequest request, int visitId)
        {
            try
            {
                PetadminStorageFile file = await _storageManager.Upload(request, _targetFilePath);
                file.VisitId = visitId;
                if (_storageBroker.AddVisitFileDataToDatabase(file) == 0)
                {
                    throw new Exception("Upload Failed! File:" + file.Name + file.FileExtension);
                }
                return _storageBroker.GetVisitFilesList(visitId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new UploadFailedException();
            }
        }

        public IEnumerable<PetadminStorageFile> DeleteVisitFile(PetadminStorageFile file)
        {
            try
            {
                bool dbAns = _storageBroker.DeleteVisitFileFromDatabase(file).Result;
                _storageManager.DeleteVisitFile(file);

                if (!dbAns)
                {
                    throw new Exception("File Deletion Failed:" + file.Name + file.FileExtension);
                }

                return _storageBroker.GetVisitFilesList(file.VisitId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new DeleteFileFailedException();
            }
        }
    }
}
