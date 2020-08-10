using System.Collections.Generic;
using RootCodeSample.Models;

namespace RootCodeSample.Services
{
    public interface IParsingService
    {
        IEnumerable<DriverSummary> ParseDataFile(IEnumerable<string> dataFile);
    }
}