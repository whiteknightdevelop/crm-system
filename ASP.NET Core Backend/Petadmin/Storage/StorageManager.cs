using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Petadmin.Core.Models;
using Petadmin.Repository.Cryptography;
using Petadmin.Services;
using Petadmin.Utilities;

namespace Petadmin.Storage
{
    public class StorageManager : IStorageManager<ProgressReport>
    {
        #region Class Initialization
        private readonly ILogger<StorageManager> _logger;
        private readonly IEncryptService _encryptService;
        private readonly long _fileSizeLimit;
        private readonly string[] _permittedExtensions = { ".bin", ".gif", "png", ".jpeg", ".jpg", ".pdf", ".doc", ".docx", ".odt" };
        private readonly string _storagePath;
        private readonly string _uploadTempPath;
        private readonly IConfiguration _configuration;
        private static readonly FormOptions DefaultFormOptions = new();

        public StorageManager(ILogger<StorageManager> logger, IEncryptService encryptService, IConfiguration configuration)
        {
            _logger = logger;
            _encryptService = encryptService;
            _configuration = configuration;
            _fileSizeLimit = configuration.GetValue<long>("FileSizeLimit");
            _storagePath = configuration.GetValue<string>("StoragePath");
            _uploadTempPath = configuration.GetValue<string>("UploadTempPath");
        }
        #endregion

        public async Task<PetadminStorageFile> Upload(HttpRequest request, string pathToUpload)
        {
            var reader = GetMultipartReader(request);
            var section = await reader.ReadNextSectionAsync();
            PetadminStorageFile file = await ReadSectionWhile(section, reader, pathToUpload);
            return file;
        }

        public async Task<PetadminStorageFile> RestoreUpload(HttpRequest request, string pathToUpload, IProgress<ProgressReport> progress)
        {
            ProgressReport report = new ProgressReport();
            report.Message = "יוצר קובץ זמני...";
            report.PercentageComplete = 5;
            progress.Report(report);
            var reader = GetMultipartReader(request);
            var section = await reader.ReadNextSectionAsync();
            report.PercentageComplete = 15;
            progress.Report(report);
            PetadminStorageFile file = await ReadSectionWhile(section, reader, pathToUpload);
            return file;
        }

        public void DeleteVisitFile(PetadminStorageFile file)
        {
            File.Delete(file.Path);
        }

        private async Task<PetadminStorageFile> ReadSectionWhile(MultipartSection section, MultipartReader reader, string pathToUpload)
        {
            var trustedFileNamePath = string.Empty;
            var file = new PetadminStorageFile();
            while (section != null)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);
                if (hasContentDispositionHeader)
                {
                    // This check assumes that there's a file
                    // present without form data. If form data
                    // is present, this method immediately fails
                    // and returns the model error.
                    if (!MultipartRequestHelper
                        .HasFileContentDisposition(contentDisposition))
                    {
                        _logger.LogError("Upload File", $"The request couldn't be processed. (HasFileContentDisposition)");
                        throw new Exception("The request couldn't be processed. (HasFileContentDisposition)");
                    }

                    // Don't trust the file name sent by the client. To display
                    // the file name, HTML-encode the value.
                    var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                        contentDisposition.FileName.Value);
                    //var trustedFileNameForFileStorage = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());

                    // **WARNING!**
                    // In the following example, the file is saved without
                    // scanning the file's contents. In most production
                    // scenarios, an anti-virus/anti-malware scanner API
                    // is used on the file before making the file available
                    // for download or for use by other systems. 
                    // For more information, see the topic that accompanies 
                    // this sample.
                    var streamedFileContent = await FileHelpers.ProcessStreamedFile(
                        section, contentDisposition, _logger,
                        _permittedExtensions, _fileSizeLimit);

                    file.Name = Path.GetFileNameWithoutExtension(trustedFileNameForDisplay);
                    file.FileExtension = Path.GetExtension(trustedFileNameForDisplay);
                    file.Size = streamedFileContent.Length;

                    string fileHash = _encryptService.ComputeSha256Hash(file.ToString());
                    DirectoryInfo directory =
                        Directory.CreateDirectory(Path.Combine(pathToUpload, DateTime.Now.Year.ToString()));
                    trustedFileNamePath = Path.Combine(directory.FullName, fileHash + file.FileExtension);

                    file.Hash = fileHash;

                    await using var targetStream = File.Create(trustedFileNamePath);
                    await targetStream.WriteAsync(streamedFileContent);

                    _logger.LogInformation(
                        "Uploaded file '{TrustedFileNameForDisplay}' saved to " +
                        "'{TargetFilePath}' as {TrustedFileNameForFileStorage}",
                        trustedFileNameForDisplay, pathToUpload,
                        trustedFileNamePath);
                }

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.
                section = await reader.ReadNextSectionAsync();
                file.Path = trustedFileNamePath;
            }
            return file;
        }

        private MultipartReader GetMultipartReader(HttpRequest request)
        {
            if (!MultipartRequestHelper.IsMultipartContentType(request.ContentType))
            {
                _logger.LogError("Upload File", $"The request couldn't be processed. (Wrong ContentType)");
                throw new Exception("The request couldn't be processed. (Wrong ContentType)");
            }

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(request.ContentType),
                DefaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, request.Body);
            return reader;
        }
    }
}
