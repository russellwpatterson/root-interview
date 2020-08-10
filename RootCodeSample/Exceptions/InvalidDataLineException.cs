using System;

public class InvalidDataLineException : Exception
{
    public InvalidDataLineException(string message) : base(message) {}
    public InvalidDataLineException(string message, Exception inner) : base("Invalid Data Line", inner) {}

    public override string ToString() 
    {
        if (InnerException != null)
        {
            return $"Invalid Data Line: {Message}; InnerException: {InnerException}";
        }

        return $"Invalid Data Line: {Message}";
    }
}