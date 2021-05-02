using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using Petadmin.Brokers.Interfaces;
using Petadmin.Models;
using Petadmin.Services;
using Petadmin.Services.Interfaces;
using Tynamix.ObjectFiller;
using Xunit;

namespace Petadmin.Test.Services
{
    public class RegisterServiceTests
    {
        #region Class Initialization
        private readonly Mock<IRegisterBroker> _registerBrokerMock;
        private readonly IRegisterService _registerService;
        private readonly Mock<ILogger<RegisterService>> _loggingMock;

        public RegisterServiceTests()
        {
            _registerBrokerMock = new Mock<IRegisterBroker>();
            _loggingMock = new Mock<ILogger<RegisterService>>();
            _registerService = new RegisterService(_registerBrokerMock.Object, _loggingMock.Object);
        }
        #endregion

        [Fact]
        public async void ShouldCallGetRegisterPageAsyncOnce()
        {
            // Arrange
            RegisterPage expectedResult = CreateRandomRegisterPage();

            SetupRegisterPageBroker(expectedResult);

            // Act
            await _registerService.GetRegisterPageAsync();

            // Assert
            _registerBrokerMock.Verify(broker => broker.GetGendersListAsync(), Times.Once, "Called Times.Once failed");
        }

        #region Private Methods
        private IEnumerable<string> CreateRandomGendersList()
        {
            int listLength = new IntRange(2, 10).GetValue();

            var list = new List<string>();
            for (var i = 0; i < listLength; i++)
            {
                var str = new Lipsum(LipsumFlavor.LoremIpsum).ToString();
                list.Add(str);
            }
            return list;
        }

        private RegisterPage CreateRandomRegisterPage()
        {
            var registerPageFiller = new Filler<RegisterPage>();
            registerPageFiller.Setup()
                .OnProperty(p => p.GendersList).Use(CreateRandomGendersList().ToList);

            RegisterPage registerPage = registerPageFiller.Create();
            return registerPage;
        }

        private void SetupRegisterPageBroker(RegisterPage expectedResult)
        {
            _registerBrokerMock.Setup(broker => broker.GetGendersListAsync())
                .ReturnsAsync(expectedResult.GendersList);
        }
        #endregion
    }
}
