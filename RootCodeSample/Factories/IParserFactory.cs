using RootCodeSample.Parsers;

namespace RootCodeSample.Factories
{
    public interface IParserFactory
    {
        IParser<T> GetParser<T>() where T : class, new();
    }
}