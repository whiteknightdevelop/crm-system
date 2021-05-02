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
    public class PrescriptionServiceTests
    {
        #region Class Initialization
        private readonly Mock<IPrescriptionBroker> _prescriptionBrokerMock;
        private readonly IPrescriptionService _prescriptionService;
        private readonly Mock<ILogger<PrescriptionService>> _loggingMock;

        public PrescriptionServiceTests()
        {
            _prescriptionBrokerMock = new Mock<IPrescriptionBroker>();
            _loggingMock = new Mock<ILogger<PrescriptionService>>();
            _prescriptionService = new PrescriptionService(_prescriptionBrokerMock.Object, _loggingMock.Object);
        }
        #endregion

        
        [Fact]
        public async void ShouldCallPrescriptionBroker()
        {
            // Arrange
            PrescriptionPage expectedResult = CreateRandomPrescriptionPage();
            int visitId = expectedResult.Visit.VisitId;

            SetupPrescriptionPageBroker(expectedResult);

            // Act
            await _prescriptionService.GetPrescriptionPageByVisitIdAsync(visitId);
            
            // Assert
            _prescriptionBrokerMock.Verify(broker => broker.GetVisitByIdAsync(visitId), Times.Once, "Called Times.Once failed");
        }


        #region GET
        [Fact]
        public async void ShouldGetPrescriptionPageDataByVisitId()
        {
            // Arrange
            PrescriptionPage expectedResult = CreateRandomPrescriptionPage();
            int visitId = expectedResult.Visit.VisitId;

            SetupPrescriptionPageBroker(expectedResult);

            //// Act
            PrescriptionPage actualResult = await _prescriptionService.GetPrescriptionPageByVisitIdAsync(visitId);

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }

        [Fact]
        public async void ShouldGetPrescriptionsListByVisitId()
        {
            // Arrange
            int visitId = new IntRange(2001, 9999).GetValue();
            List<Prescription> expectedResult = CreateRandomPrescriptionList();

            _prescriptionBrokerMock.Setup(broker => broker.GetPrescriptionsListByVisitIdAsync(visitId))
                .ReturnsAsync(expectedResult);

            //// Act
            List<Prescription> actualResult = (await _prescriptionService.GetPrescriptionsListByVisitIdAsync(visitId)).ToList();

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }

        [Fact]
        public async void ShouldThrowGetFailedExceptionWhenDbFails()
        {
            // Arrange
            PrescriptionPage expectedResult = CreateRandomPrescriptionPage();
            int visitId = expectedResult.Visit.VisitId;

            var exception = new Exception(
                message: new MnemonicString().GetValue());

            _prescriptionBrokerMock.Setup(broker => broker.GetPrescriptionsListByVisitIdAsync(visitId)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<GetFailedException>(() => _prescriptionService.GetPrescriptionsListByVisitIdAsync(visitId));
        }
        #endregion

        #region ADD
        [Fact]
        public async void ShouldAddPrescriptionAsync()
        {
            // Arrange
            Prescription inputPrescription = CreateRandomPrescription();
            var expectedResult = inputPrescription.VisitId;

             _prescriptionBrokerMock.Setup(broker => broker.AddPrescriptionAsync(inputPrescription))
            .ReturnsAsync(expectedResult);

            // Act
            int actualVisitId = await _prescriptionService.AddPrescriptionAsync(inputPrescription);

            // Assert
            Assert.Equal(actualVisitId, expectedResult);
        }

        [Fact]
        public async void ShouldThrowAddFailedExceptionWhenDbFails()
        {
            // Arrange
            Prescription inputPrescription = CreateRandomPrescription();

            var exception = new Exception(new MnemonicString().GetValue());

            _prescriptionBrokerMock.Setup(broker => broker.AddPrescriptionAsync(inputPrescription)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<AddFailedException>(() => _prescriptionService.AddPrescriptionAsync(inputPrescription));
        }
        #endregion

        #region DELETE
        [Fact]
        public async void ShouldDeletePrescriptiontAsync()
        {
            // Arrange
            Prescription inputPrescription = CreateRandomPrescription();

            _prescriptionBrokerMock.Setup(broker => broker.DeletePrescriptiontAsync(inputPrescription))
                .ReturnsAsync(true);

            // Act
            bool actualAns = await _prescriptionService.DeletePrescriptiontAsync(inputPrescription);

            // Assert
            Assert.True(actualAns);
        }

        [Fact]
        public async void ShouldThrowDeleteFailedExceptionWhenDbFails()
        {
            // Arrange
            Prescription inputPrescription = CreateRandomPrescription();

            var exception = new Exception(new MnemonicString().GetValue());

            _prescriptionBrokerMock.Setup(broker => broker.DeletePrescriptiontAsync(inputPrescription)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<DeleteFailedException>(() => _prescriptionService.DeletePrescriptiontAsync(inputPrescription));
        }
        #endregion

        #region Private Methods
        private Visit CreateRandomVisit()
        {
            var visitFiller = new Filler<Visit>();
            visitFiller.Setup()
                .OnProperty(p => p.VisitId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.AnimalId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.VisitTime).Use(DateTime.Now)
                .OnProperty(p => p.Cause).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.Symptoms).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.Comment).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.Temperature).IgnoreIt()
                .OnProperty(p => p.Weight).IgnoreIt()
                .OnProperty(p => p.Pulse).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.Diagnosis1).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.Diagnosis2).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.Diagnosis3).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.Treatment1).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.Treatment2).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.Treatment3).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.Treatment4).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.Treatment5).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.Treatment6).Use(new Lipsum(LipsumFlavor.LoremIpsum));

            Visit visit = visitFiller.Create();
            return visit;
        }

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

        private Prescription CreateRandomPrescription()
        {
            var prescriptionFiller = new Filler<Prescription>();
            prescriptionFiller.Setup()
                .OnProperty(p => p.PrescriptionId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.VisitId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.DrugName).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.DrugFrequency).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.DrugDosage).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.DrugPeriod).Use(new Lipsum(LipsumFlavor.LoremIpsum))
                .OnProperty(p => p.DrugComment).Use(new Lipsum(LipsumFlavor.LoremIpsum));

            Prescription prescription = prescriptionFiller.Create();
            return prescription;
        }

        private List<Prescription> CreateRandomPrescriptionList()
        {
            int numOfItems = 10;
            var list = new List<Prescription>();

            for (int i = 0; i < numOfItems; i++)
            {
                list.Add(CreateRandomPrescription());
            }
            return list;
        }

        private List<string> CreateRandomStringList()
        {
            int numOfItems = 10;
            var list = new List<string>();
            var str = new Lipsum(LipsumFlavor.LoremIpsum, 3, 20).ToString();

            for (int i = 0; i < numOfItems; i++)
            {
                list.Add(str);
            }
            return list;
        }

        private PrescriptionPage CreateRandomPrescriptionPage()
        {
            var prescriptionPageFiller = new Filler<PrescriptionPage>();
            prescriptionPageFiller.Setup()
                .OnProperty(p => p.Visit).Use(CreateRandomVisit())
                .OnProperty(p => p.Animal).Use(CreateRandomAnimal())
                .OnProperty(p => p.Owner).Use(CreateRandomOwner())
                .OnProperty(p => p.PrescriptionsList).Use(CreateRandomPrescriptionList())
                .OnProperty(p => p.DrugsList).Use(CreateRandomStringList())
                .OnProperty(p => p.PeriodsList).Use(CreateRandomStringList())
                .OnProperty(p => p.FrequencysList).Use(CreateRandomStringList())
                .OnProperty(p => p.DosagesList).Use(CreateRandomStringList());

            PrescriptionPage prescriptionPage = prescriptionPageFiller.Create();
            return prescriptionPage;
        }

        private void SetupPrescriptionPageBroker(PrescriptionPage expectedResult)
        {
            int visitId = expectedResult.Visit.VisitId;

            _prescriptionBrokerMock.Setup(broker => broker.GetVisitByIdAsync(visitId))
                .ReturnsAsync(expectedResult.Visit);
            _prescriptionBrokerMock.Setup(broker => broker.GetAnimalByVisitIdAsync(visitId))
                .ReturnsAsync(expectedResult.Animal);
            _prescriptionBrokerMock.Setup(broker => broker.GetOwnerByVisitIdAsync(visitId))
                .ReturnsAsync(expectedResult.Owner);
            _prescriptionBrokerMock.Setup(broker => broker.GetPrescriptionsListByVisitIdAsync(visitId))
                .ReturnsAsync(expectedResult.PrescriptionsList);
            _prescriptionBrokerMock.Setup(broker => broker.GetDrugsListAsync())
                .ReturnsAsync(expectedResult.DrugsList);
            _prescriptionBrokerMock.Setup(broker => broker.GetDrugPeriodsListAsync())
                .ReturnsAsync(expectedResult.PeriodsList);
            _prescriptionBrokerMock.Setup(broker => broker.GetDrugFrequencysListAsync())
                .ReturnsAsync(expectedResult.FrequencysList);
            _prescriptionBrokerMock.Setup(broker => broker.GetDrugDosagesListAsync())
                .ReturnsAsync(expectedResult.DosagesList);
            _prescriptionBrokerMock.Setup(broker => broker.AddPrescriptionAsync(CreateRandomPrescription()))
                .ReturnsAsync(1);
            _prescriptionBrokerMock.Setup(broker => broker.DeletePrescriptiontAsync(CreateRandomPrescription()))
                .ReturnsAsync(true);
        }
        #endregion
    }
}
