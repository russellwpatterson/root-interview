using System;

public class UndeclaredDriverException : Exception
{
    private string DriverName { get; set; }
    public UndeclaredDriverException(string driverName) => DriverName = driverName;

    public override string ToString() 
    {
        return $"Undeclared Driver: {DriverName}";
    }
}