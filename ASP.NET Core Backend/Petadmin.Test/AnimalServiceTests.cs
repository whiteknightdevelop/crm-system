using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
    public class AnimalServiceTests
    {
        #region Class Initialization
        private readonly Mock<IAnimalBroker> _animalBrokerMock;
        private readonly Mock<IVisitBroker> _visitBrokerMock;
        private readonly IAnimalService _animalService;
        private readonly Mock<ILogger<AnimalService>> _loggingMock;

        public AnimalServiceTests()
        {
            _animalBrokerMock = new Mock<IAnimalBroker>();
            _visitBrokerMock = new Mock<IVisitBroker>();
            _loggingMock = new Mock<ILogger<AnimalService>>();
            _animalService = new AnimalService(_animalBrokerMock.Object, _visitBrokerMock.Object, _loggingMock.Object);
        }
        #endregion

        [Fact]
        public void ShouldCallAnimalBroker()
        {
            // Arrange
            AnimalPage expectedResult = CreateRandomAnimalPage();
            int animalId = expectedResult.Animal.AnimalId;

            SetupAnimalPageBroker(expectedResult);

            // Act
            AnimalPage actualResult = _animalService.GetAnimalPageByIdAsync(animalId).Result;
            
            // Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
            _animalBrokerMock.Verify(broker => broker.GetAnimalByIdAsync(animalId), Times.Once, "Called Times.Once failed");
        }

        #region GET
        [Fact]
        public async void ShouldGetAnimalPageLists()
        {
            // Arrange
            AnimalPageLists expectedResult = CreateRandomAnimalPageLists();

            SetupAnimalPageListsBroker(expectedResult);

            //// Act
            AnimalPageLists actualResult = await _animalService.GetAnimalPageLists();

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }

        [Fact]
        public async void ShouldGetPreventiveRemindersListByAnimalId()
        {
            // Arrange
            Animal animal = CreateRandomAnimal();
            int animalId = animal.AnimalId;
            IEnumerable<PreventiveReminder> expectedResult = CreateRandomPreventiveReminderList(animalId);

            _animalBrokerMock.Setup(broker => broker.GetPreventiveRemindersListAsync(animalId))
                .ReturnsAsync(expectedResult);

            //// Act
            IEnumerable<PreventiveReminder> actualResult = await _animalService.GetPreventiveRemindersListAsync(animalId);

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }

        [Fact]
        public async void ShouldGetAnimalPageDataByAnimalId()
        {
            // Arrange
            AnimalPage expectedResult = CreateRandomAnimalPage();
            int animalId = expectedResult.Animal.AnimalId;

            SetupAnimalPageBroker(expectedResult);

            //// Act
            AnimalPage actualResult = await _animalService.GetAnimalPageByIdAsync(animalId);

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }

        [Fact]
        public async void ShouldThrowGetFailedExceptionWhenDbFails()
        {
            // Arrange
            int id = new IntRange(0, 10000).GetValue();

            var exception = new Exception( 
                message: new MnemonicString().GetValue());

            _animalBrokerMock.Setup(broker => broker.GetAnimalById(id)).Throws(exception);
            _animalBrokerMock.Setup(broker => broker.GetVisitsListByAnimalIdAsync(id)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<GetFailedException>(() => _animalService.GetAnimalPageByIdAsync(id));
        }
        #endregion

        #region ADD
        [Fact]
        public async void ShouldAddAnimalAsync()
        {
            // Arrange
            Animal inputAnimal = CreateRandomAnimal();
            var expectedResult = inputAnimal.AnimalId;

            _animalBrokerMock.Setup(broker => broker.AddAnimalAsync(inputAnimal))
                .ReturnsAsync(inputAnimal.AnimalId);

            // Act
            int actualAnimalId = await _animalService.AddAnimalAsync(inputAnimal);

            // Assert
            Assert.Equal(actualAnimalId, expectedResult);
        }

        [Fact]
        public async void ShouldAddPreventiveReminderAsync()
        {
            // Arrange
            PreventiveReminder expectedResult = CreateRandomPreventiveReminder();

            _animalBrokerMock.Setup(broker => broker.AddPreventiveReminderAsync(expectedResult))
                .ReturnsAsync(expectedResult.ReminderId);

            // Act
            int actualAns = await _animalService.AddPreventiveReminderAsync(expectedResult);

            // Assert
            Assert.Equal(expectedResult.ReminderId, actualAns);
        }

        [Fact]
        public async void ShouldThrowAddFailedExceptionWhenDbFails()
        {
            // Arrange
            Animal inputAnimal = CreateRandomAnimal();

            var exception = new Exception(new MnemonicString().GetValue());

            _animalBrokerMock.Setup(broker => broker.AddAnimalAsync(inputAnimal)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<AddFailedException>(() => _animalService.AddAnimalAsync(inputAnimal));
        }
        #endregion

        #region UPDATE
        [Fact]
        public async void ShouldUpdateAnimalAsync()
        {
            // Arrange
            Animal inputAnimal = CreateRandomAnimal();

            _animalBrokerMock.Setup(broker => broker.UpdateAnimalAsync(inputAnimal))
                .ReturnsAsync(true);

            // Act
            bool actualResult = await _animalService.UpdateAnimalAsync(inputAnimal);

            // Assert
            Assert.True(actualResult);
        }

        [Fact]
        public async void ShouldThrowUpdateFailedExceptionWhenDbFails()
        {
            // Arrange
            Animal inputAnimal = CreateRandomAnimal();

            var exception = new Exception(new MnemonicString().GetValue());

            _animalBrokerMock.Setup(broker => broker.UpdateAnimalAsync(inputAnimal)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<UpdateFailedException>(() => _animalService.UpdateAnimalAsync(inputAnimal));
        }
        #endregion

        #region DELETE
        [Fact]
        public async void ShouldDeleteAnimalAsync()
        {
            // Arrange
            Animal inputAnimal = CreateRandomAnimal();

            _animalBrokerMock.Setup(broker => broker.DeleteAnimalAsync(inputAnimal))
                .ReturnsAsync(true);

            // Act
            bool actualAns = await _animalService.DeleteAnimalAsync(inputAnimal);

            // Assert
            Assert.True(actualAns);
        }

        [Fact]
        public async void ShouldDeleteSelectedRemindersAsync()
        {
            // Arrange
            Animal animal = CreateRandomAnimal();
            int animalId = animal.AnimalId;
            List<PreventiveReminder> list = CreateRandomPreventiveReminderList(animalId).ToList();
            PreventiveReminder reminder = CreateRandomPreventiveReminder();

            _animalBrokerMock.Setup(broker => broker.DeleteReminderAsync(reminder))
                .ReturnsAsync(true);

            // Act
            bool actualAns = await _animalService.DeleteSelectedRemindersAsync(list);

            // Assert
            Assert.True(actualAns);
        }

        [Fact]
        public async void ShouldThrowDeleteFailedExceptionWhenDbFails()
        {
            // Arrange
            Animal inputAnimal = CreateRandomAnimal();

            var exception = new Exception(new MnemonicString().GetValue());

            _animalBrokerMock.Setup(broker => broker.DeleteAnimalAsync(inputAnimal)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<DeleteFailedException>(() => _animalService.DeleteAnimalAsync(inputAnimal));
        }

        [Fact(Skip="Not The Same Token!")]
        public async void ShouldThrowOperationCanceledExceptionWhenOneOfRemindersFailsToDelete()
        {
            // Arrange
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            cts.Cancel();
            
            Animal animal = CreateRandomAnimal();
            int animalId = animal.AnimalId;
            List<PreventiveReminder> list = CreateRandomPreventiveReminderList(animalId).ToList();
            PreventiveReminder reminder = CreateRandomPreventiveReminder();
            var exception = new Exception(new MnemonicString().GetValue());
            
            _animalBrokerMock.Setup(broker => broker.DeleteReminderAsync(reminder, cts, token))
                .ReturnsAsync(false);

            //// Act + Assert
            await Assert.ThrowsAsync<TaskCanceledException >(() => _animalService.DeleteSelectedRemindersAsync(list));
        }
        #endregion

        #region SEARCH
        [Fact]
        public async void ShouldSearchAnimal()
        {
            // Arrange
            Animal inputAnimal = CreateRandomAnimal();
            List<AnimalSearch> expectedResult = CreateRandomAnimalSearchList().ToList();

            _animalBrokerMock.Setup(broker => broker.FindAnimalAsync(inputAnimal))
                .ReturnsAsync(expectedResult);

            // Act
            List<AnimalSearch> actualResult = (await _animalService.FindAnimalAsync(inputAnimal)).ToList();

            // Assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void ShouldThrowSearchFailedExceptionWhenSearchFails()
        {
            // Arrange
            Animal inputAnimal = CreateRandomAnimal();

            var exception = new Exception( 
                message: new MnemonicString().GetValue());

            _animalBrokerMock.Setup(broker => broker.FindAnimalAsync(inputAnimal)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<SearchFailedException>(() => _animalService.FindAnimalAsync(inputAnimal));
        }
        #endregion

        #region Private Methods
        private Animal CreateRandomAnimal()
        {
            var animalFiller = new Filler<Animal>();
            animalFiller.Setup()
                .OnProperty(p => p.AnimalId).Use(new IntRange(2001, 9999));

            Animal animal = animalFiller.Create();
            return animal;
        }

        private Owner CreateRandomOwner()
        {
            var ownerFiller = new Filler<Owner>();
            ownerFiller.Setup()
                .OnProperty(p => p.OwnerId).Use(new IntRange(2001, 9999));

            Owner owner = ownerFiller.Create();
            return owner;
        }

        private AnimalPage CreateRandomAnimalPage()
        {
            var animalFiller =  new Filler<Animal>();
            animalFiller.Setup()
                .OnProperty(p => p.AnimalId).Use(new IntRange(2001, 9999));

            var animalPageFiller = new Filler<AnimalPage>();
            animalPageFiller.Setup()
                .OnProperty(p => p.Animal).Use(animalFiller.Create())
                .OnProperty(p => p.VisitsList).Use(new Filler<List<Visit>>().Create)
                .OnProperty(p => p.Lists.TypesList).Use(new Filler<List<string>>().Create)
                .OnProperty(p => p.Lists.GendersList).Use(new Filler<List<string>>().Create)
                .OnProperty(p => p.Lists.BreedsList).Use(new Filler<List<Breed>>().Create)
                .OnProperty(p => p.Lists.ColorsList).Use(new Filler<List<string>>().Create)
                .OnProperty(p => p.RemindersList).Use(new Filler<List<string>>().Create);

            AnimalPage animalPage = animalPageFiller.Create();
            return animalPage;
        }

        private AnimalPageLists CreateRandomAnimalPageLists()
        {
            var animalPageListsFiller = new Filler<AnimalPageLists>();
            animalPageListsFiller.Setup()
                .OnProperty(p => p.TypesList).Use(new Filler<List<string>>().Create)
                .OnProperty(p => p.GendersList).Use(new Filler<List<string>>().Create)
                .OnProperty(p => p.BreedsList).Use(new Filler<List<Breed>>().Create)
                .OnProperty(p => p.ColorsList).Use(new Filler<List<string>>().Create);

            AnimalPageLists animalPageLists = animalPageListsFiller.Create();
            return animalPageLists;
        }
        
        private PreventiveReminder CreateRandomPreventiveReminder()
        {
            var preventiveReminderFiller = new Filler<PreventiveReminder>();
            preventiveReminderFiller.Setup()
                .OnProperty(p => p.ReminderId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.AnimalId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.VisitId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.TreatmentId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.PreventiveReminderName).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.ReminderDate).Use(new DateTime(2014, 4, 2))
                .OnProperty(p => p.RemainingNumOfDays).Use(new IntRange(-9999, 9999))
                .OnProperty(p => p.IsReminderChecked).IgnoreIt()
                .OnProperty(p => p.IsReminderSent).IgnoreIt()
                .OnProperty(p => p.IsReminderDeleted).IgnoreIt()
                .OnProperty(p => p.PreventiveTreatmentType).IgnoreIt()
                .OnProperty(p => p.RemainingNumOfDays).Use(new IntRange(1, 2))
                ;

            PreventiveReminder preventiveReminder = preventiveReminderFiller.Create();
            return preventiveReminder;
        }

        private IEnumerable<PreventiveReminder> CreateRandomPreventiveReminderList(int animalId)
        {
            int listLength = new IntRange(2, 10).GetValue();

            List<PreventiveReminder> list = new List<PreventiveReminder>();
            for (var i = 0; i < listLength; i++)
            {
                PreventiveReminder preventiveReminder = CreateRandomPreventiveReminder();
                preventiveReminder.AnimalId = animalId;
                list.Add(preventiveReminder);
            }

            return list;
        }

        private void SetupAnimalPageListsBroker(AnimalPageLists expectedResult)
        {
            _animalBrokerMock.Setup(broker => broker.GetAnimalsTypesListAsync())
                .ReturnsAsync(expectedResult.TypesList);

            _animalBrokerMock.Setup(broker => broker.GetAnimalsGendersListAsync())
                .ReturnsAsync(expectedResult.GendersList);

            _animalBrokerMock.Setup(broker => broker.GetAnimalsBreedsListAsync())
                .ReturnsAsync(expectedResult.BreedsList);

            _animalBrokerMock.Setup(broker => broker.GetAnimalsColorsListAsync())
                .ReturnsAsync(expectedResult.ColorsList);

        }

        private void SetupAnimalPageBroker(AnimalPage expectedResult)
        {
            int animalId = expectedResult.Animal.AnimalId;

            _animalBrokerMock.Setup(broker => broker.GetAnimalByIdAsync(animalId))
                .ReturnsAsync(expectedResult.Animal);

            _animalBrokerMock.Setup(broker => broker.GetAnimalOwnerByIdAsync(animalId))
                .ReturnsAsync(expectedResult.AnimalOwner);

            _animalBrokerMock.Setup(broker => broker.GetVisitsListByAnimalIdAsync(animalId))
                .ReturnsAsync(expectedResult.VisitsList);

            _animalBrokerMock.Setup(broker => broker.GetVisitsListByAnimalIdAsync(animalId))
                .ReturnsAsync(expectedResult.VisitsList);

            _animalBrokerMock.Setup(broker => broker.GetAnimalsRemindersListAsync())
                .ReturnsAsync(expectedResult.RemindersList);

            SetupAnimalPageListsBroker(expectedResult.Lists);

            _animalBrokerMock.Setup(broker => broker.GetPreventiveRemindersListAsync(animalId))
                .ReturnsAsync(expectedResult.preventiveRemindersList);
        }


        private AnimalSearch CreateRandomAnimalSearch()
        {
            AnimalSearch animalSearch = new AnimalSearch
            {
                Animal = CreateRandomAnimal(),
                Owner = CreateRandomOwner()
            };
            return animalSearch;
        }

        private IEnumerable<AnimalSearch> CreateRandomAnimalSearchList()
        {
            int listLength = new IntRange(2, 10).GetValue();

            List<AnimalSearch> list = new List<AnimalSearch>();
            for (var i = 0; i < listLength; i++)
            {
                AnimalSearch animalSearch = CreateRandomAnimalSearch();
                list.Add(animalSearch);
            }
            return list;
        }
        #endregion














    }
}
