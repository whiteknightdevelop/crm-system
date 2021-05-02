using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Models;
using Petadmin.Hubs;
using Petadmin.Models;
using Petadmin.Services;
using Petadmin.Services.Interfaces;
using Tynamix.ObjectFiller;
using Xunit;

namespace Petadmin.Test.Services
{
    public class SystemServiceTests
    {
        #region Class Initialization
        private readonly Mock<ISystemBroker> _systemBrokerMock;
        private readonly ISystemService _systemService;
        private readonly Mock<IHubContext<SystemHub>> _hubMock;
        private readonly IConfiguration _configuration;
        private readonly Mock<ILogger<SystemService>> _loggingMock;

        public SystemServiceTests()
        {
            _systemBrokerMock = new Mock<ISystemBroker>();
            _loggingMock = new Mock<ILogger<SystemService>>();
            _hubMock = new Mock<IHubContext<SystemHub>>();

            var inMemorySettings = new Dictionary<string, string> {
                {"TopLevelKey", "TopLevelValue"},
                {"SectionName:SomeKey", "SectionValue"},
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _systemService = new SystemService(
                _systemBrokerMock.Object, _hubMock.Object, _loggingMock.Object, _configuration);

        }
        #endregion

        [Fact(Skip="Problem to test creation of file!")]
        public async void ShouldGetBackupFile()
        {
            // Arrange
            var connectionId = "ghjFYKhgdDDik";
            Backup expectedResult = CreateRandomBackup();

            SetupSystemBroker(expectedResult);

            //// Act
            Backup actualResult = await _systemService.GetBackupFile(connectionId);

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }

        #region Private Methods
        private Backup CreateRandomBackup()
        {
            string randomString = new Lipsum(LipsumFlavor.LoremIpsum).ToString();
            byte[] fileBytes = Encoding.ASCII.GetBytes(randomString!);  

            var backupFiller = new Filler<Backup>();
            backupFiller.Setup()
                .OnProperty(p => p.FileName).Use("bhGFDhk.FGf")
                .OnProperty(p => p.FilePath).Use(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
                .OnProperty(p => p.FileBytes).Use(fileBytes);

            Backup backup = backupFiller.Create();
            return backup;
        }

        private void SetupSystemBroker(Backup expectedResult)
        {
            Progress<ProgressReport> progress = new Progress<ProgressReport>();

            _systemBrokerMock.Setup(broker => broker.GetBackupFilePath(progress))
                .ReturnsAsync(expectedResult.FilePath);
            _systemBrokerMock.Setup(broker => broker.Restore(expectedResult.FilePath, progress))
                .ReturnsAsync(true);
        }
        #endregion
    }
}
