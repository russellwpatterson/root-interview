using System;
using System.Collections.Generic;
using System.Linq;
using RootCodeSample.Constants;
using RootCodeSample.Extensions;
using RootCodeSample.Models;
using RootCodeSample.Parsers;
using RootCodeSample.Validators;

namespace RootCodeSample.Services
{
    public class ParsingService : IParsingService
    {
        private readonly IParser<Driver> _driverParser;
        private readonly IParser<Trip> _tripParser;

        public ParsingService(IParser<Driver> driverParser, IParser<Trip> tripParser)
        {
            _driverParser = driverParser;
            _tripParser = tripParser;
        }

        public IEnumerable<DriverSummary> ParseDataFile(IEnumerable<string> dataFile)
        {
            var drivers = new List<Driver>();            
            var driverParser = new DriverParser(new DriverValidator());
            var tripParser = new TripParser(new TripValidator());

            foreach (var dataLine in dataFile)
            {
                if (string.IsNullOrWhiteSpace(dataLine)) 
                {
                    throw new ArgumentNullException(nameof(dataLine));
                }

                if (dataLine.StartsWith(LineType.DRIVER))
                {
                    var newDriver = driverParser.Parse(dataLine);
                    if (drivers.GetDriver(newDriver.DriverName) == null)
                    {
                        drivers.Add(newDriver);
                    }
                }                
                else if (dataLine.StartsWith(LineType.TRIP))
                {
                    var newTrip = tripParser.Parse(dataLine);

                    var driverForTrip = drivers.GetDriver(newTrip.DriverName);
                    if (driverForTrip == null)
                    {
                        // If we go to find the driver and it's not there, that means this file is bad.
                        throw new UndeclaredDriverException(newTrip.DriverName);
                    }

                    driverForTrip.Trips.Add(newTrip);
                }
                else 
                {
                    throw new InvalidDataLineTypeException();
                }
            }

            // For each Driver, create a DriverSummary object.
            return drivers.Select(d => CreateDriverSummary(d)).OrderByDescending(o => o.TotalMiles).ToList();
        }

        private DriverSummary CreateDriverSummary(Driver driver)
        {
            // Exclude trips averaging < 5mph or > 100 mph.
            var validTrips = driver.Trips.Where(t => t.TotalMiles > 0 && t.AverageSpeed >= 5 && t.AverageSpeed <= 100);

            return new DriverSummary()
            {
                Name = driver.DriverName,
                TotalMiles = validTrips.Sum(t => t.TotalMiles),
                TotalHours = validTrips.Sum(t => t.TotalHours)
            };
        }
    }
}