using Xunit;
using RootCodeSample.Validators;

namespace RootCodeSample.Tests.Validators
{
    public class DriverValidatorTests
    {
        private DriverValidator _validator = new DriverValidator();

        [Fact]
        public void DriverValidator_InvalidInputNull_ThrowsInvalidDataLineException()
        {
            // Arrange
            string dataLine = null;

            // Act
            var result = Assert.Throws<InvalidDataLineException>(() => _validator.Validate(dataLine, out string[] dataLineParts));

            // Assert
            Assert.Equal("Invalid Data Line: Record is null or only whitespace.", result.ToString());
        }

        [Fact]
        public void DriverValidator_InvalidInputWhitespace_ThrowsInvalidDataLineException()
        {
            // Arrange
            var dataLine = "     ";

            // Act
            var result = Assert.Throws<InvalidDataLineException>(() => _validator.Validate(dataLine, out string[] dataLineParts));

            // Assert
            Assert.Equal("Invalid Data Line: Record is null or only whitespace.", result.ToString());
        }

        [Fact]
        public void DriverValidator_InvalidInputNotEnoughDelimitedValues_ThrowsInvalidDataLineException()
        {
            // Arrange
            var dataLine = "jgiaigjiagijijijgiajg";

            // Act
            var result = Assert.Throws<InvalidDataLineException>(() => _validator.Validate(dataLine, out string[] dataLineParts));

            // Assert
            Assert.Equal("Invalid Data Line: Driver does not have 2 space-delimited fields.", result.ToString());
        }

        [Fact]
        public void DriverValidator_InvalidInputInvalidType_ThrowsInvalidDataLineException()
        {
            // Arrange
            var dataLine = "HumanForCar Russell";

            // Act
            var result = Assert.Throws<InvalidDataLineException>(() => _validator.Validate(dataLine, out string[] dataLineParts));

            // Assert
            Assert.Equal("Invalid Data Line: Record is not a valid Driver.", result.ToString());
        }

        [Fact]
        public void DriverValidator_ValidInput_PassesBackDataLineParts()
        {
            // Arrange
            var dataLine = "Driver Russell";

            // Act
            _validator.Validate(dataLine, out string[] dataLineParts);

            // Assert
            Assert.NotNull(dataLineParts);
            Assert.Equal(2, dataLineParts.Length);
            Assert.Equal("Driver", dataLineParts[0]);
            Assert.Equal("Russell", dataLineParts[1]);
        }
    }
}
