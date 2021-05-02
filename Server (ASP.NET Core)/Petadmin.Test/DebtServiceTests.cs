using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Exceptions;
using Petadmin.Core.Models;
using Petadmin.Models;
using Petadmin.Services;
using Petadmin.Services.Interfaces;
using Tynamix.ObjectFiller;
using Xunit;

namespace Petadmin.Test.Services
{
    public class DebtServiceTests
    {
        #region Class Initialization
        private readonly Mock<IDebtBroker> _debtBrokerMock;
        private readonly IDebtService _debtService;
        private readonly Mock<ILogger<DebtService>> _loggingMock;

        public DebtServiceTests()
        {
            _debtBrokerMock = new Mock<IDebtBroker>();
            _loggingMock = new Mock<ILogger<DebtService>>();
            _debtService = new DebtService(_debtBrokerMock.Object, _loggingMock.Object);
        }
        #endregion

        [Fact]
        public async void ShouldCallDebtBroker()
        {
            // Arrange
            DebtPage expectedResult = CreateRandomDebtPage();
            int ownerId = expectedResult.Owner.OwnerId;

            SetupDebtPageBroker(expectedResult);

            // Act
            await _debtService.GetDebtPageByOwnerIdAsync(ownerId);
            
            // Assert
            _debtBrokerMock.Verify(broker => broker.GetOwnerByIdAsync(ownerId), Times.Once, "Called Times.Once failed");
        }

        #region GET
        [Fact]
        public async void ShouldGetDebtPageByOwnerIdAsync()
        {
            // Arrange
            DebtPage expectedResult = CreateRandomDebtPage();
            int ownerId = expectedResult.Owner.OwnerId;

            SetupDebtPageBroker(expectedResult);

            //// Act
            DebtPage actualResult = await _debtService.GetDebtPageByOwnerIdAsync(ownerId);

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }

        [Fact]
        public async void ShouldGetDebtsListByOwnerIdAsync()
        {
            // Arrange
            int ownerId = new IntRange(2001, 9999).GetValue();
            List<Debt> expectedResult = CreateRandomDebtList();

            _debtBrokerMock.Setup(broker => broker.GetDebtsListByOwnerIdAsync(ownerId))
                .ReturnsAsync(expectedResult);

            //// Act
            List<Debt> actualResult = (await _debtService.GetDebtsListByOwnerIdAsync(ownerId)).ToList();

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }

        [Fact]
        public async void ShouldThrowGetFailedExceptionWhenDbFails()
        {
            // Arrange
            DebtPage expectedResult = CreateRandomDebtPage();
            int ownerId = expectedResult.Owner.OwnerId;

            var exception = new Exception(
                message: new MnemonicString().GetValue());

            _debtBrokerMock.Setup(broker => broker.GetDebtsListByOwnerIdAsync(ownerId)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<GetFailedException>(() => _debtService.GetDebtsListByOwnerIdAsync(ownerId));
        }
        #endregion

        #region ADD
        [Fact]
        public async void ShouldAddDebtAsync()
        {
            // Arrange
            Debt inputDebt = CreateRandomDebt();
            var expectedResult = inputDebt.OwnerId;

            _debtBrokerMock.Setup(broker => broker.AddDebtAsync(inputDebt))
                .ReturnsAsync(expectedResult);

            // Act
            int actualOwnerId = await _debtService.AddDebtAsync(inputDebt);

            // Assert
            Assert.Equal(actualOwnerId, expectedResult);
        }

        [Fact]
        public async void ShouldThrowAddFailedExceptionWhenDbFails()
        {
            // Arrange
            Debt inputDebt = CreateRandomDebt();

            var exception = new Exception(new MnemonicString().GetValue());

            _debtBrokerMock.Setup(broker => broker.AddDebtAsync(inputDebt)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<AddFailedException>(() => _debtService.AddDebtAsync(inputDebt));
        }
        #endregion

        #region UPDATE
        [Fact]
        public async void ShouldUpdateDebtAsync()
        {
            // Arrange
            Debt inputDebt = CreateRandomDebt();

            _debtBrokerMock.Setup(broker => broker.UpdateDebtAsync(inputDebt))
                .ReturnsAsync(true);

            // Act
            bool actualResult = await _debtService.UpdateDebtAsync(inputDebt);

            // Assert
            Assert.True(actualResult);
        }

        [Fact]
        public async void ShouldThrowUpdateFailedExceptionWhenDbFails()
        {
            // Arrange
            Debt inputDebt = CreateRandomDebt();

            var exception = new Exception(new MnemonicString().GetValue());

            _debtBrokerMock.Setup(broker => broker.UpdateDebtAsync(inputDebt)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<UpdateFailedException>(() => _debtService.UpdateDebtAsync(inputDebt));
        }
        #endregion

        #region DELETE

        [Fact]
        public async void ShouldDeleteDebtAsync()
        {
            // Arrange
            Debt inputDebt = CreateRandomDebt();

            _debtBrokerMock.Setup(broker => broker.DeleteDebtAsync(inputDebt))
                .ReturnsAsync(true);

            // Act
            bool actualAns = await _debtService.DeleteDebtAsync(inputDebt);

            // Assert
            Assert.True(actualAns);
        }

        [Fact]
        public async void ShouldThrowDeleteFailedExceptionWhenDbFails()
        {
            // Arrange
            Debt inputDebt = CreateRandomDebt();

            var exception = new Exception(new MnemonicString().GetValue());

            _debtBrokerMock.Setup(broker => broker.DeleteDebtAsync(inputDebt)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<DeleteFailedException>(() => _debtService.DeleteDebtAsync(inputDebt));
        }
        #endregion

        #region Private Methods
        private Owner CreateRandomOwner()
        {
            var ownerFiller = new Filler<Owner>();
            ownerFiller.Setup()
                .OnProperty(p => p.OwnerId).Use(new IntRange(2001, 9999));

            Owner owner = ownerFiller.Create();
            return owner;
        }

        private Debt CreateRandomDebt()
        {
            var debtFiller = new Filler<Debt>();
            debtFiller.Setup()
                .OnProperty(p => p.DebtId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.OwnerId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.DebtDate).Use(DateTime.Now)
                .OnProperty(p => p.AnimalName).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.Cause).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.DebtAmount).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.PaidAmount).Use(new IntRange(2001, 9999));

            Debt debt = debtFiller.Create();
            return debt;
        }

        private List<Debt> CreateRandomDebtList()
        {
            int numOfItems = 10;
            var list = new List<Debt>();

            for (int i = 0; i < numOfItems; i++)
            {
                list.Add(CreateRandomDebt());
            }

            return list;
        }

        private DebtPage CreateRandomDebtPage()
        {
            var debtPageFiller = new Filler<DebtPage>();
            debtPageFiller.Setup()
                .OnProperty(p => p.Owner).Use(CreateRandomOwner())
                .OnProperty(p => p.DebtsList).Use(CreateRandomDebtList());

            DebtPage debtPage = debtPageFiller.Create();
            return debtPage;
        }

        private void SetupDebtPageBroker(DebtPage expectedResult)
        {
            int ownerId = expectedResult.Owner.OwnerId;

            _debtBrokerMock.Setup(broker => broker.GetOwnerByIdAsync(ownerId))
                .ReturnsAsync(expectedResult.Owner);
            _debtBrokerMock.Setup(broker => broker.GetDebtsListByOwnerIdAsync(ownerId))
                .ReturnsAsync(expectedResult.DebtsList);
            _debtBrokerMock.Setup(broker => broker.AddDebtAsync(CreateRandomDebt()))
                .ReturnsAsync(1);
            _debtBrokerMock.Setup(broker => broker.DeleteDebtAsync(CreateRandomDebt()))
                .ReturnsAsync(true);
        }
        #endregion
    }
}
