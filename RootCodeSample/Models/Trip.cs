using System;

namespace RootCodeSample.Models
{
    public class Trip
    {
        public string DriverName { get; set;}
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalMiles { get; set; }
        public decimal AverageSpeed { get => TotalMiles / TotalHours; }

        public decimal TotalHours { get => (decimal)(EndTime - StartTime).TotalHours; }
    }
}