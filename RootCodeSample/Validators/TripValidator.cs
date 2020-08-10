using System;

namespace RootCodeSample.Validators
{
    public class TripValidator : IValidator
    {
        public void Validate(string dataLine, out string[] dataLineParts)
        {
            if (string.IsNullOrWhiteSpace(dataLine)) 
            {
                throw new InvalidDataLineException("Record is null or only whitespace.");
            }

            var lineParts = dataLine.Split(' ');
            if (lineParts.Length != 5) 
            {
                throw new InvalidDataLineException("Trip does not have 5 space-delimited fields.");
            }

            if (!"Trip".Equals(lineParts[0]))
            {
                throw new InvalidDataLineException("Record is not a valid Trip.");
            }

            if (!DateTime.TryParse(lineParts[2], out DateTime startTime))
            {
                throw new InvalidDataLineException("Trip does not have a valid start time.");
            }

            if (!DateTime.TryParse(lineParts[3], out DateTime endTime))
            {
                throw new InvalidDataLineException("Trip does not have a valid end time.");
            }

            if (startTime > endTime)
            {
                throw new InvalidDataLineException("Trip start time cannot be after end time.");
            }

            if (!decimal.TryParse(lineParts[4], out decimal miles))
            {
                throw new InvalidDataLineException("Trip does not have a valid value for miles driven.");
            }

            if (miles < 0)
            {
                throw new InvalidDataLineException("Trip miles driven cannot be less than zero.");
            }

            dataLineParts = lineParts;
        }
    }
}