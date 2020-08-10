using Xunit;
using System;
using RootCodeSample.Validators;

namespace RootCodeSample.Tests.Validators
{
    public class TripValidatorTests
    {
        private TripValidator _validator = new TripValidator();

        [Fact]
        public void TripValidator_InvalidInputNullValue_ThrowsInvalidDataLineException()
        {
            // Arrange
            string dataLine = null;

            // Act
            var result = Assert.Throws<InvalidDataLineException>(() => _validator.Validate(dataLine, out string[] dataLineParts));

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Invalid Data Line: Record is null or only whitespace.", result.ToString());
        }

        [Fact]
        public void TripValidator_InvalidInputWhitespace_ThrowsInvalidDataLineException()
        {
            // Arrange
            var dataLine = "    ";

            // Act
            var result = Assert.Throws<InvalidDataLineException>(() => _validator.Validate(dataLine, out string[] dataLineParts));

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Invalid Data Line: Record is null or only whitespace.", result.ToString());
        }

        [Fact]
        public void TripValidator_InvalidInputNotEnoughDelimitedValues_ThrowsInvalidDataLineException()
        {
            // Arrange
            var dataLine = "48289982895i9ggj9jg";

            // Act
            var result = Assert.Throws<InvalidDataLineException>(() => _validator.Validate(dataLine, out string[] dataLineParts));

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Invalid Data Line: Trip does not have 5 space-delimited fields.", result.ToString());
        }

        [Fact]
        public void TripValidator_InvalidInputInvalidDataLineType_ThrowsInvalidDataLineException()
        {
            // Arrange
            var dataLine = "Journey Russell 6:44 6:59 10";

            // Act
            var result = Assert.Throws<InvalidDataLineException>(() => _validator.Validate(dataLine, out string[] dataLineParts));

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Invalid Data Line: Record is not a valid Trip.", result.ToString());
        }

        [Fact]
        public void TripValidator_InvalidInputWithBadStartDate_ThrowsInvalidDataLineException()
        {
            // Arrange
            var dataLine = "Trip Russell Bob 6:59 10";

            // Act
            var result = Assert.Throws<InvalidDataLineException>(() => _validator.Validate(dataLine, out string[] dataLineParts));

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Invalid Data Line: Trip does not have a valid start time.", result.ToString());
        }

        [Fact]
        public void TripValidator_InvalidInputWithBadEndDate_ThrowsInvalidDataLineException()
        {
            // Arrange
            var dataLine = "Trip Russell 6:44 Bob 10";

            // Act
            var result = Assert.Throws<InvalidDataLineException>(() => _validator.Validate(dataLine, out string[] dataLineParts));

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Invalid Data Line: Trip does not have a valid end time.", result.ToString());
        }

        [Fact]
        public void TripValidator_InvalidInputWithStartAfterEnd_ThrowsInvalidDataLineException()
        {
            // Arrange
            var dataLine = "Trip Russell 7:44 6:59 10";

            // Act
            var result = Assert.Throws<InvalidDataLineException>(() => _validator.Validate(dataLine, out string[] dataLineParts));

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Invalid Data Line: Trip start time cannot be after end time.", result.ToString());
        }

        [Fact]
        public void TripValidator_InvalidInputWithBadNumberOfMiles_ThrowsInvalidDataLineException()
        {
            // Arrange
            var dataLine = "Trip Russell 6:44 6:59 Bob";

            // Act
            var result = Assert.Throws<InvalidDataLineException>(() => _validator.Validate(dataLine, out string[] dataLineParts));

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Invalid Data Line: Trip does not have a valid value for miles driven.", result.ToString());
        }

        [Fact]
        public void TripValidator_InvalidInputWithNegativeNumberOfMiles_ThrowsInvalidDataLineException()
        {
            // Arrange
            var dataLine = "Trip Russell 6:44 6:59 -45";

            // Act
            var result = Assert.Throws<InvalidDataLineException>(() => _validator.Validate(dataLine, out string[] dataLineParts));

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Invalid Data Line: Trip miles driven cannot be less than zero.", result.ToString());
        }

        [Fact]
        public void TripValidator_ValidInput_CreatesTrip()
        {
            // Arrange
            var dataLine = "Trip Russell 6:44 6:59 10";

            // Act
            _validator.Validate(dataLine, out string[] dataLineParts);

            // Assert
            Assert.NotNull(dataLineParts);
            Assert.Equal(5, dataLineParts.Length);
            Assert.Equal("Trip", dataLineParts[0]);
            Assert.Equal("Russell", dataLineParts[1]);
            Assert.Equal("6:44", dataLineParts[2]);
            Assert.Equal("6:59", dataLineParts[3]);
            Assert.Equal("10", dataLineParts[4]);
        }
        [Fact]
        public void TripValidator_ValidInputCrossesHourBoundary_CreatesTrip()
        {
            // Arrange
            var dataLine = "Trip Russell 6:44 7:14 20";

            // Act
            _validator.Validate(dataLine, out string[] dataLineParts);

            // Assert
            Assert.NotNull(dataLineParts);
            Assert.Equal(5, dataLineParts.Length);
            Assert.Equal("Trip", dataLineParts[0]);
            Assert.Equal("Russell", dataLineParts[1]);
            Assert.Equal("6:44", dataLineParts[2]);
            Assert.Equal("7:14", dataLineParts[3]);
            Assert.Equal("20", dataLineParts[4]);
        }
    }
}
