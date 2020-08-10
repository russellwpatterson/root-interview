using System;

public class InvalidDataLineTypeException : Exception
{
    public override string ToString() 
    {
        return "Line must begin with type Driver or Trip.";
    }
}