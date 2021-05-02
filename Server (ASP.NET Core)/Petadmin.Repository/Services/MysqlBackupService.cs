using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Petadmin.Core.Models;
using Petadmin.Repository.Cryptography;
using Petadmin.Repository.Services.Interfaces;


namespace Petadmin.Repository.Services
{
    public class MysqlBackupService : IBackupDatabaseService
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly IEncryptService _encryptService;
        private readonly string _filePath;
        private readonly string _encryptedFilePath;
        private byte[] _keyFile;
        private byte[] _ivFile;

        public MysqlBackupService(IConfiguration configuration, IEncryptService encryptService)
        {
            _configuration = configuration;
            _connectionString = _configuration["MYSQLSettings:ConnectionString"];
            _encryptService = encryptService;
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "backup.sql");
            _encryptedFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "last-backup.bin");
        }

        public async Task<string> Backup(IProgress<ProgressReport> progress)
        {
            return await Task.Run(async () =>
            {
                var report = new ProgressReport();

                report.PercentageComplete = 5;
                progress.Report(report);

                await using var connection = new MySqlConnection(_connectionString);
                await using var cmd = new MySqlCommand();
                using var mb = new MySqlBackup(cmd);

                report.PercentageComplete = 15;
                progress.Report(report);

                cmd.Connection = connection;
                connection.Open();

                report.PercentageComplete = 25;
                progress.Report(report);

                mb.ExportToFile(_filePath);
                await connection.CloseAsync();

                report.PercentageComplete = 50;
                progress.Report(report);


                // Start Encryption
                SetKeyAndIv();
                string buffer = await File.ReadAllTextAsync(_filePath);

                report.PercentageComplete = 60;
                progress.Report(report);

                byte[] encrypted = _encryptService.EncryptStringToBytes_Aes(buffer, _keyFile, _ivFile);
                // End Encryption

                report.PercentageComplete = 70;
                progress.Report(report);

                await File.WriteAllBytesAsync(_encryptedFilePath, encrypted);

                report.PercentageComplete = 85;
                progress.Report(report);

                // Remove backup file
                DeleteTempFile(_filePath);

                report.PercentageComplete = 100;
                progress.Report(report);

                return _encryptedFilePath;
            });
        }

        public Task<bool> Restore(string path, IProgress<ProgressReport> progress)
        { 
            return Task.Run(async () =>
            {
                ProgressReport report = new ProgressReport();
                report.Message = "מתחיל...";
                report.PercentageComplete = 25;
                progress.Report(report);

                SetKeyAndIv();

                byte[] buffer = await File.ReadAllBytesAsync(path);

                report.Message = "מפענח הצפנה...";
                report.PercentageComplete = 35;
                progress.Report(report);

                // Start Decryption
                string decrypted = _encryptService.DecryptStringFromBytes_Aes(buffer, _keyFile, _ivFile);
                // End Decryption

                report.Message = "פיענוח הסתיים...";
                report.PercentageComplete = 45;
                progress.Report(report);

                // Convert the string to UTF8 binary data.
                byte[] buffUtf8 = Encoding.UTF8.GetBytes(decrypted);
                var stream = new MemoryStream(buffUtf8);

                report.Message = "פותח ערוץ תקשורת לבסיס נתונים...";
                report.PercentageComplete = 50;
                progress.Report(report);

                await using var restoreConnection = new MySqlConnection(restoreConnectionString);
                await using var connection = new MySqlConnection(_connectionString);
                await using var cmd = new MySqlCommand();
                using var mb = new MySqlBackup(cmd);

                cmd.Connection = restoreConnection;
                restoreConnection.Open();

                var dropDatabaseIfExists = "DROP DATABASE IF EXISTS petadmin;";
                cmd.CommandText = dropDatabaseIfExists;
                cmd.ExecuteNonQuery();

                var createDatabase = "CREATE DATABASE petadmin;";
                cmd.CommandText = createDatabase;
                cmd.ExecuteNonQuery();

                await restoreConnection.CloseAsync();

                report.Message = "כותב מידע לבסיס נתונים. אנא המתן בסבלנות...";
                report.PercentageComplete = 70;
                progress.Report(report);

                cmd.Connection = connection;
                connection.Open();

                mb.ImportFromStream(stream);
                await connection.CloseAsync();

                DeleteTempFile(path);

                report.Message = "שחזור הנתונים הסתיים בהצלחה!";
                report.PercentageComplete = 100;
                progress.Report(report);

                return true;
            });

        }

        private void SetKeyAndIv()
        {
            var keyFilePath = Path.Combine(_configuration["EncryptionSettings:AES_Key"]);
            var ivFilePath = Path.Combine(_configuration["EncryptionSettings:AES_IV"]);

            _keyFile = File.ReadAllBytes(keyFilePath);
            _ivFile = File.ReadAllBytes(ivFilePath);
        }

        public void DeleteTempFile(string path)
        {
            File.Delete(path);
        }
    }
}
