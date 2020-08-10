using RootCodeSample.Models;
using RootCodeSample.Parsers;

namespace RootCodeSample.Factories
{
    public class ParserFactory : IParserFactory
    {
        private static readonly IValidatorFactory _factory = new ValidatorFactory();

        public IParser<T> GetParser<T>() where T : class, new()
        {
            var validator = _factory.GetValidator<T>();
            switch (typeof(T))
            {
                case var t when t == typeof(Driver):
                    return (IParser<T>) new DriverParser(validator);
                case var t when t == typeof(Trip):
                    return (IParser<T>) new TripParser(validator);
                default:
                    return null;
            }
        }
    }
}