using Xunit;
using RootCodeSample.Parsers;
using RootCodeSample.Validators;

namespace RootCodeSample.Tests.Parsers
{
    public class DriverParserTests
    {
        private DriverParser _parser = new DriverParser(new DriverValidator());

        [Fact]
        public void DriverParser_ValidInput_CreatesDriver()
        {
            // Arrange
            var dataLine = "Driver Russell";

            // Act
            var result = _parser.Parse(dataLine);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.DriverName);
            Assert.Equal("Russell", result.DriverName);

            Assert.NotNull(result.Trips);
            Assert.Empty(result.Trips);
        }
    }
}
