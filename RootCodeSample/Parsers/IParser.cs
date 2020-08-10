namespace RootCodeSample.Parsers
{
    public interface IParser<T> where T: class, new()
    {
        T Parse(string dataLine);
    }
}