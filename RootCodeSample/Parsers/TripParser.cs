using System;
using RootCodeSample.Models;
using RootCodeSample.Validators;

namespace RootCodeSample.Parsers
{
    public class TripParser : IParser<Trip>
    {
        private const int IDX_DRIVER_NAME = 1;
        private const int IDX_START_TIME = 2;
        private const int IDX_END_TIME = 3;
        private const int IDX_MILES_DRIVEN = 4;

        private IValidator _validator;

        public TripParser(IValidator validator)
        {
            _validator = validator;
        }

        public Trip Parse(string dataLine)
        {
            _validator.Validate(dataLine, out string[] dataLineParts);

            var driverName = dataLineParts[IDX_DRIVER_NAME];            
            var startTime = dataLineParts[IDX_START_TIME];
            var endTime = dataLineParts[IDX_END_TIME];
            var milesDriven = dataLineParts[IDX_MILES_DRIVEN];

            try
            {
                var trip = new Trip() 
                {
                    DriverName = driverName,
                    StartTime = DateTime.Parse(startTime),
                    EndTime = DateTime.Parse(endTime),
                    TotalMiles = decimal.Parse(milesDriven)
                };

                return trip;
            }
            catch (Exception ex)
            {
                throw new InvalidDataLineException($"Exception while parsing {nameof(Trip)}", ex);
            }
        }
    }
}