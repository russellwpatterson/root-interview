using System.Collections.Generic;

namespace RootCodeSample.Models
{
    public class Driver
    {
        public Driver()
        {
            Trips = new List<Trip>();
        }

        public string DriverName { get; set; }
        public IList<Trip> Trips { get; set; }
    }
}