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
using Petadmin.Services;
using Petadmin.Services.Interfaces;
using Tynamix.ObjectFiller;
using Xunit;

namespace Petadmin.Test.Services
{
    public class FollowUpServiceTests
    {
        #region Class Initialization
        private readonly Mock<IFollowUpBroker> _followUpBrokerMock;
        private readonly IFollowUpService _followUpService;
        private readonly Mock<ILogger<FollowUpService>> _loggingMock;

        public FollowUpServiceTests()
        {
            _followUpBrokerMock = new Mock<IFollowUpBroker>();
            _loggingMock = new Mock<ILogger<FollowUpService>>();
            _followUpService = new FollowUpService(_followUpBrokerMock.Object, _loggingMock.Object);
        }
        #endregion

        [Fact]
        public async void ShouldCallFollowUpBroker()
        {
            // Arrange
            FollowUpPage expectedResult = CreateRandomFollowUpPage();
            int animalId = expectedResult.Animal.AnimalId;

            SetupFollowUpPageBroker(expectedResult);

            // Act
            await _followUpService.GetFollowUpPageByAnimalIdAsync(animalId);
            
            // Assert
            _followUpBrokerMock.Verify(broker => broker.GetAnimalByIdAsync(animalId), Times.Once, "Called Times.Once failed");
        }

        #region GET
        [Fact]
        public async void ShouldGetFollowUpPageByAnimalIdAsync()
        {
            // Arrange
            FollowUpPage expectedResult = CreateRandomFollowUpPage();
            int animalId = expectedResult.Animal.AnimalId;

            SetupFollowUpPageBroker(expectedResult);

            //// Act
            FollowUpPage actualResult = await _followUpService.GetFollowUpPageByAnimalIdAsync(animalId);

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }

        [Fact]
        public async void ShouldGetFollowUpsListByAnimalIdAsync()
        {
            // Arrange
            int animalId = new IntRange(2001, 9999).GetValue();
            List<FollowUp> expectedResult = CreateRandomFollowUpList();

            _followUpBrokerMock.Setup(broker => broker.GetFollowUpsListByAnimalIdAsync(animalId))
                .ReturnsAsync(expectedResult);

            //// Act
            List<FollowUp> actualResult = (await _followUpService.GetFollowUpsListByAnimalIdAsync(animalId)).ToList();

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }

        [Fact]
        public async void ShouldGetFollowUpAllListAsync()
        {
            // Arrange
            DateTime from = new DateTime();

            List<FollowUpAllItem> expectedResult = CreateRandomFollowUpAllList();

            _followUpBrokerMock.Setup(broker => broker.GetFollowUpAllList(from))
                .ReturnsAsync(expectedResult);

            //// Act
            List<FollowUpAllItem> actualResult = (await _followUpService.GetFollowUpAllList(from)).ToList();

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }
        
        [Fact]
        public async void ShouldThrowGetFailedExceptionWhenDbFails()
        {
            // Arrange
            FollowUpPage expectedResult = CreateRandomFollowUpPage();
            int animalId = expectedResult.Animal.AnimalId;

            var exception = new Exception(
                message: new MnemonicString().GetValue());

            _followUpBrokerMock.Setup(broker => broker.GetFollowUpsListByAnimalIdAsync(animalId)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<GetFailedException>(() => _followUpService.GetFollowUpsListByAnimalIdAsync(animalId));
        }
        #endregion

        #region ADD
        [Fact]
        public async void ShouldAddFollowUpAsync()
        {
            // Arrange
            FollowUp inputFollowUp = CreateRandomFollowUp();
            var expectedResult = inputFollowUp.AnimalId;

            _followUpBrokerMock.Setup(broker => broker.AddFollowUpAsync(inputFollowUp))
                .ReturnsAsync(expectedResult);

            // Act
            int actualAnimalId = await _followUpService.AddFollowUpAsync(inputFollowUp);

            // Assert
            Assert.Equal(actualAnimalId, expectedResult);
        }

        [Fact]
        public async void ShouldThrowAddFailedExceptionWhenDbFails()
        {
            // Arrange
            FollowUp inputFollowUp = CreateRandomFollowUp();

            var exception = new Exception(new MnemonicString().GetValue());

            _followUpBrokerMock.Setup(broker => broker.AddFollowUpAsync(inputFollowUp)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<AddFailedException>(() => _followUpService.AddFollowUpAsync(inputFollowUp));
        }
        #endregion

        #region UPDATE
        [Fact]
        public async void ShouldUpdateDebtAsync()
        {
            // Arrange
            FollowUp inputFollowUp = CreateRandomFollowUp();

            _followUpBrokerMock.Setup(broker => broker.UpdateFollowUpAsync(inputFollowUp))
                .ReturnsAsync(true);

            // Act
            bool actualResult = await _followUpService.UpdateFollowUpAsync(inputFollowUp);

            // Assert
            Assert.True(actualResult);
        }

        [Fact]
        public async void ShouldThrowUpdateFailedExceptionWhenDbFails()
        {
            // Arrange
            FollowUp inputFollowUp = CreateRandomFollowUp();

            var exception = new Exception(new MnemonicString().GetValue());

            _followUpBrokerMock.Setup(broker => broker.UpdateFollowUpAsync(inputFollowUp)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<UpdateFailedException>(() => _followUpService.UpdateFollowUpAsync(inputFollowUp));
        }
        #endregion

        #region DELETE
        [Fact]
        public async void ShouldDeleteDebtAsync()
        {
            // Arrange
            FollowUp inputFollowUp = CreateRandomFollowUp();

            _followUpBrokerMock.Setup(broker => broker.DeleteFollowUpAsync(inputFollowUp))
                .ReturnsAsync(true);

            // Act
            bool actualAns = await _followUpService.DeleteFollowUpAsync(inputFollowUp);

            // Assert
            Assert.True(actualAns);
        }

        [Fact]
        public async void ShouldThrowDeleteFailedExceptionWhenDbFails()
        {
            // Arrange
            FollowUp inputFollowUp = CreateRandomFollowUp();

            var exception = new Exception(new MnemonicString().GetValue());

            _followUpBrokerMock.Setup(broker => broker.DeleteFollowUpAsync(inputFollowUp)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<DeleteFailedException>(() => _followUpService.DeleteFollowUpAsync(inputFollowUp));
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

        private Animal CreateRandomAnimal()
        {
            var animalFiller = new Filler<Animal>();
            animalFiller.Setup()
                .OnProperty(p => p.AnimalId).Use(new IntRange(2001, 9999));

            Animal animal = animalFiller.Create();
            return animal;
        }

        private FollowUp CreateRandomFollowUp()
        {
            var followUpFiller = new Filler<FollowUp>();
            followUpFiller.Setup()
                .OnProperty(p => p.FollowUpId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.AnimalId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.Date).Use(DateTime.Now)
                .OnProperty(p => p.Cause).Use(new Lipsum(LipsumFlavor.LoremIpsum));

            FollowUp followUp = followUpFiller.Create();
            return followUp;
        }

        private FollowUpAllItem CreateRandomFollowUpAllItem()
        {
            var followUpFiller = new Filler<FollowUpAllItem>();
            followUpFiller.Setup()
                .OnProperty(p => p.Animal).Use(CreateRandomAnimal())
                .OnProperty(p => p.Owner).Use(CreateRandomOwner())
                .OnProperty(p => p.FollowUp).Use(CreateRandomFollowUp);

            FollowUpAllItem followUpAllItem = followUpFiller.Create();
            return followUpAllItem;
        }

        private List<FollowUp> CreateRandomFollowUpList()
        {
            int numOfItems = 10;
            var list = new List<FollowUp>();

            for (int i = 0; i < numOfItems; i++)
            {
                list.Add(CreateRandomFollowUp());
            }
            return list;
        }

        private List<FollowUpAllItem> CreateRandomFollowUpAllList()
        {
            int numOfItems = 10;
            var list = new List<FollowUpAllItem>();

            for (int i = 0; i < numOfItems; i++)
            {
                list.Add(CreateRandomFollowUpAllItem());
            }
            return list;
        }

        private FollowUpPage CreateRandomFollowUpPage()
        {
            var followUpPageFiller = new Filler<FollowUpPage>();
            followUpPageFiller.Setup()
                .OnProperty(p => p.Animal).Use(CreateRandomAnimal())
                .OnProperty(p => p.Owner).Use(CreateRandomOwner())
                .OnProperty(p => p.FollowUpsList).Use(CreateRandomFollowUpList());

            FollowUpPage followUpPage = followUpPageFiller.Create();
            return followUpPage;
        }

        private void SetupFollowUpPageBroker(FollowUpPage expectedResult)
        {
            int animalId = expectedResult.Animal.AnimalId;

            _followUpBrokerMock.Setup(broker => broker.GetAnimalByIdAsync(animalId))
                .ReturnsAsync(expectedResult.Animal);
            _followUpBrokerMock.Setup(broker => broker.GetOwnerByAnimalIdAsync(animalId))
                .ReturnsAsync(expectedResult.Owner);
            _followUpBrokerMock.Setup(broker => broker.GetFollowUpsListByAnimalIdAsync(animalId))
                .ReturnsAsync(expectedResult.FollowUpsList);
            _followUpBrokerMock.Setup(broker => broker.AddFollowUpAsync(CreateRandomFollowUp()))
                .ReturnsAsync(1);
            _followUpBrokerMock.Setup(broker => broker.UpdateFollowUpAsync(CreateRandomFollowUp()))
                .ReturnsAsync(true);
            _followUpBrokerMock.Setup(broker => broker.DeleteFollowUpAsync(CreateRandomFollowUp()))
                .ReturnsAsync(true);
        }
        #endregion
    }
}
