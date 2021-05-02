using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Exceptions;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;
using Petadmin.Models;
using Xunit;
using Petadmin.Repository.Interfaces;
using Petadmin.Services;
using Petadmin.Services.Interfaces;
using Tynamix.ObjectFiller;

namespace Petadmin.Test.Services
{
    public class OwnerServiceTests
    {
        #region Class Initialization
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IOwnerBroker> _ownerBrokerMock;
        private readonly IOwnerService _ownerService;
        private readonly Mock<ILogger<OwnerService>> _loggingMock;

        public OwnerServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _ownerBrokerMock = new Mock<IOwnerBroker>();
            _loggingMock = new Mock<ILogger<OwnerService>>();
            _ownerService = new OwnerService(_ownerBrokerMock.Object, _loggingMock.Object);
        }
        #endregion

        [Fact]
        public void ShouldCallOwnerBroker()
        {
            // Given - Setup
            Owner inputOwner = CreateRandomOwner();
            var expectedResult = inputOwner;

            _ownerBrokerMock.Setup(broker => broker.GetOwnerByIdAsync(inputOwner.OwnerId))
                .ReturnsAsync(inputOwner);

            // When - Actual running
            Owner actualResult = _ownerService.GetOwnerByIdAsync(inputOwner.OwnerId).Result;
            

            // Then - Verifying
            _ownerBrokerMock.Verify(broker => broker.GetOwnerByIdAsync(inputOwner.OwnerId), Times.Once, "Called Times.Once failed");
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }

        #region GET
        [Fact]
        public void ShouldGetOwnerById()
        {
            // Arrange
            Owner inputOwner = CreateRandomOwner();
            var expectedResult = inputOwner;

            _ownerBrokerMock.Setup(broker => broker.GetOwnerById(inputOwner.OwnerId))
                .Returns(inputOwner);

            // Act
            Owner actualOwner = _ownerService.GetOwnerById(inputOwner.OwnerId);

            // Assert
            actualOwner.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void ShouldGetOwnerByIdAsync()
        {
            // Arrange
            Owner inputOwner = CreateRandomOwner();
            var expectedResult = inputOwner;

            _ownerBrokerMock.Setup(broker => broker.GetOwnerByIdAsync(inputOwner.OwnerId))
                .ReturnsAsync(inputOwner);

            // Act
            Owner actualOwner = _ownerService.GetOwnerByIdAsync(inputOwner.OwnerId).Result;

            // Assert
            actualOwner.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void ShouldGetOwnerPageDataByOwnerId()
        {
            // Arrange
            OwnerPage inputOwnerPage = CreateRandomOwnerPage();
            int ownerId = inputOwnerPage.Owner.OwnerId;

            _ownerBrokerMock.Setup(broker => broker.GetOwnerByIdAsync(ownerId))
                .ReturnsAsync(inputOwnerPage.Owner);

            _ownerBrokerMock.Setup(broker => broker.GetOwnerTotalDebtAmountAsync(inputOwnerPage.Owner.OwnerId))
                .ReturnsAsync(inputOwnerPage.OwnerTotalDebtAmount);

            _ownerBrokerMock.Setup(broker => broker.GetAnimalsListByOwnerIdAsync(inputOwnerPage.Owner.OwnerId))
                .ReturnsAsync(inputOwnerPage.AnimalsList);

            //// Act
            OwnerPage actualOwnerPage = await _ownerService.GetOwnerPageByIdAsync(ownerId);

            //// Assert
            inputOwnerPage.Should().BeEquivalentTo(actualOwnerPage);
        }

        [Fact]
        public async void ShouldThrowGetFailedExceptionWhenDbFails()
        {
            // Arrange
            int id = new IntRange(0, 10000).GetValue();

            var exception = new Exception( 
                message: new MnemonicString().GetValue());

            _ownerBrokerMock.Setup(broker => broker.GetOwnerByIdAsync(id)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<GetFailedException>(() => _ownerService.GetOwnerByIdAsync(id));
        }
        #endregion

        #region ADD
        [Fact]
        public void ShouldAddOwnerAsync()
        {
            // Arrange
            Owner inputOwner = CreateRandomOwner();

            _ownerBrokerMock.Setup(broker => broker.AddOwnerAsync(inputOwner))
                .ReturnsAsync(inputOwner.OwnerId);

            // Act
            int actualOwnerId = _ownerService.AddOwnerAsync(inputOwner).Result;

            // Assert
            Assert.Equal(inputOwner.OwnerId, actualOwnerId);
        }

        [Fact]
        public async void ShouldThrowAddFailedExceptionWhenDbFails()
        {
            // Arrange
            Owner inputOwner = CreateRandomOwner();

            var exception = new Exception(new MnemonicString().GetValue());

            _ownerBrokerMock.Setup(broker => broker.AddOwnerAsync(inputOwner)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<AddFailedException>(() => _ownerService.AddOwnerAsync(inputOwner));
        }
        #endregion

        #region UPDATE
        [Fact]
        public void ShouldUpdateOwnerAsync()
        {
            // Arrange
            Owner inputOwner = CreateRandomOwner();

            _ownerBrokerMock.Setup(broker => broker.UpdateOwnerAsync(inputOwner))
                .ReturnsAsync(true);

            // Act
            bool actualAns = _ownerService.UpdateOwnerAsync(inputOwner).Result;

            // Assert
            Assert.True(actualAns);
        }

        [Fact]
        public async void ShouldThrowUpdateFailedExceptionWhenDbFails()
        {
            // Arrange
            Owner inputOwner = CreateRandomOwner();

            var exception = new Exception(new MnemonicString().GetValue());

            _ownerBrokerMock.Setup(broker => broker.UpdateOwnerAsync(inputOwner)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<UpdateFailedException>(() => _ownerService.UpdateOwnerAsync(inputOwner));
        }
        #endregion

        #region DELETE
        [Fact]
        public void ShouldDeleteOwnerAsync()
        {
            // Arrange
            Owner inputOwner = CreateRandomOwner();

            _ownerBrokerMock.Setup(broker => broker.DeleteOwnerAsync(inputOwner))
                .ReturnsAsync(true);

            // Act
            bool actualAns = _ownerService.DeleteOwnerAsync(inputOwner).Result;

            // Assert
            Assert.True(actualAns);
        }

        [Fact]
        public async void ShouldThrowDeleteFailedExceptionWhenDbFails()
        {
            // Arrange
            Owner inputOwner = CreateRandomOwner();

            var exception = new Exception(new MnemonicString().GetValue());

            _ownerBrokerMock.Setup(broker => broker.DeleteOwnerAsync(inputOwner)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<DeleteFailedException>(() => _ownerService.DeleteOwnerAsync(inputOwner));
        }
        #endregion

        #region SEARCH
        [Fact]
        public async void ShouldSearchOwner()
        {
            // Arrange
            Owner inputOwner = CreateRandomOwner();
            List<Owner> expectedResult = new List<Owner> {inputOwner};

            _ownerBrokerMock.Setup(broker => broker.FindOwnerAsync(inputOwner))
                .ReturnsAsync(expectedResult);

            // Act
            List<Owner> actualResult = (await _ownerService.FindOwnerAsync(inputOwner)).ToList();

            // Assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void ShouldThrowSearchFailedExceptionWhenSearchFails()
        {
            // Arrange
            Owner inputOwner = CreateRandomOwner();

            var exception = new Exception( 
                message: new MnemonicString().GetValue());

            _ownerBrokerMock.Setup(broker => broker.FindOwnerAsync(inputOwner)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<SearchFailedException>(() => _ownerService.FindOwnerAsync(inputOwner));
        }
        #endregion

        #region Private Methods
        private Owner CreateRandomOwner()
        {
            var ownerFiller = new Filler<Owner>();
            ownerFiller.Setup()
                .OnProperty(p => p.OwnerId).Use(new IntRange(2001, 9999));
            //owner.OwnerId = new IntRange(0, 10000).GetValue();

            Owner owner = ownerFiller.Create();
            return owner;
        }

        private OwnerPage CreateRandomOwnerPage()
        {
            var ownerFiller =  new Filler<Owner>();
            ownerFiller.Setup()
                .OnProperty(p => p.OwnerId).Use(new IntRange(2001, 9999));

            var ownerPageFiller = new Filler<OwnerPage>();
            ownerPageFiller.Setup()
                .OnProperty(p => p.Owner).Use(ownerFiller.Create())
                .OnProperty(p => p.AnimalsList).Use(new Filler<List<Animal>>().Create)
                .OnProperty(p => p.OwnerTotalDebtAmount).Use(new IntRange(-2001, 9999));

            OwnerPage ownerPage = ownerPageFiller.Create();
            return ownerPage;
        }
        #endregion
    }
}
