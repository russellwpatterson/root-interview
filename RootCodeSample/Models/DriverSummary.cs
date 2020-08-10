using System;

namespace RootCodeSample.Models
{
    public class DriverSummary
    {
        public string Name { get; set; }

        public decimal TotalMiles { get; set; }

        public decimal TotalHours { get; set; }

        public int AverageSpeed 
        { 
            get 
            {
                if (TotalHours == 0) 
                {
                    return 0; 
                }
                else
                {
                    return (int) Math.Round(TotalMiles / TotalHours); 
                }
            }
        } 

        public override string ToString() 
        {
            if (TotalMiles == 0)
            {
                return $"{Name}: 0 miles";
            }

            return $"{Name}: {TotalMiles:F0} miles @ {AverageSpeed} mph";
        }
    }
}