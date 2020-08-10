using RootCodeSample.Models;
using RootCodeSample.Validators;

namespace RootCodeSample.Parsers
{
    public class DriverParser : IParser<Driver>
    {
        private const int DRIVER_NAME_INDEX = 1;

        private IValidator _validator;

        public DriverParser(IValidator validator)
        {
            _validator = validator;
        }

        public Driver Parse(string dataLine)
        {
            _validator.Validate(dataLine, out string[] dataLineParts);

            var driverName = dataLineParts[DRIVER_NAME_INDEX];
            
            return new Driver() 
            {
                DriverName = driverName
            };
        }
    }
}