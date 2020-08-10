using Xunit;
using RootCodeSample.Parsers;
using System;
using RootCodeSample.Validators;

namespace RootCodeSample.Tests.Parsers
{
    public class TripParserTests
    {
        private TripParser _parser = new TripParser(new TripValidator());

        [Fact]
        public void TripParser_ValidInput_CreatesTrip()
        {
            // Arrange
            var dataLine = "Trip Russell 6:44 6:59 10";

            // Act
            var result = _parser.Parse(dataLine);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Russell", result.DriverName);
            Assert.Equal(0.25m, result.TotalHours, 2);
            Assert.Equal(10m, result.TotalMiles);
            Assert.Equal(40m, result.AverageSpeed);
            Assert.Equal(DateTime.Parse("6:44"), result.StartTime);
            Assert.Equal(DateTime.Parse("6:59"), result.EndTime);
        }
        [Fact]
        public void TripParser_ValidInputCrossesHourBoundary_CreatesTrip()
        {
            // Arrange
            var dataLine = "Trip Russell 6:44 7:14 20";

            // Act
            var result = _parser.Parse(dataLine);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Russell", result.DriverName);
            Assert.Equal(0.5m, result.TotalHours, 2);
            Assert.Equal(20m, result.TotalMiles);
            Assert.Equal(40m, result.AverageSpeed);
            Assert.Equal(DateTime.Parse("6:44"), result.StartTime);
            Assert.Equal(DateTime.Parse("7:14"), result.EndTime);
        }
    }
}
