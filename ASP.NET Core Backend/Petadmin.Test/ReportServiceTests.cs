using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;
using Petadmin.Services;
using Petadmin.Services.Interfaces;
using Tynamix.ObjectFiller;
using Xunit;

namespace Petadmin.Test.Services
{
    public class ReportServiceTests
    {
        #region Class Initialization
        private readonly Mock<IReportBroker> _reportBrokerMock;
        private readonly IReportService _reportService;
        private readonly Mock<ILogger<ReportService>> _loggingMock;

        public ReportServiceTests()
        {
            _reportBrokerMock = new Mock<IReportBroker>();
            _loggingMock = new Mock<ILogger<ReportService>>();
            _reportService = new ReportService(_reportBrokerMock.Object, _loggingMock.Object);
        }
        #endregion

        [Fact]
        public async void ShouldCallReportBroker()
        {
            // Arrange
            List<DebtSheetItem> expectedResult = CreateRandomDebtSheetItemList();

            SetupDebtSheetBroker(expectedResult);

            // Act
            await _reportService.GetDebtSheet();
            
            // Assert
            _reportBrokerMock.Verify(broker => broker.GetDebtSheet(), Times.Once, "Called Times.Once failed");
        }

        [Fact]
        public async void ShouldGetDebtSheetAsync()
        {
            // Arrange
            List<DebtSheetItem> expectedResult = CreateRandomDebtSheetItemList();

            SetupDebtSheetBroker(expectedResult);

            // Act
            List<DebtSheetItem> actualResult = (await _reportService.GetDebtSheet()).ToList();

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }

        [Fact]
        public async void ShouldGetOwnersVisitedLastXDaysAsync()
        {
            // Arrange
            int days = new IntRange(1, 1000).GetValue();
            List<VisitedOwnersItem> expectedResult = CreateRandomVisitedOwnersItemList();

            SetupVisitedOwnersItemBroker(expectedResult, days);

            // Act
            List<VisitedOwnersItem> actualResult = (await _reportService.GetOwnersVisitedLastXDays(days)).ToList();

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }

        [Fact]
        public async void ShouldGetOwnersNotVisitedLastXDaysAsync()
        {
            // Arrange
            int days = new IntRange(1, 1000).GetValue();
            List<VisitedOwnersItem> expectedResult = CreateRandomVisitedOwnersItemList();

            SetupVisitedOwnersItemBroker(expectedResult, days);

            // Act
            List<VisitedOwnersItem> actualResult = (await _reportService.GetOwnersNotVisitedLastXDays(days)).ToList();

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }

        [Fact]
        public async void ShouldGetRabiesListByDateIntervalAsync()
        {
            // Arrange
            DateTime fromDate = new DateTime(2021, 01, 01);
            DateTime toDate = new DateTime(2021, 03, 01);

            List<RabiesReport> expectedResult = CreateRandomRabiesReportList();

            SetupRabiesReportBroker(expectedResult, fromDate, toDate);

            // Act
            List<RabiesReport> actualResult = (await _reportService.GetRabiesListByDateInterval(fromDate, toDate)).ToList();

            //// Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "Results Not Equivalent");
        }

        #region Private Methods
        private DebtSheetItem CreateRandomDebtSheetItem()
        {
            var filler = new Filler<DebtSheetItem>();
            filler.Setup()
                .OnProperty(p => p.DebtId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.OwnerId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.FirstName).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.LastName).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.Phone).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.DebtAmountSum).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.PaidAmountSum).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.TotalAmount).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.DebtDate).Use(DateTime.Now);

            DebtSheetItem item = filler.Create();
            return item;
        }

        private List<DebtSheetItem> CreateRandomDebtSheetItemList()
        {
            int numOfItems = 10;
            var list = new List<DebtSheetItem>();

            for (int i = 0; i < numOfItems; i++)
            {
                list.Add(CreateRandomDebtSheetItem());
            }
            return list;
        }


        private VisitedOwnersItem CreateRandomVisitedOwnersItem()
        {
            var filler = new Filler<VisitedOwnersItem>();
            filler.Setup()
                .OnProperty(p => p.VisitId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.AnimalId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.OwnerId).Use(new IntRange(2001, 9999))
                .OnProperty(p => p.VisitTime).Use(DateTime.Now)
                .OnProperty(p => p.Name).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.Type).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.Breed).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.Color).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.Gender).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.Active).Use(RandomBoolean())
                .OnProperty(p => p.Sterilized).Use(RandomBoolean())
                .OnProperty(p => p.Active).Use(RandomBoolean())
                .OnProperty(p => p.ChipNumber).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.FirstName).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.LastName).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.City).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.Street).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.HouseNumber).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.ApartmentNumber).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.Phone).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.NumOfDaysPassed).Use(new IntRange(2001, 9999));

            VisitedOwnersItem item = filler.Create();
            return item;
        }

        private List<VisitedOwnersItem> CreateRandomVisitedOwnersItemList()
        {
            int numOfItems = 10;
            var list = new List<VisitedOwnersItem>();

            for (int i = 0; i < numOfItems; i++)
            {
                list.Add(CreateRandomVisitedOwnersItem());
            }
            return list;
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
        private Visit CreateRandomVisit()
        {
            var filler = new Filler<Visit>();
            filler.Setup()
                .OnProperty(p => p.VisitId).Use(new IntRange(2001, 9999));

            Visit visit = filler.Create();
            return visit;
        }
        private RabiesReport CreateRandomRabiesReport()
        {
            var filler = new Filler<RabiesReport>();
            filler.Setup()
                .OnProperty(p => p.Animal).Use(CreateRandomAnimal())
                .OnProperty(p => p.Owner).Use(CreateRandomOwner())
                .OnProperty(p => p.Visit).Use(CreateRandomVisit());

            RabiesReport item = filler.Create();
            return item;
        }
        private List<RabiesReport> CreateRandomRabiesReportList()
        {
            int numOfItems = 10;
            var list = new List<RabiesReport>();

            for (int i = 0; i < numOfItems; i++)
            {
                list.Add(CreateRandomRabiesReport());
            }
            return list;
        }

        private bool RandomBoolean(){
            var gen = new Random();
            int prob = gen.Next(100);
            return prob <= 20;
        }

        private void SetupDebtSheetBroker(List<DebtSheetItem> expectedResult)
        {
            _reportBrokerMock.Setup(broker => broker.GetDebtSheet())
                .ReturnsAsync(expectedResult);
        }
        
        private void SetupVisitedOwnersItemBroker(List<VisitedOwnersItem> expectedResult, int days)
        {
            _reportBrokerMock.Setup(broker => broker.GetOwnersVisitedLastXDays(days))
                .ReturnsAsync(expectedResult);

            _reportBrokerMock.Setup(broker => broker.GetOwnersNotVisitedLastXDays(days))
                .ReturnsAsync(expectedResult);
        }
        
        private void SetupRabiesReportBroker(List<RabiesReport> expectedResult, DateTime fromDate, DateTime toDate)
        {
            _reportBrokerMock.Setup(broker => broker.GetRabiesListByDateInterval(fromDate, toDate))
                .ReturnsAsync(expectedResult);
        }
        #endregion
    }
}
