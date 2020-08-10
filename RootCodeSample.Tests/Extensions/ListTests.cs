using System.Collections.Generic;
using RootCodeSample.Extensions;
using RootCodeSample.Models;
using Xunit;

namespace RootCodeSample.Tests.Extensions
{
    public class ListTests
    {
        [Fact]        
        public void ListExtensions_ValidListOfDrivers_FindDriver()
        {
            // Arrange
            var listOfDrivers = new List<Driver>()
            {
                new Driver { DriverName = "Russell" },
                new Driver { DriverName = "Bob" }
            };

            // Act
            var driverFound = listOfDrivers.GetDriver("Russell");

            // Assert
            Assert.NotNull(driverFound);
            Assert.NotNull(driverFound.DriverName);
            Assert.Equal("Russell", driverFound.DriverName);
        }

        [Fact]        
        public void ListExtensions_ValidListOfDrivers_DontFindDriver()
        {
            // Arrange
            var listOfDrivers = new List<Driver>()
            {
                new Driver { DriverName = "Russell" },
                new Driver { DriverName = "Bob" }
            };

            // Act
            var driverFound = listOfDrivers.GetDriver("Joe");

            // Assert
            Assert.Null(driverFound);
        }
    }
}