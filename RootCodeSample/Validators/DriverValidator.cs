namespace RootCodeSample.Validators
{
    public class DriverValidator : IValidator
    {
        public void Validate(string dataLine, out string[] dataLineParts)
        {
            if (string.IsNullOrWhiteSpace(dataLine)) 
            {
                throw new InvalidDataLineException("Record is null or only whitespace.");
            }

            var lineParts = dataLine.Split(' ');
            if (lineParts.Length != 2) 
            {
                throw new InvalidDataLineException("Driver does not have 2 space-delimited fields.");
            }

            if (!"Driver".Equals(lineParts[0]))
            {
                throw new InvalidDataLineException("Record is not a valid Driver.");
            }
            
            dataLineParts = lineParts;
        }
    }
}