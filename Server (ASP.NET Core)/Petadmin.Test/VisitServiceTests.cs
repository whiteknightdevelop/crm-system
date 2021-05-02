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
    public class VisitServiceTests
    {
        #region Class Initialization
        private readonly Mock<IVisitBroker> _visitBrokerMock;
        private readonly IVisitService _visitService;
        private readonly Mock<ILogger<VisitService>> _loggingMock;

        public VisitServiceTests()
        {
            _visitBrokerMock = new Mock<IVisitBroker>();
            _loggingMock = new Mock<ILogger<VisitService>>();
            _visitService = new VisitService(_visitBrokerMock.Object, _loggingMock.Object);
        }
        #endregion

        [Fact]
        public async void ShouldCallVisitBroker()
        {
            // Arrange
            VisitPage expectedResult = CreateRandomVisitPage();
            int visitId = expectedResult.Visit.VisitId;

            SetupVisitPageBroker(expectedResult);

            // Act
            VisitPage actualResult = await _visitService.GetVisitPageByIdAsync(visitId);
            
            // Assert
            _visitBrokerMock.Verify(broker => broker.GetVisitByIdAsync(visitId), Times.Once, "Called Times.Once failed");
        }

        #region GET
        [Fact]
        public async void ShouldGetVisitPageDataByVisitId()
        {
            // Arrange
            VisitPage expectedResult = CreateRandomVisitPage();
            int visitId = expectedResult.Visit.VisitId;

            SetupVisitPageBroker(expectedResult);

            //// Act
            VisitPage actualResult = await _visitService.GetVisitPageByIdAsync(visitId);

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }

        [Fact]
        public async void ShouldGetVisitPageLists()
        {
            // Arrange
            VisitPageLists expectedResult = CreateRandomVisitPageLists();

            SetupVisitPageListsBroker(expectedResult);

            //// Act
            VisitPageLists actualResult = await _visitService.GetVisitPageLists();

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }
        

        [Fact]
        public async void ShouldGetPreventiveTreatmentsListByVisitId()
        {
            // Arrange
            int visitId = new IntRange(2001, 9999).GetValue();
            List<PreventiveTreatment> expectedResult = CreateRandomPreventiveTreatmentList();


            _visitBrokerMock.Setup(broker => broker.GetPreventiveTreatmentsListByVisitIdAsync(visitId))
                .ReturnsAsync(expectedResult);

            //// Act
            List<PreventiveTreatment> actualResult = (await _visitService.GetPreventiveTreatmentsListByVisitIdAsync(visitId)).ToList();

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }
        

        [Fact]
        public async void ShouldThrowGetFailedExceptionWhenDbFails()
        {
            // Arrange
            VisitPage expectedResult = CreateRandomVisitPage();
            int visitId = expectedResult.Visit.VisitId;

            var exception = new Exception( 
                message: new MnemonicString().GetValue());

            _visitBrokerMock.Setup(broker => broker.GetVisitByIdAsync(visitId)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<GetFailedException>(() => _visitService.GetVisitPageByIdAsync(visitId));
        }
        #endregion

        #region ADD
        [Fact]
        public async void ShouldAddVisitAsync()
        {
            // Arrange
            Visit inputVisit = CreateRandomVisit();
            var expectedResult = inputVisit.VisitId;

            _visitBrokerMock.Setup(broker => broker.AddVisitAsync(inputVisit))
                .ReturnsAsync(expectedResult);

            // Act
            int actualVisitId = await _visitService.AddVisitAsync(inputVisit);

            // Assert
            Assert.Equal(actualVisitId, expectedResult);
        }

        [Fact]
        public async void ShouldAddPreventiveTreatmentAsync()
        {
            // Arrange
            PreventiveTreatment inputPreventiveTreatment = CreateRandomPreventiveTreatment();
            var expectedResult = inputPreventiveTreatment.TreatmentId;

            _visitBrokerMock.Setup(broker => broker.AddPreventiveTreatmentAsync(inputPreventiveTreatment))
                .ReturnsAsync(expectedResult);

            // Act
            int actualPreventiveTreatmentId = await _visitService.AddPreventiveTreatmentAsync(inputPreventiveTreatment);

            // Assert
            Assert.Equal(actualPreventiveTreatmentId, expectedResult);
        }

        [Fact]
        public async void ShouldThrowAddFailedExceptionWhenDbFails()
        {
            // Arrange
            Visit inputVisit = CreateRandomVisit();

            var exception = new Exception(new MnemonicString().GetValue());

            _visitBrokerMock.Setup(broker => broker.AddVisitAsync(inputVisit)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<AddFailedException>(() => _visitService.AddVisitAsync(inputVisit));
        }
        #endregion

        #region UPDATE
        [Fact]
        public async void ShouldUpdateAnimalAsync()
        {
            // Arrange
            Visit inputVisit = CreateRandomVisit();

            _visitBrokerMock.Setup(broker => broker.UpdateVisitAsync(inputVisit))
                .ReturnsAsync(true);

            // Act
            bool actualResult = await _visitService.UpdateVisitAsync(inputVisit);

            // Assert
            Assert.True(actualResult);
        }

        [Fact]
        public async void ShouldThrowUpdateFailedExceptionWhenDbFails()
        {
            // Arrange
            Visit inputVisit = CreateRandomVisit();

            var exception = new Exception(new MnemonicString().GetValue());

            _visitBrokerMock.Setup(broker => broker.UpdateVisitAsync(inputVisit)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<UpdateFailedException>(() => _visitService.UpdateVisitAsync(inputVisit));
        }
        #endregion

        #region DELETE
        [Fact]
        public async void ShouldDeleteAnimalAsync()
        {
            // Arrange
            Visit inputVisit = CreateRandomVisit();

            _visitBrokerMock.Setup(broker => broker.DeleteVisitAsync(inputVisit))
                .ReturnsAsync(true);

            // Act
            bool actualAns = await _visitService.DeleteVisitAsync(inputVisit);

            // Assert
            Assert.True(actualAns);
        }

        [Fact]
        public async void ShouldThrowDeleteFailedExceptionWhenDbFails()
        {
            // Arrange
            Visit inputVisit = CreateRandomVisit();

            var exception = new Exception(new MnemonicString().GetValue());

            _visitBrokerMock.Setup(broker => broker.DeleteVisitAsync(inputVisit)).Throws(exception);

            //// Act + Assert
            await Assert.ThrowsAsync<DeleteFailedException>(() => _visitService.DeleteVisitAsync(inputVisit));
        }
        #endregion

        #region Private Methods
        private Visit CreateRandomVisit()
        {
            Random rnd = new Random();

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

        private PreventiveTreatment CreateRandomPreventiveTreatment()
        {
            var treatmentFiller = new Filler<PreventiveTreatment>();
            treatmentFiller.Setup()
                .OnProperty(p => p.VisitId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.TreatmentId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.Name).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.RemainingNumOfDays).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.NextTreatmentName).Use(new RealNames(NameStyle.FirstName));

            PreventiveTreatment treatment = treatmentFiller.Create();
            return treatment;
        }

        private List<PreventiveTreatment> CreateRandomPreventiveTreatmentList()
        {
            int numOfItems = 10;
            var list = new List<PreventiveTreatment>();

            for (int i = 0; i < numOfItems; i++)
            {
                list.Add(CreateRandomPreventiveTreatment());
            }
            return list;
        }

        private VisitPage CreateRandomVisitPage()
        {
            var visitFiller =  new Filler<Visit>();
            visitFiller.Setup()
                .OnProperty(p => p.VisitId).Use(new IntRange(2001, 9999));

            var visitPageFiller = new Filler<VisitPage>();
            visitPageFiller.Setup()
                .OnProperty(p => p.Visit).Use(visitFiller.Create());

            VisitPage visitPage = visitPageFiller.Create();
            return visitPage;
        }

        private VisitPageLists CreateRandomVisitPageLists()
        {
            var visitPageListsFiller = new Filler<VisitPageLists>();
            visitPageListsFiller.Setup()
                .OnProperty(p => p.TreatmentsList).Use(CreateRandomTreatmentsList().ToList)
                .OnProperty(p => p.DiagnosisList).Use(CreateRandomDiagnosesList().ToList)
                .OnProperty(p => p.AllPreventiveTreatmentsList).Use(CreateRandomPreventiveTreatmentList().ToList);

            VisitPageLists visitPageLists = visitPageListsFiller.Create();
            return visitPageLists;
        }

        private IEnumerable<Treatment> CreateRandomTreatmentsList()
        {
            int listLength = new IntRange(2, 10).GetValue();

            var list = new List<Treatment>();
            for (var i = 0; i < listLength; i++)
            {
                Treatment treatment = CreateRandomTreatment();
                list.Add(treatment);
            }

            return list;
        }

        private Treatment CreateRandomTreatment()
        {
            var treatmentFiller = new Filler<Treatment>();
            treatmentFiller.Setup()
                .OnProperty(p => p.TreatmentId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.Name).Use(new Filler<string>().Create);

            Treatment treatment = treatmentFiller.Create();
            return treatment;
        }

        private IEnumerable<Diagnosis> CreateRandomDiagnosesList()
        {
            int listLength = new IntRange(2, 10).GetValue();

            var list = new List<Diagnosis>();
            for (var i = 0; i < listLength; i++)
            {
                Diagnosis diagnosis = CreateRandomDiagnosis();
                list.Add(diagnosis);
            }
            return list;
        }

        private Diagnosis CreateRandomDiagnosis()
        {
            var diagnosisFiller = new Filler<Diagnosis>();
            diagnosisFiller.Setup()
                .OnProperty(p => p.DiagnosisId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.Name).Use(new Filler<string>().Create);

            Diagnosis diagnosis = diagnosisFiller.Create();
            return diagnosis;
        }

        private void SetupVisitPageBroker(VisitPage expectedResult)
        {
            int visitId = expectedResult.Visit.VisitId;

            _visitBrokerMock.Setup(broker => broker.GetVisitByIdAsync(visitId))
                .ReturnsAsync(expectedResult.Visit);
            _visitBrokerMock.Setup(broker => broker.GetAnimalByVisitIdAsync(visitId))
                .ReturnsAsync(expectedResult.Animal);
            _visitBrokerMock.Setup(broker => broker.GetOwnerByVisitIdAsync(visitId))
                .ReturnsAsync(expectedResult.Owner);
            _visitBrokerMock.Setup(broker => broker.GetVisitDiagnosesListAsync())
                .ReturnsAsync(expectedResult.Lists.DiagnosisList);
            _visitBrokerMock.Setup(broker => broker.GetVisitTreatmentsListAsync())
                .ReturnsAsync(expectedResult.Lists.TreatmentsList);
            _visitBrokerMock.Setup(broker => broker.GetPreventiveTreatmentsListAsync())
                .ReturnsAsync(expectedResult.Lists.AllPreventiveTreatmentsList);
            _visitBrokerMock.Setup(broker => broker.GetPreventiveTreatmentsListByVisitIdAsync(visitId))
                .ReturnsAsync(expectedResult.PreventiveTreatmentsList);

        }

        private void SetupVisitPageListsBroker(VisitPageLists expectedResult)
        {
            _visitBrokerMock.Setup(broker => broker.GetVisitDiagnosesListAsync())
                .ReturnsAsync(expectedResult.DiagnosisList);
            _visitBrokerMock.Setup(broker => broker.GetVisitTreatmentsListAsync())
                .ReturnsAsync(expectedResult.TreatmentsList);
            _visitBrokerMock.Setup(broker => broker.GetPreventiveTreatmentsListAsync())
                .ReturnsAsync(expectedResult.AllPreventiveTreatmentsList);
        }
        #endregion
    }
}
