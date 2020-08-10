using Xunit;
using RootCodeSample.Services;
using System.Linq;
using RootCodeSample.Factories;
using RootCodeSample.Models;

namespace RootCodeSample.Tests.Services
{
    public class ParsingServiceTests
    {
        private static readonly ParsingService _service;
        static ParsingServiceTests()
        {
            var factory = new ParserFactory();
            _service = new ParsingService(
                factory.GetParser<Driver>(), 
                factory.GetParser<Trip>()
            );            
        }

        [Fact]
        public void ParsingService_CompletelyInvalidFile_ThrowsInvalidDataLineTypeException()
        {
            // Arrange
            var dataFile = new[] {
                "84i9ifkf9k",
                "jgfkga92"
            };

            // Act
            var result = Assert.Throws<InvalidDataLineTypeException>(() => _service.ParseDataFile(dataFile));

            // Assert
            Assert.Equal("Line must begin with type Driver or Trip.", result.ToString());
        }

        [Fact]
        public void ParsingService_InvalidDataLineTypes_ThrowsInvalidDataLineTypeException()
        {
            // Arrange
            var dataFile = new[] {
                "Human Russell",
                "Journey Russell 5:45 6:45 60"
            };

            // Act
            var result = Assert.Throws<InvalidDataLineTypeException>(() => _service.ParseDataFile(dataFile));

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Line must begin with type Driver or Trip.", result.ToString());
        }

        [Fact]
        public void ParsingService_UndeclaredDriver_ThrowsUndeclaredDriverException()
        {
            // Arrange
            var dataFile = new[] {
                "Driver Russell",
                "Trip Sarah 4:35 8:45 300"
            };

            // Act
            var result = Assert.Throws<UndeclaredDriverException>(() => _service.ParseDataFile(dataFile));

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Undeclared Driver: Sarah", result.ToString());
        }

        [Fact]
        public void ParsingService_ValidInputOnlySlowTrips_DriverSummaryIsAllZeroes()
        {
            // Arrange
            var dataFile = new[] {
                "Driver Russell",
                "Trip Russell 6:35 7:35 4.9"
            };

            // Act
            var results = _service.ParseDataFile(dataFile);

            // Assert
            Assert.NotNull(results);
            Assert.Single(results);
            var driverSummaries = results.ToArray();

            Assert.NotNull(driverSummaries[0]);
            Assert.Equal("Russell", driverSummaries[0].Name);
            Assert.Equal(0, driverSummaries[0].TotalHours);
            Assert.Equal(0, driverSummaries[0].TotalMiles);
            Assert.Equal(0, driverSummaries[0].AverageSpeed);
            Assert.Equal("Russell: 0 miles", driverSummaries[0].ToString());
        }

        [Fact]
        public void ParsingService_ValidInputOnlyFastTrips_DriverSummaryIsAllZeroes()
        {
            // Arrange
            var dataFile = new[] {
                "Driver Russell",
                "Trip Russell 6:35 7:35 100.1"
            };

            // Act
            var results = _service.ParseDataFile(dataFile);

            // Assert
            Assert.NotNull(results);
            Assert.Single(results);
            var driverSummaries = results.ToArray();

            Assert.NotNull(driverSummaries[0]);
            Assert.Equal("Russell", driverSummaries[0].Name);
            Assert.Equal(0, driverSummaries[0].TotalHours);
            Assert.Equal(0, driverSummaries[0].TotalMiles);
            Assert.Equal(0, driverSummaries[0].AverageSpeed);
            Assert.Equal("Russell: 0 miles", driverSummaries[0].ToString());
        }

        [Fact]
        public void ParsingService_ValidInputOnlyInvalidTrips_DriverSummaryIsAllZeroes()
        {
            // Arrange
            var dataFile = new[] {
                "Driver Russell",
                "Trip Russell 6:35 7:35 4.9",
                "Trip Russell 8:35 9:35 100.1"
            };

            // Act
            var results = _service.ParseDataFile(dataFile);

            // Assert
            Assert.NotNull(results);
            Assert.Single(results);
            var driverSummaries = results.ToArray();

            Assert.NotNull(driverSummaries[0]);
            Assert.Equal("Russell", driverSummaries[0].Name);
            Assert.Equal(0, driverSummaries[0].TotalHours);
            Assert.Equal(0, driverSummaries[0].TotalMiles);
            Assert.Equal(0, driverSummaries[0].AverageSpeed);
            Assert.Equal("Russell: 0 miles", driverSummaries[0].ToString());
        }
    
        [Fact]
        public void ParsingService_ValidInputNoTrips_DriverSummariesAreAllZeroes()
        {
            // Arrange
            var dataFile = new[] {
                "Driver Russell",
                "Driver Sarah"
            };

            // Act
            var results = _service.ParseDataFile(dataFile);

            // Assert
            Assert.NotNull(results);
            Assert.Equal(2, results.Count());
            var driverSummaries = results.ToArray();

            Assert.NotNull(driverSummaries[0]);
            Assert.Equal("Russell", driverSummaries[0].Name);
            Assert.Equal(0, driverSummaries[0].TotalHours);
            Assert.Equal(0, driverSummaries[0].TotalMiles);
            Assert.Equal(0, driverSummaries[0].AverageSpeed);
            Assert.Equal("Russell: 0 miles", driverSummaries[0].ToString());

            Assert.NotNull(driverSummaries[1]);
            Assert.Equal("Sarah", driverSummaries[1].Name);
            Assert.Equal(0, driverSummaries[1].TotalHours);
            Assert.Equal(0, driverSummaries[1].TotalMiles);
            Assert.Equal(0, driverSummaries[1].AverageSpeed);
            Assert.Equal("Sarah: 0 miles", driverSummaries[1].ToString());
        }

        [Fact]
        public void ParsingService_ValidInput_CreatesDriverSummaries()
        {
            // Arrange
            var dataFile = new[] {
                "Driver Dan",
                "Driver Lauren",
                "Driver Kumi",
                "Trip Dan 07:15 07:45 17.3",
                "Trip Dan 06:12 06:32 21.8",
                "Trip Lauren 12:01 13:16 42.0"
            };

            // Act
            var results = _service.ParseDataFile(dataFile);

            // Assert
            Assert.NotNull(results);
            Assert.Equal(3, results.Count());
            var driverSummaries = results.ToArray();

            Assert.NotNull(driverSummaries[0]);
            Assert.Equal("Lauren", driverSummaries[0].Name);
            Assert.Equal(1.25m, driverSummaries[0].TotalHours);
            Assert.Equal(42, driverSummaries[0].TotalMiles);
            Assert.Equal(34, driverSummaries[0].AverageSpeed);
            Assert.Equal("Lauren: 42 miles @ 34 mph", driverSummaries[0].ToString());

            Assert.NotNull(driverSummaries[1]);
            Assert.Equal("Dan", driverSummaries[1].Name);
            Assert.Equal(.833m, driverSummaries[1].TotalHours, 3);
            Assert.Equal(39.1m, driverSummaries[1].TotalMiles);
            Assert.Equal(47, driverSummaries[1].AverageSpeed);
            Assert.Equal("Dan: 39 miles @ 47 mph", driverSummaries[1].ToString());

            Assert.NotNull(driverSummaries[2]);
            Assert.Equal("Kumi", driverSummaries[2].Name);
            Assert.Equal(0, driverSummaries[2].TotalHours);
            Assert.Equal(0, driverSummaries[2].TotalMiles);
            Assert.Equal(0, driverSummaries[2].AverageSpeed);
            Assert.Equal("Kumi: 0 miles", driverSummaries[2].ToString());
        }
    }
}
