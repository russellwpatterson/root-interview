using System.Collections.Generic;
using System.Linq;
using RootCodeSample.Models;

namespace RootCodeSample.Extensions
{
    public static class ListExtensions
    {
        public static Driver GetDriver(this IList<Driver> drivers, string driverName)
        {
            return drivers.FirstOrDefault(d => d.DriverName == driverName);
        }
    }
}